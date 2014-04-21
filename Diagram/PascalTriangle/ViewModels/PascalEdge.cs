using System;
using System.Linq;
using Telerik.Windows.Controls;
using Telerik.Windows.Diagrams.Core;

namespace Diagrams.PascalTriangle
{

    public class PascalEdge : ViewModelBase, ILink<PascalNode>
    {
        private PascalNode target;
        private PascalNode source;

        /// <summary>
        /// Gets or sets the source of the connection.
        /// </summary>
        public PascalNode Source
        {
            get
            {
                return this.source;
            }
            set
            {
                if (this.source != value)
                {
                    this.source = value;
                    this.OnPropertyChanged("Source");
                }
            }
        }

        /// <summary>
        /// Gets or sets the target of this connection.
        /// </summary>
        public PascalNode Target
        {
            get
            {
                return this.target;
            }
            set
            {
                if (this.target != value)
                {
                    this.target = value;
                    this.OnPropertyChanged("Target");
                }
            }
        }
        object ILink.Source
        {
            get
            {
                return this.Source;
            }
            set
            {
                this.Source = value as PascalNode;
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
                this.Target = value as PascalNode;
            }
        }
    }
}

