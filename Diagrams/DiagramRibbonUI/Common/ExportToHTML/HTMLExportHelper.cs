using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Resources;
using Microsoft.Win32;
using Telerik.Windows.Controls;

namespace DiagramFirstLookDemo
{
    public static class HTMLExportHelper
    {
        public const string DocumentStart = "<!DOCTYPE html> \n" +
                                             "<html xmlns=\"http://www.w3.org/1999/xhtml\"> \n";

        public const string HeadStart = "<head> \n" +
                                         "<meta charset=\"UTF-8\"/>" +
                                         "<title>RadDiagram | JS</title> \n" +
                                         "<script type=\"text/javascript\">";

        public const string ScriptEnd = "</script> \n";

        public const string HeadEnd = "</head> \n";

        public const string Body = "<body onload=\"load()\"> \n" +
                                    "<div id=\"surface\" style=\"height:768px; width:1024px;\"> \n" +
                                    "</div> \n" +
                                    "<div id=\"tooltip\" class=\"collapsed\"> \n" +
                                    "<div style=\"margin: 6px 10px 0px 10px; font-size: smaller\"> \n" +
                                    "<label id=\"contentLabel\"></label> \n" +
                                    "</div> \n" +
                                    "<div style=\"margin: 0px 10px 6px 10px\"> \n" +
                                    "<label style=\"font-weight: 600; font-size: 12px\">X:</label> \n" +
                                    "<label id=\"posXLabel\" style=\"margin-right: 5px; font-size: 12px\"></label> \n" +
                                    "<label style=\"font-weight: 600; font-size: 12px\">Y:</label> \n" +
                                    "<label id=\"posYLabel\" style=\"font-size: 12px\"></label> \n" +
                                    "</div> \n" +
                                    "</div> \n" +
                                    @"<p class=""info"">Source code on <a href=""https://github.com/telerik/diagram-html-export"" target=""_blank"">GitHub</a>  &nbsp;|&nbsp;  Use CTRL-drag to pan and the mousewheel to zoom.</p>" +
                                    "</body> \n";

        public const string DocumentEnd = "</html> \n";
        public const string RadSVGFilePath = @"/Common/ExportToHTML/RadSVG.js";
        public const string RadDiagramFilePath = @"/Common/ExportToHTML/RadDiagram.js";
        public const string JSInteractionsFilePath = @"/Common/ExportToHTML/Loader.js";
        public const string ExportStyleFilePath = @"/Common/ExportToHTML/ExportStyles.html";

        public static void CreateHTMLFile(RadDiagram diagram, string jsRadSVGFilePath = RadSVGFilePath, string jsRadDiagramFilePath = RadDiagramFilePath)
        {
            string htmlContent = string.Empty;

            htmlContent += HTMLExportHelper.DocumentStart;

            htmlContent += HTMLExportHelper.HeadStart;

            htmlContent += LoadStringFromFile(jsRadSVGFilePath);
            htmlContent += LoadStringFromFile(jsRadDiagramFilePath);

            var serialization = diagram.Serialize();
            htmlContent += "var diagramXML = '" + serialization.ToString().Replace(System.Environment.NewLine, " ") + "'; \n";

            htmlContent += LoadStringFromFile(JSInteractionsFilePath);

            htmlContent += HTMLExportHelper.ScriptEnd;

            htmlContent += LoadStringFromFile(ExportStyleFilePath);

            htmlContent += HTMLExportHelper.HeadEnd;

            htmlContent += HTMLExportHelper.Body;

            htmlContent += HTMLExportHelper.DocumentEnd;

            try
            {
                var saveFileDialog = new SaveFileDialog() { Filter = "HTML (.html;*.htm)|*.html;*.htm|All Files (*.*)|*.*" };

                var result = saveFileDialog.ShowDialog();
                if (result == true)
                {
                    using (var fileStream = saveFileDialog.OpenFile())
                    {
                        using (var writer = new StreamWriter(fileStream))
                        {
                            writer.Write(htmlContent);
                        }
                    }

#if WPF
                    Process.Start(saveFileDialog.FileName);
#endif
                }
            }
            catch (Exception exc)
            {
#if WPF
                if (exc is Win32Exception)
                {
                    // error finding the file
                }
                else
#endif
                    if (exc is ObjectDisposedException || exc is FileNotFoundException)
                    {
                        //failed to start the default browser or HTML viewer					
                    }
                    else throw;
            }
        }

        private static string LoadStringFromFile(string path)
        {
            string javaScript = string.Empty;

            using (var stream = ExtensionUtilities.GetStream(path))
            {
                using (var reader = new StreamReader(stream))
                {
                    javaScript = reader.ReadToEnd();
                }
            }

            return javaScript;
        }
    }
}
