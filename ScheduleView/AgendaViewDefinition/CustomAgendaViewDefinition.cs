using System;
using Telerik.Windows.Controls;

namespace AgendaViewDefinition
{
    public class CustomAgendaViewDefinition : DayViewDefinition
    {
        protected override string FormatVisibleRangeText(IFormatProvider formatInfo, DateTime rangeStart, DateTime rangeEnd, DateTime currentDate)
        {
            return string.Empty;
        }
    }
}
