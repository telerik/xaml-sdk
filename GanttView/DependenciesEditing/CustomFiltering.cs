using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GanttView;
using System.Linq;

namespace DependenciesEditing
{
    public class CustomFiltering : FilteringBehavior
    {
        public override System.Collections.Generic.IEnumerable<object> FindMatchingItems(string searchText, System.Collections.IList items, System.Collections.Generic.IEnumerable<object> escapedItems, string textSearchPath, TextSearchMode textSearchMode)
        {
            var dependencies = new List<Dependency>();
            var toRemove = escapedItems.OfType<Dependency>();
            for (int i = 0; i < items.Count; i++)
            {
                dependencies.Add(new Dependency() { FromTask = items[i] as GanttTask, Type = DependencyType.FinishFinish });
                dependencies.Add(new Dependency() { FromTask = items[i] as GanttTask, Type = DependencyType.FinishStart });
                dependencies.Add(new Dependency() { FromTask = items[i] as GanttTask, Type = DependencyType.StartFinish });
                dependencies.Add(new Dependency() { FromTask = items[i] as GanttTask, Type = DependencyType.StartStart });
            }
            var searchTexts = searchText.Split(new char[] { '-' });
            var titleToUpper = searchTexts.First().Trim().ToUpper();
            var typeToUpper = searchTexts.Length > 1 ? searchTexts[1].Trim().ToUpper() : string.Empty;
            
            var result = dependencies.Where(x => !toRemove.Any(y => y.FromTask == x.FromTask && y.Type == x.Type));
            result = result.Where(x => x.FromTask.Title.ToUpper().StartsWith(titleToUpper.ToUpper()) && x.Type.ToString().ToUpper().StartsWith(typeToUpper));
            return result;
        }
    }
}
