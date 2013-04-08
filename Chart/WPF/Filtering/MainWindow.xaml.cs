using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Filtering
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			this.DataContext = new List<ProductSales>()
			{
				new ProductSales { ID = 1, Quantity = 154 },
				new ProductSales { ID = 2, Quantity = 138 },
				new ProductSales { ID = 3, Quantity = 143 },
				new ProductSales { ID = 4, Quantity = 120 },
				new ProductSales { ID = 5, Quantity = 135 },
				new ProductSales { ID = 6, Quantity = 125 },
				new ProductSales { ID = 7, Quantity = 179 },
				new ProductSales { ID = 8, Quantity = 170 },
				new ProductSales { ID = 9, Quantity = 198 },
				new ProductSales { ID = 10, Quantity = 187 },
				new ProductSales { ID = 11, Quantity = 193 },
				new ProductSales { ID = 12, Quantity = 212 },
			};
		}
	}
}
