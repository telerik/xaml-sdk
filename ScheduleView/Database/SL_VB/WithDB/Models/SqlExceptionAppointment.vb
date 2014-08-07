Imports System
Imports System.Collections
Imports System.Collections.ObjectModel
Imports System.Collections.Specialized
Imports System.ComponentModel
Imports System.Linq
Imports Telerik.Windows.Controls
Imports Telerik.Windows.Controls.ScheduleView

Namespace Web

	Public Class SqlExceptionAppointment
		Implements IEditableObject
		Implements IAppointment
		Implements IExtendedAppointment
		Implements IObjectGenerator(Of IRecurrenceRule)

		Private _TimeZone As TimeZoneInfo
		Private _RecurrenceRule As IRecurrenceRule
		Private _Resource As IList

		Public Sub BeginEdit() Implements IEditableObject.BeginEdit
			MyBase.BeginEdit()
		End Sub

		Public Sub CancelEdit() Implements IEditableObject.CancelEdit
			MyBase.CancelEdit()
		End Sub

		Public Sub EndEdit() Implements IEditableObject.EndEdit
			MyBase.EndEdit()
		End Sub

		Public Property Category_ As Telerik.Windows.Controls.ICategory Implements Telerik.Windows.Controls.ScheduleView.IExtendedAppointment.Category
			Get
				Return Me.Category
			End Get
			Set(value As Telerik.Windows.Controls.ICategory)
				Me.Category = value
			End Set
		End Property

		Public Property Importance_ As Telerik.Windows.Controls.ScheduleView.Importance Implements Telerik.Windows.Controls.ScheduleView.IExtendedAppointment.Importance
			Get
				Return Me.Importance
			End Get
			Set(value As Telerik.Windows.Controls.ScheduleView.Importance)
				Me.Importance = value
			End Set
		End Property

		Public Property TimeMarker_ As Telerik.Windows.Controls.ITimeMarker Implements Telerik.Windows.Controls.ScheduleView.IExtendedAppointment.TimeMarker
			Get
				Return TryCast(Me.TimeMarker, ITimeMarker)
			End Get
			Set(value As Telerik.Windows.Controls.ITimeMarker)
				Me.TimeMarker = TryCast(value, TimeMarker)
			End Set
		End Property

		Public Function Equals_(other As Telerik.Windows.Controls.ScheduleView.IAppointment) As Boolean Implements System.IEquatable(Of Telerik.Windows.Controls.ScheduleView.IAppointment).Equals
			Dim otherAppointment = TryCast(other, SqlExceptionAppointment)
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

		Public Function Copy() As Telerik.Windows.Controls.ScheduleView.IAppointment Implements Telerik.Windows.Controls.ICopyable(Of Telerik.Windows.Controls.ScheduleView.IAppointment).Copy
            Dim appointment As IAppointment = New SqlExceptionAppointment()
            appointment.CopyFrom(Me)
            Return appointment
		End Function

		Public Sub CopyFrom(other As Telerik.Windows.Controls.ScheduleView.IAppointment) Implements Telerik.Windows.Controls.ICopyable(Of Telerik.Windows.Controls.ScheduleView.IAppointment).CopyFrom
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

			Me.Resources.Clear()
			Me.Resources.AddRange(otherAppointment.Resources)

			Me.Body = otherAppointment.Body
		End Sub

		Public Property End_ As Date Implements Telerik.Windows.Controls.ScheduleView.IAppointment.End
			Get
				Return Me.End
			End Get
			Set(value As Date)
				Me.End = value
			End Set
		End Property

		Public Property IsAllDayEvent_ As Boolean Implements Telerik.Windows.Controls.ScheduleView.IAppointment.IsAllDayEvent
			Get
				Return Me.IsAllDayEvent
			End Get
			Set(value As Boolean)
				Me.IsAllDayEvent = value
			End Set
		End Property

		Public Property RecurrenceRule As Telerik.Windows.Controls.ScheduleView.IRecurrenceRule Implements Telerik.Windows.Controls.ScheduleView.IAppointment.RecurrenceRule
			Get
				Return Me._RecurrenceRule
			End Get
			Set(value As Telerik.Windows.Controls.ScheduleView.IRecurrenceRule)
				If Not Object.Equals(Me._RecurrenceRule, value) Then
					Me._RecurrenceRule = value
					Me.OnPropertyChanged(New PropertyChangedEventArgs("RecurrenceRule"))
				End If
			End Set
		End Property

		Public Event RecurrenceRuleChanged(sender As Object, e As System.EventArgs) Implements Telerik.Windows.Controls.ScheduleView.IAppointment.RecurrenceRuleChanged

		Public ReadOnly Property Resources As System.Collections.IList Implements Telerik.Windows.Controls.ScheduleView.IAppointment.Resources
			Get
				If Me._Resource Is Nothing Then
					Me._Resource = New ObservableCollection(Of SqlResource)()
					Dim _Resources = ScheduleViewRepository.Context.SqlExceptionResources.Where(Function(x) x.SqlExceptionAppointments_ExceptionId = Me.ExceptionId).[Select](Function(x) x.SqlResource)
					Me._Resource.AddRange(_Resources)

					AddHandler TryCast(Me._Resource, INotifyCollectionChanged).CollectionChanged, AddressOf resource_CollectionChanged
				End If
				Return Me._Resource
			End Get
		End Property

		Public Property Start_ As Date Implements Telerik.Windows.Controls.ScheduleView.IAppointment.Start
			Get
				Return Me.Start
			End Get
			Set(value As Date)
				Me.Start = value
			End Set
		End Property

		Public Property Subject_ As String Implements Telerik.Windows.Controls.ScheduleView.IAppointment.Subject
			Get
				Return Me.Subject
			End Get
			Set(value As String)
				Me.Subject = value
			End Set
		End Property

		Public Property TimeZone As System.TimeZoneInfo Implements Telerik.Windows.Controls.ScheduleView.IAppointment.TimeZone
			Get
				If Me._TimeZone Is Nothing Then
					Return TimeZoneInfo.Local
				End If

				Return Me._TimeZone
			End Get
			Set(value As System.TimeZoneInfo)
				If Not Object.Equals(Me._TimeZone, value) Then
					Me._TimeZone = value
					Me.OnPropertyChanged(New PropertyChangedEventArgs("TimeZone"))
				End If
			End Set
		End Property

		Public ReadOnly Property End__ As Date Implements Telerik.Windows.Controls.ScheduleView.IDateSpan.End
			Get
				Return Me.End
			End Get
		End Property

		Public ReadOnly Property Start__ As Date Implements Telerik.Windows.Controls.ScheduleView.IDateSpan.Start
			Get
				Return Me.Start
			End Get
		End Property

		Public Function CreateNew() As Telerik.Windows.Controls.ScheduleView.IRecurrenceRule Implements Telerik.Windows.Controls.IObjectGenerator(Of Telerik.Windows.Controls.ScheduleView.IRecurrenceRule).CreateNew
			Throw New InvalidOperationException()
		End Function

		Public Function CreateNew(item As Telerik.Windows.Controls.ScheduleView.IRecurrenceRule) As Telerik.Windows.Controls.ScheduleView.IRecurrenceRule Implements Telerik.Windows.Controls.IObjectGenerator(Of Telerik.Windows.Controls.ScheduleView.IRecurrenceRule).CreateNew
			Throw New InvalidOperationException()
		End Function

		Sub resource_CollectionChanged(ByVal s As System.Object, ByVal e As NotifyCollectionChangedEventArgs)
			Select Case e.Action
				Case NotifyCollectionChangedAction.Add
					For Each resource In e.NewItems.OfType(Of SqlResource)()
						Me.SqlExceptionResources.Add(New SqlExceptionResource() With { _
						 .SqlExceptionAppointments_ExceptionId = Me.ExceptionId, _
						 .SqlResources_SqlResourceId = resource.SqlResourceId _
						})
					Next
					Exit Select
				Case NotifyCollectionChangedAction.Remove
                    For Each sqlres In e.OldItems.OfType(Of SqlResource)()
                        Dim itemsToRemove = ScheduleViewRepository.Context.SqlExceptionResources.Where(Function(x) x.SqlResources_SqlResourceId = sqlres.SqlResourceId AndAlso x.SqlExceptionAppointments_ExceptionId = Me.ExceptionId).ToList()
                        For Each item In itemsToRemove
                            ScheduleViewRepository.Context.SqlExceptionResources.Remove(item)
                        Next item
                    Next sqlres

                    Exit Select
				Case NotifyCollectionChangedAction.Replace
					Exit Select
				Case NotifyCollectionChangedAction.Reset
					Exit Select
				Case Else
					Exit Select
			End Select
		End Sub
	End Class

End Namespace

