using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Windows.Controls.Docking;

namespace ShellPrism
{
    public interface IPaneModel
    {
        DockState Position { get; }
    }
}