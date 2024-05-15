using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls.GanttView;

namespace ZeroTaskLevelInitiallyCollapsed
{
    /// <summary>
    /// Interaction logic for Example.xaml
    /// </summary>
    public partial class Example : UserControl
    {
        public Example()
        {
            InitializeComponent();
        }

        private void ExpandButton_Click(object sender, RoutedEventArgs e)
        {
            var firstTask = (this.GanttView.TasksSource as IList<GanttTask>)[0];
            this.GanttView.ExpandCollapseService.ExpandItem(firstTask);
        }

        private void CollapseButton_Click(object sender, RoutedEventArgs e)
        {
            var firstTask = (this.GanttView.TasksSource as IList<GanttTask>)[0];
            this.GanttView.ExpandCollapseService.CollapseItem(firstTask);
        }
    }
}
