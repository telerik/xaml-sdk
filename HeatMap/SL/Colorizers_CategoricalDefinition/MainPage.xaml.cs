using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Colorizers_CategoricalDefinition
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();
			radHeatMap.Definition.ItemsSource = CreateWeatherData();
		}

		public List<MonthlyTemp> CreateWeatherData()
		{
			var time = new DateTime(2004, 1, 1);
			var result = new List<MonthlyTemp>();
			Random r = new Random();

			for (int i = 0; i < 5; i++)
			{
				for (int a = 0; a < 3; a++)
				{
					result.Add(new MonthlyTemp(time, r.Next(0, 10)));
					time = time.AddMonths(1);
				}
				for (int a = 0; a < 3; a++)
				{
					result.Add(new MonthlyTemp(time, r.Next(10, 20)));
					time = time.AddMonths(1);
				}
				for (int a = 0; a < 3; a++)
				{
					result.Add(new MonthlyTemp(time, r.Next(20, 30)));
					time = time.AddMonths(1);
				}
				for (int a = 0; a < 3; a++)
				{
					result.Add(new MonthlyTemp(time, r.Next(10, 20)));
					time = time.AddMonths(1);
				}
			}
			return result;
		}
	}
}
