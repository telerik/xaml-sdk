using System.Collections.Generic;
using System.Windows;

namespace PopulatingWithData_PivotMap
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			this.DataContext = new List<TestData>()
			{
				new TestData { Category = "A", Subcategory = "100", Value = 10 },
				new TestData { Category = "B", Subcategory = "100", Value = 25 },
				new TestData { Category = "C", Subcategory = "200", Value = 40 },
				new TestData { Category = "D", Subcategory = "200", Value = 15 },
				new TestData { Category = "E", Subcategory = "200", Value = 30 },
			};
		}
	}
}
