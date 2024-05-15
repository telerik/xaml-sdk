using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using Telerik.Windows.Controls;

namespace DataValidation
{
    public class ViewModel : ViewModelBase, IDataErrorInfo
    {
        private ObservableCollection<Employee> selectedEmployees;
        private bool isMembersBoxEnabled;
        private Employee selectedEmployee;
        private Team selectedTeam;
        private string itemSearchText;
        private string employeeSearchText;

        public ViewModel()
        {
            this.SelectionChanged = new DelegateCommand(OnSelectionChangedExecute);
            this.Assign = new DelegateCommand(OnAssignExecute);

            this.Employees = new ObservableCollection<Employee>();
            this.SelectedEmployees = new ObservableCollection<Employee>();
            this.IsSearchTextValidationOn = false;

            this.Teams = new ObservableCollection<Team>()
            {
                new Team() 
                {
                    Name="Team1",
                    Employees = new ObservableCollection<Employee>()
                    {
                         new Employee() { FirstName="Maria", LastName="Anders", JobTitle="Software Engineer"},
                         new Employee() { FirstName="Ana", LastName="Trujillo", JobTitle="Support"},
                         new Employee() { FirstName="Antonio", LastName="Moreno", JobTitle="QA"},
                         new Employee() { FirstName="Thomas", LastName="Hardy", JobTitle="Designer"},
                         new Employee() { FirstName="Hanna", LastName="Moos", JobTitle="Software Engineer"},
                    }
                },
                new Team() 
                {
                    Name="Team2",
                    Employees = new ObservableCollection<Employee>
                    {
                        new Employee() { FirstName="Frederique", LastName="Citeaux", JobTitle="Frond End Developer"},
                        new Employee() { FirstName="Martin", LastName="Sommer", JobTitle="QA"},
                        new Employee() { FirstName="Laurence", LastName="Lebihan", JobTitle="Support"},
                        new Employee() { FirstName="Elizabeth", LastName="Lincoln", JobTitle="Software Engineer"},
                        new Employee() { FirstName="Victoria", LastName="Ashworth", JobTitle="Software Engineer"},
                    }
                },
            };
        }

        public ObservableCollection<Team> Teams { get; set; }
        public ObservableCollection<Employee> Employees { get; set; }

        public ICommand Assign { get; set; }
        public ICommand SelectionChanged { get; set; }

        public bool IsSearchTextValidationOn { get; set; }

        public bool IsMembersBoxEnabled
        {
            get
            {
                return this.isMembersBoxEnabled;
            }
            set
            {
                if(this.isMembersBoxEnabled != value)
                {
                    this.isMembersBoxEnabled = value;
                    this.OnPropertyChanged(() => this.IsMembersBoxEnabled);
                }
            }
        }

        public string ItemSearchText
        {
            get
            {
                return this.itemSearchText;
            }
            set
            {
                if (this.itemSearchText != value)
                {
                    this.itemSearchText = value;
                    this.OnPropertyChanged(() => this.ItemSearchText);
                }
            }
        }

        public string EmployeeSearchText
        {
            get
            {
                return this.employeeSearchText;
            }
            set
            {
                if (this.employeeSearchText != value)
                {
                    this.employeeSearchText = value;
                    this.OnPropertyChanged(() => this.EmployeeSearchText);
                }
            }
        }

        public ObservableCollection<Employee> SelectedEmployees
        {
            get
            {
                return this.selectedEmployees;
            }
            set
            {
                if (this.selectedEmployees != value)
                {
                    this.selectedEmployees = value;
                    this.OnPropertyChanged(() => this.SelectedEmployees);
                }
            }
        }

        public Employee SelectedEmployee
        {
            get
            {
                return this.selectedEmployee;
            }
            set
            {
                if (this.selectedEmployee != value)
                {
                    this.selectedEmployee = value;
#if !SILVERLIGHT
                    Mouse.Capture(null);
#endif
                    this.OnPropertyChanged(() => this.SelectedEmployee);
                    this.OnPropertyChanged(() => this.EmployeeSearchText);
                }
            }
        }

        public Team SelectedTeam
        {
            get
            {
                return this.selectedTeam;
            }
            set
            {
                if (this.selectedTeam != value)
                {
                    this.selectedTeam = value;
                    if(value != null)
                    {
                        this.IsMembersBoxEnabled = true;
                    }
                    else
                    {
                        this.IsMembersBoxEnabled = false;
                        this.EmployeeSearchText = String.Empty;
                    }

                    this.OnPropertyChanged(() => this.SelectedTeam);
                    this.OnPropertyChanged(() => this.ItemSearchText);
                    this.OnPropertyChanged(() => this.SelectedEmployee);
                }
            }
        }

        public string Error
        {
            get
            {
                return ValidateTeam() ?? ValidateEmployee();
            }
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "SelectedTeam":
                    case "ItemSearchText":
                        return this.ValidateTeam();
                    case "SelectedEmployee":
                    case "EmployeeSearchText":
                        return this.ValidateEmployee();

                }
                return null;
            }
        }

        private void OnSelectionChangedExecute(object obj)
        {
 #if SILVERLIGHT
            var teamsCollection = obj as Telerik.Windows.Controls.SelectionChangedEventArgs;
 #else
            var teamsCollection = obj as SelectionChangedEventArgs;
 #endif

            if (teamsCollection.AddedItems.Count > 0)
            {
                var addedTeam = teamsCollection.AddedItems[0] as Team;
                foreach (var item in addedTeam.Employees)
                {
                    this.Employees.Add(item);
                }
            }
            else
            {
                
                var removedTeam = teamsCollection.RemovedItems[0] as Team;
                foreach (var item in removedTeam.Employees)
                {
                    if (this.SelectedEmployees.Any(a => a == item))
                    {
                        this.SelectedEmployees.Remove(item);
                    }
                    this.Employees.Remove(item);
                }

                if (this.SelectedEmployees.Count == 0)
                {
                    this.SelectedEmployee = null;
                }

                this.ForceTextSearchValidation();
                
 #if !SILVERLIGHT
                Mouse.Capture(null);
 #endif
            }
        }

        private void OnAssignExecute(object obj)
        {
            this.ForceTextSearchValidation();
        }

        private void ForceTextSearchValidation()
        {
            this.IsSearchTextValidationOn = true;
            this.OnPropertyChanged(() => this.EmployeeSearchText);
            this.OnPropertyChanged(() => this.ItemSearchText);
            this.IsSearchTextValidationOn = false;
        }

        private string ValidateTeam()
        {
            if (this.SelectedTeam == null)
            {
                return "Team must be selected.";
            }

            if (IsSearchTextValidationOn)
            {
                return SearchTextValidation(this.ItemSearchText);
            }
            return null;
        }

        private string ValidateEmployee()
        {
            if (this.SelectedEmployee == null && this.IsMembersBoxEnabled)
            {
                return "Employee must be selected.";
            }

            if (IsSearchTextValidationOn)
            {
                return SearchTextValidation(this.EmployeeSearchText);
            }

            return null;
        }

        private string SearchTextValidation(string searchText)
        {
            if(searchText != null)
            {
                if (!searchText.Equals(""))
                {
                    return "Please, delete the SearchText!";
                }
            }

            return null;
        }
    }
}
