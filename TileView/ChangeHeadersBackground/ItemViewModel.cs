using System.Windows.Media;
using Telerik.Windows.Controls;

namespace ChangeHeadersBackground
{
	public class ItemViewModel : ViewModelBase
	{
		private readonly SolidColorBrush maximizedHeaderBackground;
		private readonly SolidColorBrush minimizedHeaderBackground;
		private readonly SolidColorBrush restoredHeaderBackground;
		private SolidColorBrush headerColor;
		private TileViewItemState currentState = TileViewItemState.Minimized;

		public ItemViewModel(SolidColorBrush maximizedHeaderBackground, SolidColorBrush restoredHeaderBackground, SolidColorBrush minimizedHeaderBackground)
		{
			this.maximizedHeaderBackground = maximizedHeaderBackground;
			this.restoredHeaderBackground = restoredHeaderBackground;
			this.minimizedHeaderBackground = minimizedHeaderBackground;

			this.SetColor();
		}

		public string Header { get; set; }
		public string Content { get; set; }

		public SolidColorBrush HeaderColor
		{
			get
			{
				return this.headerColor;
			}
			set
			{
				if (this.headerColor != value)
				{
					this.headerColor = value;
					this.OnPropertyChanged("HeaderColor");
				}
			}
		}

		public TileViewItemState CurrentState
		{
			get
			{
				return this.currentState;
			}
			set
			{
				if (this.currentState != value)
				{
					this.currentState = value;
					this.OnPropertyChanged("CurrentState");
					this.SetColor();
				}
			}
		}

		private void SetColor()
		{
			if (this.CurrentState == TileViewItemState.Maximized)
			{
				this.HeaderColor = this.maximizedHeaderBackground;
			}
			else if (this.CurrentState == TileViewItemState.Minimized)
			{
				this.HeaderColor = this.minimizedHeaderBackground;
			}
			else if (this.CurrentState == TileViewItemState.Restored)
			{
				this.HeaderColor = this.restoredHeaderBackground;
			}
		}
	}
}