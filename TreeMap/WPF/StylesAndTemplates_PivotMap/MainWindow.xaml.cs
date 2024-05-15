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

namespace StylesAndTemplates_PivotMap
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			this.pivotMap.ItemsSource = GetData();
		}

		public List<GdpInfo> GetData()
		{
			List<GdpInfo> data = new List<GdpInfo>()
			{
				new GdpInfo() { Country = "Australia", Gdp = 1146 },
				new GdpInfo() { Country = "Austria", Gdp = 385.1 },
				new GdpInfo() { Country = "Belgium", Gdp = 468.6 },
				new GdpInfo() { Country = "Brazil", Gdp = 1749 },
				new GdpInfo() { Country = "Canada", Gdp = 1565 },
				new GdpInfo() { Country = "China", Gdp = 1700 },
				new GdpInfo() { Country = "Denmark", Gdp = 318.1 },
				new GdpInfo() { Country = "France", Gdp = 2669 },
				new GdpInfo() { Country = "Germany", Gdp = 3402 },
				new GdpInfo() { Country = "Greece", Gdp = 329 },
				new GdpInfo() { Country = "India", Gdp = 1290 },
				new GdpInfo() { Country = "USA", City = "NY which is in the NY state ", Gdp = 3000 },
				new GdpInfo() { Country = "USA", City = "Columbus which is in the Ohio state", Gdp = 2000 },
				new GdpInfo() { Country = "USA", City = "Los Angeles which is in the California state", Gdp = 5000 },
				new GdpInfo() { Country = "USA", City = "Austin which is in the Texas state", Gdp = 4600 },
			};
			return data;
		}
	}
}
