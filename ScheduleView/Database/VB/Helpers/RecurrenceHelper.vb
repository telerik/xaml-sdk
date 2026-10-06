Imports Telerik.Windows.Controls.ScheduleView
Imports Telerik.Windows.Controls.ScheduleView.ICalendar

Public Class RecurrenceHelper
	Public Shared Function IsOccurrenceInRange(valueToParse As String, start As DateTime, [end] As DateTime) As Boolean
		Dim pattern As New RecurrencePattern()
		If RecurrencePatternHelper.TryParseRecurrencePattern(valueToParse, pattern) Then
			Return pattern.GetOccurrences(start, start, [end]).Count() > 0
		End If

		Return False
	End Function
End Class
