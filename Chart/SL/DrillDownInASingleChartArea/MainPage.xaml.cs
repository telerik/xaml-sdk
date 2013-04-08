using System.Collections.Generic;
using System.Windows.Controls;

namespace DrillDownInASingleChartArea
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();
			this.radChart.ItemsSource = GetChartData();
		}

		private List<Company> GetChartData()
		{
			return new List<Company>()
            { 
                new Company()
                { 
                    Name="ToyYoda",
					Sales = new ModelSalesCollection()
					{ 
						new ModelSales("Coolla", 120000),
						new ModelSales("Coolla", 115000),
						new ModelSales("Veso", 89000),
						new ModelSales("Veso", 79000)
					}
				},
				new Company()
				{ 
					Name="Marda",
					Sales =new ModelSalesCollection()
					{
						new ModelSales("Tree", 145000),
						new ModelSales("Tree", 132000),
						new ModelSales("Six", 121000),
						new ModelSales("Six", 111000)
					}
				}
			};
		}
	}
}
