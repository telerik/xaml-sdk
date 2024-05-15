using System.Windows;

namespace SpeechToTextApi
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public string Text { get; set; }

		public MainWindow()
		{
			InitializeComponent();
			this.DataContext = this;
		}
	}
}
