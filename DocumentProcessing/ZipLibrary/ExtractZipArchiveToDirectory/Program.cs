using System;
using System.Diagnostics;
using System.IO;
using Telerik.Windows.Zip.Extensions;

namespace ExtractZipArchiveToDirectory_NetStandard
{
    internal class Program
    {
        public static readonly string RootDirectory = AppDomain.CurrentDomain.BaseDirectory;
        private static readonly string SampleDataPath = RootDirectory + "Resources";

        private static void Main(string[] args)
        {
            string zipFileName = "MyFiles.zip";
            string zipFilePath = Path.Combine(SampleDataPath, zipFileName);

            string destinationFolder = Path.Combine(RootDirectory, "ExtractedFiles");

            if (Directory.Exists(destinationFolder))
            {
                Directory.Delete(destinationFolder, recursive: true);
            }

            ZipFile.ExtractToDirectory(zipFilePath, destinationFolder);

            Console.WriteLine("Listing files in: " + destinationFolder);
            foreach (string fileName in Directory.EnumerateFiles(destinationFolder))
            {
                Console.WriteLine(Path.GetFileName(fileName));
            }

            ProcessStartInfo psi = new ProcessStartInfo()
            {
                FileName = destinationFolder,
                UseShellExecute = true
            };
            Process.Start(psi);
        }
    }
}
