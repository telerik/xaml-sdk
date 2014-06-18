using System;
using System.Collections.Generic;
using System.Linq;
using Amazon.S3;
using Amazon.S3.Model;

namespace AmazonS3Provider
{
	public class Utilities
	{
		public static IEnumerable<string> ListFiles(string bucketName, string accessKeyID, string secretAccessKey)
		{
			List<string> fileNames = new List<string>();
			try
			{
				var s3Client = new AmazonS3Client(accessKeyID, secretAccessKey, Amazon.RegionEndpoint.USEast1);
				ListObjectsRequest request = new ListObjectsRequest 
				{ 
					BucketName = bucketName 
				};

				while (request != null)
				{
					ListObjectsResponse response = s3Client.ListObjects(request);

					foreach (S3Object entry in response.S3Objects)
					{
						fileNames.Add(entry.Key);
					}

					if (response.IsTruncated)
					{
						request.Marker = response.NextMarker;
					}
					else
					{
						request = null;
					}
				}
			}
			catch (Exception e)
			{
			}
			return fileNames;
		}
	}
}
