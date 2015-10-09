using System;
using System.Linq;
using System.Windows;

namespace DiagramCustomPaste
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.diagram.GraphSource = new DiagramGraphSource();
        }

    }
}
