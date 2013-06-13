using System;
using System.Linq;

namespace Windows_Explorer_Like_TreeView_WPF
{
	public class File
	{
		public File(string fullPath, string name)
		{
			this.FullPath = fullPath;
			this.Name = name;
		}

		public string FullPath
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}
	}
}
