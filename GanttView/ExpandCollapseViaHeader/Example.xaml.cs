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

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var tasks = (this.GanttView.TasksSource as IList<GanttTask>);

            foreach (var task in tasks)
            {
                this.GanttView.ExpandCollapseService.ExpandItem(task);
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var tasks = (this.GanttView.TasksSource as IList<GanttTask>);

            foreach (var task in tasks)
            {

                this.GanttView.ExpandCollapseService.CollapseItem(task);
            }
        }
    }
}
