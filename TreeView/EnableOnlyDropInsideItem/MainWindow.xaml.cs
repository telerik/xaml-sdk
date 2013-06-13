using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls.TreeView;
using Telerik.Windows.DragDrop;

namespace EnableOnlyDropInsideItem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DragDropManager.AddDragOverHandler(xTreeView, OnDragOver,true);
        }

        private void OnDragOver(object sender, Telerik.Windows.DragDrop.DragEventArgs e)
        {
            var options = DragDropPayloadManager.GetDataFromObject(e.Data, TreeViewDragDropOptions.Key) as TreeViewDragDropOptions;
            if (options.DropPosition != Telerik.Windows.Controls.DropPosition.Inside)
            {
                options.DropPosition = Telerik.Windows.Controls.DropPosition.Inside;
                options.UpdateDragVisual();
            }
        }
    }
}
