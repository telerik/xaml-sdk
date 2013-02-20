using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;

namespace RadTimeBarOverview
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			Random r = new Random();
			List<int> LinearData = new List<int>();
			for (DateTime currentDate = DateTime.Today; currentDate < DateTime.Today.AddDays(365); currentDate = currentDate.AddDays(1))
			{
				LinearData.Add(r.Next(0, 60));
			}
			this.DataContext = LinearData;
		}
	}
}
