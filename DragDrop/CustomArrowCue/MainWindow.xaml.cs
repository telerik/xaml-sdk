using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.DragDrop;

namespace CustomArrowCue
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DragDropManager.AddDragInitializeHandler(this.border1, OnBorderDrop);
            DragDropManager.AddDropHandler(this.border2, OnBorderDrop);
        }

        private void OnBorderDrop(object sender, Telerik.Windows.DragDrop.DragEventArgs e)
        {
            MessageBox.Show("Dropped.");
        }

        private void OnBorderDrop(object sender, DragInitializeEventArgs e)
        {
            e.AllowedEffects = DragDropEffects.All;
            e.DragVisual = new TextBlock() { Text = "Dragging.." };
        }
    }
}
