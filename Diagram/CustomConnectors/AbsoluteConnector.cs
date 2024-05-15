using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Telerik.Windows.Controls.Diagrams;

namespace CustomConnectors
{
    /// <summary>
    /// Represents a connector that uses its offset as absolute value.
    /// </summary>
    public class AbsoluteConnector : RadDiagramConnector
    {
        public override Point CalculateRelativePosition(System.Windows.Size shapeSize)
        {
            return this.Offset;
        }
    }
}
