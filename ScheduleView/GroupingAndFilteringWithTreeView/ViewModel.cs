using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ScheduleView;

namespace GroupingAndFilteringWithTreeView
{
    public class ViewModel : ViewModelBase
    {
        private GroupDescriptionCollection groupDescriptions;
        private ObservableCollection<Process> processes;
        private ObservableCollection<Segregation> segregations;
        private Func<object, bool> groupFilter;

        public ViewModel()
        {
            this.ExpendedCommand = new DelegateCommand(this.OnExpandedExecute);
            this.CollapsedCommand = new DelegateCommand(this.OnCollapsedExecute);
            this.Appointments = new ObservableCollection<Appointment>();

            this.processes = new ObservableCollection<Process>
            {
                new Process("Process 0"),
                new Process("Process 1"),
                new Process("Process 2"),
                new Process("Process 3")
            };

            this.segregations = new ObservableCollection<Segregation>
            {
                new Segregation { Name = "Segregation 0", Processes = this.processes },
                new Segregation { Name = "Segregation 1", Processes = this.processes },
                new Segregation { Name = "Segregation 2", Processes = this.processes },
            };

            this.Airlines = new ObservableCollection<Airline>
            {
                new Airline { Name = "Airline 0", Segregations = this.segregations },
                new Airline { Name = "Airline 1", Segregations = this.segregations },
            };
        }

        public ObservableCollection<Airline> Airlines { get; set; }

        public ObservableCollection<Appointment> Appointments { get; set; }

        public ICommand ExpendedCommand { get; private set; }

        public ICommand CollapsedCommand { get; private set; }

        public Func<object, bool> GroupFilter
        {
            get
            {
                return this.groupFilter;
            }
            private set
            {
                this.groupFilter = value;
                this.OnPropertyChanged(() => this.GroupFilter);
            }
        }

        private void UpdateGroupFilter()
        {
            this.GroupFilter = new Func<object, bool>(this.GroupFilterFunc);
        }

        private bool GroupFilterFunc(object groupName)
        {
            IResource resource = groupName as IResource;

            if (resource != null)
            {
                if (resource.ResourceType == "Airlines")
                {
                    return this.Airlines.Any(a => a.IsExpanded && a.Name == resource.ResourceName) || this.Airlines.All(a => a.IsExpanded == false);
                }
                else if (resource.ResourceType == "Segregations")
                {
                    return this.Airlines.Any(a => a.IsExpanded && a.Segregations.Any(s => s.Name == resource.ResourceName && s.IsExpanded) ||
                                                  a.Segregations.All(s => s.IsExpanded == false));
                }
                else if (resource.ResourceType == "Processes")
                {
                    return this.segregations.Any(s => s.IsExpanded && s.Processes.Any(p => p.Name == resource.ResourceName));
                }
            }

            return true;
        }

        public GroupDescriptionCollection GroupDescriptions
        {
            get
            {
                if (this.groupDescriptions == null)
                {
                    this.groupDescriptions = new GroupDescriptionCollection() { new DateGroupDescription() };
                    this.AddGroupDescription("Airlines");
                }

                return this.groupDescriptions;
            }
        }

        private void AddGroupDescription(string groupName)
        {
            if (!this.IsExistingGroup(groupName))
            {
                ResourceGroupDescription groupDescription = new ResourceGroupDescription();
                groupDescription.ResourceType = groupName;
                this.GroupDescriptions.Add(groupDescription);
            }
        }

        private void OnExpandedExecute(object param)
        {
            this.UpdateGroupFilter();
            if (this.Airlines.Any(a => a.IsExpanded == true))
            {
                this.AddGroupDescription("Segregations");
            }

            if (this.segregations.Any(s => s.IsExpanded == true))
            {
                this.AddGroupDescription("Processes");
            }
        }

        private void OnCollapsedExecute(object param)
        {
            this.UpdateGroupFilter();

            if (this.Airlines.All(a => a.IsExpanded != true))
            {
                this.RemoveGroupDescription("Processes");
                this.RemoveGroupDescription("Segregations");
            }
        }

        private void RemoveGroupDescription(string groupName)
        {
            foreach (var group in this.GroupDescriptions)
            {
                if (group is ResourceGroupDescription)
                {
                    if ((group as ResourceGroupDescription).ResourceType == groupName)
                    {
                        this.GroupDescriptions.Remove(group);
                        return;
                    }
                }
            }
        }

        private bool IsExistingGroup(string groupName)
        {
            foreach (var group in this.GroupDescriptions)
            {
                if (group is ResourceGroupDescription)
                {
                    if ((group as ResourceGroupDescription).ResourceType == groupName)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}