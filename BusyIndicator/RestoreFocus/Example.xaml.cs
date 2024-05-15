using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Telerik.Windows.Controls;

namespace RestoreFocus
{
	/// <summary>
	/// Interaction logic for Example.xaml
	/// </summary>
	public partial class Example : UserControl
	{
		//private FrameworkElement focusedElement;

		public Control focusedElement { get; set; }

		private DispatcherTimer dispatcherTimer { get; set; }

		public Example()
		{
			InitializeComponent();

			this.dispatcherTimer = new DispatcherTimer();
			dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
			dispatcherTimer.Tick += new EventHandler(timerTick);
			dispatcherTimer.Start();
		}

		private void timerTick(object sender, EventArgs e)
		{
			var isBusy = this.BusyIndicator.IsBusy;
			if (!isBusy)
			{
				this.focusedElement = FocusManagerHelper.GetFocusedElement(this.StackPanel) as Control;
				this.BusyIndicator.IsBusy = true;
			}
			else
			{
				this.BusyIndicator.IsBusy = false;
				if (this.focusedElement != null)
				{
					this.focusedElement.IsEnabledChanged += focusedElement_IsEnabledChanged;
				}
			}
		}

		private void focusedElement_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			this.focusedElement.Focus();
			this.focusedElement.IsEnabledChanged -= focusedElement_IsEnabledChanged;
		}
	}
}
