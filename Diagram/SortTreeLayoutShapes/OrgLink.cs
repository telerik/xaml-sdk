using System;
using System.Linq;
using Telerik.Windows.Controls.Diagrams.Extensions.ViewModels;

namespace SortTreeLayoutShapes
{

	/// <summary>
	/// TODO: Update summary.
	/// </summary>
	public class OrgLink : LinkViewModelBase<OrgItem>
	{
		public OrgLink(OrgItem source, OrgItem target)
			: base(source, target)
		{
		}

	}
}
