using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Diagrams.Extensions.ViewModels;
using Telerik.Windows.Diagrams.Core;

namespace SortTreeLayoutShapes
{
    /// <summary>
    /// Interaction logic for Example.xaml
    /// </summary>
    public partial class Example : UserControl
    {
        Random r = new Random();
        private bool useLayoutAnimation = false;
        public Example()
        {
            InitializeComponent();
            this.BindGraphSource();
            this.LayoutDiagram();
            this.useLayoutAnimation = true;
        }

        private void BindGraphSource()
        {
            ObservableGraphSourceBase<OrgItem, OrgLink> source = new ObservableGraphSourceBase<OrgItem, OrgLink>();
            OrgItem rootItem = new OrgItem() { Label = "CEO" };
            source.AddNode(rootItem);
            for (int i = 0; i < 3; i++)
            {
                OrgItem child = new OrgItem() { Label = "Manager " + i, HeadCount = r.Next(5, 25).ToString() };
                source.AddNode(child);
                source.AddLink(new OrgLink(rootItem, child) { Content = i });
                for (int j = 0; j < 3; j++)
                {
                    OrgItem subchild = new OrgItem() { Label = "Team Lead " + i + "." + j, HeadCount = r.Next(5, 25).ToString() };
                    source.AddNode(subchild);
                    source.AddLink(new OrgLink(child, subchild) { Content = j + 3 * i });
                }
            }
            this.diagram.GraphSource = source;
        }

        private void SetSortCriteria()
        {
            if (this.optionHCButton.IsChecked == true)
            {
                this.diagram.SortCriteria = SortCriteria.HeadCount;
            }
            else if (this.optionCNButton.IsChecked == true)
            {
                this.diagram.SortCriteria = SortCriteria.ConnectionLabel;
            }
            else if (this.optioncCSPButton.IsChecked == true)
            {
                this.diagram.SortCriteria = SortCriteria.ShapePosition;
            }

            this.diagram.SortedByConnectionLabel = this.diagram.Connections.OrderBy(c => c.Content.ToString()).ToList();
            this.diagram.SortedByHeadCount = this.diagram.Connections.OrderBy(x => double.Parse((((x.Target as RadDiagramShape).DataContext as OrgItem).HeadCount.ToString()))).ToList();
            this.diagram.SortedByShapePosition = this.diagram.Connections.OrderBy(c => c.Target.Bounds.Left).ToList();
        }

        private void LayoutDiagram()
        {
            this.SetSortCriteria();

            TreeLayoutSettings settings = new TreeLayoutSettings()
            {
                TreeLayoutType = TreeLayoutType.TreeDown,
                VerticalDistance = 100,
                UnderneathHorizontalOffset = 50,
                UnderneathVerticalTopOffset = 50,
                UnderneathVerticalSeparation = 80,
                VerticalSeparation = 80,
                AnimateTransitions = this.useLayoutAnimation,
            };

            settings.Roots.Add(this.diagram.Shapes[0]);
            this.diagram.RoutingService.Router = new AStarRouter(this.diagram);

            this.diagram.DiagramLayoutComplete += (o, e) =>
            {
                this.diagram.AutoFit(new Thickness(10));
            };
            this.diagram.LayoutAsync(LayoutType.Tree, settings);
        }

        private void RadButton_Click_2(object sender, RoutedEventArgs e)
        {
            this.LayoutDiagram();
        }
    }
}
