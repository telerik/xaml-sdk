using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Diagrams;

namespace CustomConnectorsTool
{
    /// <summary>
    /// Interaction logic for AddConnectorControl.xaml
    /// </summary>
    public partial class AddConnectorControl : UserControl
    {
        private Brush validationFailBrush = new SolidColorBrush(Colors.Red);
        private Brush validationSuccessBrush = new SolidColorBrush(Colors.Blue);

        public RadDiagramShape Shape
        {
            get { return (RadDiagramShape)GetValue(ShapeProperty); }
            set { SetValue(ShapeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Shape.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShapeProperty =
            DependencyProperty.Register("Shape", typeof(RadDiagramShape), typeof(AddConnectorControl), null);        

        public AddConnectorControl()
        {
            InitializeComponent();
            this.Loaded += (o, e) =>
                {
                    this.validationBox.Text = string.Empty;
                    this.xBox.Text = "0";
                    this.yBox.Text = "0";
                };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.Shape == null)
            {
                this.validationBox.Text = "Selected Item is not a Shape!";
                this.validationBox.Foreground = validationFailBrush;
                return;
            }

            double xPos = 0, yPos = 0;
            if (!double.TryParse(this.xBox.Text, out xPos) || !double.TryParse(this.yBox.Text, out yPos) ||
                xPos < 0 || xPos > 1 || yPos < 0 || yPos > 1)
            {
                this.validationBox.Foreground = validationFailBrush;
                this.validationBox.Text = "Incorrect X or Y !";
                return;
            }
            this.validationBox.Foreground = validationSuccessBrush;
            this.validationBox.Text = "Connector Added Successfully !";

            RadDiagramConnector connector = new RadDiagramConnector();
            connector.Offset = new Point(xPos, yPos);
            connector.Name = "CustomConnector" + connector.GetHashCode();
            this.Shape.Connectors.Add(connector);
        }        
    }
}
