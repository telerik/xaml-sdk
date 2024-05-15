using System.Collections.Generic;
using System.Linq;
using Telerik.Windows.Controls;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Telerik.Windows.Controls.ScheduleView;

namespace ResourcesPaging
{
	public class ViewModel : ViewModelBase
	{	
		private IEnumerable<IResource> resources;
		private IEnumerable<IResourceType> resourceTypes;
		private int resourcesIndex;
		private int resourcesPerPage;

		public ViewModel()
		{
		    this.resources = GenerateResources();		
		    this.Appointments = new ObservableCollection<Appointment>();
			IResourceType resourceType = new ResourceType("Room");
			resourceType.Resources.AddRange(this.resources);
			this.resourceTypes = resourceType.ToEnumerable().ToList();

			this.PreviousResources = new DelegateCommand(GetPreviousResources);
			this.NextResources = new DelegateCommand(GetNextResources);

			this.resourcesIndex = 0;
			this.ResourcesPerPage = 10;
		}

        public ObservableCollection<Appointment> Appointments
        {
            get;
            set;
        }

		public IEnumerable<IResource> Resources
		{
			get
			{
				return this.resources;
			}
		}

		public IEnumerable<IResourceType> ResourceTypes
		{
			get
			{
				return this.GetResources();
			}
		}

		public int ResourcesPerPage
		{
			get
			{
				return this.resourcesPerPage;
			}
			set
			{
				this.resourcesPerPage = value;
				OnPropertyChanged("ResourceTypes");
			}
		}

		public ICommand PreviousResources { get; private set; }
		public ICommand NextResources { get; private set; }

		private IEnumerable<IResource> GenerateResources()
		{
			var resources = new List<IResource>();

			for (int index = 1; index < 100; index++)
			{
				resources.Add(new Resource("Room " + index, "Room"));
			}
			return resources;
		}

		private IEnumerable<IResourceType> GetResources()
		{
            if (this.resourcesIndex < 0) this.resourcesIndex = 0;
			int startIndex = this.resourcesIndex;
			int endIndex = startIndex + this.ResourcesPerPage;
			if (endIndex > this.resourceTypes.First().Resources.Count)
			{
				endIndex = this.resourceTypes.First().Resources.Count;
			}

			List<Resource> resources = new List<Resource>();
			for (int i = startIndex; i < endIndex; i++)
			{
				var resource = (Resource)this.resourceTypes.First().Resources[i];
				resources.Add(resource);
			}

			IResourceType resourceType = new ResourceType("Room");
			resourceType.Resources.AddRange(resources);
			IEnumerable<IResourceType> result = resourceType.ToEnumerable();

			return result;
		}

		private void GetNextResources(object param)
		{
			int resourcesCount = this.resourceTypes.First().Resources.Count;
       
            if((resourcesIndex + this.ResourcesPerPage) < resourcesCount)
            {
			    resourcesIndex= resourcesIndex + this.ResourcesPerPage;
				OnPropertyChanged("ResourceTypes");
			}
		}

		private void GetPreviousResources(object param)
		{
			int resourcesCount = this.resourceTypes.First().Resources.Count;

			if (this.resourcesIndex > 0)
			{
                resourcesIndex = resourcesIndex - this.ResourcesPerPage;
				OnPropertyChanged("ResourceTypes");
			}
		}
	}
}
