using System;
using System.Linq;
using ScheduleViewDB.Helpers;

namespace ScheduleViewDB
{
	public class ScheduleViewRepository
	{		
		private static ScheduleViewDBEntities context;
		public static ScheduleViewDBEntities Context
		{
			get
			{
				if (context == null)
				{
					context = new ScheduleViewDBEntities();
				}
				return context;
			}
		}

		public static bool SaveData(Action action)
		{
			var isSubmited = ScheduleViewRepository.Context.SaveChanges() >= 0;
			if (action != null && isSubmited)
			{
				action();
			}
			return isSubmited;
		}
		
		public static IQueryable<SqlAppointment> GetSqlAppointmentsByRange(DateTime start, DateTime end)
		{
			var ids = GetSqlAppointmentsIdsByRange(start, end);

			var result = context.SqlAppointments.Where(a => ids.Contains(a.SqlAppointmentId)).ToList<SqlAppointment>();

			// Load the recurrent appointments
			foreach (var item in context.SqlAppointments.Where(a => !string.IsNullOrEmpty(a.RecurrencePattern)))
			{
				if (RecurrenceHelper.IsOccurrenceInRange(item.RecurrencePattern, start, end) && !result.Contains(item))
				{
					result.Add(item);
				}
			}

			// Load the exceptions
			foreach (var item in context.SqlAppointments.Where(a => a.Start < end && a.SqlExceptionOccurrences.Count != 0))
			{
				if (item.SqlExceptionOccurrences.Any(e => e.SqlExceptionAppointment != null && e.SqlExceptionAppointment.Start >= start && e.SqlExceptionAppointment.End <= end) && !result.Contains(item))
				{
					result.Add(item);
				}
			}

			return result.AsQueryable<SqlAppointment>();
		}

		private static int[] GetSqlAppointmentsIdsByRange(DateTime start, DateTime end)
		{
            var result = context.SqlAppointments.Where(a => ((a.Start >= start && a.End <= end) || (a.Start <= start && a.End <= end) || (a.Start >= start && a.End >= end) || (a.Start <= start && a.End >= end))).ToList<SqlAppointment>();

			return result.OfType<SqlAppointment>().Select(e => e.SqlAppointmentId).ToArray();
		}
	}
}
