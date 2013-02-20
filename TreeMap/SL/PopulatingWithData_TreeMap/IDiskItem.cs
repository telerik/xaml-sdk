using System.Collections.Generic;

namespace PopulatingWithData_TreeMap
{
	public interface IDiskItem
	{
		string Name { get; }
		long Size { get; }
		IEnumerable<IDiskItem> Children { get; }
	}
}
