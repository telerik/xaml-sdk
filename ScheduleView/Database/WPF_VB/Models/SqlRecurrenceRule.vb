Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Telerik.Windows.Controls.ScheduleView
Imports System.Collections.ObjectModel
Imports Telerik.Windows.Controls.ScheduleView.ICalendar
Imports System.Collections.Specialized
Imports Telerik.Windows.Controls
Imports System.ComponentModel

Public Class SqlRecurrenceRule
	Inherits ViewModelBase
	Implements IRecurrenceRule

	Public Property MasterAppointment() As SqlAppointment
		Get
			Return m_MasterAppointment
		End Get
		Private Set(value As SqlAppointment)
			m_MasterAppointment = value
		End Set
	End Property
	Private m_MasterAppointment As SqlAppointment

	Public Property Exceptions() As ICollection(Of IExceptionOccurrence)
		Get
			Return m_Exceptions
		End Get
		Private Set(value As ICollection(Of IExceptionOccurrence))
			m_Exceptions = value
		End Set
	End Property
	Private m_Exceptions As ICollection(Of IExceptionOccurrence)

	Public Sub New()
		Me.Exceptions = New ObservableCollection(Of IExceptionOccurrence)()
	End Sub

	Public Sub New(appointment As SqlAppointment)
		Me.New()
		Me.MasterAppointment = appointment
	End Sub

	Public Function Copy() As Telerik.Windows.Controls.ScheduleView.IRecurrenceRule Implements Telerik.Windows.Controls.ICopyable(Of Telerik.Windows.Controls.ScheduleView.IRecurrenceRule).Copy
		Dim rule = New SqlRecurrenceRule()
		rule.CopyFrom(Me)
		Return rule
	End Function

	Public Sub CopyFrom(other As Telerik.Windows.Controls.ScheduleView.IRecurrenceRule) Implements Telerik.Windows.Controls.ICopyable(Of Telerik.Windows.Controls.ScheduleView.IRecurrenceRule).CopyFrom
		If Me.[GetType]().FullName <> other.[GetType]().FullName Then
			Throw New ArgumentException("Invalid type")
		End If

		If TypeOf other Is SqlRecurrenceRule Then
			Me.MasterAppointment = TryCast(other, SqlRecurrenceRule).MasterAppointment
		End If

        Me.Pattern = other.Pattern.Copy()
        Me.Exceptions.Clear()

        Dim exceptions = other.Exceptions.ToList()
        other.Exceptions.Clear()

        Me.Exceptions = exceptions
	End Sub

	Public Function CreateNew() As Telerik.Windows.Controls.ScheduleView.IExceptionOccurrence Implements Telerik.Windows.Controls.IObjectGenerator(Of Telerik.Windows.Controls.ScheduleView.IExceptionOccurrence).CreateNew
		Dim excOcc = New SqlExceptionOccurrence()
		excOcc.SqlAppointment = Me.MasterAppointment
		ScheduleViewRepository.Context.AddToSqlExceptionOccurrences(excOcc)
		Return excOcc
	End Function

	Public Function CreateNew(item As Telerik.Windows.Controls.ScheduleView.IExceptionOccurrence) As Telerik.Windows.Controls.ScheduleView.IExceptionOccurrence Implements Telerik.Windows.Controls.IObjectGenerator(Of Telerik.Windows.Controls.ScheduleView.IExceptionOccurrence).CreateNew
		Dim sqlExceptionOccurrence = Me.CreateNew()
		sqlExceptionOccurrence.CopyFrom(item)
		Return sqlExceptionOccurrence
	End Function

	Public Function CreateExceptionAppointment(master As Telerik.Windows.Controls.ScheduleView.IAppointment) As Telerik.Windows.Controls.ScheduleView.IAppointment Implements Telerik.Windows.Controls.ScheduleView.IRecurrenceRule.CreateExceptionAppointment
		Return TryCast(master, SqlAppointment).ShallowCopy()
	End Function

	Private _Pattern As RecurrencePattern
	Public Property Pattern As Telerik.Windows.Controls.ScheduleView.RecurrencePattern Implements Telerik.Windows.Controls.ScheduleView.IRecurrenceRule.Pattern
		Get
			Return _Pattern
		End Get
		Set(value As Telerik.Windows.Controls.ScheduleView.RecurrencePattern)
			If Not Object.Equals(Me._Pattern, value) Then
				Me._Pattern = value
				Me.MasterAppointment.RecurrencePattern = RecurrencePatternHelper.RecurrencePatternToString(Pattern)
				Me.OnPropertyChanged(Function() Me.Pattern)
			End If
		End Set
	End Property

	Public ReadOnly Property Exceptions_ As System.Collections.Generic.ICollection(Of Telerik.Windows.Controls.ScheduleView.IExceptionOccurrence) Implements Telerik.Windows.Controls.ScheduleView.IRecurrenceRule.Exceptions
		Get
			Return Me.Exceptions
		End Get
	End Property
End Class
