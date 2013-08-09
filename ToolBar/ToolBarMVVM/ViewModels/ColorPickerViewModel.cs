using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using Telerik.Windows.Controls;

namespace ToolBarMVVM
{
	public class ColorPickerViewModel : ViewModelBase
	{
		public ColorPickerViewModel()
		{
			this.MainPaletteColors = new ObservableCollection<Color>()
			{
				Color.FromArgb(255, 253, 253, 0),
				Color.FromArgb(255, 0, 253, 0),
				Color.FromArgb(255, 0, 253, 253),
				Color.FromArgb(255, 253, 0, 253),
				Color.FromArgb(255, 0, 0 , 253 ),
				Color.FromArgb(255, 253, 0 ,0),
				Color.FromArgb(255, 0 , 0, 126),
				Color.FromArgb(255, 0, 126, 126),
				Color.FromArgb(255, 0, 126, 0),
				Color.FromArgb(255, 126, 0, 126),
				Color.FromArgb(255, 126, 0, 0),
				Color.FromArgb(255, 126, 126, 0),
				Color.FromArgb(255, 126, 126, 126),
				Color.FromArgb(255, 190, 190, 190),
				Color.FromArgb(255, 0 , 1 , 1)
			};
		}
		public ObservableCollection<Color> MainPaletteColors { get; set; }
	}
}
