using System.IO;
using System.Threading;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using System.Collections.Generic;
using Telerik.Windows.Cloud;

namespace CustomAmazonS3Provider.Provider
{
	class AmazonS3Context
	{
		private string fileName;
		private string bucketName;
		private int partSize;
		private Stream fileStream;
		private CloudUploadFileProgressChanged uploadProgressChanged;
		private CancellationToken cancellationToken;

		private IAmazonS3 client;
		private InitiateMultipartUploadResponse initResponse;
		private AbortMultipartUploadRequest abortMPURequest;
		private List<UploadPartResponse> uploadResponses;

		private long uploadedBytes;
		private int currentPartNumber;
		private long fileSize;

		internal long UploadedBytes { get { return this.uploadedBytes; } }

		public AmazonS3Context(IAmazonS3 client, string fileName, string existingBucketName, int partSize, Stream fileStream, CloudUploadFileProgressChanged uploadProgressChanged, CancellationToken cancellationToken)
		{
			this.fileName = fileName;
			this.bucketName = existingBucketName;
			this.partSize = partSize;
			this.fileStream = fileStream;
			this.uploadProgressChanged = uploadProgressChanged;
			this.cancellationToken = cancellationToken;
			this.client = client;
            this.fileStream.Position = 0;
			this.currentPartNumber = 1;
			this.fileSize = this.fileStream.Length;

			this.Init();
		}

		internal bool UploadNextPart()
		{
			if (this.uploadedBytes == this.fileSize)
			{
				return false;
			}

			UploadPartRequest uploadRequest = this.BuildUploadPartRequest();
			long requestTotalBytes = 0;

			if (this.uploadProgressChanged != null)
			{
				uploadRequest.StreamTransferProgress += ((object sender, StreamTransferProgressArgs e) =>
				{
					var progress = this.uploadedBytes + e.TransferredBytes;
					uploadProgressChanged(progress);
					requestTotalBytes = e.TotalBytes;
				});
			}

			var uploadPartResponse = this.client.UploadPartAsync(uploadRequest, this.cancellationToken);
            uploadPartResponse.Wait(this.cancellationToken);

			this.uploadResponses.Add(uploadPartResponse.Result);
			this.currentPartNumber++;
			this.uploadedBytes += requestTotalBytes;

			return true;
		}

		internal void CompleteMultipartUpload()
		{
			CompleteMultipartUploadRequest completeRequest = new CompleteMultipartUploadRequest
			{
				BucketName = this.bucketName,
				Key = this.fileName,
				UploadId = this.initResponse.UploadId,
			};
			completeRequest.AddPartETags(this.uploadResponses);

			this.client.CompleteMultipartUpload(completeRequest);
		}

		internal void AbortMultipartUpload()
		{
			this.client.AbortMultipartUpload(this.abortMPURequest);
		}

		private void Init()
		{
			var initiateRequest = new InitiateMultipartUploadRequest
			{
				BucketName = this.bucketName,
				Key = this.fileName
			};

			this.initResponse = client.InitiateMultipartUpload(initiateRequest);

			this.abortMPURequest = new AbortMultipartUploadRequest
			{
				BucketName = this.bucketName,
				Key = this.fileName,
				UploadId = this.initResponse.UploadId
			};

			this.uploadResponses = new List<UploadPartResponse>();
		}

		private UploadPartRequest BuildUploadPartRequest()
		{
			UploadPartRequest uploadRequest = new UploadPartRequest
			{
				BucketName = this.bucketName,
				Key = this.fileName,
				UploadId = this.initResponse.UploadId,
				PartNumber = this.currentPartNumber,
				PartSize = this.partSize,
				FilePosition = this.uploadedBytes,
				InputStream = this.fileStream,
			};

			return uploadRequest;
		}
	}
}
