using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ScheduleView;

namespace GroupingByTimeMarkers
{
    public class ScheduleViewHelper
    {
        private static bool IsAppointmentChangedThroughDialog;
        private static RadScheduleView scheduleView;

        public static readonly DependencyProperty IsTimeMarkerGroupingEnabledProperty =
            DependencyProperty.RegisterAttached("IsTimeMarkerGroupingEnabled", typeof(bool), typeof(ScheduleViewHelper), new PropertyMetadata(false, OnIsTimeMarkerGroupingEnabledChagned));

        public static bool GetIsTimeMarkerGroupingEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsTimeMarkerGroupingEnabledProperty);
        }

        public static void SetIsTimeMarkerGroupingEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(IsTimeMarkerGroupingEnabledProperty, value);
        }

        private static void OnIsTimeMarkerGroupingEnabledChagned(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.Dispatcher.BeginInvoke(new Action(() =>
            {
                scheduleView = d as RadScheduleView;

                if (scheduleView != null && e.NewValue != e.OldValue)
                {
                    if ((bool)e.NewValue)
                    {
                        var viewModel = scheduleView.DataContext as MyViewModel;
                        var timeMarkers = scheduleView.TimeMarkersSource;

                        var timeMarkerResources = new ResourceType("TimeMarkers");

                        foreach (TimeMarker marker in timeMarkers)
                        {
                            Resource res = new Resource(marker.TimeMarkerName);
                            timeMarkerResources.Resources.Add(res);
                        }

                        viewModel.ResourcesTypes.Add(timeMarkerResources);

                        ResourceGroupDescription groupDescription = new ResourceGroupDescription();
                        groupDescription.ResourceType = timeMarkerResources.Name;
                        groupDescription.ShowNullGroup = true;
                        viewModel.GroupDescriptions.Add(groupDescription);

                        scheduleView.AppointmentCreated += OnAppointmentCreated;
                        scheduleView.AppointmentEdited += OnAppointmentEdited;
                        scheduleView.DialogClosing += OnDialogClosing;
                        scheduleView.Loaded += OnLoaded;
                    }
                    else
                    {
                        scheduleView.AppointmentCreated -= OnAppointmentCreated;
                        scheduleView.AppointmentEdited -= OnAppointmentEdited;
                        scheduleView.DialogClosing -= OnDialogClosing;
                        scheduleView.Loaded -= OnLoaded;
                    }
                }
            }));
        }

        private static void OnLoaded(object sender, RoutedEventArgs e)
        {
            var viewModel = scheduleView.DataContext as MyViewModel;
            viewModel.Appointments = viewModel.GenerateAppointments();

            scheduleView.AppointmentsSource = viewModel.Appointments;

            foreach (var app in viewModel.Appointments)
            {
                UpdateMasterAppointment((Appointment)app);
            }
        }

        private static void OnDialogClosing(object sender, CancelRoutedEventArgs e)
        {
            var closeDialogArgs = e as CloseDialogEventArgs;

            if (closeDialogArgs != null)
            {
                if (closeDialogArgs.DialogViewModel is AppointmentDialogViewModel)
                {
                    if (closeDialogArgs.DialogResult.HasValue && closeDialogArgs.DialogResult == true)
                    {
                        IsAppointmentChangedThroughDialog = true;
                    }
                    else
                    {
                        if (closeDialogArgs.DialogResult == false)
                        {
                            IsAppointmentChangedThroughDialog = false;
                        }
                    }
                }
            }
        }

        private static void OnAppointmentEdited(object sender, AppointmentEditedEventArgs e)
        {
            scheduleView = sender as RadScheduleView;
            var editedApp = e.Appointment as Appointment;

            var resourcers = editedApp.Resources;
            var timeMarker = editedApp.TimeMarker;

            var currnetTimeMarkerResouce = scheduleView.ResourceTypesSource.Cast<ResourceType>()
                .Select(a => a.Resources).Where(a => a.ResourceType == "TimeMarkers").FirstOrDefault();

            if (!editedApp.Resources.Any(a => a.ResourceType == currnetTimeMarkerResouce.ResourceType) && IsAppointmentChangedThroughDialog)
            {
                if (timeMarker != null)
                {
                    var newResource = currnetTimeMarkerResouce.Where(a => a.DisplayName == timeMarker.TimeMarkerName).FirstOrDefault();

                    EditAppointment(editedApp, null, newResource);
                }
            }
            else
            {
                if (IsAppointmentChangedThroughDialog)
                {
                    var currentResource = editedApp.Resources.Select(a => a).Where(a => a.ResourceType == "TimeMarkers").FirstOrDefault();

                    if (timeMarker != null)
                    {
                        var newResource = currnetTimeMarkerResouce.Where(a => a.DisplayName == timeMarker.TimeMarkerName).FirstOrDefault();

                        if (currentResource != newResource)
                        {
                            EditAppointment(editedApp, currentResource, newResource);
                        }
                    }
                    else
                    {
                        EditAppointment(editedApp, currentResource, null);
                    }

                    if (editedApp.RecurrenceRule != null)
                    {
                        if (editedApp.RecurrenceRule.Exceptions.Count > 0)
                        {
                            foreach (var excApp in editedApp.RecurrenceRule.Exceptions)
                            {
                                var app = excApp.Appointment as Appointment;
                                var currentExcResource = app.Resources.Select(a => a).Where(a => a.ResourceType == "TimeMarkers").FirstOrDefault();

                                if (app.TimeMarker != null)
                                {
                                    var newExcResource = currnetTimeMarkerResouce.Where(a => a.DisplayName == app.TimeMarker.TimeMarkerName).FirstOrDefault();

                                    if(currentExcResource != null)
                                    {
                                        if (app.TimeMarker.TimeMarkerName != currentExcResource.DisplayName)
                                        {
                                            EditAppointment(app, currentExcResource, newExcResource);
                                            UpdateMasterAppointment(editedApp);
                                        }
                                    }
                                    else
                                    {
                                        EditAppointment(app, null, newExcResource);
                                        UpdateMasterAppointment(editedApp);
                                    }
                                }
                                else
                                {
                                    EditAppointment(app, currentExcResource, null);
                                    UpdateMasterAppointment(editedApp);
                                }
                            }
                        }
                    }
                }
                else
                {
                    var markers = new ObservableCollection<TimeMarker>(scheduleView.TimeMarkersSource.Cast<TimeMarker>().ToList());
                    var currResourcesNames = editedApp.Resources.Select(b => b.ResourceName);
                    var newMarker = markers.Where(a => currResourcesNames.Contains(a.TimeMarkerName)).FirstOrDefault();

                    if (editedApp.TimeMarker != newMarker)
                    {
                        editedApp.TimeMarker = newMarker;
                    }

                    if (editedApp.RecurrenceRule != null)
                    {
                        if (editedApp.RecurrenceRule.Exceptions.Count > 0)
                        {
                            foreach (var excApp in editedApp.RecurrenceRule.Exceptions)
                            {
                                var exception = excApp.Appointment as Appointment;
                                var currExcResourcesNames = exception.Resources.Select(b => b.ResourceName);
                                var newExcMarker = markers.Where(a => currExcResourcesNames.Contains(a.TimeMarkerName)).FirstOrDefault();

                                if (exception.TimeMarker != newExcMarker)
                                {
                                    exception.TimeMarker = newExcMarker;
                                }
                            }
                        }
                    }
                }

            }

            if (IsAppointmentChangedThroughDialog)
            {
                IsAppointmentChangedThroughDialog = false;
            }
        }

        private static void OnAppointmentCreated(object sender, AppointmentCreatedEventArgs e)
        {
            scheduleView = sender as RadScheduleView;
            var createdApp = e.CreatedAppointment as Appointment;

            var timeMarker = createdApp.TimeMarker as TimeMarker;
            var resourcers = createdApp.Resources;

            var currentMarkerResource = scheduleView.ResourceTypesSource.Cast<ResourceType>()
                .Select(a => a.Resources).Where(a => a.ResourceType == "TimeMarkers").FirstOrDefault();

            if (timeMarker != null)
            {
                var markers = new ObservableCollection<TimeMarker>(scheduleView.TimeMarkersSource.Cast<TimeMarker>().ToList());

                if (!createdApp.Resources.Any(a => a.ResourceType == currentMarkerResource.ResourceType))
                {
                    var newResource = currentMarkerResource.Where(a => a.DisplayName == timeMarker.TimeMarkerName).FirstOrDefault();
                    createdApp.Resources.Add(newResource);
                }
                else
                {
                    var oldMarker = markers.Where(a => a.TimeMarkerName.Equals(createdApp.Resources.Select(b => b.ResourceName).FirstOrDefault())).First();
                    var currTimeMarkerRes = createdApp.Resources.Where(a => a.ResourceName.Equals(oldMarker.TimeMarkerName)).First();
                    var newResource = currentMarkerResource.Where(a => a.DisplayName == timeMarker.TimeMarkerName).FirstOrDefault();

                    EditAppointment(createdApp, currTimeMarkerRes, newResource);
                }
            }
            else
            {
                var markers = new ObservableCollection<TimeMarker>(scheduleView.TimeMarkersSource.Cast<TimeMarker>().ToList());

                if (createdApp.Resources.Any(a => a.ResourceType == currentMarkerResource.ResourceType))
                {
                    var currentMarker = markers.Where(a => a.TimeMarkerName.Equals(createdApp.Resources.Select(b => b.ResourceName).FirstOrDefault())).First();
                    createdApp.TimeMarker = currentMarker;
                }
            }

            if (IsAppointmentChangedThroughDialog)
            {
                IsAppointmentChangedThroughDialog = false;
            }
        }

        private static void EditAppointment(Appointment app, IResource currentResource, IResource newResource)
        {
            Application.Current.RootVisual.Dispatcher.BeginInvoke(new Action(() =>
            {
                if (currentResource != null && newResource != null)
                {

                    if (scheduleView.BeginEdit(app))
                    {
                        app.Resources.Remove(currentResource);
                        app.Resources.Add(newResource);
                        scheduleView.Commit();
                    }
                }
                else
                {
                    if (currentResource != null)
                    {
                        if (scheduleView.BeginEdit(app))
                        {
                            app.Resources.Remove(currentResource);
                            scheduleView.Commit();
                        }
                    }
                    else
                    {
                        if(newResource != null)
                        {
                            if (scheduleView.BeginEdit(app))
                            {
                                app.Resources.Add(newResource);
                                scheduleView.Commit();
                            }
                        }
                    }
                }
            }));
        }

        private static void UpdateMasterAppointment(Appointment editedApp)
        {
           Application.Current.RootVisual.Dispatcher.BeginInvoke(new Action(() =>
           {
               if (scheduleView.BeginEdit(editedApp))
               {
                   scheduleView.Commit();
               }
           }));
        }
    }
}
