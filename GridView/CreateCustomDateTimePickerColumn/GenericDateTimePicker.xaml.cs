using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CreateCustomDateTimePickerColumn
{
    /// <summary>
    /// Interaction logic for GenericDateTimePicker.xaml
    /// </summary>
    public partial class GenericDateTimePicker : UserControl
    {
        public static readonly DependencyProperty SelectedDateTimeProperty =
           DependencyProperty.Register("SelectedDateTime", typeof(DateTime?), typeof(GenericDateTimePicker), new PropertyMetadata(null));

        public GenericDateTimePicker()
        {
            InitializeComponent();
        }

        public DateTime? SelectedDateTime
        {
            get
            {
                return (DateTime?)this.GetValue(SelectedDateTimeProperty);
            }
            set
            {
                this.SetValue(SelectedDateTimeProperty, value);
            }
        }
    }
}
