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
using Telerik.Windows.Controls.Gauge;

namespace LinearRadialScalesRadialScaleLabelRotationMode
{
    public partial class MainPage : UserControl
    {
        public MainPage()
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
