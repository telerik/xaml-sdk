using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls.GanttView;

namespace ExpandCollapseViaHeader
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

        private void OnCheckBoxChecked(object sender, RoutedEventArgs e)
        {
            var tasks = (this.GanttView.TasksSource as IList<GanttTask>);

            foreach (var task in tasks)
            {
                this.ExpandTask(task);
            }
        }

        private void OnCheckBoxUnchecked(object sender, RoutedEventArgs e)
        {
            var tasks = (this.GanttView.TasksSource as IList<GanttTask>);

            foreach (var task in tasks)
            {
                this.CollapseTask(task);
            }
        }

        private void ExpandTask(GanttTask task)
        {
            this.GanttView.ExpandCollapseService.ExpandItem(task);
            var childTasks = task.Children;
            if (childTasks != null)
            {
                foreach (GanttTask childTask in childTasks)
                {
                    this.ExpandTask(childTask);
                }
            }
        }

        private void CollapseTask(GanttTask task)
        {
            var childTasks = task.Children;
            if (childTasks != null)
            {
                foreach (GanttTask childTask in childTasks)
                {
                    this.CollapseTask(childTask);
                }
            }

            this.GanttView.ExpandCollapseService.CollapseItem(task);
        }
    }
}
