using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ScheduleView;

namespace ScheduleViewDB
{
	public partial class SqlExceptionAppointment : IEditableObject, IAppointment, IExtendedAppointment, IObjectGenerator<IRecurrenceRule>
	{
		public event EventHandler RecurrenceRuleChanged;

		private TimeZoneInfo timeZone;
		private IRecurrenceRule recurrenceRule;
		private IList resource;

		public TimeZoneInfo TimeZone
		{
			get
			{
				if (this.timeZone == null)
				{
					return TimeZoneInfo.Local;
				}

				return this.timeZone;
			}

			set
			{
				if (this.timeZone != value)
				{
					this.timeZone = value;
					this.OnPropertyChanged("TimeZone");
				}
			}
		}

		public IRecurrenceRule RecurrenceRule
		{
			get
			{
				return this.recurrenceRule;
			}

			set
			{
				if (this.recurrenceRule != value)
				{
					this.recurrenceRule = value;
					this.OnPropertyChanged("RecurrenceRule");
				}
			}
		}

		public IList Resources
		{
			get
			{
				if (this.resource == null)
				{
					this.resource = new ObservableCollection<SqlResource>();
					var resources = ScheduleViewRepository.Context.SqlExceptionResources.Where(x => x.SqlExceptionAppointments_ExceptionId == this.ExceptionId).Select(x => x.SqlResource);
					this.resource.AddRange(resources);

					((INotifyCollectionChanged)this.resource).CollectionChanged += resource_CollectionChanged;
				}
				return this.resource;
			}
		}

		ITimeMarker IExtendedAppointment.TimeMarker
		{
			get
			{
				return this.TimeMarker as ITimeMarker;
			}
			set
			{
				this.TimeMarker = value as TimeMarker;
			}
		}

		ICategory IExtendedAppointment.Category
		{
			get
			{
				return this.Category as ICategory;
			}
			set
			{
				this.Category = value as Category;
			}
		}

		Importance IExtendedAppointment.Importance
		{
			get
			{
				return (Importance)this.Importance;
			}
			set
			{
				this.Importance = (int)value;
			}
		}

		public IAppointment Copy()
		{
			throw new InvalidOperationException();
		}

		public void BeginEdit()
		{			
		}

		public void CancelEdit()
		{
		}

		public void EndEdit()
		{
		}

		public bool Equals(IAppointment other)
		{
			var otherAppointment = other as SqlExceptionAppointment;
			return otherAppointment != null &&
				other.Start == this.Start &&
				other.End == this.End &&
				other.Subject == this.Subject &&
				this.CategoryID == otherAppointment.CategoryID &&
				this.TimeMarker == otherAppointment.TimeMarker &&
				this.TimeZone == otherAppointment.TimeZone &&
				this.IsAllDayEvent == other.IsAllDayEvent &&
				this.RecurrenceRule == other.RecurrenceRule;
		}

		public IRecurrenceRule CreateNew()
		{
			throw new InvalidOperationException();
		}

		public IRecurrenceRule CreateNew(IRecurrenceRule item)
		{
			throw new InvalidOperationException();
		}

		public void CopyFrom(IAppointment other)
		{
			this.IsAllDayEvent = other.IsAllDayEvent;
			this.Start = other.Start;
			this.End = other.End;
			this.Subject = other.Subject;

			var otherAppointment = other as SqlAppointment;
			if (otherAppointment == null)
				return;

			this.CategoryID = otherAppointment.CategoryID;
			this.TimeMarker = otherAppointment.TimeMarker;

			this.Resources.Clear();
			this.Resources.AddRange(otherAppointment.Resources);

			this.Body = otherAppointment.Body;
		}

		void resource_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{

			switch (e.Action)
			{
				case NotifyCollectionChangedAction.Add:
					foreach (var resource in e.NewItems.OfType<SqlResource>())
					{
						ScheduleViewRepository.Context.AddToSqlExceptionResources(
						new SqlExceptionResource
						{
							SqlExceptionAppointments_ExceptionId = this.ExceptionId,
							SqlResources_SqlResourceId = resource.SqlResourceId
						});
					}
					break;
				case NotifyCollectionChangedAction.Remove:
					foreach (var sqlres in e.OldItems)
					{
						var itemsToRemove = ScheduleViewRepository.Context.SqlExceptionResources.Where(x => x.SqlResources_SqlResourceId == ((SqlResource)sqlres).SqlResourceId && x.SqlExceptionAppointments_ExceptionId == this.ExceptionId).ToList();
						foreach (var item in itemsToRemove)
						{
							ScheduleViewRepository.Context.SqlExceptionResources.DeleteObject(item);
						}
					}
					break;
				case NotifyCollectionChangedAction.Replace:
					break;
				case NotifyCollectionChangedAction.Reset:
					break;
				default:
					break;
			}
		}
	}
}
