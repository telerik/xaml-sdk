using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using Telerik.Windows.Documents.Fixed.Model;
using Telerik.Windows.Documents.Fixed.Model.ColorSpaces;
using Telerik.Windows.Documents.Fixed.Model.Editing;
using Telerik.Windows.Documents.Fixed.Model.Resources;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf.Streaming;

namespace ManipulatePages
{
    class Program
    {
        public const string ResultDirName = "Demo results";
        public const string InputFileBarChart = "InputFiles/BarChart.pdf";
        public const string InputFileCentaurFeatures = "InputFiles/CentaurFeatures2014Q3.pdf";
        public const string InputFileBackgroundImage = "InputFiles/GRAYSCALE.jpg";
        public const string InputFileMultipageDocument = "InputFiles/MultipageDocument.pdf";
        public const string InputFileSoundVideoAnd3D = "InputFiles/Sound Video and 3D.pdf";

        static void Main()
        {
            EnsureEmptyResultDirectory();

            MergeDifferentDocumentsPages("Merge demo result.pdf");
            SplitDocumentPages("Split demo result for page {0}.pdf");
            FitAndPositionMultiplePagesOnSinglePage("Fit four pages on one page demo result.pdf");
            PrependAndAppendPageContent("Prepend and append page content demo result.pdf");

            Process.Start(ResultDirName);
        }

        private static void MergeDifferentDocumentsPages(string resultFileName)
        {
            string[] documentsToMerge =
            {
                InputFileSoundVideoAnd3D,
                InputFileBarChart,
                InputFileCentaurFeatures,
                InputFileMultipageDocument
            };

            string resultFile = Path.Combine(ResultDirName, resultFileName);

            using (PdfStreamWriter fileWriter = new PdfStreamWriter(File.OpenWrite(resultFile)))
            {
                foreach (string documentName in documentsToMerge)
                {
                    using (PdfFileSource fileToMerge = new PdfFileSource(File.OpenRead(documentName)))
                    {
                        foreach (PdfPageSource pageToMerge in fileToMerge.Pages)
                        {
                            fileWriter.WritePage(pageToMerge);
                        }
                    }
                }
            }
        }

        private static void SplitDocumentPages(string resultFileFormat)
        {
            string documentToSplit = InputFileMultipageDocument;

            using (PdfFileSource fileToSplit = new PdfFileSource(File.OpenRead(documentToSplit)))
            {
                for (int i = 0; i < fileToSplit.Pages.Length; i++)
                {
                    PdfPageSource page = fileToSplit.Pages[i];
                    string splitDocumentName = Path.Combine(ResultDirName, string.Format(resultFileFormat, i + 1));

                    using (PdfStreamWriter fileWriter = new PdfStreamWriter(File.OpenWrite(splitDocumentName)))
                    {
                        fileWriter.WritePage(page);
                    }
                }
            }
        }

        private static void FitAndPositionMultiplePagesOnSinglePage(string resultFileName)
        {
            string initialDocument = InputFileMultipageDocument;
            string resultDocument = Path.Combine(ResultDirName, resultFileName);

            using (PdfStreamWriter fileWriter = new PdfStreamWriter(File.OpenWrite(resultDocument)))
            {
                using (PdfFileSource fileSource = new PdfFileSource(File.OpenRead(initialDocument)))
                {
                    double halfWidth = fileSource.Pages[0].Size.Width / 2;
                    double halfHeight = fileSource.Pages[0].Size.Height / 2;
                    Vector[] translateDirections = new Vector[] { new Vector(0, 0), new Vector(1, 0), new Vector(0, 1), new Vector(1, 1) };
                    PdfPageStreamWriter resultPage = null;

                    for (int i = 0; i < fileSource.Pages.Length; i++)
                    {
                        if (i % 4 == 0)
                        {
                            if (i > 0)
                            {
                                resultPage.Dispose();
                            }

                            resultPage = fileWriter.BeginPage(fileSource.Pages[0].Size, fileSource.Pages[0].Rotation);
                            resultPage.ContentPosition.Scale(0.5, 0.5);
                        }

                        Vector direction = translateDirections[i % 4];
                        resultPage.ContentPosition.Translate(direction.X * halfWidth, direction.Y * halfHeight);
                        PdfPageSource sourcePage = fileSource.Pages[i];
                        resultPage.WriteContent(sourcePage);
                    }

                    resultPage.Dispose();
                }
            }
        }

