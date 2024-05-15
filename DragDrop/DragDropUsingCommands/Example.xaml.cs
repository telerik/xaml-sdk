using System.Windows.Controls;

namespace DragDropUsingCommands
{
    public partial class Example : UserControl
    {
        public Example()
        {
            InitializeComponent();

            this.DataContext = new ViewModel();
        }
    }
}
