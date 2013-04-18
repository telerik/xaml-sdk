using System.Collections.Generic;

namespace StylesAndTemplates_TreeMap
{
	public class File : IDiskItem
	{
		private string _name;
		private long _size;

		public File(string name, long size)
		{
			this._name = name;
			this._size = size;
		}

		public string Name
		{
			get
			{
				return _name;
			}
		}

		public long Size
		{
			get
			{
				return _size;
			}
		}

		public IEnumerable<IDiskItem> Children
		{
			get { return null; }
		}
	}
}
