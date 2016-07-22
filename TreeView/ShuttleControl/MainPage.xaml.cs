using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace ShuttleControl
{
    public partial class MainPage : UserControl
    {
        public ShuttleTreeViewModel SourceTreeDataContext { get; set; }
        public ShuttleTreeViewModel TargetTreeDataContext { get; set; }

        public MainPage()
        {
            InitializeComponent();
            this.sourceTree.DataContext = SourceTreeDataContext = this.GetSourceTreeModel();
            this.targetTree.DataContext = TargetTreeDataContext = this.GetTargetTreeModel();
        }
        private void RadButton_Click_MoveItems(object sender, RoutedEventArgs e)
        {
            ShuttleUtilities.MoveSelectedItems(SourceTreeDataContext, TargetTreeDataContext);
        }

        private void RadButton_Click_MoveAllItems(object sender, RoutedEventArgs e)
        {
            ShuttleUtilities.MoveAllItems(SourceTreeDataContext, TargetTreeDataContext);
        }

        private void RadButton_Click_ReturnAllItems(object sender, RoutedEventArgs e)
        {
            ShuttleUtilities.MoveAllItems(TargetTreeDataContext, SourceTreeDataContext);
        }

        private void RadButton_Click_ReturnItems(object sender, RoutedEventArgs e)
        {
            ShuttleUtilities.MoveSelectedItems(TargetTreeDataContext, SourceTreeDataContext);
        }

        private ShuttleTreeViewModel GetSourceTreeModel()
        {
            ShuttleTreeViewModel sourceTreeModel = new ShuttleTreeViewModel();

            NodeViewModel metadataParentNode = new NodeViewModel("Metadata", null, false, true, "../../Images/Computer.png", false);
            NodeViewModel dbo = new NodeViewModel("dbo", metadataParentNode, false, true, "../../Images/Drive.png", false);
            metadataParentNode.Children.Add(dbo);

            sourceTreeModel.TableNode = new NodeViewModel("Tables", dbo, false, true, "../../Images/Folder.png", false);
            sourceTreeModel.ViewNode = new NodeViewModel("Views", dbo, false, true, "../../Images/Folder.png", false);

            dbo.Children.Add(sourceTreeModel.TableNode);
            dbo.Children.Add(sourceTreeModel.ViewNode);

            for (int i = 0; i < 15; i++)
            {
                NodeViewModel currentTableFolderNode = new NodeViewModel("Item " + i, sourceTreeModel.TableNode, false, true, "../../Images/File.png");
                sourceTreeModel.TableNode.Children.Add(currentTableFolderNode);
            }

            for (int i = 0; i < 15; i++)
            {
                NodeViewModel currentViewFolderNode = new NodeViewModel("Item " + i, sourceTreeModel.ViewNode, false, true, "../../Images/File.png");
                sourceTreeModel.ViewNode.Children.Add(currentViewFolderNode);
            }
            sourceTreeModel.TreeViewData.Add(metadataParentNode);
            return sourceTreeModel;
        }

        private ShuttleTreeViewModel GetTargetTreeModel()
        {
            ShuttleTreeViewModel sourceTreeModel = new ShuttleTreeViewModel();

            NodeViewModel selectedObjects = new NodeViewModel("Selected Objects", null, false, true, "../../Images/Computer.png", false);
            NodeViewModel dbo = new NodeViewModel("dbo", selectedObjects, false, true, "../../Images/Drive.png", false);
            sourceTreeModel.TableNode = new NodeViewModel("Tables", dbo, false, true, "../../Images/Folder.png", false);
            sourceTreeModel.ViewNode = new NodeViewModel("Views", dbo, false, true, "../../Images/Folder.png", false);
            selectedObjects.Children.Add(dbo);
            dbo.Children.Add(sourceTreeModel.TableNode);
            dbo.Children.Add(sourceTreeModel.ViewNode);

            sourceTreeModel.TreeViewData.Add(selectedObjects);
            return sourceTreeModel;
        }
    }
}
