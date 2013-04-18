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

namespace BindingTheColorOfSeriesItems
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();
			this.DataContext = GetData(12);
		}

		public static List<ChartData> GetData(int dataSize)
		{
			Random rnd = new Random(0);
			var result = new List<ChartData>();

			for (int i = 0; i < dataSize; i++)
			{
				result.Add(new ChartData()
				{
					Category = i,
					Value = rnd.Next(1, 100),
					Color = new SolidColorBrush(
						Color.FromArgb(255, (byte)rnd.Next(0, 256), (byte)rnd.Next(0, 256), (byte)rnd.Next(0, 256)))
				});
			}

			return result;
		}
	}
}
