using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel.DomainServices.Client;
using System.Windows.Input;
using ScheduleViewDB.Web;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ScheduleView;
using System;

namespace ScheduleViewDB.ViewModel
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
			this.TimeMarkers = new ObservableCollection<Web.TimeMarker>();
			this.Categories = new ObservableCollection<Web.Category>();

			this.Appointments.CollectionChanged += OnAppointmentsCollectionChanged;

			if (!DesignerProperties.IsInDesignTool)
			{
				this.LoadData();
			}
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

		public ObservableCollection<SqlExceptionAppointment> ExceptionAppointments
		{
			get;
			private set;
		}

		public ObservableCollection<SqlResourceType> ResourceTypes
		{
			get;
			private set;
		}

		public ObservableCollection<ScheduleViewDB.Web.TimeMarker> TimeMarkers
		{
			get;
			private set;
		}

		public ObservableCollection<ScheduleViewDB.Web.Category> Categories
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
			if (e.Action == NotifyCollectionChangedAction.Add)
			{
				var app = e.NewItems == null ? null : e.NewItems[0] as SqlAppointment;
				if (app != null && app.EntityState != EntityState.Unmodified)
				{
					ScheduleViewRepository.Context.SqlAppointments.Add(app);
				}
			}
			else if (e.Action == NotifyCollectionChangedAction.Remove)
			{
				var app = e.OldItems == null ? null : e.OldItems[0] as SqlAppointment;
				if (app != null && ScheduleViewRepository.Context.SqlAppointments.Contains(app))
				{
					if (app.RecurrenceRule != null)
					{
						foreach (SqlExceptionOccurrence item in app.RecurrenceRule.Exceptions)
						{
							ScheduleViewRepository.Context.SqlExceptionOccurrences.Remove(item);
						}
					}

					foreach (var resource in app.SqlAppointmentResources)
					{
						ScheduleViewRepository.Context.SqlAppointmentResources.Remove(resource);
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

			ScheduleViewRepository.Context.Load(ScheduleViewRepository.Context.GetSqlAppointmentsByRangeQuery(dateSpan.Start, dateSpan.End)).Completed += (o, u) =>
			{
				ScheduleViewRepository.Context.Load(ScheduleViewRepository.Context.GetSqlAppointmentResourcesByRangeQuery(dateSpan.Start, dateSpan.End));

				ScheduleViewRepository.Context.Load(ScheduleViewRepository.Context.GetSqlExceptionOccurrencesByRangeQuery(dateSpan.Start, dateSpan.End)).Completed += (os, us) =>
				{
					ScheduleViewRepository.Context.Load(ScheduleViewRepository.Context.GetSqlExceptionAppointmentsByRangeQuery(dateSpan.Start, dateSpan.End)).Completed += (ea, ua) =>
					{
						ScheduleViewRepository.Context.Load(ScheduleViewRepository.Context.GetSqlExceptionResourcesByRangeQuery(dateSpan.Start, dateSpan.End)).Completed += (s, t) =>
						{
							this.Appointments.AddRange((o as LoadOperation).Entities.Select(a => a as SqlAppointment));
							this.IsLoading = false;
						};
					};
				};
			};
		}

		private void LoadData()
		{
			ScheduleViewRepository.Context.Load(ScheduleViewRepository.Context.GetSqlResourcesQuery()).Completed += (o, e) =>
			{
				ScheduleViewRepository.Context.Load(ScheduleViewRepository.Context.GetSqlResourceTypesQuery()).Completed += (s, a) =>
				{
					this.ResourceTypes.AddRange((s as LoadOperation).Entities);
				};

				this.OnPropertyChanged(() => this.ResourceTypes);
			};

			ScheduleViewRepository.Context.Load(ScheduleViewRepository.Context.GetTimeMarkersQuery()).Completed += (o, e) =>
			{
				this.TimeMarkers.AddRange((o as LoadOperation).Entities);
			};

			ScheduleViewRepository.Context.Load(ScheduleViewRepository.Context.GetCategoriesQuery()).Completed += (o, e) =>
			{
				this.Categories.AddRange((o as LoadOperation).Entities);
			};
		}
	}
}

