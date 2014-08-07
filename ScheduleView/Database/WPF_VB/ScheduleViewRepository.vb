Public Class ScheduleViewRepository
	Private Shared m_context As ScheduleViewDBEntities
	Public Shared ReadOnly Property Context() As ScheduleViewDBEntities
		Get
			If m_context Is Nothing Then
				m_context = New ScheduleViewDBEntities()
			End If

			Return m_context
		End Get
	End Property

	Shared Sub New()
	End Sub

	Public Shared Function SaveData(action As Action) As Boolean
        Dim _IsSubmited = ScheduleViewRepository.Context.SaveChanges() >= 0
		If Not action Is Nothing AndAlso _IsSubmited Then
			action()
		End If
		Return _IsSubmited
	End Function

	Public Shared Function GetSqlAppointmentsByRange(start As DateTime, [end] As DateTime) As IQueryable(Of SqlAppointment)
		Dim ids = GetSqlAppointmentsIdsByRange(start, [end])

		Dim result = Context.SqlAppointments.Where(Function(a) ids.Contains(a.SqlAppointmentId)).ToList()

		' Load the recurrent appointments
		For Each item In Context.SqlAppointments.Where(Function(a) Not String.IsNullOrEmpty(a.RecurrencePattern))
			If RecurrenceHelper.IsOccurrenceInRange(item.RecurrencePattern, start, [end]) AndAlso Not result.Contains(item) Then
				result.Add(item)
			End If
		Next

		' Load the exceptions
		For Each item In Context.SqlAppointments.Where(Function(a) a.Start < [end] AndAlso a.SqlExceptionOccurrences.Count <> 0)
			If item.SqlExceptionOccurrences.Any(Function(e) e.SqlExceptionAppointment IsNot Nothing AndAlso e.SqlExceptionAppointment.Start >= start AndAlso e.SqlExceptionAppointment.[End] <= [end]) AndAlso Not result.Contains(item) Then
				result.Add(item)
			End If
		Next

		Return result.AsQueryable()
	End Function

	Private Shared Function GetSqlAppointmentsIdsByRange(start As DateTime, [end] As DateTime) As Integer()
		Dim result = Context.SqlAppointments.Where(Function(a) (a.Start >= start AndAlso a.[End] <= [end])).ToList()

		Return result.OfType(Of SqlAppointment)().[Select](Function(e) e.SqlAppointmentId).ToArray()
	End Function
End Class
