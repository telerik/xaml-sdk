using System.Collections.Generic;

namespace StylesAndTemplates_TreeMap
{
	public interface IDiskItem
	{
		string Name { get; }
		long Size { get; }
		IEnumerable<IDiskItem> Children { get; }
	}
}
