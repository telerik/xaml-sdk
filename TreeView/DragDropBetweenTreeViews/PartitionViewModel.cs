using System.Collections.ObjectModel;

namespace DragDropBetweenTreeViews
{
	public class PartitionViewModel
	{
		public PartitionViewModel()
		{
			this.MediaFiles = new ObservableCollection<MediaFile>();
		}

		public string Name { get; set; }

		public ObservableCollection<MediaFile> MediaFiles { get; set; }
	}
}
