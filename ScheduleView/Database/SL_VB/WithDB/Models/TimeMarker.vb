Imports System.Globalization
Imports System.Windows.Media
Imports Telerik.Windows.Controls

Namespace Web
	Public Class TimeMarker
		Implements ITimeMarker

		Public Function Equals_(other As Telerik.Windows.Controls.ITimeMarker) As Boolean Implements System.IEquatable(Of Telerik.Windows.Controls.ITimeMarker).Equals
			Return Me.TimeMarkerName <> other.TimeMarkerName
		End Function

        Private m_timeMarkerBrush As Brush

        Public Property TimeMarkerBrush() As Brush
            Get
                If Me.m_timeMarkerBrush Is Nothing Then
                    Me.m_timeMarkerBrush = SolidColorBrushHelper.FromNameString(Me.TimeMarkerBrushName)
                End If

                Return Me.m_timeMarkerBrush
            End Get
            Set(value As Brush)
                Me.TimeMarkerBrushName = TryCast(Me.m_timeMarkerBrush, SolidColorBrush).Color.ToString().Substring(1)
                Me.m_timeMarkerBrush = value
            End Set
        End Property

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
End Namespace
