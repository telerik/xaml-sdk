using System.Windows.Controls;
using System.Windows.Media;

namespace Tooltips
{
	public partial class StockTooltipControl : UserControl
	{
		public StockTooltipControl()
		{
			InitializeComponent();
		}

		public double ChangeNetPercent
		{
			set
			{
				this.changeNetPercent.Text = value.ToString("p");
				this.arrow.Fill = new SolidColorBrush(value < 0.0 ? Colors.Red : Colors.Green);
				this.rotateTransform.Angle = value < 0.0 ? 0 : 180;
			}
		}
		public double Volume
		{
			set
			{
				this.volume.Text = value.ToString(",##0");
			}
		}

		public double OneYearTargetEst
		{
			set
			{
				this.oneYearTargetEst.Text = value.ToString("C");
			}
		}

		public double PERatio
		{
			set
			{
				this.peRatio.Text = value.ToString("00.00");
			}
		}

		public double ForwardingPE
		{
			set
			{
				this.forwardingPE.Text = value.ToString("00.00");
			}
		}
	}
}
