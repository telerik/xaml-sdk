using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace ContextMenu
{
	public class League
	{
		public League(string name)
		{
			this.Name = name;
			this.Divisions = new ObservableCollection<Division>();
		}
		public string Name
		{
			get;
			set;
		}
		public ObservableCollection<Division> Divisions
		{
			get;
			set;
		}
	}
}