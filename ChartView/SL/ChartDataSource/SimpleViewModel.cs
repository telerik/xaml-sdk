using System;
using System.Collections.ObjectModel;
using System.Windows;
using Telerik.Windows.Data;

namespace ChartDataSource
{
	public class SimpleViewModel
	{
		public RadObservableCollection<SalesInfo> Data { get; set; }
		
		public SimpleViewModel()
		{
		var data = new RadObservableCollection<SalesInfo>();
		DateTime startDate = new DateTime(2012, 12, 15);
		for (int i = 0; i < 20; i += 1)
		{
			data.Add(new SalesInfo() { Time = startDate.AddDays(i), Value = i });
		}
		
		this.Data = data;
		}
	}
}
