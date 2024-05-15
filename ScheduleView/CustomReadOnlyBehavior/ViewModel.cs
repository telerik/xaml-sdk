using System;
using System.Collections.ObjectModel;
using Telerik.Windows.Controls;

namespace CustomReadOnlyBehavior
{
    public class ViewModel : ViewModelBase
    {
        public ObservableCollection<CustomAppointment> Appointments { get; set; }

        public ViewModel()
        {
            this.Appointments = new ObservableCollection<CustomAppointment>
            {
                new CustomAppointment { Subject = "Undeletable Appointment", Start = DateTime.Today.AddHours(2), End = DateTime.Today.AddHours(4), IsDeletable = false},
                new CustomAppointment { Subject = "Undraggable Appointment", Start = DateTime.Today.AddHours(5), End = DateTime.Today.AddHours(7), IsDraggable = false },
                new CustomAppointment { Subject = "Unresizable Appointment", Start = DateTime.Today.AddHours(8), End = DateTime.Today.AddHours(10), IsResizable = false },
                new CustomAppointment { Subject = "Uneditable Appointment", Start = DateTime.Today.AddHours(11), End = DateTime.Today.AddHours(13), IsEditable = false },
                new CustomAppointment { Subject = "Try to save with empty subject", Start = DateTime.Today.AddHours(14), End = DateTime.Today.AddHours(16)}
            };
        }
    }
}
