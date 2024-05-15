using System.Collections.Generic;
using System.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ScheduleView;

namespace SettingSelectedAppointments
{
    public class SelectedAppointmentsHelper
    {
        public static readonly DependencyProperty SelectedAppointmentsProperty =
           DependencyProperty.RegisterAttached("SelectedAppointments", typeof(IEnumerable<IAppointment>), typeof(SelectedAppointmentsHelper), new PropertyMetadata(null, OnSelectedAppointmentsChanged));

        public static IEnumerable<IAppointment> GetSelectedAppointments(DependencyObject obj)
        {
            return (IEnumerable<IAppointment>)obj.GetValue(SelectedAppointmentsProperty);
        }

        public static void SetSelectedAppointments(DependencyObject obj, IEnumerable<IAppointment> value)
        {
            obj.SetValue(SelectedAppointmentsProperty, value);
        }

        private static void OnSelectedAppointmentsChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var scheduleView = sender as RadScheduleView;
            if (scheduleView != null)
            {
                IEnumerable<IAppointment> selectedItems = GetSelectedAppointments(scheduleView);
                if (selectedItems != null)
                {
                    scheduleView.SelectedAppointments.Clear();
                    foreach (var item in selectedItems)
                    {
                        scheduleView.SelectedAppointments.Add(item);
                    }
                }
            }
        }
    }
}
