using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Windows_Explorer_Like_TreeView_WPF
{
	public class Drive
	{
		public Drive(string name, bool isReady)
		{
			this.Name = name;
			this.IsReady = isReady;
			this.Children = new ObservableCollection<object>();
		}
		public string Name
		{
			get;
			set;
		}
		public bool IsReady
		{
			get;
			set;
		}
		public ObservableCollection<object> Children
		{
			get;
			private set;
		}
	}
}
