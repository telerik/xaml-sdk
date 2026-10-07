using System.Windows;

namespace DragDrop
{
    /// <summary>
    /// This class serves as a container for information about the timeline groups.
    /// It is used by the drag drop behavior to store the bounds and row count of the group.
    /// </summary>
    public class GroupInfo
    {
        public int RowsCount;
        public object Key;
        public Rect Bounds;
    }
}
