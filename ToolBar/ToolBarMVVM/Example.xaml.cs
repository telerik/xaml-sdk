using System;
using System.Linq;
using System.Windows.Controls;

namespace ToolBarMVVM
{
	public partial class Example : UserControl
	{
		public Example()
		{
			InitializeComponent();
			this.DataContext = new MainViewModel();
		}
	}
}
