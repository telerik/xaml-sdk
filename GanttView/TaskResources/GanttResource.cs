using System.Windows.Media;

namespace TaskResources
{
    public class GanttResource
    {
        public GanttResource(string name, Color color)
        {
            this.Name = name;
            this.Color = new SolidColorBrush(color);
        }

        public string Name { get; set; }
        public SolidColorBrush Color { get; set; }
    }
}
