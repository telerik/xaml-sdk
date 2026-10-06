using System;
using System.ComponentModel;
using Telerik.Windows.Controls.ScheduleView;

namespace ScheduleViewDB
{
	public partial class SqlExceptionOccurrence : IExceptionOccurrence
	{
		public IAppointment Appointment
		{
			get
			{
				return this.SqlExceptionAppointment;
			}
			set
			{
				if (this.SqlExceptionAppointment != value)
				{
					if (value == null)
					{
						ScheduleViewRepository.Context.SqlExceptionAppointments.DeleteObject(this.SqlExceptionAppointment);
					}

					this.SqlExceptionAppointment = value as SqlExceptionAppointment;
					this.OnPropertyChanged("Appointment");
				}
			}
		}

		public IExceptionOccurrence Copy()
		{
			var exception = new SqlExceptionOccurrence();
			exception.CopyFrom(this);
			return exception;
		}

		public void CopyFrom(IExceptionOccurrence other)
		{
			if (this.GetType().FullName != other.GetType().FullName)
				throw new ArgumentException("Invalid type");

			this.ExceptionDate = other.ExceptionDate;
			if (other.Appointment != null)
				this.Appointment = other.Appointment.Copy();
		}
	}
}
