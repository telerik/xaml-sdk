
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ScheduleView;
using Telerik.Windows.Media.Imaging;


namespace Exporting
{
    public class ViewModel
    {
        public ObservableCollection<Appointment> Appointments { get; private set; }

        public ICommand ExportToImageCommand { get; set; }
        public ICommand ExportToXpsCommand { get; set; }

        public ViewModel()
        {
            var today = DateTime.Today;
            var endOfDay = new DateTime(today.Year, today.Month, today.Day, 23, 59, 59);
            var startOfDay = new DateTime(today.Year, today.Month, today.Day, 0, 0, 0);

            this.Appointments = new ObservableCollection<Appointment>()
			{
				new Appointment() { Subject = "First Appointment", Start = startOfDay, End = startOfDay.AddHours(1)},
				new Appointment() { Subject = "Last Appointment", Start = endOfDay.AddHours(-1), End = endOfDay}
			};

            this.ExportToImageCommand = new DelegateCommand(ExportToImageCommandExecute);
            this.ExportToXpsCommand = new DelegateCommand(ExportToXpsCommandExecute);
        }

        protected void ExportToImageCommandExecute(object param)
        {
            var scheduleView = param as RadScheduleView;

            scheduleView.MinTimeRulerExtent = scheduleView.ActualHeight - 60;
            scheduleView.MaxTimeRulerExtent = scheduleView.ActualHeight - 60;

            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                DefaultExt = "png",
                Filter = "PNG (*.png)|*.png"

            };

            if (saveFileDialog.ShowDialog() == true)
            {
                using (Stream stream = saveFileDialog.OpenFile())
                {
                    ExportExtensions.ExportToImage(scheduleView, stream, new PngBitmapEncoder());
                }
            }
        }

        protected void ExportToXpsCommandExecute(object param)
        {
            var scheduleView = param as RadScheduleView;

            scheduleView.MinTimeRulerExtent = scheduleView.ActualHeight - 60;
            scheduleView.MaxTimeRulerExtent = scheduleView.ActualHeight - 60;

            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                DefaultExt = "xps",
                Filter = "XPS (*.xps)|*.xps"
              
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                using (Stream stream = saveFileDialog.OpenFile())
                {
                    ExportExtensions.ExportToXpsImage(scheduleView, stream);
                }
            }
        }
    }
}
