using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CreateDocumentWithImages.Resources;
using Microsoft.Win32;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf.Filters;
using Telerik.Windows.Documents.Fixed.Model;
using Telerik.Windows.Documents.Fixed.Model.ColorSpaces;
using Telerik.Windows.Documents.Fixed.Model.Editing;
using Telerik.Windows.Documents.Fixed.Model.Resources;

namespace CreateDocumentWithImages
{
    public partial class MainPage : UserControl
    {
        public const string TestFileName = "test.pdf";
        public static readonly Size PageSize = new Size(Telerik.Windows.Documents.Media.Unit.MmToDip(210), Telerik.Windows.Documents.Media.Unit.MmToDip(297));
        public static readonly Thickness Margins = new Thickness(Telerik.Windows.Documents.Media.Unit.MmToDip(10));
        public static readonly Size RemainingPageSize = new Size(PageSize.Width - Margins.Left - Margins.Right, PageSize.Height - Margins.Top - Margins.Bottom);

        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Pdf file|*.pdf";

            if (saveFileDialog1.ShowDialog() == true)
            {
                using (FileStream fs = (FileStream)saveFileDialog1.OpenFile())
                {
                    byte[] documentBytes = this.CreateDocument();
                    fs.Write(documentBytes, 0, documentBytes.Length);
                }
            }
        }

        private byte[] CreateDocument()
        {
            RadFixedDocument document = new RadFixedDocument();

            byte[] resourceBytes = ResourceHelper.GetResourceBytes("Resources/rgb.jpg");
            EncodedImageData rgbJpeg = new EncodedImageData(resourceBytes, 8, 655, 983, ColorSpaceNames.DeviceRgb, new string[] { PdfFilterNames.DCTDecode });
            this.CreatePageWithImage(document, "JPEG", rgbJpeg);

            resourceBytes = ResourceHelper.GetResourceBytes("Resources/grayScale.jpg");
            EncodedImageData grayJpeg = new EncodedImageData(resourceBytes, 8, 655, 983, ColorSpaceNames.DeviceGray, new string[] { PdfFilterNames.DCTDecode });
            this.CreatePageWithImage(document, "JPEG", grayJpeg);

            // decode is used for inverting the colors of the cmyk image.
            // More information on the matter can be found here: http://graphicdesign.stackexchange.com/a/15906
            double[] decode = new double[] { 1, 0, 1, 0, 1, 0, 1, 0 };
            resourceBytes = ResourceHelper.GetResourceBytes("Resources/cmyk.jpg");
            EncodedImageData cmykJpeg = new EncodedImageData(resourceBytes, 8, 655, 983, ColorSpaceNames.DeviceCmyk, new string[] { PdfFilterNames.DCTDecode });
            this.CreatePageWithImage(document, "JPEG", cmykJpeg, decode);

            resourceBytes = ResourceHelper.GetResourceBytes("Resources/rgb.jp2");
            EncodedImageData rgbJpc = new EncodedImageData(resourceBytes, 8, 655, 983, ColorSpaceNames.DeviceRgb, new string[] { PdfFilterNames.JPXDecode });
            this.CreatePageWithImage(document, "JPEG 2000", rgbJpc);

            resourceBytes = ResourceHelper.GetResourceBytes("Resources/grayScale.jp2");
            EncodedImageData grayScaleJpc = new EncodedImageData(resourceBytes, 8, 655, 983, ColorSpaceNames.DeviceGray, new string[] { PdfFilterNames.JPXDecode });
            this.CreatePageWithImage(document, "JPEG 2000", grayScaleJpc);

            if (File.Exists(TestFileName))
            {
                File.Delete(TestFileName);
            }

            byte[] documentBytes = new PdfFormatProvider().Export(document);

            return documentBytes;
        }

        private void CreatePageWithImage(RadFixedDocument document, string imageExtension, EncodedImageData imageData, double[] decode = null)
        {
            RadFixedPage page = document.Pages.AddPage();
            page.Size = PageSize;
            FixedContentEditor editor = new FixedContentEditor(page);
            editor.Position.Translate(Margins.Left, Margins.Top);

            Block block = new Block();
            block.HorizontalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.HorizontalAlignment.Center;
            block.TextProperties.FontSize = 22;
            block.InsertText(string.Format("This is {0} image in {1} color space encoded with {2} filter.", imageExtension, imageData.ColorSpace, imageData.Filters.FirstOrDefault() ?? "None"));
            Size blockSize = block.Measure(RemainingPageSize);
            editor.DrawBlock(block, RemainingPageSize);

            editor.Position.Translate(Margins.Left, blockSize.Height + Margins.Top + 50);

            Block imageBlock = new Block();
            imageBlock.HorizontalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.HorizontalAlignment.Center;
            ImageSource imageSource = new ImageSource(imageData);
            imageSource.DecodeArray = decode;
            imageBlock.InsertImage(imageSource, new Size(imageData.Width, imageData.Height));
            editor.DrawBlock(imageBlock, RemainingPageSize);
        }
    }
}
