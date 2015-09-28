using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Diagrams.Core;

namespace MultipleRootsInSingleTreeLayout
{
    /// <summary>
    /// Interaction logic for Example.xaml
    /// </summary>
    public partial class Example : UserControl
    {
        TreeLayoutSettings settings = new TreeLayoutSettings() { TreeLayoutType = TreeLayoutType.TreeDown, VerticalSeparation = 100 };

        public Example()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.SetLayoutRoots();

            RadDiagramShape dummy = new RadDiagramShape();
            this.diagram.Items.Add(dummy);
            List<IConnection> dummyConnections = new List<IConnection>();

            foreach (var item in this.settings.Roots)
            {
                RadDiagramConnection connection = new RadDiagramConnection();
                connection.Source = dummy;
                connection.Target = item;
                this.diagram.Items.Add(connection);
                dummyConnections.Add(connection);
            }
            settings.Roots.Clear();
            settings.Roots.Add(dummy);
            this.diagram.Layout(LayoutType.Tree, settings);

            dummyConnections.ForEach(x => this.diagram.Items.Remove(x));
            this.diagram.Items.Remove(dummy);
            this.diagram.AutoFit();
        }

        private void SetLayoutRoots()
        {
            this.settings.Roots.Clear();
            foreach (string item in this.radListBox.SelectedItems)
            {
                int index = this.radListBox.Items.IndexOf(item);
                this.settings.Roots.Add(this.diagram.Shapes[index]);
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (this.routingCheckBox.IsChecked == true)
            {
                this.diagram.RouteConnections = true;
                this.diagram.RoutingService.Router = new AStarRouter(this.diagram);
                this.diagram.Connections.ForEach(conn => conn.ConnectionPoints.Clear());
                this.diagram.Connections.ForEach(conn => conn.Update());
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (this.routingCheckBox.IsChecked == false)
            {
                this.diagram.RouteConnections = false;
                this.diagram.Connections.ForEach(conn => conn.ConnectionPoints.Clear());
                this.diagram.Connections.ForEach(conn => conn.Update());
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.SetLayoutRoots();
            this.diagram.Layout(LayoutType.Tree, settings);
            this.diagram.AutoFit();
        }

        private void radListBox_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> list = new List<string>();
            this.diagram.Shapes.ForEach(sh => list.Add(sh.Content.ToString()));
            this.radListBox.ItemsSource = list;
            this.radListBox.SelectedItems.Add((this.radListBox.Items[0]));
            this.radListBox.SelectedItems.Add((this.radListBox.Items[1]));
        }
    }
}
