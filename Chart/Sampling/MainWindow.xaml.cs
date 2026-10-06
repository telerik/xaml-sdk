using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;

namespace Sampling
{
	public partial class MainWindow : Window
	{
		public MainWindow()
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
