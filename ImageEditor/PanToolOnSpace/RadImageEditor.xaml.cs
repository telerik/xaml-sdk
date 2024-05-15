using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Media.Imaging.Tools;

namespace PanToolOnSpace
{
    /// <summary>
    /// Interaction logic for RadImageEditor.xaml
    /// </summary>
    public partial class RadImageEditor : UserControl
    {
        private ITool previousTool;
        public RadImageEditor()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ImageImporter.LoadSampleImage(this.ImageEditorUI, "RadImageEditor.png");
        }

        private void OnImageEditorUIPreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Space && !e.IsRepeat)
            {
                this.previousTool = this.ImageEditorUI.ImageEditor.ExecutingTool;
                this.ImageEditorUI.ImageEditor.CancelExecuteTool();
                this.ImageEditorUI.ImageEditor.ExecuteTool(new PanTool());
                e.Handled = true;
            }
            else if (e.IsRepeat)
            {
                e.Handled = true;
            }
        }

        private void OnImageEditorUIPreviewKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Space)
            {
                this.ImageEditorUI.ImageEditor.CancelExecuteTool();
                if (this.previousTool != null)
                {
                    this.ImageEditorUI.ImageEditor.ExecuteTool(this.previousTool);
                }
            }
        }
    }
}
