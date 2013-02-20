using System;
using System.Collections.Generic;
using System.Windows;

namespace PopulatingWithData
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			DateTime date = new DateTime(2010, 1, 1);
			var infos = new List<TempInfo>() 
			{
				new TempInfo() { Year = date, Month = "Apr", Temperature = 17, Rain = 8.8},
				new TempInfo() { Year = date, Month = "May", Temperature = 20, Rain = 6.25},

				new TempInfo() { Year = date.AddYears(1), Month = "Jan", Temperature = 5, Rain=10.3},
				new TempInfo() { Year = date.AddYears(1), Month = "Feb", Temperature = 13, Rain = 9.8},
				new TempInfo() { Year = date.AddYears(1), Month = "Mar", Temperature = 21, Rain = 9.6},

				new TempInfo() { Year = date.AddYears(2), Month = "Jan", Temperature = 14, Rain = 11.5},
				new TempInfo() { Year = date.AddYears(2), Month = "Feb", Temperature = 19, Rain = 7.5},
				new TempInfo() { Year = date.AddYears(2), Month = "Mar", Temperature = 17, Rain = 10.1},
			};

			this.DataContext = infos;
		}
	}
}
