using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;
using Telerik.Windows.Controls;

namespace DifferentlyColoredSelectedBoxes
{
	public class ViewModel : ViewModelBase
	{
		private ObservableCollection<Item> items;

		public List<SolidColorBrush> brushCollection = new List<SolidColorBrush> { 
					new SolidColorBrush(Color.FromArgb(255, 90, 20, 66)), 
					new SolidColorBrush(Colors.Red),
					new SolidColorBrush(Colors.Blue),
					new SolidColorBrush(Colors.Green),
					new SolidColorBrush(Colors.Cyan),
					new SolidColorBrush(Colors.Orange),
					new SolidColorBrush(Colors.Magenta)
		};

		public ViewModel()
		{
			this.items = GetItems(100, this.brushCollection);
		}

		private static ObservableCollection<Item> GetItems(int size, List<SolidColorBrush> brush)
		{
			var result = new ObservableCollection<Item>();
			Random rand = new Random();

			for (int i = 1; i <= size; i++)
			{
				result.Add(new Item()
				{
					Name = string.Format("Item {0}", i),
					ItemBorderBrush = brush[rand.Next(brush.Count)]
				});
			}

			return result;
		}

		/// <summary>
		/// Gets or sets Items and notifies for changes
		/// </summary>
		public ObservableCollection<Item> Items
		{
			get
			{
				return this.items;
			}

			set
			{
				if (this.items != value)
				{
					this.items = value;
					this.OnPropertyChanged(() => this.Items);
				}
			}
		}
	}
}
