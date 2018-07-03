using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TextAnalyticsApi
{
	public partial class TextAnalysisControl : UserControl
	{
		private ITextAnalyticsAPI client;

		public TextAnalysisControl()
		{
			InitializeComponent();
			client = new TextAnalyticsAPI();
			client.AzureRegion = AzureRegions.Westeurope;
		}

		public string SubscriptionKey
		{
			get { return (string) GetValue(SubscriptionKeyProperty); }
			set { SetValue(SubscriptionKeyProperty, value); }
		}
		
		public static readonly DependencyProperty SubscriptionKeyProperty =
			DependencyProperty.Register("SubscriptionKey", typeof(string), typeof(TextAnalysisControl), new PropertyMetadata(null, new PropertyChangedCallback(OnSubscriptionKeyChanged)));

		private static void OnSubscriptionKeyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var control = d as TextAnalysisControl;
			control.client.SubscriptionKey = e.NewValue.ToString();
		}

		public double Confidence
		{
			get { return (double) GetValue(ConfidenceProperty); }
			set { SetValue(ConfidenceProperty, value); }
		}

		public static readonly DependencyProperty ConfidenceProperty =
			DependencyProperty.Register("Confidence", typeof(double), typeof(TextAnalysisControl), new PropertyMetadata(0.0));

		private async Task<double?> MakeAnalysisRequest(string text)
		{
			LanguageBatchResult result = await client.DetectLanguageAsync(
					new BatchInput(
						new List<Input>()
						{
						  new Input("1", text)
						}));

			var detectedLanguage = result.Documents.First().DetectedLanguages.First();
			var englishProbability = detectedLanguage.Name == "English" ? detectedLanguage.Score : 0;

			SentimentBatchResult sentimentResult = await client.SentimentAsync(
			new MultiLanguageBatchInput(new List<MultiLanguageInput>()
			{
				new MultiLanguageInput(detectedLanguage.Iso6391Name, "1", text)
			}));

			double? sentiment = 0;
			if (sentimentResult.Documents.Any())
			{
				sentiment = sentimentResult.Documents.First().Score;
			}

			return (englishProbability + sentiment) / 2;
		}

		private async void RadButton_Click(object sender, RoutedEventArgs e)
		{
			var result = await MakeAnalysisRequest(tb.Text);
			this.Confidence = (double) (result * 100);
		}
	}
}