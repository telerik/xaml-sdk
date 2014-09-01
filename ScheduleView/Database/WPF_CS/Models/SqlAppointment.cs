using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ScheduleView;
using Telerik.Windows.Controls.ScheduleView.ICalendar;

namespace ScheduleViewDB
{
	public partial class SqlAppointment : IAppointment, IExtendedAppointment, IObjectGenerator<IRecurrenceRule>
	{
		public event EventHandler RecurrenceRuleChanged;
		private List<SqlExceptionOccurrence> exceptionOccurrences;
		private List<SqlExceptionAppointment> exceptionAppointments;
		private IList resources;
		private TimeZoneInfo timeZone;
		private IRecurrenceRule recurrenceRule;

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

		public IList Resources
		{
			get
			{
				if (this.resources == null)
				{
					this.resources = this.SqlAppointmentResources.Select(ar => ar.SqlResource).ToList();
				}

				return this.resources;
			}
		}

		public IRecurrenceRule RecurrenceRule
		{
			get
			{
				if (this.recurrenceRule == null && this.EntityState == EntityState.Unchanged)
				{
					this.recurrenceRule = this.GetRecurrenceRule(this.RecurrencePattern);
				}

				return this.recurrenceRule;
			}

			set
			{
				if (this.recurrenceRule != value)
				{
					if (value == null)
					{
						this.RecurrencePattern = null;
					}
					this.recurrenceRule = value;
					this.OnPropertyChanged("RecurrenceRule");
				}
			}
		}

		public IAppointment Copy()
		{
			IAppointment appointment = new SqlAppointment();
			appointment.CopyFrom(this);
			return appointment;
		}

		void IEditableObject.BeginEdit()
		{
			if (this.exceptionOccurrences == null)
			{
				this.exceptionOccurrences = new List<SqlExceptionOccurrence>();
			}

			if (this.exceptionAppointments == null)
			{
				this.exceptionAppointments = new List<SqlExceptionAppointment>();
			}

			this.exceptionOccurrences.Clear();
			this.exceptionOccurrences.AddRange(this.SqlExceptionOccurrences.ToList());

			this.exceptionAppointments.Clear();
			this.exceptionAppointments.AddRange(this.SqlExceptionOccurrences.Select(o => o.Appointment).Where(a => a != null).ToList());
		}

		void IEditableObject.CancelEdit()
		{
			var exceptionOccurencesToRemove = this.SqlExceptionOccurrences.Except(this.exceptionOccurrences).ToList();
			foreach (var ex in exceptionOccurencesToRemove)
			{
				ScheduleViewRepository.Context.SqlExceptionOccurrences.DeleteObject(ex);
				if (ex.Appointment != null)
				{
					ScheduleViewRepository.Context.SqlExceptionAppointments.DeleteObject((SqlExceptionAppointment)ex.Appointment);
					foreach (var resource in (ex.Appointment as SqlExceptionAppointment).SqlExceptionResources)
					{
						ScheduleViewRepository.Context.SqlExceptionResources.DeleteObject(resource);
					}
				}
			}
		}

		void IEditableObject.EndEdit()
		{
			var temp = this.SqlAppointmentResources.ToList();
            var resources = this.Resources.OfType<SqlResource>().ToList();

			foreach (var item in temp)
			{
				ScheduleViewRepository.Context.SqlAppointmentResources.DeleteObject(item);
			}

            foreach (var sqlResource in resources)
			{
				ScheduleViewRepository.Context.AddToSqlAppointmentResources(new SqlAppointmentResource { SqlAppointment = this, SqlResources_SqlResourceId = sqlResource.SqlResourceId });
			}

			var removedExceptionAppointments = this.exceptionAppointments.Except(this.SqlExceptionOccurrences.Select(o => o.Appointment).OfType<SqlExceptionAppointment>());
			foreach (var exceptionAppointment in removedExceptionAppointments)
			{
				var excResources = exceptionAppointment.SqlExceptionResources.ToList();
				foreach (var item in excResources)
				{
					ScheduleViewRepository.Context.SqlExceptionResources.DeleteObject(item);
				}
			}
		}

		public bool Equals(IAppointment other)
		{
			var otherAppointment = other as SqlAppointment;
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
			return this.CreateDefaultRecurrenceRule();
		}

		public IRecurrenceRule CreateNew(IRecurrenceRule item)
		{
			var sqlRecurrenceRule = this.CreateNew();
			sqlRecurrenceRule.CopyFrom(item);
			return sqlRecurrenceRule;
		}

		public IAppointment ShallowCopy()
		{
			var appointment = new SqlExceptionAppointment();
			appointment.CopyFrom(this);
			return appointment;
		}

		void ICopyable<IAppointment>.CopyFrom(IAppointment other)
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
			this.RecurrenceRule = other.RecurrenceRule == null ? null : other.RecurrenceRule.Copy() as SqlRecurrenceRule;
			this.RecurrencePattern = otherAppointment.RecurrencePattern;

			this.Resources.Clear();
			this.Resources.AddRange(otherAppointment.Resources);

			this.Body = otherAppointment.Body;
		}

		private IRecurrenceRule GetRecurrenceRule(string pattern)
		{
			if (string.IsNullOrEmpty(pattern))
			{
				return null;
			}

			var recurrenceRuleGenerator = this as IObjectGenerator<IRecurrenceRule>;
			var recurrenceRule = recurrenceRuleGenerator != null ? recurrenceRuleGenerator.CreateNew() : this.CreateDefaultRecurrenceRule();
			var recurrencePattern = new RecurrencePattern();
			RecurrencePatternHelper.TryParseRecurrencePattern(pattern, out recurrencePattern);
			recurrenceRule.Pattern = recurrencePattern;
			foreach (SqlExceptionOccurrence exception in this.SqlExceptionOccurrences)
			{
				recurrenceRule.Exceptions.Add(exception);
			}

			return recurrenceRule;
		}

		private IRecurrenceRule CreateDefaultRecurrenceRule()
		{
			return new SqlRecurrenceRule(this);
		}
	}
}
