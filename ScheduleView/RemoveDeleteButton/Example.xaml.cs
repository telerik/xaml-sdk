using System.Windows.Controls;
using Telerik.Windows.Controls.ScheduleView;

namespace RemoveDeleteButton
{
    public partial class Example : UserControl
    {
        public Example()
        {
            InitializeComponent();
        }

        private void ScheduleView_ShowDialog(object sender, Telerik.Windows.Controls.ShowDialogEventArgs e)
        {
            if (e.DialogViewModel is ConfirmDialogViewModel)
            {
                e.DefaultDialogResult = false;
                e.Cancel = true;
            }
        }
    }
}
