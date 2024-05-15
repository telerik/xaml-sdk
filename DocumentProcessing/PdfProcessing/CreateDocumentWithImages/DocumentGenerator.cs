using System;
using System.Diagnostics;
using System.IO;
#if NETCOREAPP
using Telerik.Documents.Primitives;
#else
using System.Windows.Media.Imaging;
using System.Windows;
using Telerik.Windows.Zip;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf.Filters;
#endif
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf.Export;
using Telerik.Windows.Documents.Fixed.Model;
using Telerik.Windows.Documents.Fixed.Model.ColorSpaces;
using Telerik.Windows.Documents.Fixed.Model.Editing;
using Telerik.Windows.Documents.Fixed.Model.Resources;

namespace CreateDocumentWithImages
{
    internal class DocumentGenerator
    {
        public static readonly Size PageSize = new Size(Telerik.Windows.Documents.Media.Unit.MmToDip(210), Telerik.Windows.Documents.Media.Unit.MmToDip(297));
        public static readonly Thickness Margins = new Thickness(Telerik.Windows.Documents.Media.Unit.MmToDip(10));
        public static readonly Size RemainingPageSize = new Size(PageSize.Width - Margins.Left - Margins.Right, PageSize.Height - Margins.Top - Margins.Bottom);
        public const int OpaqueAlpha = 255;

        private readonly ImageQuality imageQuality;
        private readonly RadFixedDocument document;

        public DocumentGenerator(ImageQuality imageQuality)
        {
            this.imageQuality = imageQuality;
            this.document = new RadFixedDocument();

            this.GenerateDocumentContent();
        }

        public void SaveFile(string filePath)
        {
            string defaultFileName = string.Format("PDF with ImageQuality {0}.pdf", this.imageQuality);

            string resultFile = Path.Combine(filePath, defaultFileName);

            PrepareDirectory(filePath, resultFile);

            PdfFormatProvider provider;
            provider = new PdfFormatProvider();

            using (Stream stream = File.OpenWrite(resultFile))
            {
                provider.ExportSettings.ImageQuality = this.imageQuality;
                provider.Export(this.document, stream);
            }

            Console.WriteLine("Document created.");

            ProcessStartInfo psi = new ProcessStartInfo()
            {
                FileName = resultFile,
                UseShellExecute = true
            };
            Process.Start(psi);
        }

        private static void PrepareDirectory(string filePath, string resultFile)
        {
            if (Directory.Exists(filePath))
            {
                if (File.Exists(resultFile))
                {
                    File.Delete(resultFile);
                }
            }
            else
            {
                Directory.CreateDirectory(filePath);
            }
        }

        private void GenerateDocumentContent()
        {
            // The default image quality is controlled by PdfFormatProvider.ExportSettings.
            this.AddPageWithImage("This is JPEG image with RGB colorspace.", CreateImageSource("Resources/rgb.jpg"));
            this.AddPageWithImage("This is JPEG image with Grayscale colorspace.", CreateImageSource("Resources/grayScale.jpg"));
            this.AddPageWithImage("This is JPEG image with CMYK colorspace.", CreateImageSource("Resources/cmyk.jpg"));
#if !NETCOREAPP
            this.AddPageWithImage("This is PNG image with transparency encoded with DCTDecode.", CreateImageSource("Resources/transparent.png"));
#endif
#if !NETCOREAPP
            // This example shows how to insert RGBA image compressed with FlateDecode.
            // This approach guarantees lossless image quality compression different from the previous compression which exports the images with the lossy DCTDecode filter.
            EncodedImageData imageDataFlateDecode = this.EncodeRgbaPngImageWithFlateDecode("Resources/transparent.png");
            this.AddPageWithImage("This is RGBA PNG image encoded with FlateDecode.", new ImageSource(imageDataFlateDecode));
#endif

            // JPEG2000 images must be inserted with ImageQuality.High. 
            // Exporting this image format with lower quality requires decoding the JPEG2000 image, which is currently not supported by RadPdfProcessing.
            // That is why the image quality must be specified in the constructor of the ImageSource class in order to override PdfFormatProvider.ExportSettings.ImageQuality value.
            this.AddPageWithImage("This is JPEG2000 image with RGB colorspace.", CreateImageSource("Resources/rgb.jp2", ImageQuality.High));
            this.AddPageWithImage("This is JPEG2000 image with Grayscale colorspace.", CreateImageSource("Resources/grayScale.jp2", ImageQuality.High));
#if !NETCOREAPP
            // This example shows how to insert dualtone image compressed with FlateDecode and with BitsPerComponent 1.
            // Exporting this image with the above mentioned properties will reduce the size of the produced PDF file.
            // Inserting image with EncodedImageData guarantees that the image is inserted with maximum image quality. 
            // The image is exported as it is without respecting PdfFormatProvider.ExportSettings.ImageQuality value.
            EncodedImageData encodedImageData = EncodeDualtonePngImage("Resources/dualtone.png");
            this.AddPageWithImage("This is Dualtone PNG image encoded with FlateDecode.", new ImageSource(encodedImageData));
#endif
        }

