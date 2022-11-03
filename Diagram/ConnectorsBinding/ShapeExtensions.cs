using System.Collections.ObjectModel;
using System.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Diagrams;
using Telerik.Windows.Diagrams.Core;

namespace ConnectorsBinding
{
    public class ShapeExtensions
    {
        public static ObservableCollection<ShapeConnector> GetConnectorsCollection(DependencyObject obj)
        {
            return (ObservableCollection<ShapeConnector>)obj.GetValue(ConnectorsCollectionProperty);
        }

        public static void SetConnectorsCollection(DependencyObject obj, ObservableCollection<ShapeConnector> value)
        {
            obj.SetValue(ConnectorsCollectionProperty, value);
        }

        public static readonly DependencyProperty ConnectorsCollectionProperty =
            DependencyProperty.RegisterAttached("ConnectorsCollection", typeof(ObservableCollection<ShapeConnector>), typeof(ShapeExtensions), new PropertyMetadata(OnConnectorsCollectionChanged));

        private static void OnConnectorsCollectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RadDiagramShape shape = (RadDiagramShape)d;

            if (e.OldValue != null)
            {
                shape.Connectors.Clear();
            }

            if ((ObservableCollection<ShapeConnector>)e.NewValue != null)
            {
                shape.Connectors.Clear();

                ObservableCollection<ShapeConnector> connectors = (ObservableCollection<ShapeConnector>)e.NewValue;

                foreach (ShapeConnector connector in connectors)
                {
                    shape.Connectors.Add(new RadDiagramConnector() { Name = connector.Name, Offset = connector.Position });
                }
            }
        }
    }
}
