using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Diagrams.Core;

namespace CustomTools_SL
{
    public class ForbidNWSEResizing : ManipulationTool
    {
        public ForbidNWSEResizing()
            : base(ManipulationTool.ToolNameNWSE)
        { }

        public override bool MouseMove(PointerArgs e)
        {
            return false;
        }
    }
}
