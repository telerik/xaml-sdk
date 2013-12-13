using System.Linq;
using Telerik.Windows.Controls.ScheduleView;
using Telerik.Windows.DragDrop.Behaviors;

namespace CustomDragDropBehavior
{
    public class AppointmentToCustomerConverter : DataConverter
    {
        public override string[] GetConvertToFormats()
        {
            return new string[] { typeof(ScheduleViewDragDropPayload).FullName, typeof(Meeting).FullName };
        }

        public override object ConvertTo(object data, string format)
        {
            var payload = DataObjectHelper.GetData(data, typeof(ScheduleViewDragDropPayload), false) as ScheduleViewDragDropPayload;
            if (payload != null)
            {
                var customers = payload.DraggedAppointments;
                return customers.OfType<CustomAppointment>().Select(a => new Meeting { Name = a.Subject });
            }

            return null;
        }
    }
}
