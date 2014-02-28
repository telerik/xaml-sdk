using System.Windows;
using Telerik.Windows.Controls.Gauge;
using System.Linq;

namespace LinearRadialScalesRadialScaleLabelRotationMode
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var enums = GetEnumValues();
            labelRotationMode.ItemsSource = enums;
        }

        public static RotationMode[] GetEnumValues()
        {
            var type = typeof(RotationMode);
            return (from field in type.GetFields()
            where field.IsLiteral
            select (RotationMode)field.GetValue(type)).ToArray();
        }
    }
}
