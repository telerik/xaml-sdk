using System.Collections.Generic;
using System.Linq;

namespace Mappings
{
	public class Folder : IDiskItem
	{
		private string _name;
		private IEnumerable<IDiskItem> _children;

		public Folder(string name, IEnumerable<IDiskItem> children)
		{
			this._name = name;
			this._children = children;
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
				return this.Children.Sum(child => child.Size);
			}
		}

		public IEnumerable<IDiskItem> Children
		{
			get
			{
				return _children;
			}
		}
	}
}
