using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Windows.Controls.Diagrams.Extensions.ViewModels;

namespace StyleSelectors.ViewModels
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class EllipseNode : NodeViewModelBase
    {
        public EllipseNodeType Type { get; set; }
    }

    public enum EllipseNodeType
    {
        Start,
        End
    }
}
