using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls.LayoutControl;

namespace AddCustomElementInToolBox
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            LayoutControlHierarchicalNodeProxy buttonToolBoxProxy = new LayoutControlHierarchicalNodeProxy();
            buttonToolBoxProxy.Header = "Button";
            buttonToolBoxProxy.OriginalItemType = typeof(Button);
            buttonToolBoxProxy.OriginalItem = new Button() { Content = "ToolBox Button" };

            LayoutControlHierarchicalNodeProxy textBoxToolBoxProxy = new LayoutControlHierarchicalNodeProxy();
            textBoxToolBoxProxy.Header = "TextBox";
            textBoxToolBoxProxy.OriginalItemType = typeof(TextBox);
            textBoxToolBoxProxy.OriginalItem = new TextBox() { Text = "ToolBox TextBox" };

            this.toolBoxView.NewItems.Add(buttonToolBoxProxy);
            this.toolBoxView.NewItems.Add(textBoxToolBoxProxy);
        }
    }
}
