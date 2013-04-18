using System.Collections.ObjectModel;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ScheduleView;
using System.Windows.Input;
using System;

namespace SpecialSlotsToolTip
{
	public class ViewModel: ViewModelBase
	{
		public ObservableCollection<Appointment> Appointments { get; set; }
		public ObservableCollection<Slot> SpecialSlots { get; set; }
	

		public ViewModel()
		{
			
			this.Appointments = new ObservableCollection<Appointment>();
			this.SpecialSlots = new ObservableCollection<Slot>()
			{ 
				this.CreateLunchTimeSlot()
			};
		}

		private Slot CreateLunchTimeSlot()
		{
			Slot slot = new Slot() { Start = DateTime.Today.AddHours(12), End=DateTime.Today.AddHours(13)};
			slot.RecurrencePattern = new RecurrencePattern(null, RecurrenceDays.WeekDays, RecurrenceFrequency.Weekly, 1, null, null);
			return slot;
		}
	}
}
