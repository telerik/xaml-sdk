
Option Compare Binary
Option Infer On
Option Strict On
Option Explicit On

Imports ScheduleView_DB_SL_VB.Web
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.Data
Imports System.Linq
Imports System.ServiceModel.DomainServices.EntityFramework
Imports System.ServiceModel.DomainServices.Hosting
Imports System.ServiceModel.DomainServices.Server


'Implements application logic using the ScheduleViewDBEntities context.
' TODO: Add your application logic to these methods or in additional methods.
' TODO: Wire up authentication (Windows/ASP.NET Forms) and uncomment the following to disable anonymous access
' Also consider adding roles to restrict access as appropriate.
'<RequiresAuthentication> _
<EnableClientAccess()> _
Public Class SVDomainService
	Inherits LinqToEntitiesDomainService(Of ScheduleViewDBEntities)

	'TODO:
	' Consider constraining the results of your query method.  If you need additional input you can
	' add parameters to this method or create additional query methods with different names.
	'To support paging you will need to add ordering to the 'Categories' query.
	Public Function GetCategories() As IQueryable(Of Category)
		Return Me.ObjectContext.Categories
	End Function

	Public Sub InsertCategory(ByVal category As Category)
		If ((category.EntityState = EntityState.Detached) _
					= False) Then
			Me.ObjectContext.ObjectStateManager.ChangeObjectState(category, EntityState.Added)
		Else
			Me.ObjectContext.Categories.AddObject(category)
		End If
	End Sub

	Public Sub UpdateCategory(ByVal currentCategory As Category)
		Me.ObjectContext.Categories.AttachAsModified(currentCategory)
	End Sub

	Public Sub DeleteCategory(ByVal category As Category)
		If ((category.EntityState = EntityState.Detached) _
					= False) Then
			Me.ObjectContext.ObjectStateManager.ChangeObjectState(category, EntityState.Deleted)
		Else
			Me.ObjectContext.Categories.Attach(category)
			Me.ObjectContext.Categories.DeleteObject(category)
		End If
	End Sub

	Public Function GetSqlAppointmentsByRange(start As DateTime, [end] As DateTime) As IQueryable(Of SqlAppointment)
		Dim ids = GetSqlAppointmentsIdsByRange(start, [end])

		Dim result = Me.ObjectContext.SqlAppointments.Where(Function(a) ids.Contains(a.SqlAppointmentId)).ToList()

		' Load the recurrent appointments
		For Each item In Me.ObjectContext.SqlAppointments.Where(Function(a) Not String.IsNullOrEmpty(a.RecurrencePattern))
			If Helper.IsOccurrenceInRange(item.RecurrencePattern, start, [end]) AndAlso Not result.Contains(item) Then
				result.Add(item)
			End If
		Next

		' Load the exceptions
		For Each item In Me.ObjectContext.SqlAppointments.Where(Function(a) a.Start < [end] AndAlso a.SqlExceptionOccurrences.Count <> 0)
			If item.SqlExceptionOccurrences.Any(Function(e) e.SqlExceptionAppointment IsNot Nothing AndAlso e.SqlExceptionAppointment.Start >= start AndAlso e.SqlExceptionAppointment.[End] <= [end]) Then
				result.Add(item)
			End If
		Next

		Return result.AsQueryable()
	End Function

	Public Sub InsertSqlAppointment(ByVal sqlAppointment As SqlAppointment)
		If ((sqlAppointment.EntityState = EntityState.Detached) _
					= False) Then
			Me.ObjectContext.ObjectStateManager.ChangeObjectState(sqlAppointment, EntityState.Added)
		Else
			Me.ObjectContext.SqlAppointments.AddObject(sqlAppointment)
		End If
	End Sub

	Public Sub UpdateSqlAppointment(ByVal currentSqlAppointment As SqlAppointment)
		Me.ObjectContext.SqlAppointments.AttachAsModified(currentSqlAppointment)
	End Sub

	Public Sub DeleteSqlAppointment(ByVal sqlAppointment As SqlAppointment)
		If ((sqlAppointment.EntityState = EntityState.Detached) _
					= False) Then
			Me.ObjectContext.ObjectStateManager.ChangeObjectState(sqlAppointment, EntityState.Deleted)
		Else
			Me.ObjectContext.SqlAppointments.Attach(sqlAppointment)
			Me.ObjectContext.SqlAppointments.DeleteObject(sqlAppointment)
		End If
	End Sub

	Public Function GetSqlAppointmentResourcesByRange(start As DateTime, [end] As DateTime) As IQueryable(Of SqlAppointmentResource)
		Dim ids = GetSqlAppointmentsIdsByRange(start, [end])

		Return Me.ObjectContext.SqlAppointmentResources.Where(Function(x) ids.Contains(x.SqlAppointments_SqlAppointmentId))
	End Function

	Public Sub InsertSqlAppointmentResource(ByVal sqlAppointmentResource As SqlAppointmentResource)
		If ((sqlAppointmentResource.EntityState = EntityState.Detached) _
					= False) Then
			Me.ObjectContext.ObjectStateManager.ChangeObjectState(sqlAppointmentResource, EntityState.Added)
		Else
			Me.ObjectContext.SqlAppointmentResources.AddObject(sqlAppointmentResource)
		End If
	End Sub

	Public Sub UpdateSqlAppointmentResource(ByVal currentSqlAppointmentResource As SqlAppointmentResource)
		Me.ObjectContext.SqlAppointmentResources.AttachAsModified(currentSqlAppointmentResource)
	End Sub

	Public Sub DeleteSqlAppointmentResource(ByVal sqlAppointmentResource As SqlAppointmentResource)
		If ((sqlAppointmentResource.EntityState = EntityState.Detached) _
					= False) Then
			Me.ObjectContext.ObjectStateManager.ChangeObjectState(sqlAppointmentResource, EntityState.Deleted)
		Else
			Me.ObjectContext.SqlAppointmentResources.Attach(sqlAppointmentResource)
			Me.ObjectContext.SqlAppointmentResources.DeleteObject(sqlAppointmentResource)
		End If
	End Sub

	Public Function GetSqlExceptionAppointmentsByRange(start As DateTime, [end] As DateTime) As IQueryable(Of SqlExceptionAppointment)
		Dim ids = GetExceptionOccurrenceIdsByRange(start, [end])

		Return Me.ObjectContext.SqlExceptionAppointments.Where(Function(x) ids.Contains(x.ExceptionId))
	End Function

	Public Sub InsertSqlExceptionAppointment(ByVal sqlExceptionAppointment As SqlExceptionAppointment)
		If ((sqlExceptionAppointment.EntityState = EntityState.Detached) _
					= False) Then
			Me.ObjectContext.ObjectStateManager.ChangeObjectState(sqlExceptionAppointment, EntityState.Added)
		Else
			Me.ObjectContext.SqlExceptionAppointments.AddObject(sqlExceptionAppointment)
		End If
	End Sub

	Public Sub UpdateSqlExceptionAppointment(ByVal currentSqlExceptionAppointment As SqlExceptionAppointment)
		Me.ObjectContext.SqlExceptionAppointments.AttachAsModified(currentSqlExceptionAppointment)
	End Sub

	Public Sub DeleteSqlExceptionAppointment(ByVal sqlExceptionAppointment As SqlExceptionAppointment)
		If ((sqlExceptionAppointment.EntityState = EntityState.Detached) _
					= False) Then
			Me.ObjectContext.ObjectStateManager.ChangeObjectState(sqlExceptionAppointment, EntityState.Deleted)
		Else
			Me.ObjectContext.SqlExceptionAppointments.Attach(sqlExceptionAppointment)
			Me.ObjectContext.SqlExceptionAppointments.DeleteObject(sqlExceptionAppointment)
		End If
	End Sub

	Public Function GetSqlExceptionOccurrencesByRange(start As DateTime, [end] As DateTime) As IQueryable(Of SqlExceptionOccurrence)
		Dim ids = GetSqlAppointmentsIdsByRange(start, [end])

		Return Me.ObjectContext.SqlExceptionOccurrences.Where(Function(x) ids.Contains(x.MasterSqlAppointmentId))
	End Function

	Public Sub InsertSqlExceptionOccurrence(ByVal sqlExceptionOccurrence As SqlExceptionOccurrence)
		If ((sqlExceptionOccurrence.EntityState = EntityState.Detached) _
					= False) Then
			Me.ObjectContext.ObjectStateManager.ChangeObjectState(sqlExceptionOccurrence, EntityState.Added)
		Else
			Me.ObjectContext.SqlExceptionOccurrences.AddObject(sqlExceptionOccurrence)
		End If
	End Sub

	Public Sub UpdateSqlExceptionOccurrence(ByVal currentSqlExceptionOccurrence As SqlExceptionOccurrence)
		Me.ObjectContext.SqlExceptionOccurrences.AttachAsModified(currentSqlExceptionOccurrence)
	End Sub

	Public Sub DeleteSqlExceptionOccurrence(ByVal sqlExceptionOccurrence As SqlExceptionOccurrence)
		If ((sqlExceptionOccurrence.EntityState = EntityState.Detached) _
					= False) Then
			Me.ObjectContext.ObjectStateManager.ChangeObjectState(sqlExceptionOccurrence, EntityState.Deleted)
		Else
			Me.ObjectContext.SqlExceptionOccurrences.Attach(sqlExceptionOccurrence)
			Me.ObjectContext.SqlExceptionOccurrences.DeleteObject(sqlExceptionOccurrence)
		End If
	End Sub

	Public Function GetSqlExceptionResourcesByRange(start As DateTime, [end] As DateTime) As IQueryable(Of SqlExceptionResource)
		Dim ids = GetExceptionOccurrenceIdsByRange(start, [end])

		Return Me.ObjectContext.SqlExceptionResources.Where(Function(x) ids.Contains(x.SqlExceptionAppointments_ExceptionId))
	End Function

	Public Sub InsertSqlExceptionResource(ByVal sqlExceptionResource As SqlExceptionResource)
		If ((sqlExceptionResource.EntityState = EntityState.Detached) _
					= False) Then
			Me.ObjectContext.ObjectStateManager.ChangeObjectState(sqlExceptionResource, EntityState.Added)
		Else
			Me.ObjectContext.SqlExceptionResources.AddObject(sqlExceptionResource)
		End If
	End Sub

	Public Sub UpdateSqlExceptionResource(ByVal currentSqlExceptionResource As SqlExceptionResource)
		Me.ObjectContext.SqlExceptionResources.AttachAsModified(currentSqlExceptionResource)
	End Sub

	Public Sub DeleteSqlExceptionResource(ByVal sqlExceptionResource As SqlExceptionResource)
		If ((sqlExceptionResource.EntityState = EntityState.Detached) _
					= False) Then
			Me.ObjectContext.ObjectStateManager.ChangeObjectState(sqlExceptionResource, EntityState.Deleted)
		Else
			Me.ObjectContext.SqlExceptionResources.Attach(sqlExceptionResource)
			Me.ObjectContext.SqlExceptionResources.DeleteObject(sqlExceptionResource)
		End If
	End Sub

	Public Function GetSqlResources() As IQueryable(Of SqlResource)
		Return Me.ObjectContext.SqlResources
	End Function

	Public Sub InsertSqlResource(ByVal sqlResource As SqlResource)
		If ((sqlResource.EntityState = EntityState.Detached) _
					= False) Then
			Me.ObjectContext.ObjectStateManager.ChangeObjectState(sqlResource, EntityState.Added)
		Else
			Me.ObjectContext.SqlResources.AddObject(sqlResource)
		End If
	End Sub

	Public Sub UpdateSqlResource(ByVal currentSqlResource As SqlResource)
		Me.ObjectContext.SqlResources.AttachAsModified(currentSqlResource)
	End Sub

	Public Sub DeleteSqlResource(ByVal sqlResource As SqlResource)
		If ((sqlResource.EntityState = EntityState.Detached) _
					= False) Then
			Me.ObjectContext.ObjectStateManager.ChangeObjectState(sqlResource, EntityState.Deleted)
		Else
			Me.ObjectContext.SqlResources.Attach(sqlResource)
			Me.ObjectContext.SqlResources.DeleteObject(sqlResource)
		End If
	End Sub

	Public Function GetSqlResourceTypes() As IQueryable(Of SqlResourceType)
		Return Me.ObjectContext.SqlResourceTypes
	End Function

	Public Sub InsertSqlResourceType(ByVal sqlResourceType As SqlResourceType)
		If ((sqlResourceType.EntityState = EntityState.Detached) _
					= False) Then
			Me.ObjectContext.ObjectStateManager.ChangeObjectState(sqlResourceType, EntityState.Added)
		Else
			Me.ObjectContext.SqlResourceTypes.AddObject(sqlResourceType)
		End If
	End Sub

	Public Sub UpdateSqlResourceType(ByVal currentSqlResourceType As SqlResourceType)
		Me.ObjectContext.SqlResourceTypes.AttachAsModified(currentSqlResourceType)
	End Sub

	Public Sub DeleteSqlResourceType(ByVal sqlResourceType As SqlResourceType)
		If ((sqlResourceType.EntityState = EntityState.Detached) _
					= False) Then
			Me.ObjectContext.ObjectStateManager.ChangeObjectState(sqlResourceType, EntityState.Deleted)
		Else
			Me.ObjectContext.SqlResourceTypes.Attach(sqlResourceType)
			Me.ObjectContext.SqlResourceTypes.DeleteObject(sqlResourceType)
		End If
	End Sub

	Public Function GetTimeMarkers() As IQueryable(Of TimeMarker)
		Return Me.ObjectContext.TimeMarkers
	End Function

	Public Sub InsertTimeMarker(ByVal timeMarker As TimeMarker)
		If ((timeMarker.EntityState = EntityState.Detached) _
					= False) Then
			Me.ObjectContext.ObjectStateManager.ChangeObjectState(timeMarker, EntityState.Added)
		Else
			Me.ObjectContext.TimeMarkers.AddObject(timeMarker)
		End If
	End Sub

	Public Sub UpdateTimeMarker(ByVal currentTimeMarker As TimeMarker)
		Me.ObjectContext.TimeMarkers.AttachAsModified(currentTimeMarker)
	End Sub

	Public Sub DeleteTimeMarker(ByVal timeMarker As TimeMarker)
		If ((timeMarker.EntityState = EntityState.Detached) _
					= False) Then
			Me.ObjectContext.ObjectStateManager.ChangeObjectState(timeMarker, EntityState.Deleted)
		Else
			Me.ObjectContext.TimeMarkers.Attach(timeMarker)
			Me.ObjectContext.TimeMarkers.DeleteObject(timeMarker)
		End If
	End Sub

	Private Function GetSqlAppointmentsIdsByRange(start As DateTime, [end] As DateTime) As Integer()
        Dim result = Me.ObjectContext.SqlAppointments.Where(Function(a) ((a.Start >= start AndAlso a.[End] <= [end]) OrElse (a.Start <= start AndAlso a.[End] <= [end]) OrElse (a.Start >= start AndAlso a.[End] >= [end]) OrElse (a.Start <= start AndAlso a.[End] >= [end]))).ToList()
		Return result.OfType(Of SqlAppointment)().[Select](Function(e) e.SqlAppointmentId).ToArray()
	End Function

	Private Function GetExceptionOccurrenceIdsByRange(start As DateTime, [end] As DateTime) As Integer()
		Return GetSqlExceptionOccurrencesByRange(start, [end]).OfType(Of SqlExceptionOccurrence)().[Select](Function(eo) eo.ExceptionId).ToArray()
	End Function

End Class

