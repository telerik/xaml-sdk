using Telerik.Windows.Controls.ChartView;

namespace AnnotationsProvider
{
    public class CustomAnnotationsDescriptorSelector : ChartAnnotationDescriptorSelector
    {
        public ChartAnnotationDescriptor DailyDescriptor { get; set; }

        public ChartAnnotationDescriptor MonthlyDescriptor { get; set; }

        public override ChartAnnotationDescriptor SelectDescriptor(ChartAnnotationsProvider provider, object context)
        {
            DailyLimitationViewModel dailyVM = context as DailyLimitationViewModel;
            MonthlyLimitationViewModel monthlyVM = context as MonthlyLimitationViewModel;

            if (dailyVM != null)
            {
                return this.DailyDescriptor;
            }
            else if (monthlyVM != null)
            {
                return this.MonthlyDescriptor;
            }

            return null;
        }
    }
}
