using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace SummaryTaskConsistency
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

        private void ChangeRendering_Click(object sender, RoutedEventArgs e)
        {
            var interationTask = (this.GnattView.TasksSource as ObservableCollection<MyGanttTask>)[0];
            var ganttAPI = interationTask.Children[0] as MyGanttTask;
            if (ganttAPI.Start.Date != DateTime.Now.AddDays(-2.5).Date)
            {
                ganttAPI.End = DateTime.Now.AddDays(8);
                ganttAPI.Start = DateTime.Now.AddDays(-2.5);
            }
            else
            {
                ganttAPI.End = DateTime.Now.AddDays(2);
                ganttAPI.Start = DateTime.Now;
            }
        }
    }
}
