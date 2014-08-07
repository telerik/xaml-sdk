Imports Telerik.Windows.Controls.ScheduleView
Imports Telerik.Windows.Controls
Imports System.ComponentModel
Imports System.Collections
Imports Telerik.Windows.Controls.ScheduleView.ICalendar

Public Class SqlAppointment
	Implements IAppointment, IExtendedAppointment, IObjectGenerator(Of IRecurrenceRule)

	Public Event RecurrenceRuleChanged As EventHandler
	Private _ExceptionOccurrences As List(Of SqlExceptionOccurrence)
	Private _ExceptionAppointments As List(Of SqlExceptionAppointment)
	Private _Resources As IList
	Private _TimeZone As TimeZoneInfo
	Private _RecurrenceRule As IRecurrenceRule

	Private Property Category_ As ICategory Implements IExtendedAppointment.Category
		Get
			Return TryCast(Me.Category, ICategory)
		End Get
		Set(value As ICategory)
			Me.Category = TryCast(value, Category)
		End Set
	End Property

	Private Property Importance_ As Importance Implements IExtendedAppointment.Importance
		Get
			Return Me.Importance
		End Get
		Set(value As Importance)
            Me.Importance = value
		End Set
	End Property

	Private Property TimeMarker_ As ITimeMarker Implements IExtendedAppointment.TimeMarker
		Get
			Return TryCast(Me.TimeMarker, ITimeMarker)
		End Get
		Set(value As ITimeMarker)
			Me.TimeMarker = TryCast(value, TimeMarker)
		End Set
	End Property

	Public Property TimeZone As TimeZoneInfo
		Get
			If Me._TimeZone Is Nothing Then
				Return TimeZoneInfo.Local
			End If

			Return Me._TimeZone
		End Get

		Set(value As TimeZoneInfo)
			If Not Object.Equals(Me._TimeZone, value) Then
				Me._TimeZone = value
				Me.OnPropertyChanged("TimeZone")
			End If
		End Set
	End Property

	Public Property RecurrenceRule As Telerik.Windows.Controls.ScheduleView.IRecurrenceRule Implements Telerik.Windows.Controls.ScheduleView.IAppointment.RecurrenceRule
		Get
			If Me._RecurrenceRule Is Nothing AndAlso Me.EntityState = System.Data.EntityState.Unchanged Then
				Me._RecurrenceRule = Me.GetRecurrenceRule(Me.RecurrencePattern)
			End If

			Return Me._RecurrenceRule
		End Get

		Set(value As Telerik.Windows.Controls.ScheduleView.IRecurrenceRule)
			If Not Object.Equals(Me.recurrenceRule, value) Then
				If value Is Nothing Then
					Me.RecurrencePattern = Nothing
				End If
				Me._RecurrenceRule = value
				Me.OnPropertyChanged("RecurrenceRule")
			End If
		End Set
	End Property

	Public ReadOnly Property Resources() As IList Implements IAppointment.Resources
		Get
			If Me._Resources Is Nothing Then
				Me._Resources = Me.SqlAppointmentResources.[Select](Function(ar) ar.SqlResource).ToList()
			End If

			Return Me._Resources
		End Get
	End Property

	Public Property Subject_ As String Implements Telerik.Windows.Controls.ScheduleView.IAppointment.Subject
		Get
			Return Me.Subject
		End Get
		Set(value As String)
			Me.Subject = value
		End Set
	End Property

	Public Property TimeZone_ As System.TimeZoneInfo Implements Telerik.Windows.Controls.ScheduleView.IAppointment.TimeZone
		Get
			Return Me.TimeZone
		End Get
		Set(value As System.TimeZoneInfo)
			Me.TimeZone = value
		End Set
	End Property

	Public Property End_ As Date Implements Telerik.Windows.Controls.ScheduleView.IAppointment.End
		Get
			Return Me.End
		End Get
		Set(value As Date)
			Me.End = value
		End Set
	End Property

	Public Property Start_ As Date Implements Telerik.Windows.Controls.ScheduleView.IAppointment.Start
		Get
			Return Me.Start
		End Get
		Set(value As Date)
			Me.Start = value
		End Set
	End Property

	Public ReadOnly Property End1 As Date Implements Telerik.Windows.Controls.ScheduleView.IDateSpan.End
		Get
			Return Me.End
		End Get
	End Property

	Public ReadOnly Property Start1 As Date Implements Telerik.Windows.Controls.ScheduleView.IDateSpan.Start
		Get
			Return Me.Start
		End Get
	End Property

	Public Property IsAllDayEvent_ As Boolean Implements Telerik.Windows.Controls.ScheduleView.IAppointment.IsAllDayEvent
		Get
			Return Me.IsAllDayEvent
		End Get
		Set(value As Boolean)
			Me.IsAllDayEvent = value
		End Set
	End Property

	Public Function Copy() As IAppointment
		Dim appointment As IAppointment = New SqlAppointment()
		appointment.CopyFrom(Me)
		Return appointment
	End Function

	Private Sub BeginEdit_() Implements System.ComponentModel.IEditableObject.BeginEdit
		If Me._ExceptionOccurrences Is Nothing Then
			Me._ExceptionOccurrences = New List(Of SqlExceptionOccurrence)()
		End If

		If Me._ExceptionAppointments Is Nothing Then
			Me._ExceptionAppointments = New List(Of SqlExceptionAppointment)()
		End If

		Me._ExceptionOccurrences.Clear()
		Me._ExceptionOccurrences.AddRange(Me.SqlExceptionOccurrences.ToList())

		Me._ExceptionAppointments.Clear()
		Me._ExceptionAppointments.AddRange(Me.SqlExceptionOccurrences.[Select](Function(o) o.Appointment).Where(Function(a) a IsNot Nothing).ToList())
	End Sub

	Private Sub CancelEdit_() Implements System.ComponentModel.IEditableObject.CancelEdit
        Dim exceptionOccurenceToRemove As IEnumerable(Of SqlExceptionOccurrence) = Me.SqlExceptionOccurrences.Except(Me._ExceptionOccurrences).ToList
		For Each ex As SqlExceptionOccurrence In exceptionOccurenceToRemove
			ScheduleViewRepository.Context.SqlExceptionOccurrences.DeleteObject(ex)
			If ex.Appointment IsNot Nothing Then
				ScheduleViewRepository.Context.SqlExceptionAppointments.DeleteObject(DirectCast(ex.Appointment, SqlExceptionAppointment))
				For Each resource In TryCast(ex.Appointment, SqlExceptionAppointment).SqlExceptionResources
					ScheduleViewRepository.Context.SqlExceptionResources.DeleteObject(resource)
				Next
			End If
		Next
	End Sub

	Private Sub EndEdit() Implements IEditableObject.EndEdit
		Dim resourceList = Me.SqlAppointmentResources.ToList()
		For Each resource In resourceList
			ScheduleViewRepository.Context.SqlAppointmentResources.DeleteObject(resource)
		Next

		For Each sqlResource In Me.Resources.OfType(Of SqlResource)()
			Me.SqlAppointmentResources.Add(New SqlAppointmentResource() With { _
			 .SqlAppointment = Me, _
			 .SqlResources_SqlResourceId = sqlResource.SqlResourceId _
			})
		Next

		Dim removedExceptionAppointments = Me._ExceptionAppointments.Except(Me.SqlExceptionOccurrences.[Select](Function(o) o.Appointment).OfType(Of SqlExceptionAppointment)())
		For Each exceptionAppointment In removedExceptionAppointments
			For Each item As SqlExceptionResource In exceptionAppointment.SqlExceptionResources
				ScheduleViewRepository.Context.SqlExceptionResources.DeleteObject(item)
			Next
		Next
	End Sub

	Public Function Equals_(other As Telerik.Windows.Controls.ScheduleView.IAppointment) As Boolean Implements System.IEquatable(Of Telerik.Windows.Controls.ScheduleView.IAppointment).Equals
		Dim otherAppointment = TryCast(other, SqlAppointment)
		Return otherAppointment IsNot Nothing AndAlso
		 other.Start = Me.Start AndAlso
		 other.[End] = Me.[End] AndAlso
		 other.Subject = Me.Subject AndAlso
		 Object.Equals(Me.CategoryID, otherAppointment.CategoryID) AndAlso
		 Object.Equals(Me.TimeMarker, otherAppointment.TimeMarker) AndAlso
		 Object.Equals(Me.TimeZone, otherAppointment.TimeZone) AndAlso
		 Me.IsAllDayEvent = other.IsAllDayEvent AndAlso
		 Object.Equals(Me.RecurrenceRule, other.RecurrenceRule)
	End Function

	Public Function CreateNew() As Telerik.Windows.Controls.ScheduleView.IRecurrenceRule Implements Telerik.Windows.Controls.IObjectGenerator(Of Telerik.Windows.Controls.ScheduleView.IRecurrenceRule).CreateNew
		Return Me.CreateDefaultRecurrenceRule()
	End Function

	Public Function CreateNew(item As Telerik.Windows.Controls.ScheduleView.IRecurrenceRule) As Telerik.Windows.Controls.ScheduleView.IRecurrenceRule Implements Telerik.Windows.Controls.IObjectGenerator(Of Telerik.Windows.Controls.ScheduleView.IRecurrenceRule).CreateNew
		Dim sqlRecurrenceRule = Me.CreateNew()
		sqlRecurrenceRule.CopyFrom(item)
		Return sqlRecurrenceRule
	End Function

	Public Function ShallowCopy() As IAppointment
		Dim appointment As IAppointment = New SqlExceptionAppointment()
		appointment.CopyFrom(Me)
		Return appointment
	End Function

	Private Sub CopyFrom(other As IAppointment) Implements ICopyable(Of IAppointment).CopyFrom
		Me.IsAllDayEvent = other.IsAllDayEvent
		Me.Start = other.Start
		Me.[End] = other.[End]
		Me.Subject = other.Subject

		Dim otherAppointment = TryCast(other, SqlAppointment)
		If otherAppointment Is Nothing Then
			Return
		End If

		Me.CategoryID = otherAppointment.CategoryID
		Me.TimeMarker = otherAppointment.TimeMarker
		Me.RecurrenceRule = If(other.RecurrenceRule Is Nothing, Nothing, TryCast(other.RecurrenceRule.Copy(), SqlRecurrenceRule))
		Me.RecurrencePattern = otherAppointment.RecurrencePattern

		Me.Resources.Clear()
		Me.Resources.AddRange(otherAppointment.Resources)

		Me.Body = otherAppointment.Body
	End Sub

	Private Function GetRecurrenceRule(pattern As String) As IRecurrenceRule
		If String.IsNullOrEmpty(pattern) Then
			Return Nothing
		End If

		Dim recurrenceRuleGenerator = TryCast(Me, IObjectGenerator(Of IRecurrenceRule))
		Dim recurrenceRule = If(recurrenceRuleGenerator IsNot Nothing, recurrenceRuleGenerator.CreateNew(), Me.CreateDefaultRecurrenceRule())
		Dim recurrencePattern = New RecurrencePattern()
		RecurrencePatternHelper.TryParseRecurrencePattern(pattern, recurrencePattern)
		recurrenceRule.Pattern = recurrencePattern
		For Each exception As SqlExceptionOccurrence In Me.SqlExceptionOccurrences
			recurrenceRule.Exceptions.Add(exception)
		Next

		Return recurrenceRule
	End Function

	Private Function CreateDefaultRecurrenceRule() As IRecurrenceRule
		Return New SqlRecurrenceRule(Me)
	End Function

	Public Function Copy_() As Telerik.Windows.Controls.ScheduleView.IAppointment Implements Telerik.Windows.Controls.ICopyable(Of Telerik.Windows.Controls.ScheduleView.IAppointment).Copy
		Dim appointment As IAppointment = New SqlAppointment()
		appointment.CopyFrom(Me)
		Return appointment
	End Function

	Public Event RecurrenceRuleChanged_(sender As Object, e As System.EventArgs) Implements Telerik.Windows.Controls.ScheduleView.IAppointment.RecurrenceRuleChanged
End Class
