using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Media.Imaging.Tools;

namespace HandleToolCommit
{
    /// <summary>
    /// Interaction logic for RadImageEditor.xaml
    /// </summary>
    public partial class RadImageEditor : UserControl
    {
        private bool shouldCancelOnCommitting;
        private bool shouldExecuteSameToolAfterCommit;

        private ImageInfo imageInfo;

        public RadImageEditor()
        {
            InitializeComponent();

            this.imageInfo = new ImageInfo();
            this.DataContext = this.imageInfo;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ImageImporter.LoadSampleImage(this.ImageEditorUI, "RadImageEditor.png");
            this.ImageEditorUI.ImageEditor.ExecuteTool(new ResizeTool());

            this.AttachToToolCommitEvents();
            this.InitializeImageInfo();
            this.InitializeToolCommitEventsExecutionValues();
        }

        private void AttachToToolCommitEvents()
        {
            this.ImageEditorUI.ImageEditor.ToolCommitting += this.ImageEditor_ToolCommitting;
            this.ImageEditorUI.ImageEditor.ToolCommitted += this.ImageEditor_ToolCommitted;
        }

        private void InitializeImageInfo()
        {
            this.imageInfo.HeightBefore = this.imageInfo.HeightCurrent = this.ImageEditorUI.Image.Height;
            this.imageInfo.WidthBefore = this.imageInfo.WidthCurrent = this.ImageEditorUI.Image.Width;
        }

        private void InitializeToolCommitEventsExecutionValues()
        {
            this.shouldCancelOnCommitting = false;
            this.shouldExecuteSameToolAfterCommit = true;
        }

        private void ImageEditor_ToolCommitting(object sender, ToolCommittingEventArgs e)
        {
            this.imageInfo.HeightBefore = this.ImageEditorUI.Image.Height;
            this.imageInfo.WidthBefore = this.ImageEditorUI.Image.Width;

            e.Cancel = this.shouldCancelOnCommitting;

            if (e.Cancel)
            {
                // Uncomment the following line if you want to close the tool settings panel after the tool was canceled.
                //this.ImageEditorUI.ImageEditor.CancelExecuteTool();
            }
        }

        private void ImageEditor_ToolCommitted(object sender, ToolCommittedEventArgs e)
        {
            this.imageInfo.HeightCurrent = this.ImageEditorUI.Image.Height;
            this.imageInfo.WidthCurrent = this.ImageEditorUI.Image.Width;

            this.imageInfo.ExecutedTool = e.Tool.ToString();
            e.ExecuteSameToolAfterCommit = this.shouldExecuteSameToolAfterCommit;
        }

        private void CancelOnCommitting_Click(object sender, RoutedEventArgs e)
        {
            this.shouldCancelOnCommitting = true;   
        }

        private void ExecuteOnlyOnce_Click(object sender, RoutedEventArgs e)
        {
            this.shouldExecuteSameToolAfterCommit = false;
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            this.InitializeToolCommitEventsExecutionValues();
        }
    }
}
