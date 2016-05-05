using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Telerik.Windows.Cloud;

namespace DragAndDrop
{
    public class DummyProvider : ICloudUploadProvider
    {
        private Random random;

        public DummyProvider()
        {
            this.random = new Random();
        }

        public Task<object> UploadFileAsync(string fileName, Stream fileStream, CloudUploadFileProgressChanged uploadProgressChanged, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew<object>(() => UploadFile(fileName, fileStream, uploadProgressChanged, cancellationToken), cancellationToken);
        }

        private object UploadFile(string fileName, Stream fileStream, CloudUploadFileProgressChanged uploadProgressChanged, CancellationToken cancellationToken)
        {
            long fileLength = fileStream.Length;
            int uploadIterations = this.random.Next(10, 40);
            int chunkSize = (int)(fileLength / uploadIterations);

            for (int i = 0; i < uploadIterations; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();
                uploadProgressChanged(i * chunkSize);
                Thread.Sleep(this.random.Next(200, 900));
            }

            uploadProgressChanged(fileLength);

            return fileName;
        }
    }
}
