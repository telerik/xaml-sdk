using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace DataBinding_BusinessObjects
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();

			DateTime today = DateTime.Today;
			List<MyCost> data = new List<MyCost>()
			{
				new MyCost() { Cost = 1, UnitCost = 2, MyDate=today },
				new MyCost() { Cost = 2, UnitCost = 4, MyDate= today.AddDays(1)},
				new MyCost() { Cost = 3, UnitCost = 6, MyDate=today.AddDays(2) },
				new MyCost() { Cost = 4, UnitCost = 4, MyDate=today.AddDays(3)},
				new MyCost() { Cost = 5, UnitCost = 8, MyDate=today.AddDays(4)},
			};
			this.DataContext = data;
		}
	}
}
