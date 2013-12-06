using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Telerik.Windows.Controls.GridView;

namespace RadGridView_WPF_AR_8
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			EventManager.RegisterClassHandler(typeof(GridViewCell),
			   TextBlock.ToolTipOpeningEvent,
			   new RoutedEventHandler(OnToolTipOpening));
		}

		private void OnToolTipOpening(object sender, RoutedEventArgs e)
		{
			// show tooltip only when text is trimmed
			var cell = (sender as GridViewCell);
			if (cell != null)
				e.Handled = !IsTextTrimmed(cell);
		}

		static bool IsTextTrimmed(GridViewCell cell)
		{
			Typeface typeface = new Typeface(cell.FontFamily,
				cell.FontStyle,
				cell.FontWeight,
				cell.FontStretch);
			FormattedText formmatedText = new FormattedText(
				cell.Value.ToString(),
				System.Threading.Thread.CurrentThread.CurrentCulture,
				cell.FlowDirection,
				typeface,
				cell.FontSize,
				cell.Foreground);

			return formmatedText.Width > cell.ActualWidth;
		}
	}
}
