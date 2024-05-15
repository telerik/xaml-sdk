using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Telerik.Windows.Cloud;

namespace AzureProvider
{
    // nuget package: Install-Package WindowsAzure.Storage

    public partial class AzureUploadProvider : ICloudUploadProvider
    {
        private CloudStorageAccount account;

        public AzureUploadSettings UploadFileSettings { get; set; }

        public AzureUploadProvider(CloudStorageAccount account)
        {
            this.account = account;
        }

        public AzureUploadProvider(string accountName, string accountKey)
        {
            this.account = CloudStorageAccount.Parse("DefaultEndpointsProtocol=http;AccountName=" + accountName + ";AccountKey=" + accountKey);
        }

        public System.Threading.Tasks.Task<object> UploadFileAsync(string fileName, Stream fileStream, CloudUploadFileProgressChanged uploadProgressChanged, CancellationToken cancellationToken)
        {
            return System.Threading.Tasks.Task.Factory.StartNew<object>(() => UploadFileOnBlocks(fileName, fileStream, uploadProgressChanged, cancellationToken), cancellationToken);
        }

        private Uri UploadFileOnBlocks(string fileName, Stream fileStream, CloudUploadFileProgressChanged uploadProgressChanged, CancellationToken cancellationToken)
        {
            const int NumberOfTasks = 2;
            const int NumberOfBlocks = 50;

            var settings = this.GetUploadFileSettings();
            var blobClient = this.account.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(settings.Container);
            container.CreateIfNotExists();
            var blobName = this.GetBlobName(settings.Path, fileName);
            var blob = container.GetBlockBlobReference(blobName);

            int MinPartSize = (int)Math.Pow(2, 17);
            int MaxPartSize = (int)Math.Pow(2, 22);
            int partSize = Math.Min(MaxPartSize, Math.Max(MinPartSize, (int)(fileStream.Length / NumberOfBlocks)));
            BlocksSyncContext context = new BlocksSyncContext(blob, fileStream, partSize, uploadProgressChanged, cancellationToken);

            Task[] tasks = new Task[NumberOfTasks];
            for (int i = 0; i < NumberOfTasks; i++)
            {
                Task task = System.Threading.Tasks.Task.Factory.StartNew(() => UploadBlocks(context), cancellationToken);
                task.ContinueWith(t => { if (t.IsFaulted) context.Faulted = true; });
                tasks[i] = task;
            }

            Task.WaitAll(tasks, cancellationToken);
            // If the WaitAll throws, perhaps there are uncommitted blocks and the blocks have different id lengths, may be the uncommitted blocks need to be disposed first.
            // http://gauravmantri.com/2013/05/18/windows-azure-blob-storage-dealing-with-the-specified-blob-or-block-content-is-invalid-error/

            var blockList = Enumerable.Range(1, context.Index).ToList<int>().ConvertAll(i => GenerateBlockId(i));
            blob.PutBlockList(blockList);
            return blob.Uri;
        }

        private string GetBlobName(string path, string fileName)
        {
            if (string.IsNullOrEmpty(path))
            {
                return fileName;
            }

            path += path.EndsWith("/") ? "" : "/";
            return path + fileName;
        }

        private static void UploadBlocks(BlocksSyncContext context)
        {
            int readBytes;
            int i;
            byte[] buffer = new byte[context.BlockSize];

            while (true)
            {
                if (context.Faulted)
                {
                    return;
                }

                context.CancellationToken.ThrowIfCancellationRequested();

                lock (context.Locker)
                {
                    readBytes = context.FileStream.Read(buffer, 0, context.BlockSize);
                    if (readBytes == 0)
                    {
                        break;
                    }

                    context.Index++;
                    i = context.Index;
                }

                string blockId = GenerateBlockId(i);
                using (var blockStream = new MemoryStream(buffer, 0, readBytes))
                {
                    context.Blob.PutBlock(blockId, blockStream, null, null, new BlobRequestOptions { UseTransactionalMD5 = true }, null);
                    context.CancellationToken.ThrowIfCancellationRequested();
                    context.OnBlockUploaded(readBytes);
                }
            }
        }

        private static string GenerateBlockId(int index)
        {
            string id = string.Format(System.Globalization.CultureInfo.InvariantCulture, "Telerik is awesome!!! {0:D4}", index);
            string blockId = Convert.ToBase64String(Encoding.UTF8.GetBytes(id));

            return blockId;
        }

        private AzureUploadSettings GetUploadFileSettings()
        {
            AzureUploadSettings settings = this.UploadFileSettings;
            if (settings == null)
            {
                throw new Exception("You must set the UploadFileSettings of the AzureUploadProvider first.");
            }

            return settings;
        }
    }
}
