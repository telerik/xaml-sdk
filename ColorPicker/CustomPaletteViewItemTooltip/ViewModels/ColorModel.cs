using System;
using System.Linq;
using System.Windows.Media;
using Telerik.Windows.Controls;

namespace CustomPaletteViewItemTooltip.ViewModels
{
	public class ColorModel : ViewModelBase
	{
		private Color currColor;
		public Color CustomColor
		{
			get { return this.currColor; }
			set
			{
				if (this.currColor != value)
				{
					this.currColor = value;
					this.OnPropertyChanged("CustomColor");
				}
			}
		}

		public string ToolTipString { get; set; }
	}
}
