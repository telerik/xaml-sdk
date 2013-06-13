using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Diagrams;
using Telerik.Windows.Controls.Diagrams.Extensions;

namespace DiagramDesignToolBox
{
	/// <summary>
	/// Interaction logic for Example.xaml
	/// </summary>
	public partial class Example : UserControl
	{
		private FileManager fileManager;

		static Example()
		{
			var saveBinding = new CommandBinding(DiagramCommands.Save, ExecuteSave, CanExecutedSave);
			var openBinding = new CommandBinding(DiagramCommands.Open, ExecuteOpen);

			CommandManager.RegisterClassCommandBinding(typeof(Example), saveBinding);
			CommandManager.RegisterClassCommandBinding(typeof(Example), openBinding);
		}

		public Example()
		{
			InitializeComponent();

			this.Loaded += this.OnLoaded;
			this.fileManager = new FileManager(this.diagram);
			this.DataContext = new MainViewModel();
		}

		private static void CanExecutedSave(object sender, CanExecuteRoutedEventArgs e)
		{
			var owner = sender as Example;
			e.CanExecute = owner != null && owner.diagram != null && owner.diagram.Items.Count > 0;
		}

		private static void ExecuteSave(object sender, ExecutedRoutedEventArgs e)
		{
			var owner = sender as Example;
			if (owner != null)
				owner.fileManager.SaveToFile();
		}

		private static void ExecuteOpen(object sender, ExecutedRoutedEventArgs e)
		{
			var owner = sender as Example;
			if (owner != null)
			{
				owner.diagram.Clear();
				owner.fileManager.LoadFromFile();
			}
		}

		private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			SamplesFactory.StakeholderSample(this.diagram);
		}

		private void ExportToHtml(object sender, RoutedEventArgs e)
		{
			HTMLExportHelper.CreateHTMLFile(this.diagram);
		}
	}
}
