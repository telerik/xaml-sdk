using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace FormatExpressions
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
			InitializeComponent();
			var items = new List<TradeData>()
			{
				new TradeData { FromDate = new DateTime(2012, 08, 10), Open = 23.12, Close = 23.42, High = 23.56, Low = 23.07, Volume = 63908410 },
				new TradeData { FromDate = new DateTime(2012, 08, 11), Open = 23.22, Close = 23.13, High = 23.26, Low = 23.09, Volume = 62324520 },
				new TradeData { FromDate = new DateTime(2012, 08, 12), Open = 23.17, Close = 23.53, High = 23.09, Low = 23.03, Volume = 61936270 },
				new TradeData { FromDate = new DateTime(2012, 08, 13), Open = 23.53, Close = 23.62, High = 23.66, Low = 23.46, Volume = 60097235 },
				new TradeData { FromDate = new DateTime(2012, 08, 14), Open = 23.55, Close = 23.69, High = 23.70, Low = 23.51, Volume = 62895687 },
			};
			this.DataContext = items;
		}
	}
}
