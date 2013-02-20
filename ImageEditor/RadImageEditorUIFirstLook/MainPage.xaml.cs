using System;
using System.Linq;
using System.Windows.Controls;
using Telerik.Windows.Media.Imaging.Tools;

namespace RadImageEditorUIFirstLook
{
    public partial class MainPage : UserControl
    {
        public static string SampleImageFolder { get; set; }

        public MainPage()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded_1(object sender, System.Windows.RoutedEventArgs e)
        {
            ImageExampleHelper.LoadSampleImage(this.ImageEditorUI, "RadImageEditor.png");
            this.ImageEditorUI.ImageEditor.ExecuteTool(new ResizeTool());
        }
    }
}
