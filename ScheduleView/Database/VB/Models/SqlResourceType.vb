Imports System.Linq
Imports Telerik.Windows.Controls

Public Class SqlResourceType
	Implements IResourceType

	Public Property AllowMultipleSelection_ As Boolean Implements Telerik.Windows.Controls.IResourceType.AllowMultipleSelection
		Get
			Return Me.AllowMultipleSelection
		End Get
		Set(value As Boolean)
			Me.AllowMultipleSelection = value
		End Set
	End Property

	Public Property DisplayName_ As String Implements Telerik.Windows.Controls.IResourceType.DisplayName
		Get
			Return Me.DisplayName
		End Get
		Set(value As String)
			Me.DisplayName = value
		End Set
	End Property

	Public Property Name_ As String Implements Telerik.Windows.Controls.IResourceType.Name
		Get
			Return Me.Name
		End Get
		Set(value As String)
			Me.Name = value
		End Set
	End Property

	Public ReadOnly Property Resources As System.Collections.IList Implements Telerik.Windows.Controls.IResourceType.Resources
		Get
			Return Me.SqlResources.ToList()
		End Get
	End Property
End Class