        private static void PrependAndAppendPageContent(string resultFileName)
        {
            string initialDocument = InputFileMultipageDocument;
            string resultDocument = Path.Combine(ResultDirName, resultFileName);

            using (PdfStreamWriter fileWriter = new PdfStreamWriter(File.OpenWrite(resultDocument)))
            {
                using (PdfFileSource fileSource = new PdfFileSource(File.OpenRead(initialDocument)))
                {
                    RadFixedPage backgroundContentOwner = GenerateBackgroundImageContent(InputFileBackgroundImage);
                    RadFixedPage foregroundContentOwner = GenerateForegroundTextContent("WATERMARK");

                    foreach (PdfPageSource pageSource in fileSource.Pages)
                    {
                        using (PdfPageStreamWriter pageWriter = fileWriter.BeginPage(pageSource.Size, pageSource.Rotation))
                        {
                            using (pageWriter.SaveContentPosition())
                            {
                                double xCenteringTranslation = (pageSource.Size.Width - backgroundContentOwner.Size.Width) / 2;
                                double yCenteringTranslation = (pageSource.Size.Height - backgroundContentOwner.Size.Height) / 2;
                                pageWriter.ContentPosition.Translate(xCenteringTranslation, yCenteringTranslation);
                                pageWriter.WriteContent(backgroundContentOwner);
                            }

                            pageWriter.WriteContent(pageSource);

                            using (pageWriter.SaveContentPosition())
                            {
                                double xCenteringTranslation = (pageSource.Size.Width - foregroundContentOwner.Size.Width) / 2;
                                double yCenteringTranslation = (pageSource.Size.Height - foregroundContentOwner.Size.Height) / 2;
                                pageWriter.ContentPosition.Translate(xCenteringTranslation, yCenteringTranslation);
                                pageWriter.WriteContent(foregroundContentOwner);
                            }
                        }
                    }
                }
            }
        }

        private static RadFixedPage GenerateForegroundTextContent(string text)
        {
            Block block = new Block();
            block.BackgroundColor = new RgbColor(50, 0, 0, 0);
            block.GraphicProperties.FillColor = new RgbColor(255, 0, 0);
            block.TextProperties.FontSize = 120;
            block.InsertText(text);
            Size horizontalTextSize = block.Measure();
            double rotatedTextSquareSize = (horizontalTextSize.Width + horizontalTextSize.Height) / Math.Sqrt(2);

            RadFixedPage foregroundContentOwner = new RadFixedPage();
            foregroundContentOwner.Size = new Size(rotatedTextSquareSize, rotatedTextSquareSize);
            FixedContentEditor foregroundEditor = new FixedContentEditor(foregroundContentOwner);
            foregroundEditor.Position.Translate(horizontalTextSize.Height / Math.Sqrt(2), 0);
            foregroundEditor.Position.Rotate(45);
            foregroundEditor.DrawBlock(block);

            return foregroundContentOwner;
        }

        private static RadFixedPage GenerateBackgroundImageContent(string watermarkImage)
        {
            using (Stream imageStream = File.OpenRead(watermarkImage))
            {
                ImageSource image = new ImageSource(imageStream);
                RadFixedPage backgroundContentOwner = new RadFixedPage();
                backgroundContentOwner.Size = new Size(image.Width, image.Height);
                FixedContentEditor imagePageEditor = new FixedContentEditor(backgroundContentOwner);
                imagePageEditor.DrawImage(image);

                return backgroundContentOwner;
            }
        }

        private static void EnsureEmptyResultDirectory()
        {
            if (Directory.Exists(ResultDirName))
            {
                foreach(string fileName in Directory.EnumerateFiles(ResultDirName))
                {
                    File.Delete(fileName);
                }
            }
            else
            {
                Directory.CreateDirectory(ResultDirName);
            }
        }
    }
}
