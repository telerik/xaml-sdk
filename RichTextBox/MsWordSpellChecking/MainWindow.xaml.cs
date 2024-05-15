using System;
using System.Linq;
using System.Windows;
using Telerik.Windows.Documents.FormatProviders.Txt;
using WordSpellCheck;

namespace MsWordSpellChecking_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WordSpellChecker wordSpellChecker;

        public MainWindow()
        {
            InitializeComponent();

            this.wordSpellChecker = new WordSpellChecker();
            this.radRichTextBox.SpellChecker = this.wordSpellChecker;

            this.radRichTextBox.Document = new TxtFormatProvider().Import("The quik broun foxx jumpd ovur the lasy dok.");

            this.Closed += this.MainWindow_Closed;
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            this.wordSpellChecker.Dispose();
        }
    }
}