using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace LayoutMode
{
	public class SimpleViewModel
	{
		public IEnumerable<ChartData> BarSeriesData { get; set; }
		public IEnumerable<ChartData> LineSeriesData { get; set; }
	}
}
