using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Windows.Controls.ScheduleView;
using System.Collections.ObjectModel;
using Telerik.Windows.Controls.ScheduleView.ICalendar;
using System.Collections.Specialized;
using Telerik.Windows.Controls;

namespace ScheduleViewDB
{
	public class SqlRecurrenceRule : ViewModelBase, IRecurrenceRule
	{
		private RecurrencePattern pattern;

		public SqlRecurrenceRule()
		{
			this.Exceptions = new ObservableCollection<IExceptionOccurrence>();
		}

		public SqlRecurrenceRule(SqlAppointment appointment)
			: this()
		{
			this.MasterAppointment = appointment;
		}

		public SqlAppointment MasterAppointment { get; private set; }

		public RecurrencePattern Pattern
		{
			get
			{
				return pattern;
			}
			set
			{
				if (this.pattern != value)
				{
					this.pattern = value;
					this.MasterAppointment.RecurrencePattern = RecurrencePatternHelper.RecurrencePatternToString(pattern);
					this.OnPropertyChanged("Pattern");
				}
			}
		}

		public ICollection<IExceptionOccurrence> Exceptions
		{
			get;
			private set;
		}

		public IRecurrenceRule Copy()
		{
			var rule = new SqlRecurrenceRule();
			rule.CopyFrom(this);
			return rule;
		}

		public IExceptionOccurrence CreateNew()
		{
			var excOcc = new SqlExceptionOccurrence();
			excOcc.SqlAppointment = this.MasterAppointment;
			ScheduleViewRepository.Context.AddToSqlExceptionOccurrences(excOcc);
			return excOcc;
		}

		public IExceptionOccurrence CreateNew(IExceptionOccurrence item)
		{
			var sqlExceptionOccurrence = this.CreateNew();
			sqlExceptionOccurrence.CopyFrom(item);
			return sqlExceptionOccurrence;
		}

		public void CopyFrom(IRecurrenceRule other)
		{
			if (this.GetType().FullName != other.GetType().FullName)
			{
				throw new ArgumentException("Invalid type");
			}

			if (other is SqlRecurrenceRule)
			{
				this.MasterAppointment = (other as SqlRecurrenceRule).MasterAppointment;
			}

            this.Pattern = other.Pattern.Copy();
            this.Exceptions.Clear();

            var exceptions = other.Exceptions.ToList();
            other.Exceptions.Clear();

            this.Exceptions = exceptions;
		}

		public IAppointment CreateExceptionAppointment(IAppointment master)
		{
			return (master as SqlAppointment).ShallowCopy();
		}
	}
}
