using System;
using System.Collections.Generic;
using System.Windows;

namespace Tooltip
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			List<ProfitDifferenceContext> items = CreateItems();
			this.chart.Series[0].ItemsSource = items;
		}

		private List<ProfitDifferenceContext> CreateItems()
		{
			List<ProfitDifferenceContext> items = new List<ProfitDifferenceContext>();
			Random r = new Random(0);
			const int itemsCount = 4;
			for (int i = 0; i < itemsCount; i++)
			{
				ProfitDifferenceContext profitDiffContext = new ProfitDifferenceContext()
				{
					Quarter = string.Format("Q{0}", i + 1),
					Profit = r.Next(1, 20),
				};
				items.Add(profitDiffContext);
			}
			for (int i = 0; i < itemsCount; i++)
			{
				ProfitDifferenceContext profitDiffContext = items[i];
				if (i > 0)
				{
					ProfitDifferenceContext prevPoint = items[i - 1];
					profitDiffContext.PreviousDifference = profitDiffContext.Profit - prevPoint.Profit;
					profitDiffContext.PreviousQuarter = prevPoint.Quarter;
				}
				if (i < itemsCount - 1)
				{
					ProfitDifferenceContext nextPoint = items[i + 1];
					profitDiffContext.NextDifference = nextPoint.Profit - nextPoint.Profit;
					profitDiffContext.NextQuarter = nextPoint.Quarter;
				}
			}
			return items;
		}
	}
}
