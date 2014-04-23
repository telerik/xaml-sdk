using System.Collections.ObjectModel;

namespace DragDropBetweenTreeViews
{
	public class ApplicationViewModel
	{
		public ApplicationViewModel()
		{
			this.Resources = new ObservableCollection<Resource>();
		}

		public string Name { get; set; }

		public ObservableCollection<Resource> Resources { get; set; }
	}
}
