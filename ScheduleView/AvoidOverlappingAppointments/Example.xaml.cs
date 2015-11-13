using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ScheduleView;


namespace AvoidOverlappingAppointments
{
    public partial class Example : UserControl
    {
        private Slot destinationSlot = null;     

        public Example()
        {
            this.DataContext = new ViewModel();
            InitializeComponent();
        }

        private void RadScheduleView_ShowDialog(object sender, ShowDialogEventArgs e)
        {           
            var viewModel = e.DialogViewModel as RecurrenceChoiceDialogViewModel;
            if (viewModel != null)
            {                
                if (viewModel.RecurrenceChoiceDialogMode == RecurrenceChoiceDialogMode.Dragging || viewModel.RecurrenceChoiceDialogMode == RecurrenceChoiceDialogMode.Resizing)
                {
                    var scheduleView = sender as RadScheduleView;
                    destinationSlot = scheduleView.HighlightedSlots[0] as Slot;
                }
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            var recurrenceChoiceDialogViewModel = (sender as RadButton).DataContext as RecurrenceChoiceDialogViewModel;

            //the following code is used when the user drags/resizes an occurrence, and in RecurrenceChoiceDialog chooses to edit the whole series.
            if (recurrenceChoiceDialogViewModel.IsSeriesModeSelected == true && destinationSlot != null)
            {
                if (CheckAllOccurrencesDestinationSlots(recurrenceChoiceDialogViewModel.Occurrence, destinationSlot, null))
                    WindowCommands.Cancel.Execute(null, sender as RadButton);
            }
            else
                WindowCommands.Confirm.Execute(null, sender as RadButton);
        }

        private bool CheckAllOccurrencesDestinationSlots(Occurrence currentOccurrence, Slot currentDestinationSlot, Occurrence editedOccurrence)
        {           
            var currentApp = currentOccurrence.Appointment as Appointment;

            var offsetOfTheOccurrence = currentApp.Start - currentOccurrence.Start;
            var destSlotOfMasterApp = OffsetSlot(currentDestinationSlot, offsetOfTheOccurrence);


            var occurrences = currentApp.GetOccurrences(scheduleView.VisibleRange.Start, scheduleView.VisibleRange.End);

            destinationSlot = null;
            foreach (var occ in occurrences)
            {
                var occurrenceDestinationSlot = OffsetSlot(destSlotOfMasterApp, occ.Start - currentApp.Start);

                var appsInOccurrenceDestinationSlot = scheduleView.AppointmentsSource
                    .OfType<IAppointment>()
                    .Where((IAppointment a) => a != occ.Appointment)
                    .All((IAppointment a) => !ConflictChecking.AreOverlapping(a, occurrenceDestinationSlot, editedOccurrence));

                if (!appsInOccurrenceDestinationSlot)
                {
                    ShowErrorWindow();
                    return true;
                }
            }

            return false;
        }

        private static Slot OffsetSlot(Slot slot, TimeSpan offset)
        {
            var newSlot = new Slot(slot);
            newSlot.Start = slot.Start.Add(offset);
            newSlot.End = slot.End.Add(offset);
            return newSlot;
        }

        private void ShowErrorWindow()
        {
            RadWindow.Alert("There are appointments in the destination slots");
        }       
    }
}
