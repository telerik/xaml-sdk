using System.Windows;

namespace PdfElementsEditor
{
    /// <summary>
    /// Interaction logic for TextEditDialog.xaml
    /// </summary>
    public partial class TextEditDialog : Window
    {
        private TextFragmentModel model;

        public TextFragmentModel Model
        {
            get
            {
                return model;
            }
            set
            {
                model = value;
                this.DataContext = model;
            }
        }

        public TextEditDialog()
        {
            InitializeComponent();
            this.Model = new TextFragmentModel();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
    public class TextFragmentModel
    {
        public string Text { get; set; }
        public double FontSize { get; set; }
        public string FontName { get; set; }
    }
}
