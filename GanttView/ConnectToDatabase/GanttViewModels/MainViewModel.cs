using System;
using System.Collections.ObjectModel;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Scheduling;
using ConnectToDatabase_WPF.Data;

namespace ConnectToDatabase_WPF
{
    public class MainViewModel : ViewModelBase
    {
        private const double TimelineWidth = 1000;
        private TasksDataManager dataManager = new TasksDataManager();
        
        private ObservableCollection<CustomGanttTask> tasks;
        private DateTime visibleRangeStart;
        private DateTime visibleRangeEnd;

        public DelegateCommand CreateDatabaseCommand { get; set; }
        public DelegateCommand UpdateDatabaseCommand { get; set; }
        public DelegateCommand FetchAllTaskCommand { get; set; }

        public ObservableCollection<CustomGanttTask> Tasks
        {
            get { return tasks; }
            set
            {
                tasks = value; OnPropertyChanged("Tasks");
            }
        }

        public DateTime VisibleRangeStart
        {
            get { return visibleRangeStart; }
            set
            {
                visibleRangeStart = value;
                OnPropertyChanged("VisibleRangeStart");
                OnPropertyChanged("PixelLength");
            }
        }

        public DateTime VisibleRangeEnd
        {
            get { return visibleRangeEnd; }
            set
            {
                visibleRangeEnd = value;
                OnPropertyChanged("VisibleRangeEnd");
                OnPropertyChanged("PixelLength");
            }
        }

        public bool IsVisible
        {
            get
            {
                return this.dataManager.IsDatabaseInitialized();
            }
        }  

        public TimeSpan PixelLength
        {
            get
            {
                var range = (this.visibleRangeEnd - this.visibleRangeStart).Ticks;
                var ticksPerPixel = range / TimelineWidth;                
                return ticksPerPixel > 0 ? TimeSpan.FromTicks((long)ticksPerPixel) : TimeLineSettings.DefaultPixelLength;
            }            
        }

        public MainViewModel()
        {   
            this.CreateDatabaseCommand = new DelegateCommand(OnCreateDatabaseExecute, OnCanCreateDatabaseExecute);
            this.UpdateDatabaseCommand = new DelegateCommand(OnUpdateDatabaseExecute, OnCanUpdateDatabaseExecute);
            this.FetchAllTaskCommand = new DelegateCommand(OnFetchAllTasksExecute, OnCanFetchAllTasksExecute);

            if (this.dataManager.IsDatabaseInitialized())
            {
                this.FetchAllTaskCommand.Execute(null);
            }
        }

        private void SetVisibleRange()
        {
            this.VisibleRangeStart = this.dataManager.GetRangeStart();
            this.VisibleRangeEnd = this.dataManager.GetRangeEnd();
        }

        private bool OnCanFetchAllTasksExecute(object obj)
        {
            return this.dataManager.IsDatabaseInitialized() ? true : false;
        }

        private void OnFetchAllTasksExecute(object obj)
        {
            this.Tasks = this.dataManager.GetAllTasks();
            this.SetVisibleRange();
        }

        private void OnUpdateDatabaseExecute(object obj)
        {
            this.dataManager.UpdateDatabase();
        }

        private bool OnCanUpdateDatabaseExecute(object obj)
        {
            return this.dataManager.IsDatabaseInitialized() ? true : false;
        }

        private void OnCreateDatabaseExecute(object obj)
        {
            this.dataManager.InitializeDatabase();
            this.Tasks = this.dataManager.GetAllTasks();
            this.SetVisibleRange();
            this.UpdateDatabaseCommand.InvalidateCanExecute();
            this.CreateDatabaseCommand.InvalidateCanExecute();
            this.FetchAllTaskCommand.InvalidateCanExecute();
            this.OnPropertyChanged("IsVisible");
        }

        private bool OnCanCreateDatabaseExecute(object obj)
        {
            return this.dataManager.IsDatabaseInitialized() ? false : true;
        }
    }
}
