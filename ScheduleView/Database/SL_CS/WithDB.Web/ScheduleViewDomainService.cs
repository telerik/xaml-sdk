
namespace ScheduleViewDB.Web
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.ComponentModel.DataAnnotations;
	using System.Data;
	using System.Linq;
	using System.ServiceModel.DomainServices.EntityFramework;
	using System.ServiceModel.DomainServices.Hosting;
	using System.ServiceModel.DomainServices.Server;
	using System.Data.Objects;


	// Implements application logic using the ScheduleViewDBEntities context.
	// TODO: Add your application logic to these methods or in additional methods.
	// TODO: Wire up authentication (Windows/ASP.NET Forms) and uncomment the following to disable anonymous access
	// Also consider adding roles to restrict access as appropriate.
	// [RequiresAuthentication]
	[EnableClientAccess()]
	public class ScheduleViewDomainService : LinqToEntitiesDomainService<ScheduleViewDBEntities>
	{

		public IQueryable<Category> GetCategories()
		{
			return this.ObjectContext.Categories;
		}

		public void InsertCategory(Category category)
		{
			if ((category.EntityState != EntityState.Detached))
			{
				this.ObjectContext.ObjectStateManager.ChangeObjectState(category, EntityState.Added);
			}
			else
			{
				this.ObjectContext.Categories.AddObject(category);
			}
		}

		public void UpdateCategory(Category currentCategory)
		{
			this.ObjectContext.Categories.AttachAsModified(currentCategory);
		}

		public void DeleteCategory(Category category)
		{
			if ((category.EntityState != EntityState.Detached))
			{
				this.ObjectContext.ObjectStateManager.ChangeObjectState(category, EntityState.Deleted);
			}
			else
			{
				this.ObjectContext.Categories.Attach(category);
				this.ObjectContext.Categories.DeleteObject(category);
			}
		}

		public IQueryable<SqlAppointment> GetSqlAppointmentsByRange(DateTime start, DateTime end)
		{
			var ids = GetSqlAppointmentsIdsByRange(start, end);

			var result = this.ObjectContext.SqlAppointments.Where(a => ids.Contains(a.SqlAppointmentId)).ToList<SqlAppointment>();

			// Load the recurrent appointments
			foreach (var item in this.ObjectContext.SqlAppointments.Where(a => !string.IsNullOrEmpty(a.RecurrencePattern)))
			{
				if (Helper.IsOccurrenceInRange(item.RecurrencePattern, start, end) && !result.Contains(item))
				{
					result.Add(item);
				}
			}

			// Load the exceptions
			foreach (var item in this.ObjectContext.SqlAppointments.Where(a => a.Start < end && a.SqlExceptionOccurrences.Count != 0))
			{
				if (item.SqlExceptionOccurrences.Any(e => e.SqlExceptionAppointment != null &&
															e.SqlExceptionAppointment.Start >= start &&
															e.SqlExceptionAppointment.End <= end))
				{
					result.Add(item);
				}
			}

			return result.AsQueryable<SqlAppointment>();
		}

		public void InsertSqlAppointment(SqlAppointment sqlAppointment)
		{
			if ((sqlAppointment.EntityState != EntityState.Detached))
			{
				this.ObjectContext.ObjectStateManager.ChangeObjectState(sqlAppointment, EntityState.Added);
			}
			else
			{
				this.ObjectContext.SqlAppointments.AddObject(sqlAppointment);
			}
		}

		public void UpdateSqlAppointment(SqlAppointment currentSqlAppointment)
		{
			this.ObjectContext.SqlAppointments.AttachAsModified(currentSqlAppointment);
		}

		public void DeleteSqlAppointment(SqlAppointment sqlAppointment)
		{
			if ((sqlAppointment.EntityState != EntityState.Detached))
			{
				this.ObjectContext.ObjectStateManager.ChangeObjectState(sqlAppointment, EntityState.Deleted);
			}
			else
			{
				this.ObjectContext.SqlAppointments.Attach(sqlAppointment);
				this.ObjectContext.SqlAppointments.DeleteObject(sqlAppointment);
			}
		}

		public IQueryable<SqlAppointmentResource> GetSqlAppointmentResourcesByRange(DateTime start, DateTime end)
		{
			var ids = GetSqlAppointmentsIdsByRange(start, end);

			return this.ObjectContext.SqlAppointmentResources.Where(x => ids.Contains(x.SqlAppointments_SqlAppointmentId));
		}

		public void InsertSqlAppointmentResource(SqlAppointmentResource sqlAppointmentResource)
		{
			if ((sqlAppointmentResource.EntityState != EntityState.Detached))
			{
				this.ObjectContext.ObjectStateManager.ChangeObjectState(sqlAppointmentResource, EntityState.Added);
			}
			else
			{
				this.ObjectContext.SqlAppointmentResources.AddObject(sqlAppointmentResource);
			}
		}

		public void UpdateSqlAppointmentResource(SqlAppointmentResource currentSqlAppointmentResource)
		{
			this.ObjectContext.SqlAppointmentResources.AttachAsModified(currentSqlAppointmentResource);
		}

		public void DeleteSqlAppointmentResource(SqlAppointmentResource sqlAppointmentResource)
		{
			if ((sqlAppointmentResource.EntityState != EntityState.Detached))
			{
				this.ObjectContext.ObjectStateManager.ChangeObjectState(sqlAppointmentResource, EntityState.Deleted);
			}
			else
			{
				this.ObjectContext.SqlAppointmentResources.Attach(sqlAppointmentResource);
				this.ObjectContext.SqlAppointmentResources.DeleteObject(sqlAppointmentResource);
			}
		}

		public IQueryable<SqlExceptionAppointment> GetSqlExceptionAppointmentsByRange(DateTime start, DateTime end)
		{
			var ids = GetExceptionOccurrenceIdsByRange(start, end);

			return this.ObjectContext.SqlExceptionAppointments.Where(x => ids.Contains(x.ExceptionId));
		}

		public void InsertSqlExceptionAppointment(SqlExceptionAppointment sqlExceptionAppointment)
		{
			if ((sqlExceptionAppointment.EntityState != EntityState.Detached))
			{
				this.ObjectContext.ObjectStateManager.ChangeObjectState(sqlExceptionAppointment, EntityState.Added);
			}
			else
			{
				this.ObjectContext.SqlExceptionAppointments.AddObject(sqlExceptionAppointment);
			}
		}

		public void UpdateSqlExceptionAppointment(SqlExceptionAppointment currentSqlExceptionAppointment)
		{
			this.ObjectContext.SqlExceptionAppointments.AttachAsModified(currentSqlExceptionAppointment);
		}

		public void DeleteSqlExceptionAppointment(SqlExceptionAppointment sqlExceptionAppointment)
		{
			if ((sqlExceptionAppointment.EntityState != EntityState.Detached))
			{
				this.ObjectContext.ObjectStateManager.ChangeObjectState(sqlExceptionAppointment, EntityState.Deleted);
			}
			else
			{
				this.ObjectContext.SqlExceptionAppointments.Attach(sqlExceptionAppointment);
				this.ObjectContext.SqlExceptionAppointments.DeleteObject(sqlExceptionAppointment);
			}
		}

		public IQueryable<SqlExceptionOccurrence> GetSqlExceptionOccurrencesByRange(DateTime start, DateTime end)
		{
			var ids = GetSqlAppointmentsIdsByRange(start, end);

			return this.ObjectContext.SqlExceptionOccurrences.Where(x => ids.Contains(x.MasterSqlAppointmentId));
		}

		public void InsertSqlExceptionOccurrence(SqlExceptionOccurrence sqlExceptionOccurrence)
		{
			if ((sqlExceptionOccurrence.EntityState != EntityState.Detached))
			{
				this.ObjectContext.ObjectStateManager.ChangeObjectState(sqlExceptionOccurrence, EntityState.Added);
			}
			else
			{
				this.ObjectContext.SqlExceptionOccurrences.AddObject(sqlExceptionOccurrence);
			}
		}

		public void UpdateSqlExceptionOccurrence(SqlExceptionOccurrence currentSqlExceptionOccurrence)
		{
			this.ObjectContext.SqlExceptionOccurrences.AttachAsModified(currentSqlExceptionOccurrence);
		}

		public void DeleteSqlExceptionOccurrence(SqlExceptionOccurrence sqlExceptionOccurrence)
		{
			if ((sqlExceptionOccurrence.EntityState != EntityState.Detached))
			{
				this.ObjectContext.ObjectStateManager.ChangeObjectState(sqlExceptionOccurrence, EntityState.Deleted);
			}
			else
			{
				this.ObjectContext.SqlExceptionOccurrences.Attach(sqlExceptionOccurrence);
				this.ObjectContext.SqlExceptionOccurrences.DeleteObject(sqlExceptionOccurrence);
			}
		}

		public IQueryable<SqlExceptionResource> GetSqlExceptionResourcesByRange(DateTime start, DateTime end)
		{
			var ids = GetExceptionOccurrenceIdsByRange(start, end);

			return this.ObjectContext.SqlExceptionResources.Where(x => ids.Contains(x.SqlExceptionAppointments_ExceptionId));
		}

		public void InsertSqlExceptionResource(SqlExceptionResource sqlExceptionResource)
		{
			if ((sqlExceptionResource.EntityState != EntityState.Detached))
			{
				this.ObjectContext.ObjectStateManager.ChangeObjectState(sqlExceptionResource, EntityState.Added);
			}
			else
			{
				this.ObjectContext.SqlExceptionResources.AddObject(sqlExceptionResource);
			}
		}

		public void UpdateSqlExceptionResource(SqlExceptionResource currentSqlExceptionResource)
		{
			this.ObjectContext.SqlExceptionResources.AttachAsModified(currentSqlExceptionResource);
		}

		public void DeleteSqlExceptionResource(SqlExceptionResource sqlExceptionResource)
		{
			if ((sqlExceptionResource.EntityState != EntityState.Detached))
			{
				this.ObjectContext.ObjectStateManager.ChangeObjectState(sqlExceptionResource, EntityState.Deleted);
			}
			else
			{
				this.ObjectContext.SqlExceptionResources.Attach(sqlExceptionResource);
				this.ObjectContext.SqlExceptionResources.DeleteObject(sqlExceptionResource);
			}
		}

		public IQueryable<SqlResource> GetSqlResources()
		{
			return this.ObjectContext.SqlResources;
		}

		public void InsertSqlResource(SqlResource sqlResource)
		{
			if ((sqlResource.EntityState != EntityState.Detached))
			{
				this.ObjectContext.ObjectStateManager.ChangeObjectState(sqlResource, EntityState.Added);
			}
			else
			{
				this.ObjectContext.SqlResources.AddObject(sqlResource);
			}
		}

		public void UpdateSqlResource(SqlResource currentSqlResource)
		{
			this.ObjectContext.SqlResources.AttachAsModified(currentSqlResource);
		}

		public void DeleteSqlResource(SqlResource sqlResource)
		{
			if ((sqlResource.EntityState != EntityState.Detached))
			{
				this.ObjectContext.ObjectStateManager.ChangeObjectState(sqlResource, EntityState.Deleted);
			}
			else
			{
				this.ObjectContext.SqlResources.Attach(sqlResource);
				this.ObjectContext.SqlResources.DeleteObject(sqlResource);
			}
		}

		public IQueryable<SqlResourceType> GetSqlResourceTypes()
		{
			return this.ObjectContext.SqlResourceTypes;
		}

		public void InsertSqlResourceType(SqlResourceType sqlResourceType)
		{
			if ((sqlResourceType.EntityState != EntityState.Detached))
			{
				this.ObjectContext.ObjectStateManager.ChangeObjectState(sqlResourceType, EntityState.Added);
			}
			else
			{
				this.ObjectContext.SqlResourceTypes.AddObject(sqlResourceType);
			}
		}

		public void UpdateSqlResourceType(SqlResourceType currentSqlResourceType)
		{
			this.ObjectContext.SqlResourceTypes.AttachAsModified(currentSqlResourceType);
		}

		public void DeleteSqlResourceType(SqlResourceType sqlResourceType)
		{
			if ((sqlResourceType.EntityState != EntityState.Detached))
			{
				this.ObjectContext.ObjectStateManager.ChangeObjectState(sqlResourceType, EntityState.Deleted);
			}
			else
			{
				this.ObjectContext.SqlResourceTypes.Attach(sqlResourceType);
				this.ObjectContext.SqlResourceTypes.DeleteObject(sqlResourceType);
			}
		}

		public IQueryable<TimeMarker> GetTimeMarkers()
		{
			return this.ObjectContext.TimeMarkers;
		}

		public void InsertTimeMarker(TimeMarker timeMarker)
		{
			if ((timeMarker.EntityState != EntityState.Detached))
			{
				this.ObjectContext.ObjectStateManager.ChangeObjectState(timeMarker, EntityState.Added);
			}
			else
			{
				this.ObjectContext.TimeMarkers.AddObject(timeMarker);
			}
		}

		public void UpdateTimeMarker(TimeMarker currentTimeMarker)
		{
			this.ObjectContext.TimeMarkers.AttachAsModified(currentTimeMarker);
		}

		public void DeleteTimeMarker(TimeMarker timeMarker)
		{
			if ((timeMarker.EntityState != EntityState.Detached))
			{
				this.ObjectContext.ObjectStateManager.ChangeObjectState(timeMarker, EntityState.Deleted);
			}
			else
			{
				this.ObjectContext.TimeMarkers.Attach(timeMarker);
				this.ObjectContext.TimeMarkers.DeleteObject(timeMarker);
			}
		}

		private int[] GetSqlAppointmentsIdsByRange(DateTime start, DateTime end)
		{
            var result = this.ObjectContext.SqlAppointments.Where(a => ((a.Start >= start && a.End <= end) || (a.Start <= start && a.End <= end) || (a.Start >= start && a.End >= end) || (a.Start <= start && a.End >= end))).ToList<SqlAppointment>();

			return result.OfType<SqlAppointment>().Select(e => e.SqlAppointmentId).ToArray();
		}

		private int[] GetExceptionOccurrenceIdsByRange(DateTime start, DateTime end)
		{
			return GetSqlExceptionOccurrencesByRange(start, end).OfType<SqlExceptionOccurrence>().Select(eo => eo.ExceptionId).ToArray();
		}

		protected override bool ResolveConflicts(IEnumerable<System.Data.Objects.ObjectStateEntry> conflicts)
		{
			foreach (ObjectStateEntry conflict in conflicts)
			{
				ObjectContext.Refresh(RefreshMode.ClientWins, conflict.Entity);
			}
			return base.ResolveConflicts(conflicts);
		}
	}
}


