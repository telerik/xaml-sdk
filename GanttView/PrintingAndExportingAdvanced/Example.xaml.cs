using System.Windows.Controls;
using Telerik.Windows.Controls.GanttView;

namespace PrintingAndExportingAdvanced
{
	/// <summary>
	/// Interaction logic for Example.xaml
	/// </summary>
	public partial class Example : UserControl
	{
		public Example()
		{
			InitializeComponent();
			this.DataContext = new PrintingAndExportingViewModel(this.BeginExporting);
		}

		private void BeginExporting(ImageExportSettings settings)
		{
			using (var export = this.gantt.ExportingService.BeginExporting(settings))
			{
				var printPreviewDialog = new CustomPrintDialog(export);
				printPreviewDialog.Show();
			}
		}
	}
}
