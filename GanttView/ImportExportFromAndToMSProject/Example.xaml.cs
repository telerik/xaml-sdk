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
        private dynamic msApplication;

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
            MessageBox.Show("MSProject will start exporting RadGanttView soon. While the process in running we do not recommend interacting with MS Project because the export might break.", "Start Exporting", MessageBoxButton.OK, MessageBoxImage.Information);

            try
            {
                this.msApplication = Activator.CreateInstance(Type.GetTypeFromProgID("MSProject.Application", true));
            }
            catch (COMException)
            {
                MessageBox.Show("You need to have MSProject installed on your computer in order to use the export functionality of the example.", "No Installation");
                return;
            }

            System.Windows.Application.Current.MainWindow.WindowState = WindowState.Minimized;

            try
            {
                this.msApplication.AppMaximize();
                this.msApplication.FileNew(Missing.Value, Missing.Value, Missing.Value, Missing.Value);

                var msProject = this.msApplication.ActiveProject;
                msProject.ManuallyScheduledTasksAutoRespectLinks = false;
                FillProjectWithTasks(msProject, this.radGanttView1.TasksSource.OfType<IGanttTask>(), null, false);
                this.FillTasksWithDependencies(((System.Collections.IEnumerable)msProject.Tasks).Cast<dynamic>(), this.radGanttView1.TasksSource.OfType<IGanttTask>());
                this.msApplication.Visible = true;
            }
            catch (COMException)
            {
                try
                {
                    this.msApplication.Quit((int)ProjectSaveType.DoNotSave);
                }
                catch (COMException)
                {
                    return;
                }
                System.Windows.Application.Current.MainWindow.WindowState = WindowState.Normal;
                MessageBox.Show("Export has failed. Please, do not interact with MSProject while Exporting is performed.", "Interaction exception");
            }
        }

        /// <summary>
        /// A method that adds the Tasks from RadGanttView to MS Project using a recursion.
        /// </summary>
        /// <param name="project">The MS Project.</param>
        /// <param name="tasks">A collection of IGanttTasks.</param>
        /// <param name="parentTask">The MSProject task which children will be populated from the IGanttTask.</param>
        /// <param name="isFirstTaskAfterAddedSummary">A property indicating whether the first child of a summary Task will be added.</param>
        private void FillProjectWithTasks(dynamic project, IEnumerable<IGanttTask> tasks, dynamic parentTask, bool isFirstTaskAfterAddedSummary)
        {
            foreach (GanttTask ganttTask in tasks)
            {
                dynamic task = null;
                dynamic activeCell = null;
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

                        if (task == null)
                        {
                            this.msApplication.Quit((int)ProjectSaveType.DoNotSave);
                            System.Windows.Application.Current.MainWindow.WindowState = WindowState.Normal;
                            MessageBox.Show("Export has failed. Please, do not interact with MSProject while Exporting is performed.", "Interaction exception");
                            break;
                        }

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
        private void FillTasksWithDependencies(IEnumerable<dynamic> tasks, IEnumerable<IGanttTask> ganttTasks)
        {
            foreach (GanttTask currentGanttTask in ganttTasks)
            {
                if (currentGanttTask.Dependencies.Count > 0)
                {
                    dynamic msProjectTask = tasks.FirstOrDefault(a => ((string)a.Name).Equals(this.ReplaceInvalidCharacters(currentGanttTask.Title)));
                    if (msProjectTask != null)
                    {
                        foreach (var dependentTask in currentGanttTask.Dependencies)
                        {
                            dynamic dependentTaskInMsProject = tasks.FirstOrDefault(a => ((string)a.Name).Equals(this.ReplaceInvalidCharacters(dependentTask.FromTask.Title)));

                            if (dependentTaskInMsProject != null)
                            {
                                ProjectTaskLinkType taskPjType = ProjectTaskLinkType.FinishToFinish;
                                switch (dependentTask.Type)
                                {
                                    case DependencyType.FinishFinish:
                                        taskPjType = ProjectTaskLinkType.FinishToFinish;
                                        break;
                                    case DependencyType.FinishStart:
                                        taskPjType = ProjectTaskLinkType.FinishToStart;
                                        break;
                                    case DependencyType.StartFinish:
                                        taskPjType = ProjectTaskLinkType.StartToFinish;
                                        break;
                                    case DependencyType.StartStart:
                                        taskPjType = ProjectTaskLinkType.StartToStart;
                                        break;
                                }

                                msProjectTask.LinkPredecessors(dependentTaskInMsProject, (int)taskPjType, Type.Missing);
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
        private void OutlineTaskInProject(dynamic parentTask, dynamic task)
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

        private enum ProjectSaveType
        {
            DoNotSave = 0
        }

        private enum ProjectTaskLinkType
        {
            FinishToFinish = 0,
            FinishToStart = 1,
            StartToFinish = 2,
            StartToStart = 3
        }
    }
}
