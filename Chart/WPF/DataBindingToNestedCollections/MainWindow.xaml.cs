using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Documents;

namespace DataBindingToNestedCollections
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			List<ObservableCollection<TradeData>> sampleData = new List<ObservableCollection<TradeData>>();
			sampleData.Add(TradeData.GetWeeklyData("CSCO"));
			sampleData.Add(TradeData.GetWeeklyData("MSFT"));
			this.radChart.ItemsSource = sampleData;
		}
	}
}
