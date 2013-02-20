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

namespace OpenWithDropDownButton
{
	public partial class Example : UserControl
	{
		public Example()
		{
			InitializeComponent();
		}

		private void RadButton_Click_1(object sender, RoutedEventArgs e)
		{
			this.RadAutoCompleteBox.Focus();
			this.RadAutoCompleteBox.Populate(this.RadAutoCompleteBox.SearchText);
		}
	}
}
