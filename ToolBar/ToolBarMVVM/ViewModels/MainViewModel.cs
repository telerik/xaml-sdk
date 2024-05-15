using System;
using System.Collections.ObjectModel;
using System.Linq;
using Telerik.Windows.Controls;

namespace ToolBarMVVM
{
	public class MainViewModel
	{
		public MainViewModel()
		{
			this.PopulateSampleViewModel();
		}
		public ObservableCollection<ViewModelBase> Items { get; set; }

		private void PopulateSampleViewModel()
		{
			this.Items = new ObservableCollection<ViewModelBase>()
			{
				new TextBlockViewModel("Foreground:"),
				new ColorPickerViewModel(),
				new TextBlockViewModel("Background:"),
				new ColorPickerViewModel(),
				new TextBlockViewModel("BorderColor:"),
				new ColorPickerViewModel(),
				new SeparatorViewModel(),
				new ButtonViewModel("SAVE !", "Save colors configuration."),
			};
		}
	}
}
