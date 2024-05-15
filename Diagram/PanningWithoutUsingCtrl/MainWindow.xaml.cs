using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Telerik.Windows.Diagrams.Core;

namespace PanningWithoutUsingCtrl
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

        private void RadDiagram_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftShift || e.Key == Key.RightShift)
            {
                diagram.ActiveTool = MouseTool.PointerTool;
            }
        }

        private void RadDiagram_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftShift || e.Key == Key.RightShift)
            {
                diagram.ActiveTool = MouseTool.PanTool;
            }
        }
	}
}
