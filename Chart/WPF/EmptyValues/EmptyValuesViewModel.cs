using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Windows.Controls.Charting;

namespace EmptyValues
{
	public class EmptyValuesViewModel
	{
		Random r = new Random(0);

		public RadHierarchicalObservableCollection<ChartData> List { get; set; }

		public EmptyValuesViewModel()
		{
			List = new RadHierarchicalObservableCollection<ChartData>();
			for (int i = 0; i < 10; i++)
			{
				List.Add(new ChartData { XVal = i, YVal = r.Next(0, 100) });
			}

			List[3].YVal = null;
			List[4].YVal = null;
			List[7].YVal = null;
		}
	}
}
