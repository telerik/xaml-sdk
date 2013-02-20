using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Diagrams;
using Telerik.Windows.Controls.Diagrams.Extensions;

namespace MVVM
{
	public partial class MainPage : UserControl
	{
		private FileManager fileManager;

		static MainPage()
		{
			var saveBinding = new CommandBinding(DiagramCommands.Save, ExecuteSave, CanExecutedSave);
			var openBinding = new CommandBinding(DiagramCommands.Open, ExecuteOpen);

			CommandManager.RegisterClassCommandBinding(typeof(MainPage), saveBinding);
			CommandManager.RegisterClassCommandBinding(typeof(MainPage), openBinding);
		}

		public MainPage()
		{
			InitializeComponent();

			this.fileManager = new FileManager(this.diagram);
			this.DataContext = new CarsGraphSource();
		}

		private static void CanExecutedSave(object sender, CanExecuteRoutedEventArgs e)
		{
			var owner = sender as MainPage;
			e.CanExecute = owner != null && owner.diagram != null && owner.diagram.Items.Count > 0;
		}

		private static void ExecuteSave(object sender, ExecutedRoutedEventArgs e)
		{
			var owner = sender as MainPage;
			if (owner != null)
				owner.fileManager.SaveToFile();
		}

		private static void ExecuteOpen(object sender, ExecutedRoutedEventArgs e)
		{
			var owner = sender as MainPage;
			if (owner != null)
			{
				(owner.DataContext as CarsGraphSource).InternalItems.Clear();
				(owner.DataContext as CarsGraphSource).InternalLinks.Clear();
				owner.fileManager.LoadFromFile();
			}
		}
	}
}
