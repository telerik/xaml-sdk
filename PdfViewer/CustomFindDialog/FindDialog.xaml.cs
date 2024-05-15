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
using Telerik.Windows.Controls; 
using Telerik.Windows.Documents.Fixed.UI.Dialogs;

namespace CustomFindDialog
{ 
    public partial class FindDialog : RadWindow, IFindDialog
    {
        FindDialogViewModel findDialogViewModel;

        public FindDialog()
        {
            InitializeComponent();
        }

        public void ShowDialog(FindDialogContext context)
        {
            this.Show();
            this.TextBoxFind.Text = context.FixedDocumentViewer.GetSelectedText();
            this.findDialogViewModel = new FindDialogViewModel(context);
            this.DataContext = this.findDialogViewModel;
            this.TextBoxFind.Focus();
        }

        private void Dialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Escape)
            {
                this.Close();
            }
        }
    }
}
