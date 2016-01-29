using System.Collections.Generic;
using System.Linq;
using Telerik.Windows.Controls;

namespace MinimumInputPrefixLengthBeforeFiltering
{
    public class MinimumInputFilteringBehavior : FilteringBehavior
    {
        public override IEnumerable<object> FindMatchingItems(string searchText, System.Collections.IList items, IEnumerable<object> escapedItems, string textSearchPath, TextSearchMode textSearchMode)
        {
            if(searchText.Length >= 5)
            {
                return base.FindMatchingItems(searchText, items, escapedItems, textSearchPath, textSearchMode);
            }

            return Enumerable.Empty<object>(); 
        }
    }
}
