using System.Collections.Generic;
using System.Collections.ObjectModel;
using Telerik.Windows.Diagrams.Core;

namespace Diagrams.PascalTriangle
{
    public class PascalTriangleGraphSource : IGraphSource
    {
        public PascalTriangleGraphSource()
        {
            this.InternalItems = new ObservableCollection<PascalNode>();
            this.InternalEdges = new ObservableCollection<PascalEdge>();
        }

        public ObservableCollection<PascalNode> InternalItems
        {
            get;
            private set;
        }

        public ObservableCollection<PascalEdge> InternalEdges
        {
            get;
            private set;
        }

        IEnumerable<ILink> IGraphSource.Links
        {
            get { return this.InternalEdges; }
        }

        System.Collections.IEnumerable IGraphSource.Items
        {
            get { return this.InternalItems; }
        }
    }
}
