using System.Collections.ObjectModel;
using Telerik.Windows.Controls.Diagrams.Extensions.ViewModels;

namespace MVVM
{
	public class Brand : ContainerNodeViewModelBase<NodeViewModelBase>
	{
		private ObservableCollection<string> children;

		public Brand()
		{
			this.children = new ObservableCollection<string>();
		}

		public ObservableCollection<string> Children
		{
			get
			{
				return this.children;
			}
			set
			{
				this.children = value;
			}
		}
	}
}
