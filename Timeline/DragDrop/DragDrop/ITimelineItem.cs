using System;

namespace DragDrop
{
    /// <summary>
    /// This interface should be implemented by the model used to populate the ItemsSource of the timeline control.
    /// It is used in the drag drop behavior.
    /// </summary>
    public interface ITimelineItem
    {
        DateTime StartDate { get; set; }
        object GroupKey { get; set; }
        int RowIndex { get; set; }
    }
}
