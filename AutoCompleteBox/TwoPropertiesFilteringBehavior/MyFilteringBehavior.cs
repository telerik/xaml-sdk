using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Windows.Controls;

namespace TwoPropertiesFilteringBehavior
{
	public class MyFilteringBehavior : FilteringBehavior
	{
		// This behavior is case sensitive
		public override IEnumerable<object> FindMatchingItems(string searchText, IList items, IEnumerable<object> escapedItems, string textSearchPath, TextSearchMode textSearchMode)
		{
			var strings = searchText.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
			if (strings.Length == 0)
			{
				return base.FindMatchingItems(searchText, items, escapedItems, textSearchPath, textSearchMode);
			}

			string name = string.Empty;
			string capital = string.Empty;
			if (strings.Length > 0)
			{
				name = strings[0].TrimEnd(' ');
			}

			if (strings.Length > 1)
			{
				capital = strings[1].TrimStart(' ');
			}

			IEnumerable<object> results = null;
			name = name.ToLower();
			capital = capital.ToLower();
			if (textSearchMode == TextSearchMode.Contains)
			{
				results = items.OfType<Country>().Where(x => x.Name.Contains(name) && x.Capital.Contains(capital));
			}
			else
			{
				results = items.OfType<Country>().Where(x => x.Name.ToLower().StartsWith(name) && x.Capital.ToLower().StartsWith(capital));
			}

			return results.Where(x => !escapedItems.Contains(x));
		}
	}
}
