using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ScheduleView;
using System.Collections;

namespace CutCopyPasteAppointments
{
	public partial class MainPage : UserControl
	{
		private ViewModel viewModel;	

		public MainPage()
		{
            viewModel = new ViewModel();
            this.DataContext = viewModel;
			CommandManager.SetCommandBindings(this, new CommandBindingCollection()
			{              
				new CommandBinding(MyCommands.CutCommand, CutCommand_Executed, CutCopyCommand_CanExecute),
				new CommandBinding(MyCommands.CopyCommand, CopyCommand_Executed, CutCopyCommand_CanExecute),
				new CommandBinding(MyCommands.PasteCommand, PasteCommand_Executed, PasteCommand_CanExecute)
			});	

			InitializeComponent();		

		}
        protected void CutCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            viewModel.CutCommand.Execute(this.ScheduleView.SelectedAppointments);
        }

        private void CutCopyCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = viewModel.CutCommand.CanExecute(this.ScheduleView.SelectedAppointments);
        }

		private void CopyCommand_Executed(object sender, ExecutedRoutedEventArgs e)
		{
            viewModel.CopyCommand.Execute(this.ScheduleView.SelectedAppointments);
		}
	

		private void PasteCommand_Executed(object sender, ExecutedRoutedEventArgs e)
		{
            viewModel.PasteCommand.Execute(this.ScheduleView.SelectedSlot);	
		}

		private void PasteCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
            e.CanExecute = viewModel.PasteCommand.CanExecute(this.ScheduleView.SelectedSlot);
		}

		
	}
}
