using System;
using System.Linq;
using System.Windows.Controls;

namespace ChangeScaleFactor
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            pdfViewer.DocumentChanged += pdfViewer_DocumentChanged;
        }

        void pdfViewer_DocumentChanged(object sender, EventArgs e)
        {
            pdfViewer.Commands.FixedDocumentViewer.ScaleFactor = 0.5;    
        }

        private void tbCurrentPage_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                if (e.Key == System.Windows.Input.Key.Enter)
                {
                    textBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                }
            }
        }

        private void tbFind_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                this.pdfViewer.Commands.FindCommand.Execute(this.tbFind.Text);
                this.btnPrev.Visibility = System.Windows.Visibility.Visible;
                this.btnNext.Visibility = System.Windows.Visibility.Visible;
            }
        }
    }
}
