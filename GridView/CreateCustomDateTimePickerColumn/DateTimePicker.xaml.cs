using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace CreateCustomDateTimePickerColumn
{
	public partial class DateTimePicker : UserControl
	{
		public static readonly DependencyProperty SelectedDateProperty =
		   DependencyProperty.Register("SelectedDate", typeof(DateTime?), typeof(DateTimePicker), new PropertyMetadata(null));

		public DateTimePicker()
		{
			InitializeComponent();
		}

		public DateTime? SelectedDate
		{
			get
			{
				return (DateTime?)this.GetValue(SelectedDateProperty);
			}
			set
			{
				this.SetValue(SelectedDateProperty, value);
			}
		}

		private void HandlePickersSelectionChanged()
		{
			if (this.Calendar.SelectedDate != null && this.TimePicker.SelectedTime != null)
			{
				this.SelectedDate = this.Calendar.SelectedDate + this.TimePicker.SelectedTime;
			}
		}

	#if !SILVERLIGHT
		private void OnTimePickerSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
	#else
		private void OnTimePickerSelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangedEventArgs e)
	#endif
		{
			this.HandlePickersSelectionChanged();
		}

	#if !SILVERLIGHT
		private void OnCalendarSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
	#else
		private void OnCalendarSelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangedEventArgs e)
	#endif
		{
			this.HandlePickersSelectionChanged();
		}
	}
}

