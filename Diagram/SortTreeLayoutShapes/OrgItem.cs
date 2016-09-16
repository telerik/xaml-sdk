using System;
using System.Linq;
using Telerik.Windows.Controls.Diagrams.Extensions.ViewModels;

namespace SortTreeLayoutShapes
{
	public class OrgItem : HierarchicalNodeViewModel
	{
		public OrgItem()
		{
			
		}

		public string Label { get; set; }

        public string HeadCount { get; set; }
	}
}