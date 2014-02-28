Imports System.ComponentModel
Imports Telerik.Windows.Controls

Namespace Web

	Public Class SqlResource
		Implements IResource

		Public Function Equals_(other As Telerik.Windows.Controls.IResource) As Boolean Implements System.IEquatable(Of Telerik.Windows.Controls.IResource).Equals
			Return other IsNot Nothing AndAlso other.ResourceName = Me.ResourceName AndAlso other.ResourceType = Me.ResourceType
		End Function

		Public Property DisplayName_ As String Implements Telerik.Windows.Controls.IResource.DisplayName
			Get
				Return Me.DisplayName
			End Get
			Set(value As String)
				Me.DisplayName = value
			End Set
		End Property

		Public Property ResourceName_ As String Implements Telerik.Windows.Controls.IResource.ResourceName
			Get
				Return Me.ResourceName
			End Get
			Set(value As String)
				Me.ResourceName = value
			End Set
		End Property

		Public Property ResourceType As String Implements Telerik.Windows.Controls.IResource.ResourceType
			Get
				Return Me.SqlResourceType.Name
			End Get
			Set(value As String)
				If Not Object.Equals(Me.SqlResourceType.Name, value) Then
					Me.SqlResourceType.Name = value
					Me.OnPropertyChanged(New PropertyChangedEventArgs("ResourceType"))
				End If
			End Set
		End Property

	End Class

End Namespace

