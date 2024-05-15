using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Telerik.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ScheduleView;

namespace HierarchicalGroupingAndFilteringWithTreeView
{
    public class ViewModel : ViewModelBase
    {
        private Func<object, bool> groupFilter;
        private List<Team> expandedTeams;

        public ViewModel()
        {
            this.ExpandedCommand = new DelegateCommand(this.OnExpandedExecute);
            this.CollapsedCommand = new DelegateCommand(this.OnCollapsedExecute);
            this.expandedTeams = new List<Team>();
            this.TreeViewItems = this.GetItems();

            var commonResourceType = new ResourceType("Common");
            commonResourceType.Resources.Add(new Resource("Division A"));
            commonResourceType.Resources.Add(new Resource("Team I"));
            commonResourceType.Resources.Add(new Resource("Team II"));
            commonResourceType.Resources.Add(new Resource("Team III"));
            commonResourceType.Resources.Add(new Resource("Division B"));
            commonResourceType.Resources.Add(new Resource("Team Blue"));
            commonResourceType.Resources.Add(new Resource("Team Red"));
            commonResourceType.Resources.Add(new Resource("Team Yellow"));

            this.ResourceTypes = new ObservableCollection<ResourceType>() { commonResourceType };

            var app1 = new Appointment()
            {
                Subject = "Team I Test",
                Start = DateTime.Today,
                End = DateTime.Today.AddDays(2)
            };

            app1.Resources.Add(new Resource("Team I", "Common"));

            var app2 = new Appointment()
            {
                Subject = "Team Red Test",
                Start = DateTime.Today.AddDays(4),
                End = DateTime.Today.AddDays(6)
            };

            app2.Resources.Add(new Resource("Team Red", "Common"));

            var app3 = new Appointment()
            {
                Subject = "Division B Test",
                Start = DateTime.Today.AddDays(2),
                End = DateTime.Today.AddDays(4)
            };

            app3.Resources.Add(new Resource("Division B", "Common"));

            this.Appointments = new ObservableCollection<Appointment>() { app1, app2, app3 };

            UpdateGroupFilter();
        }

        public ObservableCollection<Division> TreeViewItems { get; set; }
        public ObservableCollection<Appointment> Appointments { get; set; }
        public ObservableCollection<ResourceType> ResourceTypes { get; set; }
        public ICommand ExpandedCommand { get; private set; }
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

        private ObservableCollection<Division> GetItems()
        {
            var col = new ObservableCollection<Division>();

            var d = new Division("Division A");
            d.Teams.Add(new Team(1, "Team I"));
            d.Teams.Add(new Team(2, "Team II"));
            d.Teams.Add(new Team(3, "Team III"));
            col.Add(d);

            d = new Division("Division B");
            d.Teams.Add(new Team(6, "Team Blue"));
            d.Teams.Add(new Team(7, "Team Red"));
            d.Teams.Add(new Team(8, "Team Yellow"));
            col.Add(d);

            return col;
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
                if (resource.DisplayName.Contains("Division"))
                {
                    return true;
                }

                if (expandedTeams.Any(p => p.Name == resource.DisplayName))
                {
                    return true;
                }
            }

            return false;
        }

        private void OnExpandedExecute(object param)
        {
            var args = param as RadRoutedEventArgs;

            if ((args.OriginalSource as RadTreeViewItem).DataContext is Division)
            {
                this.expandedTeams.AddRange(((args.OriginalSource as RadTreeViewItem).DataContext as Division).Teams);
                UpdateGroupFilter();
            }
        }

        private void OnCollapsedExecute(object param)
        {
            var args = param as RadRoutedEventArgs;
            foreach (Team team in (args.OriginalSource as RadTreeViewItem).ItemsSource)
            {
                this.expandedTeams.Remove(team);
            }

            UpdateGroupFilter();
        }
    }
}
