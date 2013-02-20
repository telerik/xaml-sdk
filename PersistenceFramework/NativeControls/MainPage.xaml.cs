using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;

namespace NativeControls
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();
		}

		private void buttonBold_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			RadToggleButton button = sender as RadToggleButton;
			if (button != null)
			{
				if (button.IsChecked == true)
				{
					targetText.FontWeight = FontWeights.Bold;
				}
				else
				{
					targetText.ClearValue(System.Windows.Controls.TextBox.FontWeightProperty);
				}
			}
		}

		private void buttonItalic_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			RadToggleButton button = sender as RadToggleButton;
			if (button != null)
			{
				if (button.IsChecked == true)
				{
					targetText.FontStyle = FontStyles.Italic;
				}
				else
				{
					targetText.ClearValue(System.Windows.Controls.TextBox.FontStyleProperty);
				}
			}
		}
	}
}
