using System;
using System.Linq;
using System.Windows.Controls;

namespace IpAddresses
{
	/// <summary>
	/// Interaction logic for Example.xaml
	/// </summary>
	public partial class Example : UserControl
	{
		public Example()
		{
			InitializeComponent();
			this.DataContext = new ValidationViewModel();
		}			
	}
}
