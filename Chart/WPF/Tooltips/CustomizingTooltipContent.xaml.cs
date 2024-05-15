using System.Windows.Controls;
using Telerik.Windows.Controls.Charting;

namespace Tooltips
{
	public partial class CustomizingTooltipContent : UserControl
	{
		public CustomizingTooltipContent()
		{
			InitializeComponent();
		}

		private void ChartArea_ItemToolTipOpening(ItemToolTip2D tooltip, ItemToolTipEventArgs e)
		{
			TradeData tradeData = e.DataPoint.DataItem as TradeData;
			StockTooltipControl stockTooltip = new StockTooltipControl();
			stockTooltip.ChangeNetPercent = 1 - (tradeData.Close / tradeData.Open);
			stockTooltip.Volume = tradeData.Volume;
			//The next three properties shows some fictional data to illustrate the idea
			stockTooltip.OneYearTargetEst = tradeData.Close * 1.1;
			stockTooltip.PERatio = 16.80;
			stockTooltip.ForwardingPE = 17.93;
			tooltip.Content = stockTooltip;
		}
	}
}
