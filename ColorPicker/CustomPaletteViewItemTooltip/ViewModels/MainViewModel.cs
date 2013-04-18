using System;
using System.Collections.ObjectModel;
using System.Linq;
using Telerik.Windows.Controls;
using System.Windows.Media;

namespace CustomPaletteViewItemTooltip.ViewModels
{

	public class MainViewModel : ViewModelBase
	{
		#region PrivateFilds
		private ObservableCollection<ColorModel> mainPaletteColors;
		private ObservableCollection<ColorModel> headerPaletteColors;
		private ObservableCollection<ColorModel> standartPaletteColors;
		#endregion PrivateFilds

		#region Constructor
		public MainViewModel()
		{
			mainPaletteColors = new ObservableCollection<ColorModel>();
			headerPaletteColors = new ObservableCollection<ColorModel>();
			standartPaletteColors = new ObservableCollection<ColorModel>();

			GenerateSampleData();
		}
		#endregion Constructor

		#region GenerateSampleData
		private void GenerateSampleData()
		{
			Random rand = new Random();
			for (int i = 0; i < 50; i++)
			{
				ColorModel color = new ColorModel()
				{
					CustomColor = Color.FromArgb(255, (byte)rand.Next(0, 255), (byte)rand.Next(0, 255), (byte)rand.Next(0, 255)),
					ToolTipString = "Custom ToolTip " + i,
				};
				this.mainPaletteColors.Add(color);
			}

			for (int i = 0; i < 10; i++)
			{
				ColorModel color = new ColorModel()
				{
					CustomColor = Color.FromArgb(255, (byte)rand.Next(0, 255), (byte)rand.Next(0, 255), (byte)rand.Next(0, 255)),
					ToolTipString = "Custom ToolTip " + i,
				};
				this.headerPaletteColors.Add(color);
			}

			for (int i = 0; i < 10; i++)
			{
				ColorModel color = new ColorModel()
				{
					CustomColor = Color.FromArgb(255, (byte)rand.Next(0, 255), (byte)rand.Next(0, 255), (byte)rand.Next(0, 255)),
					ToolTipString = "Custom ToolTip " + i,
				};
				this.standartPaletteColors.Add(color);
			}
		}
		#endregion GenerateSampleData

		#region Properties
		public ObservableCollection<ColorModel> MainPaletteColors
		{
			get { return this.mainPaletteColors; }
			set
			{
				if (this.mainPaletteColors != value)
				{
					this.mainPaletteColors = value;
					this.OnPropertyChanged("MainPaletteColors");
				}
			}
		}

		public ObservableCollection<ColorModel> StandartPaletteColors
		{
			get { return this.standartPaletteColors; }
			set
			{
				if (this.standartPaletteColors != value)
				{
					this.standartPaletteColors = value;
					this.OnPropertyChanged("StandartPaletteColors");
				}
			}
		}

		public ObservableCollection<ColorModel> HeaderPaletteColors
		{
			get { return this.headerPaletteColors; }
			set
			{
				if (this.headerPaletteColors != value)
				{
					this.headerPaletteColors = value;
					this.OnPropertyChanged("HeaderPaletteColors");
				}
			}
		}
		#endregion Properties
	}
}