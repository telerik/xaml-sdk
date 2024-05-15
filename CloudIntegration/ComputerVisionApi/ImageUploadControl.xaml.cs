using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;

namespace ComputerVisionApi
{
	/// <summary>
	/// Interaction logic for ImageUploadControl.xaml
	/// </summary>
	public partial class ImageUploadControl : UserControl
	{
		// Replace or verify the region.
		//
		// You must use the same region in your REST API call as you used to obtain your subscription keys.
		// For example, if you obtained your subscription keys from the westus region, replace 
		// "westcentralus" in the URI below with "westus".
		//
		// NOTE: Free trial subscription keys are generated in the westcentralus region, so if you are using
		// a free trial subscription key, you should not need to change this region.
		const string uriBase = "https://westeurope.api.cognitive.microsoft.com/vision/v1.0/analyze";

		public ImageUploadControl()
		{
			InitializeComponent();
			this.DataContext = this;
		}

		public string SubscriptionKey
		{
			get { return (string) GetValue(SubscriptionKeyProperty); }
			set { SetValue(SubscriptionKeyProperty, value); }
		}

		public static readonly DependencyProperty SubscriptionKeyProperty =
			DependencyProperty.Register("SubscriptionKey", typeof(string), typeof(ImageUploadControl), new PropertyMetadata(null));

		public double Confidence
		{
			get { return (double) GetValue(ConfidenceProperty); }
			set { SetValue(ConfidenceProperty, value); }
		}

		public static readonly DependencyProperty ConfidenceProperty =
			DependencyProperty.Register("Confidence", typeof(double), typeof(ImageUploadControl), new PropertyMetadata(0.0));

		private async void Button1_Click(object sender, RoutedEventArgs e)
		{
			RadOpenFileDialog openFileDialog = new RadOpenFileDialog();
			openFileDialog.Owner = this;
			openFileDialog.Filter = "|Image Files|*.jpg;*.png";
			openFileDialog.ShowDialog();
			if (openFileDialog.DialogResult == true)
			{
				string fileName = openFileDialog.FileName;
				var result = await MakeAnalysisRequest(fileName);
				var res = JsonConvert.DeserializeObject<ImageResponse>(result);
				this.Confidence = (double) (this.CalculatePortraitProbability(res) * 100);
			}
		}

		/// <summary>
		/// Gets the analysis of the specified image file by using the Computer Vision REST API.
		/// </summary>
		/// <param name="imageFilePath">The image file.</param>
		private async Task<string> MakeAnalysisRequest(string imageFilePath)
		{
			HttpClient client = new HttpClient();

			// Request headers.
			client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", this.SubscriptionKey);

			// Request parameters. A third optional parameter is "details".
			string requestParameters = "visualFeatures=Categories,Faces,Tags&language=en";

			// Assemble the URI for the REST API Call.
			string uri = uriBase + "?" + requestParameters;

			HttpResponseMessage response;

			// Request body. Posts a locally stored JPEG image.
			byte[] byteData = GetImageAsByteArray(imageFilePath);

			using (ByteArrayContent content = new ByteArrayContent(byteData))
			{
				// This example uses content type "application/octet-stream".
				// The other content types you can use are "application/json" and "multipart/form-data".
				content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

				// Execute the REST API call.
				response = await client.PostAsync(uri, content);

				// Get the JSON response.
				return await response.Content.ReadAsStringAsync();
			}
		}


		/// <summary>
		/// Returns the contents of the specified file as a byte array.
		/// </summary>
		/// <param name="imageFilePath">The image file to read.</param>
		/// <returns>The byte array of the image data.</returns>
		static byte[] GetImageAsByteArray(string imageFilePath)
		{
			FileStream fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);
			BinaryReader binaryReader = new BinaryReader(fileStream);
			return binaryReader.ReadBytes((int) fileStream.Length);
		}

		private decimal CalculatePortraitProbability(ImageResponse res)
		{
			if (res.Faces != null && res.Faces.Any())
			{
				if (res.Faces.Count > 1)
				{
					return 0;
				}
				else
				{
					var portraitCategory = res.Categories.FirstOrDefault(r => r.Name == "people_portrait");
					if (portraitCategory != null)
					{
						return portraitCategory.Score;
					}
					else
					{
						var personCategory = res.Categories.FirstOrDefault(r => r.Name == "people_");
						if (personCategory != null)
						{
							return personCategory.Score;
						}
						else
						{
							var personTag = res.Tags.FirstOrDefault(t => t.Name == "person");
							if (personTag != null)
							{
								return personTag.Confidence * 0.7M;
							}
						}
					}
				}
			}

			return 0;
		}
	}

	public class ImageResponse
	{
		public IList<Category> Categories { get; set; }

		public IList<Tag> Tags { get; set; }

		public IList<object> Faces { get; set; }
	}

	public class Tag
	{
		public string Name { get; set; }

		public decimal Confidence { get; set; }
	}

	public class Category
	{
		public string Name { get; set; }
		public decimal Score { get; set; }
	}
}
