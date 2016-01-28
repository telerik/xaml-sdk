#if !SILVERLIGHT
using Microsoft.Win32;
#endif
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Windows;
#if SILVERLIGHT
using System.Windows.Controls;
#endif
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Telerik.Windows.Controls;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf.Export;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf.Filters;
using Telerik.Windows.Documents.Fixed.Model;
using Telerik.Windows.Documents.Fixed.Model.ColorSpaces;
using Telerik.Windows.Documents.Fixed.Model.Editing;
using Telerik.Windows.Documents.Fixed.Model.Resources;
using Telerik.Windows.Zip;

namespace CreateDocumentWithImages
{
    public class MainViewModel : ViewModelBase
    {
        public const string TestFileName = "test.pdf";
        public static readonly Size PageSize = new Size(Telerik.Windows.Documents.Media.Unit.MmToDip(210), Telerik.Windows.Documents.Media.Unit.MmToDip(297));
        public static readonly Thickness Margins = new Thickness(Telerik.Windows.Documents.Media.Unit.MmToDip(10));
        public static readonly Size RemainingPageSize = new Size(PageSize.Width - Margins.Left - Margins.Right, PageSize.Height - Margins.Top - Margins.Bottom);
        private readonly RadFixedDocument document;
        private readonly PdfFormatProvider provider;
        private readonly ObservableCollection<ImageQuality> imageQualityValues;
        private readonly ICommand saveFileCommand;
        private ImageQuality selectedImageQuality;

        public MainViewModel()
        {
            this.document = new RadFixedDocument();
            this.provider = new PdfFormatProvider();
            this.saveFileCommand = new DelegateCommand((parameter) => SaveFile());
            this.imageQualityValues = new ObservableCollection<ImageQuality>();
            this.InitializeImageQualityValues();

            this.GenerateDocumentContent();
        }

        private void InitializeImageQualityValues()
        {
            foreach (ImageQuality value in Enum.GetValues(typeof(ImageQuality)))
            {
                this.ImageQualityValues.Add(value);
            }

            this.SelectedImageQuality = this.provider.ExportSettings.ImageQuality;
        }

        public ObservableCollection<ImageQuality> ImageQualityValues
        {
            get
            {
                return this.imageQualityValues;
            }
        }

        public ICommand SaveFileCommand
        {
            get
            {
                return this.saveFileCommand;
            }
        }

        public ImageQuality SelectedImageQuality
        {
            get
            {
                return this.selectedImageQuality;
            }
            set
            {
                if (this.selectedImageQuality != value)
                {
                    this.selectedImageQuality = value;
                    this.OnPropertyChanged("SelectedImageQuality");
                }
            }
        }

        private void SaveFile()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Pdf file|*.pdf";
            ImageQuality imageQuality = this.SelectedImageQuality;
            string defaultFileName = string.Format("PDF with ImageQuality {0}.pdf", imageQuality);
#if SILVERLIGHT
            saveFileDialog.DefaultFileName = defaultFileName;
#else
            saveFileDialog.FileName = defaultFileName;
#endif

            if (saveFileDialog.ShowDialog() == true)
            {
                using (Stream stream = saveFileDialog.OpenFile())
                {
                    this.provider.ExportSettings.ImageQuality = imageQuality;
                    this.provider.Export(this.document, stream);
                }
            }
        }

        private void GenerateDocumentContent()
        {
            // The default image quality is controlled by PdfFormatProvider.ExportSettings.
            this.AddPageWithImage("This is JPEG image with RGB colorspace.", CreateImageSource("Resources/rgb.jpg"));
            this.AddPageWithImage("This is JPEG image with Grayscale colorspace.", CreateImageSource("Resources/grayScale.jpg"));
            this.AddPageWithImage("This is JPEG image with CMYK colorspace.", CreateImageSource("Resources/cmyk.jpg"));
            this.AddPageWithImage("This is PNG image with transparency", CreateImageSource("Resources/transparent.png"));

            // JPEG2000 images must be inserted with ImageQuality.High. 
            // Exporting this image format with lower quality requires decoding the JPEG2000 image, which is currently not supported by RadPdfProcessing.
            // That is why the image quality must be specified in the constructor of the ImageSource class in order to override PdfFormatProvider.ExportSettings.ImageQuality value.
            this.AddPageWithImage("This is JPEG2000 image with RGB colorspace.", CreateImageSource("Resources/rgb.jp2", ImageQuality.High));
            this.AddPageWithImage("This is JPEG2000 image with Grayscale colorspace.", CreateImageSource("Resources/grayScale.jp2", ImageQuality.High));

            // This example shows how to insert dualtone image compressed with FlateDecode and with BitsPerComponent 1.
            // Exporting this image with the above mentioned properties will reduce the size of the produced PDF file.
            // Inserting image with EncodedImageData guarantees that the image is inserted with maximum image quality. 
            // The image is exported as it is without respecting PdfFormatProvider.ExportSettings.ImageQuality value.
            EncodedImageData encodedImageData = EncodeDualtonePngImage("Resources/dualtone.png");
            this.AddPageWithImage("This is Dualtone PNG image encoded with FlateDecode.", new ImageSource(encodedImageData));
        }

