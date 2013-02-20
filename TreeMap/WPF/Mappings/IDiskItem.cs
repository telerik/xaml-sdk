using System.Collections.Generic;

namespace Mappings
{
	public interface IDiskItem
	{
		string Name { get; }
		long Size { get; }
		IEnumerable<IDiskItem> Children { get; }
	}
}
