using System;
using System.Linq;
using System.Windows;

namespace Diagrams.PascalTriangle
{
	public partial class App : Application
	{
        public App()
		{
			this.StartupUri = new Uri("MainWindow.xaml", UriKind.Relative);
		}
	}
}
