using System;
using System.Linq;
using System.Windows.Controls;
using ChangeHeadersBackground;

namespace ChangeHeadersBackground_SL
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();
			this.DataContext = new MainViewModel();
		}
	}
}
