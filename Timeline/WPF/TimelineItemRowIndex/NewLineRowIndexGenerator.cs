using System.Collections.Generic;
using Telerik.Windows.Controls.Timeline;

namespace TimelineItemRowIndex
{
    public class NewLineRowIndexGenerator : IItemRowIndexGenerator
    {
        public void GenerateRowIndexes(List<TimelineRowItem> dataItems)
        {
            foreach (TimelineRowItem item in dataItems)
            {
                item.RowIndex = (item.DataItem as TimelineData).RowIndex;
            }
        }
    }
}
