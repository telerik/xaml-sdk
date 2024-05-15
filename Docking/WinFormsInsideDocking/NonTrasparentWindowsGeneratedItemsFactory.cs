using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using Telerik.Windows.Controls.Docking;
using Telerik.Windows.Controls.Navigation;

namespace WinFormsInsideDocking_WPF
{
	public class NonTrasparentWindowsGeneratedItemsFactory : DefaultGeneratedItemsFactory
	{
		public override ToolWindow CreateToolWindow()
		{
			var window = base.CreateToolWindow();

            RadWindowInteropHelper.SetAllowTransparency(window, false);
            RadWindowInteropHelper.SetClipMaskCornerRadius(window, new CornerRadius(3));
            RadWindowInteropHelper.SetOpaqueWindowBackground(window, Brushes.LightGray);
            
			return window;
		}
	}
}
