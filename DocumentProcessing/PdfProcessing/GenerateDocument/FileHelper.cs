using System;
using System.IO;
using System.Reflection;
using System.Windows;

namespace GenerateDocument
{
    public static class FileHelper
    {
        private static readonly string SampleDataFolder = "SampleData/";      

        public static Stream GetSampleResourceStream(string resource)
        {
            var streamInfo = Application.GetResourceStream(GetResourceUri(SampleDataFolder + resource));
            if (streamInfo != null)
            {
                return streamInfo.Stream;
            }

            return null;
        }

        private static Uri GetResourceUri(string resource)
        {
            AssemblyName assemblyName = new AssemblyName(typeof(FileHelper).Assembly.FullName);
            string resourcePath = "/" + assemblyName.Name + ";component/" + resource;
            Uri resourceUri = new Uri(resourcePath, UriKind.Relative);

            return resourceUri;
        }
    }
}