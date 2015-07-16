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

namespace CreateCustomDateTimePickerColumn
{
    public partial class GenericDateTimePickerSL : UserControl
    {
         public static readonly DependencyProperty SelectedDateTimeProperty =
           DependencyProperty.Register("SelectedDateTime", typeof(DateTime?), typeof(GenericDateTimePickerSL), new PropertyMetadata(null));

         public GenericDateTimePickerSL()
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

        private void HandlePickersSelectionChanged()
        {
            if (this.Calendar.SelectedDate != null && this.TimePicker.SelectedTime != null)
            {
                this.SelectedDateTime = this.Calendar.SelectedDate + this.TimePicker.SelectedTime;
            }
        }

        private void TimePicker_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangedEventArgs e)
        {
            this.HandlePickersSelectionChanged();
        }

        private void Calendar_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangedEventArgs e)
        {
            this.HandlePickersSelectionChanged();
        }
    }
}
