using Microsoft.CognitiveServices.SpeechRecognition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SpeechToTextApi
{
	/// <summary>
	/// Interaction logic for ImageUploadControl.xaml
	/// </summary>
	public partial class RecordVoiceControl : UserControl
	{
		private MicrophoneRecognitionClient micClient;

		public RecordVoiceControl()
		{
			InitializeComponent();
		}
		
		public string SubscriptionKey
		{
			get { return (string) GetValue(SubscriptionKeyProperty); }
			set { SetValue(SubscriptionKeyProperty, value); }
		}

		public static readonly DependencyProperty SubscriptionKeyProperty =
			DependencyProperty.Register("SubscriptionKey", typeof(string), typeof(RecordVoiceControl), new PropertyMetadata(null));

		public string Text
		{
			get { return (string) GetValue(TextProperty); }
			set { SetValue(TextProperty, value); }
		}
		
		public static readonly DependencyProperty TextProperty =
			DependencyProperty.Register("Text", typeof(string), typeof(RecordVoiceControl), new PropertyMetadata(null));

		private void recordButton_Checked(object sender, RoutedEventArgs e)
		{
			if (this.micClient == null)
			{
				this.micClient = SpeechRecognitionServiceFactory.CreateMicrophoneClient(
					SpeechRecognitionMode.LongDictation,
					"en-US",
					this.SubscriptionKey);

				this.micClient.OnResponseReceived += (s, a) => { this.WriteResponseResult(a); };
			}

			this.micClient.StartMicAndRecognition();
		}

		private void recordButton_Unchecked(object sender, RoutedEventArgs e)
		{
			this.micClient.EndMicAndRecognition();
		}

		private void WriteResponseResult(SpeechResponseEventArgs e)
		{
			Dispatcher.BeginInvoke(new Action(() =>
			{
				for (int i = 0; i < e.PhraseResponse.Results.Length; i++)
				{
					this.Text += e.PhraseResponse.Results[i].DisplayText;
				}
			}), DispatcherPriority.Background);
		}
	}
}
