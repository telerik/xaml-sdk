using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace Office2016InspiredRibbonView_WPF.Appearance
{
    public class Office2016ResourceExtension : MarkupExtension
    {
        public Resources Resource { get; set; }

        public Office2016ResourceExtension()
        {

        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
            {
                throw new ArgumentNullException("serviceProvider");
            }

            string propertyPath;
            if (Office2016Palette.TryGetResource(this.Resource, out propertyPath))
            {
                Binding binding = new Binding(propertyPath)
                {
                    Source = Office2016Palette.Palette,
                    Converter = new ColorToSolidColorBrushConverter(),
                    Mode = BindingMode.OneWay
                };
                return binding.ProvideValue(serviceProvider);
            }

            return null;
        }
    }
}
