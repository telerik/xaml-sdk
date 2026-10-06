using System;
using System.Windows;
using Telerik.Windows.Controls.TimeBar;

namespace Events
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			radTimeBar1.ItemIntervalChanged += new EventHandler<DrillEventArgs>(radTimeBar1_ItemIntervalChanged);
		}

		void radTimeBar1_ItemIntervalChanged(object sender, DrillEventArgs e)
		{
			if (radTimeBar1.CurrentItemInterval is MonthInterval)
			{
				radTimeBar1.SelectionStart = new DateTime(2012, 05, 1);
				radTimeBar1.SelectionEnd = new DateTime(2012, 07, 1);
			}
		}
	}
}
