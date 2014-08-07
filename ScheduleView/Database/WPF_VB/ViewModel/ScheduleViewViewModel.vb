Imports Telerik.Windows.Controls
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports Telerik.Windows.Controls.ScheduleView
Imports System.Collections.Specialized
Imports System.Data

Public Class ScheduleViewViewModel
	Inherits ViewModelBase

	Private _IsLoading As Boolean
	Private _IsInitialLoad As Boolean

	Public Sub New()
		Me._IsInitialLoad = True
		Me.VisibleRangeChanged = New DelegateCommand(AddressOf Me.VisibleRangeExecuted, Function(param) True)
		Me.SaveCommand = New DelegateCommand(AddressOf Me.OnSaveExecuted, Function(param) True)
		Me.ResourceTypes = New ObservableCollection(Of SqlResourceType)()
		Me.Appointments = New ObservableCollection(Of SqlAppointment)()
		Me.TimeMarkers = New ObservableCollection(Of TimeMarker)()
		Me.Categories = New ObservableCollection(Of Category)()
		AddHandler Me.Appointments.CollectionChanged, AddressOf OnAppointmentsCollectionChanged

		Me.LoadData()

	End Sub

	Public Property IsLoading As Boolean
		Get
			Return Me._IsLoading
		End Get

		Set(value As Boolean)
			If Me._IsLoading <> value Then
				Me._IsLoading = value
				Me.OnPropertyChanged(Function() Me.IsLoading)
			End If
		End Set
	End Property

	Public Property Appointments() As ObservableCollection(Of SqlAppointment)
		Get
			Return m_Appointments
		End Get
		Private Set(value As ObservableCollection(Of SqlAppointment))
			m_Appointments = value
		End Set
	End Property
	Private m_Appointments As ObservableCollection(Of SqlAppointment)

	Public Property ResourceTypes() As ObservableCollection(Of SqlResourceType)
		Get
			Return m_ResourceTypes
		End Get
		Private Set(value As ObservableCollection(Of SqlResourceType))
			m_ResourceTypes = value
		End Set
	End Property
	Private m_ResourceTypes As ObservableCollection(Of SqlResourceType)

	Public Property TimeMarkers() As ObservableCollection(Of TimeMarker)
		Get
			Return m_TimeMarkers
		End Get
		Private Set(value As ObservableCollection(Of TimeMarker))
			m_TimeMarkers = value
		End Set
	End Property
	Private m_TimeMarkers As ObservableCollection(Of TimeMarker)

	Public Property Categories() As ObservableCollection(Of Category)
		Get
			Return m_Categories
		End Get
		Private Set(value As ObservableCollection(Of Category))
			m_Categories = value
		End Set
	End Property
	Private m_Categories As ObservableCollection(Of Category)

	Public Property VisibleRangeChanged() As ICommand
		Get
			Return m_VisibleRangeChanged
		End Get
		Private Set(value As ICommand)
			m_VisibleRangeChanged = value
		End Set
	End Property
	Private m_VisibleRangeChanged As ICommand

	Public Property SaveCommand As ICommand
		Get
			Return m_SaveCommand
		End Get
		Private Set(value As ICommand)
			m_SaveCommand = value
		End Set
	End Property
	Private m_SaveCommand As ICommand

	Private Sub OnSaveExecuted(param As Object)
		Dim saved = ScheduleViewRepository.SaveData(Nothing)
	End Sub

	Private Sub VisibleRangeExecuted(param As Object)
		If Not _IsLoading Then
			Me.GenerateAppointments(TryCast(param, DateSpan))
		End If
	End Sub

	Private Sub OnAppointmentsCollectionChanged(sender As Object, e As NotifyCollectionChangedEventArgs)
		If e.Action = NotifyCollectionChangedAction.Add Then
			Dim app As SqlAppointment = Nothing
			If Not e.NewItems Is Nothing Then
				app = TryCast(e.NewItems(0), SqlAppointment)
			End If

			If app IsNot Nothing Then
				If app.EntityState = EntityState.Added Or app.EntityState = EntityState.Detached Then
					ScheduleViewRepository.Context.AddToSqlAppointments(app)
				End If
			End If
		ElseIf e.Action = NotifyCollectionChangedAction.Remove Then
			Dim app = If(e.OldItems Is Nothing, Nothing, TryCast(e.OldItems(0), SqlAppointment))
			If app IsNot Nothing AndAlso ScheduleViewRepository.Context.SqlAppointments.Any(Function(a) a.SqlAppointmentId = app.SqlAppointmentId) Then
				If app.RecurrenceRule IsNot Nothing Then
					Dim tempList = app.RecurrenceRule.Exceptions.ToList()

					For Each item As SqlExceptionOccurrence In tempList
						ScheduleViewRepository.Context.SqlExceptionOccurrences.DeleteObject(item)
					Next
				End If

                Dim tempAppList = ScheduleViewRepository.Context.SqlAppointmentResources.Where(Function(i) i.SqlAppointments_SqlAppointmentId = app.SqlAppointmentId).ToList()

                For Each resource In tempAppList
                    ScheduleViewRepository.Context.SqlAppointmentResources.DeleteObject(resource)
                Next
                ScheduleViewRepository.Context.SqlAppointments.DeleteObject(app)
            End If
		End If
	End Sub

	Private Sub GenerateAppointments(dateSpan As DateSpan)
		If Not Me._IsInitialLoad Then
			ScheduleViewRepository.SaveData(Sub()
												Me.LoadAppointments(dateSpan)
											End Sub)
		Else
			LoadAppointments(dateSpan)
			Me._IsInitialLoad = False
		End If
	End Sub

	Private Sub LoadAppointments(dateSpan As DateSpan)
		Me.Appointments.Clear()
		Me.IsLoading = True
		Me.Appointments.AddRange(ScheduleViewRepository.GetSqlAppointmentsByRange(dateSpan.Start, dateSpan.[End]))
		Me.IsLoading = False
	End Sub

    Private Sub LoadData()
        For Each resType As SqlResourceType In ScheduleViewRepository.Context.SqlResourceTypes
            Dim a = TryCast(resType.Resources, IEnumerable(Of SqlResource))
        Next
        Me.ResourceTypes.AddRange(ScheduleViewRepository.Context.SqlResourceTypes)
        Me.TimeMarkers.AddRange(ScheduleViewRepository.Context.TimeMarkers)
        Me.Categories.AddRange(ScheduleViewRepository.Context.Categories)
    End Sub
End Class
