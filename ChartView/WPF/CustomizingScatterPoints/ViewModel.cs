using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Windows.Controls;

namespace CustomizingScatterPoints
{
	public class ViewModel : ViewModelBase
	{
		private List<ChartData> data;

		public ViewModel()
		{
			this.Data = this.GetData();
		}

		public List<ChartData> Data
		{
			get
			{
				return this.data;
			}
			set
			{
				if (this.data != value)
				{
					this.data = value;
					this.OnPropertyChanged("Data");
				}
			}
		}

		private List<ChartData> GetData()
		{
			List<ChartData> data = new List<ChartData>();
			data.Add(new ChartData(0.1, 100));
			data.Add(new ChartData(0.1, 101));
			data.Add(new ChartData(11, 106));
			data.Add(new ChartData(101, 104));
			data.Add(new ChartData(101, 108));

			return data;
		}
	}
}