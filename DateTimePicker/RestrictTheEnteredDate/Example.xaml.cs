using System.Windows.Controls;

namespace RestrictTheEnteredDate
{
    /// <summary>
    /// Interaction logic for Example.xaml
    /// </summary>
    public partial class Example : UserControl
    {
        public Example()
        {
            InitializeComponent();
        }
        private void RadDateTimePicker_ParseDateTimeValue(object sender, Telerik.Windows.Controls.ParseDateTimeEventArgs args)
        {
            if (args.IsParsingSuccessful)
            {
                var dateTimePicker = (Telerik.Windows.Controls.RadDateTimePicker)sender;
                if (args.Result.Value.Date < dateTimePicker.SelectableDateStart)
                {
                    args.Result = dateTimePicker.SelectableDateStart;
                }
                if (args.Result.Value.Date > dateTimePicker.SelectableDateEnd)
                {
                    args.Result = dateTimePicker.SelectableDateEnd;
                }
                args.Handled = true;
            }
        }
    }
}
