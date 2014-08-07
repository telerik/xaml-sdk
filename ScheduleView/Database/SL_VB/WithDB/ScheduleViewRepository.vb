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

    Public Shared Function SaveData(ByVal action As Action) As Boolean
        If ScheduleViewRepository.Context.HasChanges AndAlso (Not ScheduleViewRepository.Context.IsSubmitting) Then
            Try
                ScheduleViewRepository.Context.SubmitChanges(AddressOf OnSubmitChangesCompleted, action)
                Return True
            Catch e1 As System.Exception
                Return False
            End Try
        End If

        If action IsNot Nothing Then
            action()
        End If

        Return False
    End Function

    Private Shared Sub OnSubmitChangesCompleted(ByVal submitOperation As ServiceModel.DomainServices.Client.SubmitOperation)
        If Not submitOperation.HasError Then
            Dim action As Action = TryCast(submitOperation.UserState, Action)
            If action IsNot Nothing Then
                action()
            End If
        End If
    End Sub

End Class
