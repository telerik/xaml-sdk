Imports System
Imports System.ComponentModel
Imports Telerik.Windows.Controls.ScheduleView

Public Class SqlExceptionOccurrence
	Implements IExceptionOccurrence

	Public Function Copy() As Telerik.Windows.Controls.ScheduleView.IExceptionOccurrence Implements Telerik.Windows.Controls.ICopyable(Of Telerik.Windows.Controls.ScheduleView.IExceptionOccurrence).Copy
		Dim exception = New SqlExceptionOccurrence()
		exception.CopyFrom(Me)
		Return exception
	End Function

	Public Sub CopyFrom(other As Telerik.Windows.Controls.ScheduleView.IExceptionOccurrence) Implements Telerik.Windows.Controls.ICopyable(Of Telerik.Windows.Controls.ScheduleView.IExceptionOccurrence).CopyFrom
		If Me.[GetType]().FullName <> other.[GetType]().FullName Then
			Throw New ArgumentException("Invalid type")
		End If

		Me.ExceptionDate = other.ExceptionDate
		If other.Appointment IsNot Nothing Then
			Me.Appointment = other.Appointment.Copy()
		End If
	End Sub

	Public Property Appointment As Telerik.Windows.Controls.ScheduleView.IAppointment Implements Telerik.Windows.Controls.ScheduleView.IExceptionOccurrence.Appointment
		Get
			Return Me.SqlExceptionAppointment
		End Get
		Set(value As Telerik.Windows.Controls.ScheduleView.IAppointment)
			If Not Object.Equals(Me.SqlExceptionAppointment, value) Then
				If value Is Nothing Then
					ScheduleViewRepository.Context.SqlExceptionAppointments.DeleteObject(Me.SqlExceptionAppointment)
				End If

				Me.SqlExceptionAppointment = TryCast(value, SqlExceptionAppointment)
				Me.OnPropertyChanged("Appointment")
			End If
		End Set
	End Property

	Public Property ExceptionDate_ As Date Implements Telerik.Windows.Controls.ScheduleView.IExceptionOccurrence.ExceptionDate
		Get
			Return Me.ExceptionDate
		End Get
		Set(value As Date)
			Me.ExceptionDate = value
		End Set
	End Property
End Class
