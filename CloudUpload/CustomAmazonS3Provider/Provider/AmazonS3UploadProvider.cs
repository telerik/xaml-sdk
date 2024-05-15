using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CustomAmazonS3Provider.Provider;
using Telerik.Windows.Cloud;
using Amazon.S3;

namespace AmazonS3Provider
{
	public class AmazonS3UploadProvider : ICloudUploadProvider
	{
		private IAmazonS3 s3Client;
		const int MinPartSize = 5242880;

		public AmazonS3UploadSettings UploadFileSettings { get; set; }

		public AmazonS3UploadProvider(string accessKeyID, string secretAccessKey)
		{
			this.s3Client = new AmazonS3Client(accessKeyID, secretAccessKey, Amazon.RegionEndpoint.USEast1);
		}

		public Task<object> UploadFileAsync(string fileName, Stream fileStream, CloudUploadFileProgressChanged uploadProgressChanged, CancellationToken cancellationToken)
		{
			return System.Threading.Tasks.Task.Factory.StartNew<object>(() => UploadFileOnBlocks(fileName, fileStream, uploadProgressChanged, cancellationToken), cancellationToken);
		}
	
		private object UploadFileOnBlocks(string fileName, Stream fileStream, CloudUploadFileProgressChanged uploadProgressChanged, CancellationToken cancellationToken)
		{
			int partSize = MinPartSize;
			string existingBucketName = GetUploadFileSettings().Bucket;

			var context = new AmazonS3Context(s3Client, fileName, existingBucketName, partSize, fileStream, uploadProgressChanged, cancellationToken);

			try
			{
				while (context.UploadNextPart());
				context.CompleteMultipartUpload();
			}
			catch (Exception e)
			{
				context.AbortMultipartUpload();
				throw;
			}

			return fileName;
		}

		private AmazonS3UploadSettings GetUploadFileSettings()
		{
			AmazonS3UploadSettings settings = this.UploadFileSettings;
			if (settings == null)
			{
				throw new Exception("You must set the UploadFileSettings of the AmazonS3UploadProvider first.");
			}

			return settings;
		}
	}
}
