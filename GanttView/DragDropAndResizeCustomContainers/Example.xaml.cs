using System.Windows.Controls;

namespace DragDropAndResizeCustomContainers
{
    public partial class Example : UserControl
    {
        public Example()
        {
            InitializeComponent();
            this.DataContext = new MyViewModel();
        }
    }
}
