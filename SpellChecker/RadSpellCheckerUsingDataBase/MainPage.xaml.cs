using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Documents.Proofing;
using System.Globalization;

namespace RadSpellCheckerUsingDataBase
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            TextBoxSpellChecker tbSpellChecker = (TextBoxSpellChecker)ControlSpellCheckersManager.GetControlSpellChecker(typeof(TextBox));
            ((DocumentSpellChecker)tbSpellChecker.SpellChecker).AddCustomDictionary(new RadDbCustomDictionary(), new CultureInfo("en-US"));
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            RadSpellChecker.Check(this.textBox1, SpellCheckingMode.WordByWord);
        }
    }
}
