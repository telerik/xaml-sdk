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

namespace TodayButton
{
	public partial class CustomCalendar : UserControl
	{
		public CustomCalendar()
		{
			InitializeComponent();
		}

		private void RadButton_Click(object sender, RoutedEventArgs e)
		{
			this.calendar.SelectedDate = DateTime.Today;
			this.calendar.DisplayDate = DateTime.Today;
		}
	}
}
