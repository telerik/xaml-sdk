using System;
using System.Windows;
using System.Windows.Input;
using Telerik.Windows.Controls;

namespace EventDoubleClick
{
	public class GanttDoubleClickExtension
	{
		public static DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command",
																		typeof(DelegateCommand),
																		typeof(GanttDoubleClickExtension),
																		new PropertyMetadata(null, OnCommandPropertyChanged));

		public static DependencyProperty CommandParameterProperty = DependencyProperty.RegisterAttached("CommandParameter",
																		typeof(object),
																		typeof(GanttDoubleClickExtension), new PropertyMetadata(null));

		public static readonly DependencyProperty ClickCountProperty = DependencyProperty.RegisterAttached("ClickCount",
																		typeof(int),
																		typeof(GanttDoubleClickExtension),
																		new PropertyMetadata(1));

		private static readonly DependencyProperty BehaviorProperty = DependencyProperty.RegisterAttached("Behavior",
																		typeof(GanttDoubleClickExtension),
																		typeof(GanttDoubleClickExtension),
																		new PropertyMetadata(null));

		private static readonly TimeSpan DoubleClickThreshold = TimeSpan.FromMilliseconds(1450);
		private DateTime lastClick;
		private int clicks = 0;

		public static int GetClickCount(DependencyObject obj)
		{
			return (int)obj.GetValue(ClickCountProperty);
		}

		public static void SetClickCount(DependencyObject obj, int value)
		{
			obj.SetValue(ClickCountProperty, value);
		}

		public static object GetCommandParameter(DependencyObject obj)
		{
			return (object)obj.GetValue(CommandParameterProperty);
		}

		public static void SetCommandParameter(DependencyObject obj, object value)
		{
			obj.SetValue(CommandParameterProperty, value);
		}

		public static DelegateCommand GetCommand(DependencyObject obj)
		{
			return (DelegateCommand)obj.GetValue(CommandProperty);
		}

		public static void SetCommand(DependencyObject obj, ICommand value)
		{
			obj.SetValue(CommandProperty, value);
		}

		private static GanttDoubleClickExtension GetBehavior(DependencyObject obj)
		{
			return (GanttDoubleClickExtension)obj.GetValue(BehaviorProperty);
		}

		private static void SetBehavior(DependencyObject obj, GanttDoubleClickExtension value)
		{
			obj.SetValue(BehaviorProperty, value);
		}

		private static void OnCommandPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var element = (FrameworkElement)d;

			if (e.NewValue != null)
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
			var behavior = new GanttDoubleClickExtension();
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
			element.MouseLeftButtonDown += OnMouseLeftButtonDown;
		}

		private void Detach(FrameworkElement element)
		{
			element.MouseLeftButtonDown -= OnMouseLeftButtonDown;
		}

		private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			var target = sender as DependencyObject;

			if (DateTime.Now - lastClick > DoubleClickThreshold)
			{
				clicks = 1;
				lastClick = DateTime.Now;
			}
			else
			{
				clicks++;
			}

			if (clicks == GetClickCount(target))
			{
				var command = GetCommand(target);
				if (command != null)
					command.Execute(GetCommandParameter(target));
			}
		}
	}
}