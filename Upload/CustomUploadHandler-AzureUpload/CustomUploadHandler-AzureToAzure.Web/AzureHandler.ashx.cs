using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using Telerik.Windows;

namespace CustomUploadHandler_AzureToAzure.Web
{
	/// <summary>
	/// Summary description for AzureHandler
	/// </summary>
	public class AzureHandler : RadUploadHandler
	{
		private const string AccountName = "";
		private const string AccountKey = "";
		private const string ConrainerName = ""; //// Should be in lower case.

		private static readonly CloudStorageAccount account;
		private static readonly List<string> blocksList = new List<string>();
		private static int blockId;
		private static CloudBlockBlob blob;
		private static CloudBlobContainer container;

		static AzureHandler()
		{
			account = CloudStorageAccount.Parse("DefaultEndpointsProtocol=http;AccountName=" + AzureHandler.AccountName + ";AccountKey=" + AzureHandler.AccountKey);
			AzureHandler.InitializeContainer();
		}

		public override bool SaveChunkData(string filePath, long position, byte[] buffer, int contentLength, out int savedBytes)
		{
			if (this.IsNewFileRequest())
			{
				this.InitializeBlob();
			}

			blockId++;
			using (var blockStream = new MemoryStream(buffer))
			{
				var blockGuid = GenerateBlockId(blockId, this.GetFileName());
				blocksList.Add(blockGuid);
				blob.PutBlock(blockGuid, blockStream, null, null, new BlobRequestOptions { UseTransactionalMD5 = true }, null);
				savedBytes = buffer.Length;
			}

			if (this.IsFinalFileRequest())
			{
				blob.PutBlockList(blocksList);
				AzureHandler.ClearBlob();
			}

			return true;
		}

		private static void InitializeContainer()
		{
			var blobClient = account.CreateCloudBlobClient();
			container = blobClient.GetContainerReference(AzureHandler.ConrainerName);
			container.CreateIfNotExists();
		}

		private static void ClearBlob()
		{
			blocksList.Clear();
			blockId = 0;
		}

		private static string GenerateBlockId(int index, string fileName)
		{
			string id = string.Format(System.Globalization.CultureInfo.InvariantCulture, "Telerik is awesome2!!! {0:D4} {1}", index, fileName);
			string blockId = Convert.ToBase64String(Encoding.UTF8.GetBytes(id));

			return blockId;
		}

		private void InitializeBlob()
		{
			var blobName = this.GetFileName();
			blob = container.GetBlockBlobReference(blobName);
		}

	}
}