using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace DataBindingToNestedCollections
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();
			List<ObservableCollection<TradeData>> sampleData = new List<ObservableCollection<TradeData>>();
			sampleData.Add(TradeData.GetWeeklyData("CSCO"));
			sampleData.Add(TradeData.GetWeeklyData("MSFT"));
			this.radChart.ItemsSource = sampleData;
		}
	}
}
