using System.IO;
using System.Windows;

namespace DragAndDrop
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void CloudUpload_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, true))
            {
                string[] droppedFilePaths = e.Data.GetData(DataFormats.FileDrop, true) as string[];

                foreach (var path in droppedFilePaths)
                {
                    FileInfo fileInfo = new FileInfo(path);
                    FileStream fileStream = fileInfo.OpenRead();
                    this.CloudUpload.AddFile(fileInfo.Name, fileStream, true);
                }
            }
        }
    }
}
