using System;
using System.Collections.ObjectModel;

namespace DataBindingToNestedCollections
{
	public class TradeData
	{
		public string Emission { get; set; }

		public DateTime FromDate { get; set; }

		public DateTime ToDate { get; set; }

		public double Open { get; set; }

		public double High { get; set; }

		public double Low { get; set; }

		public double Close { get; set; }

		public double Volume { get; set; }

		public static ObservableCollection<TradeData> GetWeeklyData(string code)
		{
			ObservableCollection<TradeData> tradeData;
			if (code == "CSCO")
			{
				tradeData = new ObservableCollection<TradeData>()
				{
                    new TradeData() { Emission="CSCO", FromDate = new DateTime( 2009, 8, 10 ), Open = 21.9800, High = 22.1200, Low = 21.5000, Close = 21.6400, Volume = 46602230 },
                    new TradeData() { Emission="CSCO", FromDate = new DateTime( 2009, 8, 11 ), Open = 21.4700, High = 21.5300, Low = 21.0600, Close = 21.1700, Volume = 53545300 },
                    new TradeData() { Emission="CSCO", FromDate = new DateTime( 2009, 8, 12 ), Open = 21.2300, High = 21.6100, Low = 21.1800, Close = 21.4300, Volume = 49330780 },
                    new TradeData() { Emission="CSCO", FromDate = new DateTime( 2009, 8, 13 ), Open = 21.5400, High = 21.6500, Low = 21.3400, Close = 21.5100, Volume = 38914230 },
                    new TradeData() { Emission="CSCO", FromDate = new DateTime( 2009, 8, 14 ), Open = 21.4700, High = 21.5500, Low = 21.0000, Close = 21.3100, Volume = 35131850 },
                    new TradeData() { Emission="CSCO", FromDate = new DateTime( 2009, 8, 17 ), Open = 20.9400, High = 20.9600, Low = 20.6800, Close = 20.7300, Volume = 32361720 },
                    new TradeData() { Emission="CSCO", FromDate = new DateTime( 2009, 8, 18 ), Open = 20.8200, High = 21.1500, Low = 20.7900, Close = 21.0100, Volume = 33300840 },
                    new TradeData() { Emission="CSCO", FromDate = new DateTime( 2009, 8, 19 ), Open = 20.7600, High = 21.4400, Low = 20.7400, Close = 21.2400, Volume = 39197070 },
                    new TradeData() { Emission="CSCO", FromDate = new DateTime( 2009, 8, 20 ), Open = 21.2500, High = 21.9600, Low = 21.2000, Close = 21.8900, Volume = 55194380 },
                    new TradeData() { Emission="CSCO", FromDate = new DateTime( 2009, 8, 21 ), Open = 22.0800, High = 22.2700, Low = 21.7700, Close = 22.1900, Volume = 56572840 },
                    new TradeData() { Emission="CSCO", FromDate = new DateTime( 2009, 8, 24 ), Open = 22.2200, High = 22.6400, Low = 21.9300, Close = 22.0600, Volume = 52670800 },
                    new TradeData() { Emission="CSCO", FromDate = new DateTime( 2009, 8, 25 ), Open = 22.1550, High = 22.2500, Low = 21.8500, Close = 22.0000, Volume = 28401080 },
                    new TradeData() { Emission="CSCO", FromDate = new DateTime( 2009, 8, 26 ), Open = 21.9100, High = 22.0000, Low = 21.8100, Close = 21.9300, Volume = 33523570 },
                    new TradeData() { Emission="CSCO", FromDate = new DateTime( 2009, 8, 27 ), Open = 21.6700, High = 21.9100, Low = 21.4700, Close = 21.8800, Volume = 30278510 },
                    new TradeData() { Emission="CSCO", FromDate = new DateTime( 2009, 8, 28 ), Open = 22.1500, High = 22.3500, Low = 21.8000, Close = 22.0000, Volume = 35933920 },
                    new TradeData() { Emission="CSCO", FromDate = new DateTime( 2009, 8, 31 ), Open = 21.7400, High = 21.8400, Low = 21.5100, Close = 21.6000, Volume = 31401760 },
                    new TradeData() { Emission="CSCO", FromDate = new DateTime( 2009, 9, 1 ), Open = 21.4600, High = 21.8700, Low = 21.0800, Close = 21.1700, Volume = 48741440 },
                    new TradeData() { Emission="CSCO", FromDate = new DateTime( 2009, 9, 2 ), Open = 21.0900, High = 21.7300, Low = 21.0700, Close = 21.5500, Volume = 40225600 },
                    new TradeData() { Emission="CSCO", FromDate = new DateTime( 2009, 9, 3 ), Open = 21.7300, High = 21.9000, Low = 21.3400, Close = 21.4900, Volume = 33830210 },
                    new TradeData() { Emission="CSCO", FromDate = new DateTime( 2009, 9, 4 ), Open = 21.5900, High = 21.9600, Low = 21.4000, Close = 21.8400, Volume = 28951200 },
                    new TradeData() { Emission="CSCO", FromDate = new DateTime( 2009, 9, 8 ), Open = 22.0000, High = 22.2100, Low = 21.7900, Close = 21.9200, Volume = 37912164 } };
			}
			else
			{
				tradeData = new ObservableCollection<TradeData>()
				{
                    new TradeData() { Emission="MSFT", FromDate = new DateTime( 2009, 8, 10 ), Open = 23.4600, High = 23.5500, Low = 23.3000, Close = 23.4200, Volume = 35258950 },
                    new TradeData() { Emission="MSFT", FromDate = new DateTime( 2009, 8, 11 ), Open = 23.3200, High = 23.4000, Low = 23.0500, Close = 23.1300, Volume = 33611790 },
                    new TradeData() { Emission="MSFT", FromDate = new DateTime( 2009, 8, 12 ), Open = 23.1300, High = 23.9000, Low = 23.0300, Close = 23.5300, Volume = 61936270 },
                    new TradeData() { Emission="MSFT", FromDate = new DateTime( 2009, 8, 13 ), Open = 23.6300, High = 23.8500, Low = 23.4000, Close = 23.6200, Volume = 38951990 },
                    new TradeData() { Emission="MSFT", FromDate = new DateTime( 2009, 8, 14 ), Open = 23.6200, High = 23.8000, Low = 23.5100, Close = 23.6900, Volume = 46328540 },
                    new TradeData() { Emission="MSFT", FromDate = new DateTime( 2009, 8, 17 ), Open = 23.3200, High = 23.6000, Low = 23.2300, Close = 23.2500, Volume = 42462890 },
                    new TradeData() { Emission="MSFT", FromDate = new DateTime( 2009, 8, 18 ), Open = 23.2900, High = 23.6520, Low = 23.2700, Close = 23.5800, Volume = 38831620 },
                    new TradeData() { Emission="MSFT", FromDate = new DateTime( 2009, 8, 19 ), Open = 23.2500, High = 23.7200, Low = 23.2500, Close = 23.6500, Volume = 41814320 },
                    new TradeData() { Emission="MSFT", FromDate = new DateTime( 2009, 8, 20 ), Open = 23.6000, High = 23.8700, Low = 23.5400, Close = 23.6700, Volume = 39502680 },
                    new TradeData() { Emission="MSFT", FromDate = new DateTime( 2009, 8, 21 ), Open = 23.9300, High = 24.4200, Low = 23.7700, Close = 24.4100, Volume = 68995700},
                    new TradeData() { Emission="MSFT", FromDate = new DateTime( 2009, 8, 24 ), Open = 24.4100, High = 24.7326, Low = 24.2800, Close = 24.6400, Volume = 54159300 },
                    new TradeData() { Emission="MSFT", FromDate = new DateTime( 2009, 8, 25 ), Open = 24.6000, High = 24.8200, Low = 24.4600, Close = 24.6400, Volume = 43961480 },
                    new TradeData() { Emission="MSFT", FromDate = new DateTime( 2009, 8, 26 ), Open = 24.5900, High = 24.7500, Low = 24.4200, Close = 24.5500, Volume = 41060010 },
                    new TradeData() { Emission="MSFT", FromDate = new DateTime( 2009, 8, 27 ), Open = 24.4100, High = 24.7800, Low = 24.3000, Close = 24.6900, Volume = 45433940 },
                    new TradeData() { Emission="MSFT", FromDate = new DateTime( 2009, 8, 28 ), Open = 25.0700, High = 25.4900, Low = 24.6100, Close = 24.6800, Volume = 55789640 },
                    new TradeData() { Emission="MSFT", FromDate = new DateTime( 2009, 8, 31 ), Open = 24.5700, High = 24.8500, Low = 24.2900, Close = 24.6500, Volume = 49582950 },
                    new TradeData() { Emission="MSFT", FromDate = new DateTime( 2009, 9, 1 ), Open = 24.3500, High = 24.7400, Low = 23.9000, Close = 24.0000, Volume = 62571800 },
                    new TradeData() { Emission="MSFT", FromDate = new DateTime( 2009, 9, 2 ), Open = 23.8200, High = 24.1400, Low = 23.7800, Close = 23.8600, Volume = 40726040 },
                    new TradeData() { Emission="MSFT", FromDate = new DateTime( 2009, 9, 3 ), Open = 23.9100, High = 24.1400, Low = 23.7600, Close = 24.1100, Volume = 34110810 },
                    new TradeData() { Emission="MSFT", FromDate = new DateTime( 2009, 9, 4 ), Open = 24.0900, High = 24.8001, Low = 24.0800, Close = 24.6200, Volume = 44987570 },
                    new TradeData() { Emission="MSFT", FromDate = new DateTime( 2009, 9, 8 ), Open = 24.6200, High = 24.8400, Low = 24.4100, Close = 24.8200, Volume = 52243880 } };
			}
			return tradeData;
		}
	}
}