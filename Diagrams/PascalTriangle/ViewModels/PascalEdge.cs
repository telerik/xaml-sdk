using System;
using System.Linq;
using Telerik.Windows.Diagrams.Core;

namespace Diagrams.PascalTriangle
{
    public class PascalEdge : ILink<PascalNode>
    {
        public PascalNode Source
        {
            get;
            set;
        }

        public PascalNode Target
        {
            get;
            set;
        }

        object ILink.Source
        {
            get
            {
                return this.Source;
            }
            set
            {
            }
        }

        object ILink.Target
        {
            get
            {
                return this.Target;
            }
            set
            {
            }
        }
    }
}
