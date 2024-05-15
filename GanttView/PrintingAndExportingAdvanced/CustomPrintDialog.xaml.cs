using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GanttView;

namespace PrintingAndExportingAdvanced
{
	/// <summary>
	/// Interaction logic for CustomPrintDialog.xaml
	/// </summary>
	public partial class CustomPrintDialog : RadWindow
	{
		private IEnumerable<BitmapSource> exportImages;

		public CustomPrintDialog(IImageExporter imageExporter)
		{
			InitializeComponent();
			this.exportImages = imageExporter.ImageInfos.ToList().Select(info => info.Export());
			var pageCollection = new ObservableCollection<PreviewPage>();
			var pageCount = 1;
			foreach (var exportImage in this.exportImages)
			{
				pageCollection.Add(new PreviewPage() { BitmapSource = exportImage, PageNumber = pageCount});
				pageCount++;
			}
			this.listBox.ItemsSource = pageCollection;
		}

		private void ButtonPrintClick(object sender, RoutedEventArgs e)
		{
			PrintingService.Print(this.exportImages);
			this.Close();
		}
	}
}
