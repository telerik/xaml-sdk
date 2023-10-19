using System.Windows;
using System.Windows.Data;

namespace ValidationWithIDataErrorInfo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RadDataForm_AutoGeneratingField(object sender, Telerik.Windows.Controls.Data.DataForm.AutoGeneratingFieldEventArgs e)
        {
            e.DataField.DataMemberBinding.ValidatesOnDataErrors = true;
            e.DataField.DataMemberBinding.NotifyOnValidationError = true;
            e.DataField.DataMemberBinding.UpdateSourceTrigger = UpdateSourceTrigger.Default;
        }
    }
}
