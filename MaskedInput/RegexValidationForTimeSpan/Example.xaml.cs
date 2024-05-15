using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace RegexValidationForTimeSpan_WPF
{
    public partial class Example : UserControl
    {
        public Example()
        {
            InitializeComponent();            
            this.comboBox.ItemsSource = new ObservableCollection<RegexTimeSpan>()
            {
                new RegexTimeSpan(){ Input="#2:#2",Regex = @"^[\s\d]?[\s\d]:[s\d][s\d]$" },
                new RegexTimeSpan(){ Input="#4:#2",Regex = @"^[\s\d]?[\s\d]?[\s\d]?[\s\d]:[s\d][s\d]$" },
                new RegexTimeSpan(){ Input="#4:#4",Regex = @"^[\s\d]?[\s\d]?[\s\d]?[\s\d]:[s\d][s\d][s\d][s\d]$" },
            };

        }
        private void mask_ValueChanged(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            string[] parts = this.mask.Value.Split(new char[] { ':' });
            var hours = double.Parse(parts[0].ToString());
            var minutes = double.Parse(parts[1].ToString());

            TimeSpan fromHours = TimeSpan.FromHours(hours);
            TimeSpan fromMinutes = TimeSpan.FromMinutes(minutes);
            TimeSpan subTotal = fromHours + fromMinutes;
            this.timeSpanPicker.Value = subTotal;

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.mask.Value != "")
            {
                this.mask.ClearCommand.Execute(null);
            }
        }
    }
}
