Imports System.Globalization
Imports System.Windows.Media
Imports Telerik.Windows.Controls

Public Class TimeMarker
	Implements ITimeMarker

	Public Function Equals_(other As Telerik.Windows.Controls.ITimeMarker) As Boolean Implements System.IEquatable(Of Telerik.Windows.Controls.ITimeMarker).Equals
		Return Me.TimeMarkerName <> other.TimeMarkerName
	End Function

	Public Property TimeMarkerName_ As String Implements Telerik.Windows.Controls.ITimeMarker.TimeMarkerName
		Get
			Return Me.TimeMarkerName
		End Get
		Set(value As String)
			Me.TimeMarkerName = value
		End Set
	End Property

	Private Function GenerateColorFromString(colorArgb As String) As SolidColorBrush
		Dim colorHex = UInteger.Parse(colorArgb, NumberStyles.HexNumber)
		Dim a As Byte = CByte((&HFF000000UI And colorHex) / &H1000000)
		Dim r As Byte = CByte((&HFF0000 And colorHex) / &H10000)
		Dim g As Byte = CByte((&HFF00 And colorHex) / &H100)
		Dim b As Byte = CByte((&HFF And colorHex) / &H1)
		Return New SolidColorBrush(Color.FromArgb(a, r, g, b))
	End Function
End Class
