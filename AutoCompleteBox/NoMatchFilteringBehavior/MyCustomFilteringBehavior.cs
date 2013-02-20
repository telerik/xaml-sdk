using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Telerik.Windows.Controls;

namespace NoMatchFilteringBehavior
{
	public class MyCustomFilteringBehavior : FilteringBehavior
	{
		public override IEnumerable<object> FindMatchingItems(string searchText, IList items, IEnumerable<object> escapedItems, string textSearchPath, TextSearchMode textSearchMode)
		{
			var matches = base.FindMatchingItems(searchText, items, escapedItems, textSearchPath, textSearchMode);

			if (matches.Count() == 0 && !string.IsNullOrEmpty(searchText))
			{
				return (List<object>)items;
			}

			return base.FindMatchingItems(searchText, items, escapedItems, textSearchPath, textSearchMode);
		}
	}
}
