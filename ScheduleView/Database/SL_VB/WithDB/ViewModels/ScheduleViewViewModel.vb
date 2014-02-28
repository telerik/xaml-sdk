Imports Telerik.Windows.Controls
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports Telerik.Windows.Controls.ScheduleView
Imports System.Collections.Specialized
Imports System.ServiceModel.DomainServices.Client
Imports ScheduleViewDB.Web


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
		Me.TimeMarkers = New ObservableCollection(Of Web.TimeMarker)()
		Me.Categories = New ObservableCollection(Of Web.Category)()
		AddHandler Me.Appointments.CollectionChanged, AddressOf OnAppointmentsCollectionChanged
		If Not DesignerProperties.IsInDesignTool Then
			Me.LoadData()
		End If

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

	Public ReadOnly Property FirstDayOfWeek() As DayOfWeek
		Get
			Return DateTime.Today.DayOfWeek
		End Get
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

	Public Property TimeMarkers() As ObservableCollection(Of Web.TimeMarker)
		Get
			Return m_TimeMarkers
		End Get
		Private Set(value As ObservableCollection(Of Web.TimeMarker))
			m_TimeMarkers = value
		End Set
	End Property
	Private m_TimeMarkers As ObservableCollection(Of Web.TimeMarker)

	Public Property Categories() As ObservableCollection(Of Web.Category)
		Get
			Return m_Categories
		End Get
		Private Set(value As ObservableCollection(Of Web.Category))
			m_Categories = value
		End Set
	End Property
	Private m_Categories As ObservableCollection(Of Web.Category)

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
		ScheduleViewRepository.SaveData(Nothing)
	End Sub

	Private Sub VisibleRangeExecuted(param As Object)
		If Not DesignerProperties.IsInDesignTool Then
			If Not _IsLoading Then
				Me.GenerateAppointments(TryCast(param, DateSpan))
			End If
		End If
	End Sub

	Private Sub OnAppointmentsCollectionChanged(sender As Object, e As NotifyCollectionChangedEventArgs)
		If e.Action = NotifyCollectionChangedAction.Add Then
			Dim app = If(e.NewItems Is Nothing, Nothing, TryCast(e.NewItems(0), SqlAppointment))
			If app IsNot Nothing AndAlso app.EntityState <> EntityState.Unmodified Then
				ScheduleViewRepository.Context.SqlAppointments.Add(app)
			End If
		ElseIf e.Action = NotifyCollectionChangedAction.Remove Then
			Dim app = If(e.OldItems Is Nothing, Nothing, TryCast(e.OldItems(0), SqlAppointment))
			If app IsNot Nothing AndAlso ScheduleViewRepository.Context.SqlAppointments.Contains(app) Then
				If app.RecurrenceRule IsNot Nothing Then
					For Each item As SqlExceptionOccurrence In app.RecurrenceRule.Exceptions
						ScheduleViewRepository.Context.SqlExceptionOccurrences.Remove(item)
					Next
				End If
				For Each resource In app.SqlAppointmentResources
					ScheduleViewRepository.Context.SqlAppointmentResources.Remove(resource)
				Next
				ScheduleViewRepository.Context.SqlAppointments.Remove(app)
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
	Private exceptionOccurenceIds As Integer()
	Private args As LoadOperation
	Private context As SVDomainContext = ScheduleViewRepository.Context
	Private _DateSpan As DateSpan

	Private Sub LoadAppointments(dateSpan As DateSpan)

		Me._DateSpan = dateSpan
		Me.Appointments.Clear()

		Me.IsLoading = True

		AddHandler context.Load(context.GetSqlAppointmentsByRangeQuery(dateSpan.Start, dateSpan.[End])).Completed, AddressOf CompletedHandler
	End Sub

	Private Sub CompletedHandler(ByVal s As System.Object, ByVal e As System.EventArgs)
		args = TryCast(s, LoadOperation)
		context.Load(context.GetSqlAppointmentResourcesByRangeQuery(_DateSpan.Start, _DateSpan.[End]))
		AddHandler context.Load(context.GetSqlExceptionOccurrencesByRangeQuery(_DateSpan.Start, _DateSpan.[End])).Completed, AddressOf ExceptionCompletedHandler
	End Sub

	Private Sub ExceptionCompletedHandler(ByVal s As System.Object, ByVal e As System.EventArgs)
		AddHandler context.Load(context.GetSqlExceptionAppointmentsByRangeQuery(_DateSpan.Start, _DateSpan.[End])).Completed, AddressOf ExceptionByOccurrenceCompletedHandler
	End Sub

	Private Sub ExceptionByOccurrenceCompletedHandler(ByVal s As System.Object, ByVal e As System.EventArgs)
		AddHandler context.Load(context.GetSqlExceptionResourcesByRangeQuery(_DateSpan.Start, _DateSpan.[End])).Completed, AddressOf ExceptionResourceCompletedHandler
	End Sub

	Private Sub ExceptionResourceCompletedHandler(ByVal s As System.Object, ByVal e As System.EventArgs)
		Me.Appointments.AddRange(args.Entities.Select(Function(a) TryCast(a, SqlAppointment)))
		Me.IsLoading = False
	End Sub

	Private Sub LoadData()
		AddHandler context.Load(context.GetSqlResourcesQuery()).Completed, AddressOf SqlResourceCompetedHandler
		AddHandler context.Load(context.GetTimeMarkersQuery()).Completed, AddressOf TimeMarkerCompletedHandler
		AddHandler context.Load(context.GetCategoriesQuery()).Completed, AddressOf CategoryCompletedHandler
	End Sub

	Private Sub SqlResourceCompetedHandler(sender As Object, e As EventArgs)
		AddHandler context.Load(context.GetSqlResourceTypesQuery()).Completed, AddressOf SqlResourceTypesCompletedHandler
		Me.OnPropertyChanged(Function() Me.ResourceTypes)
	End Sub

	Private Sub SqlResourceTypesCompletedHandler(sender As Object, e As EventArgs)
		Me.ResourceTypes.AddRange(TryCast(sender, LoadOperation).Entities)
	End Sub

	Private Sub TimeMarkerCompletedHandler(sender As Object, e As EventArgs)
		Me.TimeMarkers.AddRange(TryCast(sender, LoadOperation).Entities)
	End Sub

	Private Sub CategoryCompletedHandler(sender As Object, e As EventArgs)
		Me.Categories.AddRange(TryCast(sender, LoadOperation).Entities)
	End Sub
End Class

