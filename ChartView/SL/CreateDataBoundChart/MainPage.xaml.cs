using System.Collections.ObjectModel;
using System.Windows.Controls;
using Telerik.Windows.Controls.ChartView;

namespace CreateDataBoundChart
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();
			ObservableCollection<Product> products = new ObservableCollection<Product>()
			{
				new Product() { Name = "Soda", QuantitySold = 20 },
				new Product() { Name = "Beer", QuantitySold = 30 },
				new Product() { Name = "Milk", QuantitySold = 50 },
				new Product() { Name = "DVDs", QuantitySold = 10 },
				new Product() { Name = "Zinc", QuantitySold = 60 },
				new Product() { Name = "Java", QuantitySold = 40 },
				new Product() { Name = "Ribs", QuantitySold = 20 },
				new Product() { Name = "Hugs", QuantitySold = 80 },
			};

			this.DataContext = products;

			BarSeries barSeries = this.chart2.Series[0] as BarSeries;
			barSeries.CategoryBinding = new PropertyNameDataPointBinding() { PropertyName = "Name" };
			barSeries.ValueBinding = new GenericDataPointBinding<Product, double>() { ValueSelector = product => product.QuantitySold };
			this.chart2.Series[0].ItemsSource = products;

			this.chart3.Series[0].ItemsSource = new double[] { 20, 30, 50, 10, 60, 40, 20, 80 };
		}
	}
}
