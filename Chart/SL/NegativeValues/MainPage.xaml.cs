using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace NegativeValues
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();
			this.DataContext = new List<ChartData>()
			{
				new ChartData { XVal = -5, YVal = 138 },
				new ChartData { XVal = -4, YVal = -143 },
				new ChartData { XVal = -3, YVal = -120 },
				new ChartData { XVal = -1, YVal = -125 },
				new ChartData { XVal = 0, YVal = 179 },
				new ChartData { XVal = 1, YVal = 170 },
				new ChartData { XVal = 3, YVal = 187 },
				new ChartData { XVal = 5, YVal = 176 },
			};
		}
	}
}
