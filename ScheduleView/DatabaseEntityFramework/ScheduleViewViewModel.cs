using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ScheduleView;

namespace DatabaseEntityFramework
{
    public class ScheduleViewViewModel : ViewModelBase
    {
        private bool isBusy;
        private bool isInitialLoad;

        public ScheduleViewViewModel()
        {
            this.isInitialLoad = true;
            this.VisibleRangeChanged = new DelegateCommand(this.VisibleRangeExecuted, (param) => true);
            this.SaveCommand = new DelegateCommand(OnSaveExecuted, (param) => true);
            this.ResourceTypes = new ObservableCollection<SqlResourceType>();
            this.Appointments = new ObservableCollection<SqlAppointment>();
            this.TimeMarkers = new ObservableCollection<TimeMarker>();
            this.Categories = new ObservableCollection<Category>();

            this.Appointments.CollectionChanged += OnAppointmentsCollectionChanged;

            this.LoadData();
        }

        public bool IsLoading
        {
            get
            {
                return this.isBusy;
            }
            set
            {
                if (this.isBusy != value)
                {
                    this.isBusy = value;
                    this.OnPropertyChanged(() => this.IsLoading);
                }
            }
        }

        public ICommand VisibleRangeChanged
        {
            get;
            private set;
        }

        public ICommand SaveCommand
        {
            get;
            private set;
        }

        public ObservableCollection<SqlAppointment> Appointments
        {
            get;
            private set;
        }

        public ObservableCollection<SqlResourceType> ResourceTypes
        {
            get;
            private set;
        }

        public ObservableCollection<TimeMarker> TimeMarkers
        {
            get;
            private set;
        }

        public ObservableCollection<Category> Categories
        {
            get;
            private set;
        }

        private void OnSaveExecuted(object param)
        {
            ScheduleViewRepository.SaveData(null);
        }

        private void VisibleRangeExecuted(object param)
        {
            if (!IsLoading)
            {
                this.GenerateAppointments(param as DateSpan);
            }
        }

        private void OnAppointmentsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //Synchronization with the DB
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var app = e.NewItems == null ? null : e.NewItems[0] as SqlAppointment;
                if (app != null)
                {
                    if (ScheduleViewRepository.Context.Entry(app).State != EntityState.Unchanged)
                    {
                        ScheduleViewRepository.Context.SqlAppointments.Add(app);
                    }
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                var app = e.OldItems == null ? null : e.OldItems[0] as SqlAppointment;
                if (app != null && ScheduleViewRepository.Context.SqlAppointments.Any(a => a.SqlAppointmentId == app.SqlAppointmentId))
                {
                    if (app.RecurrenceRule != null)
                    {
                        var tempList = app.RecurrenceRule.Exceptions.ToList();

                        foreach (SqlExceptionOccurrence item in tempList)
                        {
                            ScheduleViewRepository.Context.SqlExceptionOccurrences.Remove(item);
                        }
                    }

                    var tempAppList = ScheduleViewRepository.Context.SqlAppointmentResources.Where(i => i.SqlAppointment_SqlAppointmentId == app.SqlAppointmentId).ToList();

                    foreach (var item in tempAppList)
                    {
                        ScheduleViewRepository.Context.SqlAppointmentResources.Remove(item);
                    }

                    ScheduleViewRepository.Context.SqlAppointments.Remove(app);
                }
            }
        }

        private void GenerateAppointments(DateSpan dateSpan)
        {
            if (!this.isInitialLoad)
            {
                ScheduleViewRepository.SaveData(() => this.LoadAppointments(dateSpan));
            }
            else
            {
                LoadAppointments(dateSpan);

                isInitialLoad = false;
            }
        }

        private void LoadAppointments(DateSpan dateSpan)
        {
            this.Appointments.Clear();

            this.IsLoading = true;

            this.Appointments.AddRange(ScheduleViewRepository.GetSqlAppointmentsByRange(dateSpan.Start, dateSpan.End));

            this.IsLoading = false;
        }

        void LoadData()
        {

            //this is needed to order resources
            foreach (var resType in ScheduleViewRepository.Context.SqlResourceTypes)
            {
                var a = resType.SqlResources as IEnumerable<SqlResource>;
            }
            this.ResourceTypes.AddRange(ScheduleViewRepository.Context.SqlResourceTypes);

            this.TimeMarkers.AddRange(ScheduleViewRepository.Context.TimeMarkers);

            this.Categories.AddRange(ScheduleViewRepository.Context.Categories);
        }
    }
}
