using System.IO;
using System.Threading;
using Microsoft.WindowsAzure.Storage.Blob;
using Telerik.Windows.Cloud;

namespace AzureProvider
{
    public partial class AzureUploadProvider
    {
        private class BlocksSyncContext
        {
            internal readonly CloudBlockBlob Blob;
            internal readonly Stream FileStream;
            internal readonly int BlockSize;
            internal readonly CancellationToken CancellationToken;
            internal readonly object Locker;
            internal readonly CloudUploadFileProgressChanged ProgressChanged;
            internal int Index;
            internal bool Faulted;

            private long uploadedBytes;
            private object progressChangedLocker;

            internal BlocksSyncContext(CloudBlockBlob blob, Stream fileStream, int blockSize, CloudUploadFileProgressChanged progressChanged, CancellationToken cancellationToken)
            {
                this.Blob = blob;
                this.FileStream = fileStream;
                this.ProgressChanged = progressChanged;
                this.CancellationToken = cancellationToken;
                this.BlockSize = blockSize;
                this.FileStream.Position = 0;
                this.Index = 0;
                this.Locker = new object();
                this.progressChangedLocker = new object();
            }

            internal void OnBlockUploaded(long uploadedBlockSize)
            {
                var progressChanged = this.ProgressChanged;
                if (progressChanged == null)
                {
                    return;
                }

                lock (this.progressChangedLocker)
                {
                    this.uploadedBytes += uploadedBlockSize;
                    progressChanged(this.uploadedBytes);
                }
            }
        }
    }
}
