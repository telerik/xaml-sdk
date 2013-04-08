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

namespace Sampling
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();
			this.RadChart1.DefaultView.ChartArea.AxisX.LabelStep = 4;

			List<ChartData> data = new List<ChartData>();
			for (int i = 0; i < 1000; i++)
			{
				data.Add(new ChartData() { YVal = i });
			}

			this.RadChart1.ItemsSource = data;
		}
	}
}
