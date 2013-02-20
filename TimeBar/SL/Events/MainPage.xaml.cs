using System;
using System.Windows.Controls;
using Telerik.Windows.Controls.TimeBar;

namespace Events
{
	public partial class MainPage : UserControl
	{
		public MainPage()
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
