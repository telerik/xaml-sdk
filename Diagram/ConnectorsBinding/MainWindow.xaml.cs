using System.Windows;

namespace ConnectorsBinding
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.diagram.GraphSource = new GraphSourceViewModel(); 
        }
    }
}
