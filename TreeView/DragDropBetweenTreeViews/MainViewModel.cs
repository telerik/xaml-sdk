using System;
using System.Collections.ObjectModel;
using System.Linq;
using Telerik.Windows.Controls;

namespace DragDropBetweenTreeViews
{
	public class MainViewModel
	{
		public MainViewModel()
		{
			this.LocalMachinePartitions = new ObservableCollection<PartitionViewModel>();
			this.Applications = new ObservableCollection<ApplicationViewModel>();
			this.GenetareSampleData();
		}

		public ObservableCollection<PartitionViewModel> LocalMachinePartitions
		{
			get;
			set;
		}

		public ObservableCollection<ApplicationViewModel> Applications
		{
			get; 
			set;
		}

		private void GenetareSampleData()
		{
			string defaultImagePath = "Images/MediaFiles/{0}";
			ObservableCollection<MediaFile> firstPartishionFiles = new ObservableCollection<MediaFile>();
			firstPartishionFiles.Add(new MediaFile() { ImageTitle = "1PersonalFolders.png", ImageFilePath = string.Format(defaultImagePath, "Images/1PersonalFolders.png") });
			firstPartishionFiles.Add(new MediaFile() { ImageTitle = "2DeletedItems.png", ImageFilePath = string.Format(defaultImagePath, "Images/2DeletedItems.png") });
			firstPartishionFiles.Add(new MediaFile() { ImageTitle = "3Drafts.png", ImageFilePath = string.Format(defaultImagePath, "Images/3Drafts.png") });
			firstPartishionFiles.Add(new MediaFile() { ImageTitle = "4Inbox.png", ImageFilePath = string.Format(defaultImagePath, "Images/4Inbox.png") });
			firstPartishionFiles.Add(new MediaFile() { ImageTitle = "search.png", ImageFilePath = string.Format(defaultImagePath, "Images/search.png") });
			

			this.LocalMachinePartitions.Add(new PartitionViewModel()
			{
				Name = @"C:/Images",
				MediaFiles = firstPartishionFiles
			});

			ObservableCollection<MediaFile> secondPartishionFiles = new ObservableCollection<MediaFile>();
			secondPartishionFiles.Add(new MediaFile() { ImageTitle = "beach_small.png", ImageFilePath = string.Format(defaultImagePath, "Photos/beach_small.png") });
			secondPartishionFiles.Add(new MediaFile() { ImageTitle = "forest_small.png", ImageFilePath = string.Format(defaultImagePath, "Photos/forest_small.png") });
			secondPartishionFiles.Add(new MediaFile() { ImageTitle = "vista_small.png", ImageFilePath = string.Format(defaultImagePath, "Photos/vista_small.png") });
			
			this.LocalMachinePartitions.Add(new PartitionViewModel() 
			{
				Name = @"D:/Photos",
				MediaFiles = secondPartishionFiles
			});

			this.Applications.Add(new ApplicationViewModel() { Name = "Web Client" });
			this.Applications.Add(new ApplicationViewModel() { Name = "Desktop Client" });
		}
	}
}