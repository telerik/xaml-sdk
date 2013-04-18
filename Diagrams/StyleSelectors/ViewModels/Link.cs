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
    public class Link : LinkViewModelBase<NodeViewModelBase>
    {
        public LinkType Type { get; set; }

        public Link()
            : base()
        { }

        public Link(NodeViewModelBase source, NodeViewModelBase target)
            : base(source, target)
        {
        }
    }

    public enum LinkType
    {
        RightToLeft,
        LeftToRight,
        Normal
    }
}
