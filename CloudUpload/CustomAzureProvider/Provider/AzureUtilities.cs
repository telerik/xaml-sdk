using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure.Storage;

namespace AzureProvider
{
    public static class Utilities
    {
        public static IEnumerable<Uri> ListUris(string accountName, string accountKey, string container)
        {
            var account = CloudStorageAccount.Parse("DefaultEndpointsProtocol=http;AccountName=" + accountName + ";AccountKey=" + accountKey);
            var blobClient = account.CreateCloudBlobClient();
			var blobContainer = blobClient.GetContainerReference(container);
            var aResult = blobContainer.BeginListBlobsSegmented(null, null, null);
            var blobs = blobContainer.EndListBlobsSegmented(aResult);
            var uris = blobs.Results.Select(b => b.Uri);

            return uris;
        }
    }
}
