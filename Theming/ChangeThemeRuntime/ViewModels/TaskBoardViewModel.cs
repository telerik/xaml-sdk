using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls.TaskBoard;

namespace ChangeThemeRuntime.ViewModels
{
    public class TaskBoardViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<TaskBoardCardModel> tasks;

        public ObservableCollection<TaskBoardCardModel> Tasks
        {
            get
            {
                if (this.tasks == null)
                {
                    this.tasks = this.GetTasks();
                }

                return this.tasks;
            }
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, args);
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<TaskBoardCardModel> GetTasks()
        {
            ObservableCollection<TaskBoardCardModel> tasks = new ObservableCollection<TaskBoardCardModel>();
            TaskBoardCardModel task;

            //Not Done
            task = new TaskBoardCardModel() { Id = "1", Assignee = "John", Description = "Add ability to use the control in design time.", State = "Not Done", Title = "Provide Design Time Support", CategoryName = "Blue" };
            task.Tags.Add("design time");
            task.Tags.Add("must");
            task.Tags.Add("P10");
            tasks.Add(task);
            task = new TaskBoardCardModel() { Id = "2", Assignee = "John", Description = "Provide touch support for the new control.", State = "Not Done", Title = "Provide Touch Support", CategoryName = "Blue" };
            task.Tags.Add("touch support");
            task.Tags.Add("P10");
            task.Tags.Add("must");
            tasks.Add(task);
            task = new TaskBoardCardModel() { Id = "3", Assignee = "John", Description = "Localize all strings for the new control.", State = "Not Done", Title = "Implement Localization", CategoryName = "Blue" };
            task.Tags.Add("localization");
            task.Tags.Add("should");
            task.Tags.Add("P6");
            tasks.Add(task);

            //In Progress
            task = new TaskBoardCardModel() { Id = "4", Assignee = "Jane", Description = "Create a new documentation article describing all features of the new control.", State = "In Progress", Title = "Prepare Documentation", CategoryName = "Blue" };
            task.Tags.Add("documentation");
            tasks.Add(task);
            task = new TaskBoardCardModel() { Id = "5", Assignee = "Jane", Description = "Create a new documentation article for the control.", State = "In Progress", Title = "Add New Documentation Article", CategoryName = "Orange" };
            tasks.Add(task);
            task = new TaskBoardCardModel() { Id = "6", Assignee = "Jean", Description = "Review wording, spelling and formatting of the documentation article.", State = "In Progress", Title = "Review Documentation Article", CategoryName = "Orange" };
            tasks.Add(task);
            task = new TaskBoardCardModel() { Id = "7", Assignee = "Jean", Description = "Upload the new article to docs site.", State = "In Progress", Title = "Upload Documentation", CategoryName = "Orange" };
            tasks.Add(task);

            //Done
            task = new TaskBoardCardModel() { Id = "8", Assignee = "Jean", Description = "Add design project with separate views for all built-in themes.", State = "Done", Title = "Prepare Design", CategoryName = "Blue" };
            task.Tags.Add("design");
            task.Tags.Add("must");
            task.Tags.Add("P10");
            tasks.Add(task);
            task = new TaskBoardCardModel() { Id = "9", Assignee = "Jean", Description = "There are some incorrect localized strings for French.", State = "Done", Title = "Update Inaccurate Localized Strings", CategoryName = "Red" };
            task.Tags.Add("localization");
            tasks.Add(task);
            task = new TaskBoardCardModel() { Id = "10", Assignee = "Jean", Description = "The background of the control should be white.", State = "Done", Title = "Update Incorrect Background", CategoryName = "Red" };
            tasks.Add(task);

            return tasks;
        }

    }
}