        private void AddPageWithImage(string description, ImageSource imageSource)
        {
            RadFixedPage page = this.document.Pages.AddPage();
            page.Size = PageSize;
            FixedContentEditor editor = new FixedContentEditor(page);
            editor.GraphicProperties.StrokeThickness = 0;
            editor.GraphicProperties.IsStroked = false;
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
            using (Stream stream = File.OpenRead(resourceImagePath))
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

#if !NETCOREAPP
        private EncodedImageData EncodeRgbaPngImageWithFlateDecode(string resourceImagePath)
        {
            using (Stream stream = File.OpenRead(resourceImagePath))
            {
                PngBitmapDecoder decoder = new PngBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                BitmapSource source = decoder.Frames[0];

                /* For Silverlight you can use the following code to create the source:
                BitmapImage source = new BitmapImage();
                source.SetSource(stream); */

                byte[] rawData, rawAlpha;
                GetRawDataFromRgbaSource(source, out rawData, out rawAlpha);
                byte[] data = CompressDataWithDeflate(rawData);
                byte[] alpha = rawAlpha == null ? null : CompressDataWithDeflate(rawAlpha);

                return new EncodedImageData(data, alpha, 8, source.PixelWidth, source.PixelHeight, ColorSpaceNames.DeviceRgb, new string[] { PdfFilterNames.FlateDecode });
            }
        }
#endif
#if !NETCOREAPP
        private void GetRawDataFromRgbaSource(BitmapSource source, out byte[] data, out byte[] alpha)
        {
            int[] pixels = new int[source.PixelWidth * source.PixelHeight];
            source.CopyPixels(pixels, source.PixelWidth * 4, 0);

            /* For Silverlight you can use the following code to extract the pixels:
             int[] pixels = new WriteableBitmap(source).Pixels;
            */

            data = new byte[pixels.Length * 3];
            alpha = new byte[pixels.Length];
            bool shouldExportAlpha = false;

            for (int i = 0; i < pixels.Length; i++)
            {
                int pixel = pixels[i];
                byte b = (byte)(pixel & 0xFF);
                byte g = (byte)((pixel >> 8) & 0xFF);
                byte r = (byte)((pixel >> 16) & 0xFF);
                byte a = (byte)((pixel >> 24) & 0xFF);

                data[3 * i] = r;
                data[3 * i + 1] = g;
                data[3 * i + 2] = b;
                alpha[i] = a;

                if (a != OpaqueAlpha)
                {
                    shouldExportAlpha = true;
                }
            }

            if (!shouldExportAlpha)
            {
                alpha = null;
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
            using (Stream stream = File.OpenRead(resourceImagePath))
            {
                PngBitmapDecoder decoder = new PngBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                BitmapSource source = decoder.Frames[0];

                /* For Silverlight use the following code to create the source:
                BitmapImage source = new BitmapImage();
                source.SetSource(stream); */

                width = source.PixelWidth;
                height = source.PixelHeight;
                int stride = (width + 7) / 8;
                data = new byte[stride * height];

                source.CopyPixels(data, stride, 0); // For Silverlight, invoke the CopyDualtonePixels() method: CopyDualtonePixels(data, source);

            }
        }

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
#endif

        /* Use the following method in Silverlight 
        private static void CopyDualtonePixels(byte[] data, BitmapImage dualtoneImage)
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
        } */
    }
}