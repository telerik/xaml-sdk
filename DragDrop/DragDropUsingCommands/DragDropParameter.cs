using System.Collections;

namespace DragDropUsingCommands
{
    public class DragDropParameter
    {
        public object DraggedItem { get; set; }

        public IEnumerable ItemsSource { get; set; }
    }
}
