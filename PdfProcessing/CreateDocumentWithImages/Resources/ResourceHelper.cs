using System;
using System.IO;
using System.Reflection;
using System.Windows;

namespace CreateDocumentWithImages.Resources
{
    public class ResourceHelper
    {
        public static Uri GetResourceUri(string resource)
        {
            AssemblyName assemblyName = new AssemblyName(typeof(ResourceHelper).Assembly.FullName);
            string resourcePath = "/" + assemblyName.Name + ";component/" + resource;
            Uri resourceUri = new Uri(resourcePath, UriKind.Relative);

            return resourceUri;
        }

        public static Stream GetResourceStream(string resource)
        {
            return Application.GetResourceStream(ResourceHelper.GetResourceUri(resource)).Stream;
        }

        public static byte[] GetResourceBytes(string resource)
        {
            MemoryStream memory = new MemoryStream();

            using(Stream stream = GetResourceStream(resource))
            {
                stream.CopyTo(memory);
            }

            return memory.ToArray();
        }
    }
}
