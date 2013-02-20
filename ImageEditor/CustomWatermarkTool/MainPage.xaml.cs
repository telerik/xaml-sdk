using System;
using System.Linq;
using System.Windows.Controls;
using Telerik.Windows.Media.Imaging.Tools;

namespace CustomWatermarkTool
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            
            ImageExampleHelper.LoadSampleImage(this.imageEditor, "RadImageEditor.png");
            this.imageEditor.ImageEditor.ExecuteTool(this.LayoutRoot.Resources["WatermarkTool"] as ITool);
        }
    }
}
