using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Windows_Explorer_Like_TreeView_WPF
{
	public class ItemStyleSelector : StyleSelector
	{
		public override System.Windows.Style SelectStyle(object item, System.Windows.DependencyObject container)
		{
			if (item is Drive)
				return this.DriveStyle;
			else if (item is Directory)
				return this.DirectoryStyle;
			else if (item is File)
				return this.FileStyle;
			return null;
		}

		public Style DirectoryStyle
		{
			get;
			set;
		}
		public Style FileStyle
		{
			get;
			set;
		}
		public Style DriveStyle
		{
			get;
			set;
		}
	}
}
