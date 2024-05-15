using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Media.Imaging.Tools;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Localization
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            LocalizationManager.Manager = new LocalizationManager()
            {
                ResourceManager = RadImageEditorResources.ResourceManager
            };

            InitializeComponent();

            ImageExampleHelper.LoadSampleImage(this.ImageEditorUI, "RadImageEditor.png");
            this.ImageEditorUI.ImageEditor.ExecuteTool(new ResizeTool());
        }
    }
}
