using Telerik.Windows.Controls.ScheduleView;

namespace CustomDragDropBehavior
{
    public class CustomAppointment : Appointment
    {
        public bool IsReadOnly { get; set; }
        public bool IsDraggedFromListBox { get; set; }
    }
}
