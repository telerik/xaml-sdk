using Microsoft.Office.Interop.MSProject;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls.GanttView;

namespace ImportExportFromAndToMSProject
{
    /// <summary>
    /// Interaction logic for Example.xaml
    /// </summary>
    public partial class Example : UserControl
    {
        private ViewModel viewModel;

        public Example()
        {
            InitializeComponent();

            this.viewModel = new ViewModel();
            this.DataContext = this.viewModel;
        }

        private void ImportFromXmlButtonClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".xml";
            dlg.Multiselect = false;
            dlg.Filter = "XML (*.xml)|*.xml";
            dlg.InitialDirectory = Path.GetFullPath(@"..\..\XMLFilesToLoad");

            if (dlg.ShowDialog() == true)
            {
                viewModel.ImportFromFile(dlg.OpenFile());
            }
        }

        private void ExportToMSProjectButtonClick(object sender, RoutedEventArgs e)
        {
            var msApplication = new Microsoft.Office.Interop.MSProject.Application();
            msApplication.AppMaximize();
            msApplication.FileNew(Missing.Value, Missing.Value, Missing.Value, Missing.Value);

            var msProject = msApplication.ActiveProject;
            msProject.ManuallyScheduledTasksAutoRespectLinks = false;
            FillProjectWithTasks(msProject, this.radGanttView1.TasksSource.OfType<IGanttTask>(), null, false);
            FillTasksWithDependencies(msProject.Tasks.OfType<Task>(), this.radGanttView1.TasksSource.OfType<IGanttTask>());
            msApplication.Visible = true;
        }

        /// <summary>
        /// A method that adds the Tasks from RadGanttView to MS Project using a recursion.
        /// </summary>
        /// <param name="project">The MS Project.</param>
        /// <param name="tasks">A collection of IGanttTasks.</param>
        /// <param name="parentTask">The MSProject task which children will be populated from the IGanttTask.</param>
        /// <param name="isFirstTaskAfterAddedSummary">A property indicating whether the first child of a summary Task will be added.</param>
        private void FillProjectWithTasks(Project project, IEnumerable<IGanttTask> tasks, Task parentTask, bool isFirstTaskAfterAddedSummary)
        {
            foreach (GanttTask ganttTask in tasks)
            {
                Task task = null;
                Cell activeCell = null;
                try
                {
                    var taskName = this.ReplaceInvalidCharacters(ganttTask.Title);

                    if (ganttTask.IsSummary)
                    {
                        project.Application.InsertSummaryTask();
                        activeCell = project.Application.ActiveCell;
                        task = activeCell.Task;
                        task.Name = taskName;
                    }
                    else
                    {
                        if (isFirstTaskAfterAddedSummary)
                        {
                            activeCell = project.Application.ActiveCell;
                            if (!(bool)activeCell.Task.IsDurationValid)
                            {
                                task = activeCell.Task;
                                task.Name = taskName;
                                isFirstTaskAfterAddedSummary = false;
                            }
                        }
                        else
                        {
                            task = project.Tasks.Add(Name = taskName, Type.Missing);
                        }

                        project.Application.SelectCellDown(Type.Missing, Type.Missing);

                        OutlineTaskInProject(parentTask, task);
                    }

                    task.Start = ganttTask.Start;
                    task.Finish = ganttTask.End;
                    task.Deadline = ganttTask.Deadline ?? task.Deadline;
                    task.Milestone = ganttTask.IsMilestone;

                    if (ganttTask.IsSummary)
                    {
                        OutlineTaskInProject(parentTask, task);
                    }

                    if (ganttTask.Children.Count > 0)
                    {
                        project.Application.SelectCellDown(Type.Missing, Type.Missing);
                        this.FillProjectWithTasks(project, ganttTask.Children, task, true);
                        isFirstTaskAfterAddedSummary = false;
                    }
                }

                finally
                {
                    if (activeCell != null) Marshal.ReleaseComObject(activeCell);
                    if (task != null) Marshal.ReleaseComObject(task);
                }
            }
        }

        /// <summary>
        /// A method that adds the Dependencies from RadGanttView to MS Project using a recursion 
        /// The method gets the Dependencies from an IGanttTask and adds them as LinkPredecessors collection of MS Project Task.
        /// </summary>
        /// <param name="tasks">A collection of MS Project Tasks.</param>
        /// <param name="ganttTasks">A collection of IGanttTasks.</param>
        private void FillTasksWithDependencies(IEnumerable<Task> tasks, IEnumerable<IGanttTask> ganttTasks)
        {
            foreach (GanttTask currentGanttTask in ganttTasks)
            {
                if (currentGanttTask.Dependencies.Count > 0)
                {
                    var msProjectTask = tasks.Where(a => a.Name.Equals(this.ReplaceInvalidCharacters(currentGanttTask.Title))).FirstOrDefault();
                    if (msProjectTask != null)
                    {
                        foreach (var dependentTask in currentGanttTask.Dependencies)
                        {
                            var dependentTaskInMsProject = tasks.Where(a => a.Name.Equals(this.ReplaceInvalidCharacters(dependentTask.FromTask.Title))).FirstOrDefault();

                            if (dependentTaskInMsProject != null)
                            {
                                PjTaskLinkType taskPjType = default(PjTaskLinkType);
                                switch (dependentTask.Type)
                                {
                                    case DependencyType.FinishFinish:
                                        taskPjType = PjTaskLinkType.pjFinishToFinish;
                                        break;
                                    case DependencyType.FinishStart:
                                        taskPjType = PjTaskLinkType.pjFinishToStart;
                                        break;
                                    case DependencyType.StartFinish:
                                        taskPjType = PjTaskLinkType.pjStartToFinish;
                                        break;
                                    case DependencyType.StartStart:
                                        taskPjType = PjTaskLinkType.pjStartToStart;
                                        break;
                                }

                                msProjectTask.LinkPredecessors(dependentTaskInMsProject, taskPjType, Type.Missing);
                            }
                        }
                    }
                }

                if (currentGanttTask.Children.Count > 0)
                {
                    this.FillTasksWithDependencies(tasks, currentGanttTask.Children);
                }
            }
        }

        private string ReplaceInvalidCharacters(string initialTitle)
        {
            return new Regex("[-,/():\".;\'`’ ]").Replace(initialTitle, "_"); ;
        }

        /// <summary>
        /// A method that sets the correct Outline for the MS Project Tasks.
        /// </summary>
        /// <param name="parentTask">The MSProject parent Task.</param>
        /// <param name="task">The MSProject Task to be outlined.</param>
        private void OutlineTaskInProject(Task parentTask, Task task)
        {
            if (parentTask != null && task.OutlineLevel != 1 && (parentTask.OutlineLevel + 1) != task.OutlineLevel)
            {
                while ((parentTask.OutlineLevel + 1) != task.OutlineLevel)
                {
                    task.OutlineLevel--;
                }
            }
            else
            {
                if (parentTask == null && task.OutlineLevel != 1)
                {
                    task.OutlineLevel = 1;
                }
            }
        }
    }
}
