using System.Collections.Generic;
using System.Linq;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ScheduleView;

namespace AvoidOverlappingAppointments
{
	public class ConflictCheckingDragDropBehavior : ScheduleViewDragDropBehavior
	{
        public override bool CanDrop(DragDropState state)
        {
            var draggedOccurrence = state.Appointment as Occurrence;

            return state.DestinationAppointmentsSource
                .OfType<IAppointment>()
                .Where((IAppointment a) => !state.DraggedAppointments.Contains(a))
                .All((IAppointment a) => state.DestinationSlots.All((Slot s) => !ConflictChecking.AreOverlapping(a, s, draggedOccurrence)));
        }

        public override bool CanResize(DragDropState state)
        {
            var draggedOccurrence = state.Appointment as Occurrence;

            return state.DestinationAppointmentsSource
                    .OfType<IAppointment>()
                    .Where((IAppointment a) => a != state.Appointment)
                    .All((IAppointment a) => state.DestinationSlots.All((Slot s) => !ConflictChecking.AreOverlapping(a, s, draggedOccurrence)));

        }
	}

	public static class ConflictChecking
	{
        private static bool AreIntersected(IEnumerable<IResource> first, IEnumerable<IResource> other)
        {
            IEnumerable<IResource> firstResources = first.Distinct();

            IEnumerable<IResource> otherResources = other.Distinct();

            if (firstResources.Count() == 0)
            {
                return false;
            }

            var result = firstResources.Where((IResource r) => otherResources.Contains(r)).Count() == firstResources.Count();

            return result;
        }

        public static bool AreOverlapping(IAppointment appointment, Slot slot, Occurrence draggedOccurrence)
        {
            //check whether the dragged appointment goes over an appointment or an occurrence
            if (appointment.RecurrenceRule == null)
                return (appointment.IntersectsWith(slot) && AreIntersected(appointment.Resources.OfType<IResource>(), slot.Resources.OfType<IResource>()));
            else
                return CheckOccurrences(appointment, slot, draggedOccurrence);

        }

        public static bool CheckOccurrences(IAppointment app, Slot slot, Occurrence draggedOccurrence)
        {
            var occurrences = app.GetOccurrences(slot.Start, slot.End).Where(p => !p.Equals(draggedOccurrence));
            var realOccurrences = new List<Occurrence>();
            foreach (var occ in occurrences)
            {
                if (occurrences != null)
                {
                    if (occ.IntersectsWith(slot) && AreIntersected(occ.Appointment.Resources.OfType<IResource>(), slot.Resources.OfType<IResource>()))
                    {
                        realOccurrences.Add(occ);
                    }
                }
            }
            return realOccurrences.Count() > 0;
        }
	}
}
