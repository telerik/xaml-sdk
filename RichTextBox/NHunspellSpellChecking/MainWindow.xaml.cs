using System;
using System.IO;
using System.Linq;
using System.Windows;
using Telerik.Windows.Documents.FormatProviders.Txt;

namespace NHunspellSpellChecking
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly string AffFilePath = @"NHunspellSpellChecking;component/ThirdPartyLibrary/Dictionaries/main.aff";
        private static readonly string DicFilePath = @"NHunspellSpellChecking;component/ThirdPartyLibrary/Dictionaries/main.dic";

        private HunspellSpellChecker hunspellSpellChecker;

        public MainWindow()
        {
            InitializeComponent();

            using (Stream affFile = Application.GetResourceStream(new Uri(AffFilePath, UriKind.RelativeOrAbsolute)).Stream)
            using (Stream dicFile = Application.GetResourceStream(new Uri(DicFilePath, UriKind.RelativeOrAbsolute)).Stream)
            {
                this.hunspellSpellChecker = new HunspellSpellChecker(affFile, dicFile);
                this.radRichTextBox.SpellChecker = this.hunspellSpellChecker;
            }

            this.radRichTextBox.Document = new TxtFormatProvider().Import("The quik broun foxx jumpd ovur the lasy dok.");

            this.Closed += this.MainWindow_Closed;
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            this.hunspellSpellChecker.Dispose();
        }
    }
}