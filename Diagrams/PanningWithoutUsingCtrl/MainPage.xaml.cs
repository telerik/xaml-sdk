using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using Telerik.Windows.Diagrams.Core;

namespace PanningWithoutUsingCtrl
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void RadDiagram_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Shift)
            {
                diagram.ActiveTool = MouseTool.PointerTool;
            }
        }

        private void RadDiagram_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Shift)
            {
                diagram.ActiveTool = MouseTool.PanTool;
            }
        }
    }
}
