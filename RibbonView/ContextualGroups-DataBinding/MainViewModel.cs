using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;

namespace ContextualGroups_DataBinding
{
	public class MainViewModel : ViewModelBase
	{
		ObservableCollection<TabViewModel> tabs;
		ObservableCollection<ContextualGroupViewModel> contextualGroups;
		private const int TabsCount = 5;
		private const int ContextualGroupsCount = 1; 
		private const string ContextualGroupName = "ContextualGroup1";

		public MainViewModel()
		{
			this.Tabs = this.GenerateTabs();
			this.ContextualGroups = this.GenerateContextualGroups();
		}

		private ObservableCollection<ContextualGroupViewModel> GenerateContextualGroups()
		{
			var groups = new ObservableCollection<ContextualGroupViewModel>();
			for (int i = 0; i < ContextualGroupsCount; i++)
			{
				var contextualGroup = new ContextualGroupViewModel()
				{
					Header = "Contextual Group Header",
					GroupName = ContextualGroupName,
					IsActive = true,
				};
				groups.Add(contextualGroup);
			}
			return groups;
		}

		private ObservableCollection<TabViewModel> GenerateTabs()
		{
			var tabViewModels = new ObservableCollection<TabViewModel>();
			
			for (int i = 0; i < TabsCount; i++)
			{
				var groupName = i >= 3 ? ContextualGroupName : string.Empty;
				var tabItem = new TabViewModel()
				{
					Header = "Tab "+i,
					ContextualGroupName = groupName,
				};
				tabViewModels.Add(tabItem);
			}
			return tabViewModels;
		}

		public ObservableCollection<ContextualGroupViewModel> ContextualGroups
		{
			get
			{
				return this.contextualGroups;
			}
			set
			{
				this.contextualGroups = value;
				//OnPropertyChanged("ContextualGroups");
			}
		}

		public ObservableCollection<TabViewModel> Tabs
		{
			get
			{
				return this.tabs;
			}
			set
			{
				this.tabs = value;
				//OnPropertyChanged("Tabs");
			}
		}
	}
}
