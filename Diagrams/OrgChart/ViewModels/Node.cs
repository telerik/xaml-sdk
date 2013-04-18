using System.Collections.Specialized;
using System.Diagnostics;
using Telerik.Windows.Controls.Diagrams.Extensions.ViewModels;

namespace OrgChart.ViewModels
{
	[DebuggerDisplay("FirstName: {FirstName}")]
	public class Node : HierarchicalNodeViewModel
	{
		private ItemDisplayMode currentDisplayMode;

		public Node()
		{
			this.Children.CollectionChanged += OnChildrenCollectionChanged;
			this.CurrentDisplayMode = ItemDisplayMode.Standard;
		}

		public int HeadCount { get; set; }

		public Branch Branch { get; set; }

		public string JobPosition { get; set; }

		public bool AreChildrenCollapsed { get; set; }

		public bool AreChildrenSelected { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string FullName
		{
			get
			{
				return string.Format("{0} {1}", this.FirstName, this.LastName);
			}
		}

		public string Email { get; set; }

		public string Phone { get; set; }

		public string ImagePath { get; set; }

		public string Address { get; set; }

		public string Path { get; set; }

		public ItemDisplayMode CurrentDisplayMode
		{
			get { return this.currentDisplayMode; }
			set
			{
				if (this.currentDisplayMode != value && value != ItemDisplayMode.None)
				{
					this.currentDisplayMode = value;
					this.OnPropertyChanged("CurrentDisplayMode");
				}
			}
		}

		private void OnChildrenCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (e.Action == NotifyCollectionChangedAction.Add)
			{
				Node firstChild = e.NewItems[0] as Node;
				this.HeadCount += firstChild.HeadCount + 1;
			}
			else if (e.Action == NotifyCollectionChangedAction.Remove)
			{
				Node firstChild = e.OldItems[0] as Node;
				this.HeadCount -= (firstChild.HeadCount + 1);
			}
		}
	}
}