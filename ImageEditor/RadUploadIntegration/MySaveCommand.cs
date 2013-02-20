using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using Telerik.Windows.Media.Imaging.ImageEditorCommands;
using Telerik.Windows.Media.Imaging;
using Telerik.Windows.Media.Imaging.FormatProviders;
using System.IO;

namespace RadUploadIntegration
{
    public class MySaveCommand : ImageEditorCommandBase
    {
        public MySaveCommand(RadImageEditor owner)
            : base(owner)
        {
        }

        protected override void ExecuteOverride(object parameter)
        {
			this.Owner.CommitTool();
            RadBitmap image = this.Owner.Image;

            RadUpload upload = new RadUpload();
            MemoryStream stream = new MemoryStream();

            PngFormatProvider png = new PngFormatProvider();
            png.Export(image, stream);
            stream.Seek(0, SeekOrigin.Begin);

            RadUploadSelectedFile file = new RadUploadSelectedFile(stream, "image" + image.GetHashCode() + ".png");
            upload.TargetFolder = "UploadFolder";
            upload.UploadServiceUrl = "SampleUploadHandler.ashx";
            upload.CurrentSession.SelectedFiles.Add(file);

            upload.PrepareSelectedFilesForUpload();
            upload.UploadFinished += new RoutedEventHandler(upload_UploadFinished);

            upload.StartUpload();
        }

        void upload_UploadFinished(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Finished!");
        }

        protected override bool CanExecuteOverride(object parameter)
        {
            return base.CanExecuteOverride(parameter);
        }
    }

}