        private void AddPageWithImage(string description, ImageSource imageSource)
        {
            RadFixedPage page = this.document.Pages.AddPage();
            page.Size = PageSize;
            FixedContentEditor editor = new FixedContentEditor(page);
            editor.GraphicProperties.FillColor = new RgbColor(200, 200, 200);
            editor.DrawRectangle(new Rect(0, 0, PageSize.Width, PageSize.Height));
            editor.Position.Translate(Margins.Left, Margins.Top);

            Block block = new Block();
            block.HorizontalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.HorizontalAlignment.Center;
            block.TextProperties.FontSize = 22;
            block.InsertText(description);
            Size blockSize = block.Measure(RemainingPageSize);
            editor.DrawBlock(block, RemainingPageSize);

            editor.Position.Translate(Margins.Left, blockSize.Height + Margins.Top + 20);

            Block imageBlock = new Block();
            imageBlock.HorizontalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.HorizontalAlignment.Center;
            imageBlock.InsertImage(imageSource);
            editor.DrawBlock(imageBlock, RemainingPageSize);
        }

        private static ImageSource CreateImageSource(string resourceImagePath, ImageQuality? imageQuality = null)
        {
            using (Stream stream = GetResourceStream(resourceImagePath))
            {
                if (imageQuality.HasValue)
                {
                    return new ImageSource(stream, imageQuality.Value);
                }
                else
                {
                    return new ImageSource(stream);
                }
            }
        }

        private static EncodedImageData EncodeDualtonePngImage(string resourceImagePath)
        {
            byte[] imageData;
            int width;
            int height;
            GetDualtoneDataFromPngImage(resourceImagePath, out imageData, out width, out height);
            byte[] compressedData = CompressDataWithDeflate(imageData);
            int bitsPerComponent = 1;

            return new EncodedImageData(compressedData, bitsPerComponent, width, height, ColorSpaceNames.DeviceGray, new string[] { PdfFilterNames.FlateDecode });
        }

        private static void GetDualtoneDataFromPngImage(string resourceImagePath, out byte[] data, out int width, out int height)
        {
            using (Stream stream = GetResourceStream(resourceImagePath))
            {
#if SILVERLIGHT
                BitmapImage source = new BitmapImage();
                source.SetSource(stream);
#else
                PngBitmapDecoder decoder = new PngBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                BitmapSource source = decoder.Frames[0];
#endif
                width = source.PixelWidth;
                height = source.PixelHeight;
                int stride = (width + 7) / 8;
                data = new byte[stride * height];
#if SILVERLIGHT
                CopyPixels(data, source);
#else           
                source.CopyPixels(data, stride, 0);
#endif
            }
        }

#if SILVERLIGHT
        private static void CopyPixels(byte[] data, BitmapImage dualtoneImage)
        {
            int width = dualtoneImage.PixelWidth;
            int height = dualtoneImage.PixelHeight;
            int blackPixel = 255 << 24;

            WriteableBitmap writeableBitmap = new WriteableBitmap(dualtoneImage);
            int currentByte = 0;
            int currentBit = 0;
            int currentPixel = 0;

            for (int row = 0; row < height; row++)
            {
                for (int column = 0; column < width; column++)
                {
                    if (currentBit > 7)
                    {
                        currentBit = 0;
                        currentByte++;
                    }

                    if (writeableBitmap.Pixels[currentPixel] != blackPixel)
                    {
                        data[currentByte] = (byte)(data[currentByte] | (1 << (7 - currentBit)));
                    }

                    currentBit++;
                    currentPixel++;
                }

                currentBit = 0;
                currentByte++;
            }
        }
#endif

        private static byte[] CompressDataWithDeflate(byte[] data)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                using (CompressedStream compressedStream = new CompressedStream(stream, StreamOperationMode.Write, new DeflateSettings()))
                {
                    compressedStream.Write(data, 0, data.Length);
                }

                return stream.ToArray();
            }
        }

        private static Stream GetResourceStream(string resource)
        {
            return Application.GetResourceStream(GetResourceUri(resource)).Stream;
        }

        private static Uri GetResourceUri(string resource)
        {
            AssemblyName assemblyName = new AssemblyName(typeof(MainViewModel).Assembly.FullName);
            string resourcePath = "/" + assemblyName.Name + ";component/" + resource;
            Uri resourceUri = new Uri(resourcePath, UriKind.Relative);

            return resourceUri;
        }
    }
}
