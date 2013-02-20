using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace DataBinding
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();

			Random r = new Random();
			List<double> myData = new List<double>();
			for (int i = 0; i < 20; i++)
			{
				myData.Add(r.Next(0, 100));
			}
			myLinearSparkline.ItemsSource = myData;
		}
	}
}
