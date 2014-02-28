Imports Telerik.Windows.Controls
Imports System.Globalization

Namespace Web

	Public Class Category
		Implements ICategory

		Private m_categoryBrush As Brush
		Public Property CategoryBrush() As Brush
			Get
				If Me.m_categoryBrush Is Nothing Then
					Me.m_categoryBrush = SolidColorBrushHelper.FromNameString(Me.CategoryBrushName)
				End If

				Return Me.m_categoryBrush
			End Get
			Set(value As Brush)
				Me.CategoryBrushName = TryCast(Me.m_categoryBrush, SolidColorBrush).Color.ToString().Substring(1)
				Me.m_categoryBrush = value
			End Set
		End Property

		Public Overloads Function Equals(other As Telerik.Windows.Controls.ICategory) As Boolean Implements System.IEquatable(Of Telerik.Windows.Controls.ICategory).Equals
			Return Me.DisplayName = other.DisplayName & Me.CategoryName = other.CategoryName
		End Function


		Private Property CategoryName_ As String Implements Telerik.Windows.Controls.ICategory.CategoryName
			Get
				Return Me.CategoryName
			End Get
			Set(value As String)
				Me.CategoryName = value
			End Set
		End Property

		Private Property DisplayName_ As String Implements Telerik.Windows.Controls.ICategory.DisplayName
			Get
				Return Me.DisplayName
			End Get
			Set(value As String)
				Me.DisplayName = value
			End Set
		End Property
	End Class

End Namespace
