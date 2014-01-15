using System.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Media.Imaging.Tools;


namespace Localization
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
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
