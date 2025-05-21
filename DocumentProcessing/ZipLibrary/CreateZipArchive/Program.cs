using System.Diagnostics;
using System.IO;
#if NETCOREAPP
using Telerik.Zip;
#else
using Telerik.Windows.Zip;
#endif

namespace CreateZipArchive_NetStandard
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string zipFileName = "MyFiles.zip";

            using (Stream stream = File.Open(zipFileName, FileMode.Create))
            {
                using (ZipArchive archive = ZipArchive.Create(stream))
                {
                    using (ZipArchiveEntry entry = archive.CreateEntry("text.txt"))
                    {
                        StreamWriter writer = new StreamWriter(entry.Open());
                        writer.WriteLine("Progress!");
                        writer.Flush();
                    }
                }
            }

            ProcessStartInfo psi = new ProcessStartInfo()
            {
                FileName = zipFileName,
                UseShellExecute = true
            };
            Process.Start(psi);
        }
    }
}
