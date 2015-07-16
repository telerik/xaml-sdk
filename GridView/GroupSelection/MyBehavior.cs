using System.Windows;
using System.Linq;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using System.Collections.Generic;
using Telerik.Windows.Data;
using System.Windows.Threading;
using System;

namespace WpfApplication1
{
	public class MyBehavior : ViewModelBase
	{
		public static readonly DependencyProperty IsEnabledPropery =
		DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(MyBehavior),
			new PropertyMetadata(new PropertyChangedCallback(OnIsEnabledPropertyChanged)));

		public static void SetIsEnabled(DependencyObject dependencyObject, bool value)
		{
			dependencyObject.SetValue(IsEnabledPropery, value);
		}

		public static bool GetIsEnabled(DependencyObject dependencyObject)
		{
			return (bool)dependencyObject.GetValue(IsEnabledPropery);
		}

		public static void OnIsEnabledPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
		{
			var checkBox = dependencyObject as CheckBox;

			if (checkBox != null)
			{
				var behavior = new MyBehavior(checkBox);
				checkBox.Dispatcher.BeginInvoke(new Action
				(() =>
				{
					var grid = checkBox.ParentOfType<RadGridView>();
					if (grid != null)
					{
						grid.SelectionChanged += behavior.grid_SelectionChanged;
					}
				}));				
			}
		}

		CheckBox checkBox = null;
		RadGridView grid = null;
        DependencyPropertyChangedEventHandler dataContextChangedHandler;
        RoutedEventHandler clickHandler;
        GridViewGroupRow groupRow = null;

		public MyBehavior(CheckBox source)
		{
			
			this.checkBox = source;
            this.checkBox.Unloaded += checkBox_Unloaded;
			checkBox.Dispatcher.BeginInvoke(new Action
				(() =>
				{
					grid = checkBox.ParentOfType<RadGridView>();
                    
					groupRow = checkBox.ParentOfType<GridViewGroupRow>();
                    
					if (grid != null && groupRow != null)
					{
                        groupRow.Unloaded += groupRow_Unloaded;
						this.UpdateIsChecked(groupRow.Group);
					}

					if (grid != null)
					{
						grid.SelectionMode = System.Windows.Controls.SelectionMode.Extended;
						grid.SelectionUnit = GridViewSelectionUnit.FullRow;

                        checkBox.Click += clickHandler = (s, e) =>
                        {
                            grid.SelectionChanged -= grid_SelectionChanged;

                            if (checkBox.IsChecked == true)
                            {
                                grid.Select(this.GetSubGroupItems(((GroupViewModel)checkBox.DataContext).Group));
                            }
                            else
                            {
                                grid.Unselect(this.GetSubGroupItems(((GroupViewModel)checkBox.DataContext).Group));
                            }

                            grid.SelectionChanged += grid_SelectionChanged;
                        }; 

						groupRow.DataContextChanged += dataContextChangedHandler = (s, e) =>
						{
							this.UpdateIsChecked(e.NewValue as IGroup);
						};
					}
				}
				));
		}

        void groupRow_Unloaded(object sender, RoutedEventArgs e)
        {
            if (dataContextChangedHandler != null)
            {
                groupRow.DataContextChanged -= dataContextChangedHandler;
            }
        }

        void checkBox_Unloaded(object sender, RoutedEventArgs e)
        {
            if (clickHandler != null)
            {
                checkBox.Click -= clickHandler;
            }
        }

		private void UpdateIsChecked(IGroup group)
		{
			var groupItems = GetSubGroupItems(group);
			if (grid.SelectedItems.Intersect(groupItems).Count() == groupItems.Count())
			{
				checkBox.IsChecked = true;
			}
			else if (grid.SelectedItems.Intersect(groupItems).Any())
			{
				checkBox.IsChecked = null;
			}
			else
			{
				checkBox.IsChecked = false;
			}
		}

		private IEnumerable<object> GetSubGroupItems(IGroup gr)
		{
			if (!gr.HasSubgroups)
			{
				return gr.Items.OfType<object>();
			}
			else
			{
				List<object> items = new List<object>();
				foreach (var subGr in gr.Subgroups)
				{
					items.AddRange(GetSubGroupItems(subGr));
				}
				return items;
			}
		}

		void grid_SelectionChanged(object sender, SelectionChangeEventArgs e)
		{
			if (checkBox.DataContext == null)
			{
				return;
			}
			var groupItems = this.GetSubGroupItems((checkBox.DataContext as GroupViewModel).Group);
			if ((sender as RadGridView).SelectedItems.Intersect(groupItems).Count() == groupItems.Count())
			{
				checkBox.IsChecked = true;
			}
			else if ((sender as RadGridView).SelectedItems.Intersect(groupItems).Any())
			{
				checkBox.IsChecked = null;
			}
			else
			{
				checkBox.IsChecked = false;
			}
		}
	}
}
