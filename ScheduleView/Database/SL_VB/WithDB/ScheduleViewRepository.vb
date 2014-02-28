Imports ScheduleViewDB.Web

Public Class ScheduleViewRepository

	Private Shared m_context As SVDomainContext
	Public Shared ReadOnly Property Context() As SVDomainContext
		Get
			If m_context Is Nothing Then
				m_context = New SVDomainContext()
			End If

			Return m_context
		End Get
	End Property

	Shared Sub New()
	End Sub

	Public Shared Sub SaveData(action As Action)
		If ScheduleViewRepository.Context.HasChanges AndAlso Not ScheduleViewRepository.Context.IsSubmitting Then
			Try
				ScheduleViewRepository.Context.SubmitChanges(AddressOf OnSubmitChangesCompleted, action)
			Catch ex As System.Exception
				Throw
			End Try
		End If
	End Sub

	Private Shared Sub OnSubmitChangesCompleted(ByVal submitOperation As ServiceModel.DomainServices.Client.SubmitOperation)
		If Not submitOperation.HasError Then
			Dim action As Action = TryCast(submitOperation.UserState, Action)
			action()
		End If

	End Sub

End Class
