using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ChartView;

namespace AnnotationsProvider
{
    public partial class AnnotationCreated : UserControl
    {
        public AnnotationCreated()
        {
            InitializeComponent();
        }

        private void ChartAnnotationsProvider_AnnotationCreated(object sender, ChartAnnotationCreatedEventArgs e)
        {
            ChartAnnotationsProvider provider = sender as ChartAnnotationsProvider;
            DailyLimitationViewModel dailyContext = e.Context as DailyLimitationViewModel;
            MonthlyLimitationViewModel monthlyContext = e.Context as MonthlyLimitationViewModel;

            if (dailyContext != null)
            {
                CartesianGridLineAnnotation lineAnnotation = new CartesianGridLineAnnotation();
                lineAnnotation.Value = dailyContext.StartValue;
                Binding axisBinding = new Binding("VerticalAxis") { RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor) { AncestorType = typeof(RadCartesianChart) } };
                BindingOperations.SetBinding(lineAnnotation, CartesianGridLineAnnotation.AxisProperty, axisBinding);
                lineAnnotation.Stroke = new SolidColorBrush(Colors.Red);
                lineAnnotation.StrokeThickness = 2;
                e.Annotation = lineAnnotation;
            }
            else if (monthlyContext != null)
            {
                CartesianMarkedZoneAnnotation markedZoneAnnotation = new CartesianMarkedZoneAnnotation();
                markedZoneAnnotation.HorizontalFrom = monthlyContext.StartMonth;
                markedZoneAnnotation.HorizontalTo = monthlyContext.EndMonth;
                markedZoneAnnotation.VerticalFrom = monthlyContext.StartValue;
                markedZoneAnnotation.VerticalTo = monthlyContext.EndValue;
                markedZoneAnnotation.Fill = new SolidColorBrush(Colors.Green);
                e.Annotation = markedZoneAnnotation;
            }
        }
    }
}
