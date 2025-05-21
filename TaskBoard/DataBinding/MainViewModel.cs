using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Telerik.Windows.Controls.TaskBoard;

namespace DataBinding
{
    public class MainViewModel
    {
        private List<Employee> _employees;
        public ObservableCollection<Task> TaskModels { get; set; }
        public ObservableCollection<TaskBoardCardModel> TaskCardModels { get; set; }
        public CollectionViewSource CollectionView { get; set; }

        public MainViewModel()
        {
            TaskModels = GetTaskModels();
            TaskCardModels = GetTaskCardModels();

            CollectionView = new CollectionViewSource() { Source = TaskCardModels };
            CollectionView.GroupDescriptions.Add(new PropertyGroupDescription(nameof(TaskBoardCardModel.State)));
        }

        public List<Employee> Employees
        {
            get
            {
                if (_employees == null)
                {
                    _employees = new List<Employee>
                    {
                        new Employee() { Id = 1, Name ="Bella", Icon = new BitmapImage(new Uri(@"/DataBinding;component/Images/Bella.png", UriKind.RelativeOrAbsolute)) },
                        new Employee() { Id = 2, Name ="Smith",Icon = new BitmapImage(new Uri(@"/DataBinding;component/Images/Smith.png", UriKind.RelativeOrAbsolute))  },
                        new Employee() { Id = 3, Name ="John",Icon = new BitmapImage(new Uri(@"/DataBinding;component/Images/John.png", UriKind.RelativeOrAbsolute)) },
                    };
                }
                return _employees;
            }
        }

        private ObservableCollection<Task> GetTaskModels()
        {
            ObservableCollection<Task> tasks = new ObservableCollection<Task>
            {
                new Task()
                {
                    Assignee = this.Employees[0],
                    Header = "RadDocking: Create Unit Test ",
                    Description = "Add Unit Tests",
                    State = "Done",
                    CategoryName = "Low",
                    ShowIndicator = true,
                    Tags = new List<object>(){ ".Net Core", "R3" }
                },

                new Task()
                {
                    Assignee = this.Employees[1],
                    Header = "RadPanelBar: IsExpanded property is not respected",
                    Description = "Fix Bug",
                    State = "Not Done",
                    CategoryName = "Medium",
                    ShowIndicator= true,
                    Tags = new List<object>(){ "Important", "R2" }
                },

                new Task()
                {
                    Assignee = this.Employees[2],
                    Header = "RadChartView: Implement Animation Feature",
                    Description = "Implement animations for all series in RadChartView",
                    State = "In Progress",
                    CategoryName = "High",
                    ShowIndicator = true,
                    Tags = new List<object>(){ "R1", DateTime.Now }
                }
            };
            return tasks;
        }

        public ObservableCollection<TaskBoardCardModel> GetTaskCardModels()
        {
            ObservableCollection<TaskBoardCardModel> tasks = new ObservableCollection<TaskBoardCardModel>();
            TaskBoardCardModel task = new TaskBoardCardModel()
            {
                Assignee = "Bella",
                Title = "RadDocking: Unit Test",
                Description = "Add Unit Tests",
                State = "Not Done",
                CategoryName = "Low",
                IconPath = @"/DataBinding;component/Images/Bella.png"

            };
            task.Tags.Add(new TagModel() { TagName = "Important", TagColor = Brushes.Red });
            task.Tags.Add(new TagModel() { TagName = "2", TagColor = Brushes.Yellow });
            task.Tags.Add(new TagModel() { TagName = DateTime.Now.ToShortDateString(), TagColor = Brushes.Green });
            tasks.Add(task);

            task = new TaskBoardCardModel()
            {
                Assignee = "John",
                Title = "RadPanelBar: IsExpanded property is not respected",
                Description = "Fix Bug",
                State = "In Progress",
                CategoryName = "Medium",
                IconPath = @"/DataBinding;component/Images/John.png"
            };
            task.Tags.Add(new TagModel() { TagName = "R1", TagColor = Brushes.Blue });
            tasks.Add(task);

            task = new TaskBoardCardModel()
            {
                Assignee = "Smith",
                Title = "RadChartView: Implement Animation Feature",
                Description = "Implement animations for all series in RadChartView.",
                State = "Done",
                CategoryName = "High",
                IconPath = @"/DataBinding;component/Images/Smith.png"
            };
            task.Tags.Add(new TagModel() { TagName = "Complex", TagColor = Brushes.Red });
            tasks.Add(task);

            return tasks;
        }
    }
}
