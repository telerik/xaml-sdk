using System.Collections.Generic;
using Telerik.Windows.Controls.Timeline;

namespace DragDrop
{
    /// <summary>
    /// The purpose of this generator is only to allow setting the RowIndex of the timeline items via the model.
    /// </summary>
    public class CustomRowIndexGenerator : IItemRowIndexGenerator
    {
        public void GenerateRowIndexes(List<TimelineRowItem> dataItems)
        {
            foreach (TimelineRowItem item in dataItems)
            {
                var dataItem = (ITimelineItem)item.DataItem;
                if (dataItem.RowIndex > -1)
                {
                    item.RowIndex = dataItem.RowIndex;
                }
            }
        }
    }
}
