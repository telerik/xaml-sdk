using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls.ScheduleView;

namespace SlotsAndAppointmentsNavigation
{
	/// <summary>
	/// Interaction logic for Example.xaml
	/// </summary>
	public partial class Example : UserControl
	{
		public Example()
        {
            this.DataContext = new ViewModel();
			InitializeComponent();
		}

		private void PreviousSlot_Click(object sender, RoutedEventArgs e)
		{
			var serviceProvider = ScheduleView.GetServiceProvider();
			var slotSelectionService = serviceProvider.GetService<SlotSelectionService>();
			var slotIterationProvider = serviceProvider.GetService<ISlotIterationService>();

			Slot previous;
			var last = slotSelectionService.GetSelection();
			if (last == null)
			{
				previous = slotIterationProvider.GetSlots().Last();
			}
			else
			{
				previous = last.Copy();

				previous.End = last.Start;
				previous.Start = last.Start.Add(-last.Duration());

				if (!ScheduleView.VisibleRange.Contains(previous.Start))
				{
					previous = slotIterationProvider.GetSlots().Last();
				}
			}
			slotSelectionService.SetSelection(previous);
			ScheduleView.ScrollIntoView(previous);
		}

		private void NextSlot_Click(object sender, RoutedEventArgs e)
		{
			var serviceProvider = ScheduleView.GetServiceProvider();
			var slotSelectionService = serviceProvider.GetService<SlotSelectionService>();
			var slotIterationProvider = serviceProvider.GetService<ISlotIterationService>();

			Slot next;
			var start = slotSelectionService.GetSelection();
			if (start == null)
			{
				next = slotIterationProvider.GetSlots().First();
			}
			else
			{
				next = start.Copy();
				next.Start = start.End;
				next.End = start.End.Add(start.Duration());

				if (!ScheduleView.VisibleRange.Contains(next.Start))
				{
					next = slotIterationProvider.GetSlots().First();
				}
			}
			slotSelectionService.SetSelection(next);
			ScheduleView.ScrollIntoView(next);		
		}

		private void PreviousAppointment_Click(object sender, RoutedEventArgs e)
		{
			var serviceProvider = ScheduleView.GetServiceProvider();
			var occurrenceSelectionService = serviceProvider.GetService<AppointmentSelectionService>();
			var occurrenceIterationProvider = serviceProvider.GetService<IOccurrenceIterationProvider>();
			var start = occurrenceSelectionService.GetSelection().FirstOrDefault();

			var previous = occurrenceIterationProvider.GetOccurrencesTo(start).Take(occurrenceIterationProvider.GetOccurrencesTo(start).Count() - 1).LastOrDefault() ?? occurrenceIterationProvider.GetOccurrences().LastOrDefault();
			if (previous != null)
			{
				occurrenceSelectionService.SetSelection(previous);
				ScheduleView.ScrollIntoView(previous);
			}
		}

		private void NextAppointment_Click(object sender, RoutedEventArgs e)
		{		
			var serviceProvider = ScheduleView.GetServiceProvider();
			var occurrenceSelectionService = serviceProvider.GetService<AppointmentSelectionService>();
			var occurrenceIterationProvider = serviceProvider.GetService<IOccurrenceIterationProvider>();
			var start = occurrenceSelectionService.GetSelection().FirstOrDefault();
			var next = occurrenceIterationProvider.GetOccurrencesFrom(start).Skip(1).FirstOrDefault() ?? occurrenceIterationProvider.GetOccurrences().FirstOrDefault();

			if (next != null)
			{
				occurrenceSelectionService.SetSelection(next);
				ScheduleView.ScrollIntoView(next);
			}
		}
	}
}
