Imports System.Windows.Markup
Imports System.Globalization
Imports System.Text
Imports System.IO

Friend NotInheritable Class SolidColorBrushHelper
	Private Sub New()
	End Sub
	Public Shared Function FromNameString(brushName As String) As Brush
		Dim s As String = "<SolidColorBrush " + "xmlns=" + "'http://schemas.microsoft.com/winfx/2006/xaml/presentation'" + " Color='" + brushName + "' />"

		Dim byteArray = Encoding.ASCII.GetBytes(s)

		Dim stream = New MemoryStream(byteArray)
		Try
			Return DirectCast(XamlReader.Load(stream), Brush)
		Catch generatedExceptionName As Exception
			Return FromHexString(brushName)
		End Try
	End Function

	Private Shared Function FromHexString(colorArgb As String) As Brush
		Dim colorHex = UInteger.Parse(colorArgb, NumberStyles.HexNumber)
		Dim a As Byte = CByte((&HFF000000UI And colorHex) / &H1000000)
		Dim r As Byte = CByte((&HFF0000 And colorHex) / &H10000)
		Dim g As Byte = CByte((&HFF00 And colorHex) / &H100)
		Dim b As Byte = CByte((&HFF And colorHex) / &H1)
		Return New SolidColorBrush(Color.FromArgb(a, r, g, b))
	End Function
End Class
