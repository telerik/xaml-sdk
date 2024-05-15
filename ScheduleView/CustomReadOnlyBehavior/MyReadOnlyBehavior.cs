using Telerik.Windows.Controls.ScheduleView;

namespace CustomReadOnlyBehavior
{
    public class MyReadOnlyBehavior : ReadOnlyBehavior
    {
        public override bool CanDeleteAppointment(IReadOnlySettings readOnlySettings, IOccurrence occurrence)
        {
            var customAppointment = GetCustomAppointmentFromOccurence(occurrence);

            if (customAppointment != null)
            {
                return customAppointment.IsDeletable;
            }
            else
            {
                return base.CanDeleteAppointment(readOnlySettings, occurrence);
            }
        }

        public override bool CanDragAppointment(IReadOnlySettings readOnlySettings, IOccurrence occurrence)
        {
            var customAppointment = GetCustomAppointmentFromOccurence(occurrence);

            if (customAppointment != null)
            {
                return customAppointment.IsDraggable;
            }
            else
            {
                return base.CanDragAppointment(readOnlySettings, occurrence);
            }
        }

        public override bool CanEditAppointment(IReadOnlySettings readOnlySettings, IOccurrence occurrence)
        {
            var customAppointment = GetCustomAppointmentFromOccurence(occurrence);

            if (customAppointment != null)
            {
                return customAppointment.IsEditable;
            }
            else
            {
                return base.CanEditAppointment(readOnlySettings, occurrence);
            }
            
        }

        // If the subject of an appointment is empty, it cannot be saved
        public override bool CanSaveAppointment(IReadOnlySettings readOnlySettings, IOccurrence occurrence)
        {
            var customAppointment = GetCustomAppointmentFromOccurence(occurrence);

            if (customAppointment != null)
            {
                return customAppointment.Subject != string.Empty;
            }
            else
            {
                return base.CanSaveAppointment(readOnlySettings, occurrence);
            }
        }

        public override bool CanResizeAppointment(IReadOnlySettings readOnlySettings, IOccurrence occurrence)
        {
            var customAppointment = GetCustomAppointmentFromOccurence(occurrence);

            if (customAppointment != null)
            {
                return customAppointment.IsResizable;
            }
            else
            {
                return base.CanResizeAppointment(readOnlySettings, occurrence);
            }
        }

        private CustomAppointment GetCustomAppointmentFromOccurence(IOccurrence occurrence)
        {
            if(occurrence is CustomAppointment)
            {
                return occurrence as CustomAppointment;
            }
            else
            {
                var occ = (occurrence as Occurrence).Appointment as CustomAppointment;
                return occ;
            }
        }
    }
}
