using System.Windows;
using System.Windows.Input;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Rendering;

namespace GridSorting
{
	public class ColumnHeaderExtensions
	{
		public static bool? GetIsSortedAscending(DependencyObject obj)
		{
			return (bool?)obj.GetValue(IsSortedAscendingProperty);
		}

		public static void SetIsSortedAscending(DependencyObject obj, bool? value)
		{
			obj.SetValue(IsSortedAscendingProperty, value);
		}

		public static readonly DependencyProperty IsSortedAscendingProperty =
			DependencyProperty.RegisterAttached("IsSortedAscending", typeof(bool?), typeof(ColumnHeaderExtensions), new PropertyMetadata(default(bool?)));

		public static readonly DependencyProperty AllowSortingWithClickProperty =
			DependencyProperty.RegisterAttached("AllowSortingWithClick", typeof(bool), typeof(ColumnHeaderExtensions), new PropertyMetadata(false, OnAllowSortingWithClickPropertyChanged));

		private static readonly DependencyProperty BehaviorProperty =
			DependencyProperty.RegisterAttached("Behavior", typeof(ColumnHeaderExtensions), typeof(ColumnHeaderExtensions), new PropertyMetadata(null));

		private TasksDataSource dataSource;
		private TreeContainer.TreeItemProxy proxy;

		private ColumnHeaderExtensions()
		{

		}

		public static bool GetAllowSortingWithClick(DependencyObject obj)
		{
			return (bool)obj.GetValue(AllowSortingWithClickProperty);
		}

		public static void SetAllowSortingWithClick(DependencyObject obj, bool value)
		{
			obj.SetValue(AllowSortingWithClickProperty, value);
		}

		private static ColumnHeaderExtensions GetBehavior(DependencyObject obj)
		{
			return (ColumnHeaderExtensions)obj.GetValue(BehaviorProperty);
		}

		private static void SetBehavior(DependencyObject obj, ColumnHeaderExtensions value)
		{
			obj.SetValue(BehaviorProperty, value);
		}
		private static void OnAllowSortingWithClickPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var element = (FrameworkElement)d;

			if ((bool)e.NewValue)
			{
				AttachBehavior(element);
			}
			else
			{
				DetachBehavior(element);
			}
		}

		private static void AttachBehavior(FrameworkElement element)
		{
			var proxy = element.DataContext as ColumnHeaderContainer.TreeItemProxy;
			var behavior = new ColumnHeaderExtensions { proxy = proxy };
			behavior.Attach(element);
			SetBehavior(element, behavior);
		}

		private static void DetachBehavior(FrameworkElement element)
		{
			var behavior = GetBehavior(element);
			behavior.Detach(element);
			element.ClearValue(BehaviorProperty);
		}

		private void Attach(FrameworkElement element)
		{
			element.MouseLeftButtonDown += this.OnHeaderMouseLeftButtonDown;
			this.dataSource = element.ParentOfType<RadGanttView>().DataContext as TasksDataSource;
		}

		private void Detach(FrameworkElement element)
		{
			element.MouseLeftButtonDown -= this.OnHeaderMouseLeftButtonDown;
			this.dataSource = null;
			this.proxy = null;
		}

		private void OnHeaderMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			var columnDefinition = this.proxy.DataContext.SourceItem as ColumnDefinition;
			if (columnDefinition != null)
			{
				var header = sender as DependencyObject;

				var isSortedAscending = GetIsSortedAscending(header);

				var sortAscending = isSortedAscending.HasValue && !isSortedAscending.Value;
				SetIsSortedAscending(header, sortAscending);

				this.dataSource.SourtBy(columnDefinition.MemberBinding, sortAscending);
			}
		}
	}
}