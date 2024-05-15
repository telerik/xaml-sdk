using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Windows.Controls;
using Telerik.Windows.Diagrams.Core;

namespace SortTreeLayoutShapes
{
    public class OrgDiagram : RadDiagram, IGraph
    {
        ConnectionCollection IGraph.Connections
        {
            get
            {
                bool isLayouting = this.ServiceLocator.GetService<ILayoutService>().IsLayouting;

                return isLayouting ? new ConnectionCollection(this.SortedConnections)
                    : this.Connections;
            }
        }

        public IList<IConnection> SortedConnections
        {
            get 
            {
                if (this.SortCriteria == SortCriteria.HeadCount)
                {
                    return this.SortedByHeadCount;
                }
                else if (this.SortCriteria == SortCriteria.ConnectionLabel)
                {
                  return this.SortedByConnectionLabel;
                }
                else if (this.SortCriteria == SortCriteria.ShapePosition)
                {
                  return this.SortedByShapePosition;
                }

                return new List<IConnection>();
            }
        }

        public List<IConnection> SortedByConnectionLabel { get; set; }
        public List<IConnection> SortedByHeadCount { get; set; }
        public List<IConnection> SortedByShapePosition { get; set; }

        public SortCriteria SortCriteria
        {
            get;
            set;
        }
    }
}