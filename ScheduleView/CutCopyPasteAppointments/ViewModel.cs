using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Windows.Controls;
using System.Collections;
using Telerik.Windows.Controls.ScheduleView;
using System.Collections.ObjectModel;


namespace CutCopyPasteAppointments
{
	public class ViewModel : ViewModelBase
	{	
		private bool isContextMenuOpen;

		private List<Appointment> cutAppointments;
		private bool isCopied = true;

		public ViewModel()
		{
            var today = DateTime.Today;
            this.Appointments = new ObservableCollection<Appointment>()
            {
                new Appointment() { Subject = "Meeting with John", Start = today.AddHours(10), End = today.AddHours(12) },
                new Appointment() { Subject = "Interview", Start = today.AddDays(1).AddHours(14), End = today.AddDays(1).AddHours(15) }
            };
	
			this.CutCommand = new DelegateCommand(this.CutCommandExecuted, CanExecuteCutCopyCommand);
			this.CopyCommand = new DelegateCommand(this.CopyCommandExecuted, CanExecuteCutCopyCommand);
			this.PasteCommand = new DelegateCommand(this.pasteCommandExecuted, CanExecutePasteCommand);
			
            cutAppointments = new List<Appointment>();		
		}


        public ObservableCollection<Appointment> Appointments { get; set; }

		public DelegateCommand CutCommand { get; set; }		

		public DelegateCommand CopyCommand { get; set; }
	
		public DelegateCommand PasteCommand { get; set; }
		
	
		public bool IsContextMenuOpen
		{
			get
			{
				return this.isContextMenuOpen;
			}
			set
			{
				this.isContextMenuOpen = value;

				this.CommandsInvalidateCanExecute();
			}
		}


        private static bool CanExecuteCutCopyCommand(object parameter)
		{
			IEnumerable appointments = parameter as IEnumerable;		
			return appointments != null && appointments.OfType<Appointment>().Any();
		}

        private bool CanExecutePasteCommand(object parameter)
		{
			return cutAppointments.Count > 0;
		}

		private void CommandsInvalidateCanExecute()
		{		
			this.CutCommand.InvalidateCanExecute();
			this.CopyCommand.InvalidateCanExecute();
			this.PasteCommand.InvalidateCanExecute();
		}

		private void SaveCopiedAppointments(IEnumerable apps, bool setIsCopied)
		{
			if (apps != null)
			{
				cutAppointments.Clear();
				cutAppointments.AddRange(apps);
				this.isCopied = setIsCopied;
			}
		}

		private void CutCommandExecuted(object parameter)
		{
			SaveCopiedAppointments(parameter as IEnumerable, false);
		}

		private void CopyCommandExecuted(object parameter)
		{
			SaveCopiedAppointments(parameter as IEnumerable, true);
		}

		private void pasteCommandExecuted(object parameter)
		{
			var slot = parameter as Slot;
			foreach (Appointment app in cutAppointments)
			{
				if (isCopied)
				{
					var newApp = new Appointment(){
					Start = slot.Start,
					End = slot.Start.Add(app.End-app.Start),
					Subject = app.Subject
					};
					this.Appointments.Add(newApp);
					
				}
				else
				{
                    var appDuration = app.End - app.Start;
					this.Appointments.Remove(app);
					app.Start = slot.Start;
                    app.End = slot.Start.Add(appDuration);
					this.Appointments.Add(app);
				}				
			}
			this.cutAppointments.RemoveAll();
		}
	}
}
