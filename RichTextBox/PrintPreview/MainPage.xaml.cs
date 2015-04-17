using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Documents.FormatProviders.Xaml;
using Telerik.Windows.Documents.UI;

namespace PrintPreviewUsingRichTextBox
{
    public partial class MainPage : UserControl
    {
        RadWindow window = new RadWindow();

        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Grid grid = new Grid();
            RowDefinition rowDefinition = new RowDefinition();
            rowDefinition.Height = GridLength.Auto;
            grid.RowDefinitions.Add(rowDefinition);
            grid.RowDefinitions.Add(new RowDefinition());

            StackPanel panel = new StackPanel();
            panel.Orientation = Orientation.Horizontal;
            panel.SetValue(Grid.RowProperty, 0);

            InsertButton("Print", btnPrint_Click, panel);
            InsertButton("Close", btnClose_Click, panel);

            grid.Children.Add(panel);
            
            XamlFormatProvider provider = new XamlFormatProvider();
            string documentContent = provider.Export(this.radRichTextBox.Document);

            RadRichTextBox rtb = new RadRichTextBox();
            rtb.Document = provider.Import(documentContent);
            rtb.IsReadOnly = true;
            rtb.IsFocusable = false;
            rtb.IsSelectionEnabled = false;
            rtb.IsContextMenuEnabled = false;
            rtb.IsSelectionMiniToolBarEnabled = false;
            rtb.SetValue(Grid.RowProperty, 1);

            grid.Children.Add(rtb);

            window.Height = 100;
            window.Width = 100;
            this.window.WindowState = WindowState.Maximized;
            this.window.Content = grid;
            this.window.Show();
        }

        private void InsertButton(string buttonText, RoutedEventHandler buttonEventHandler, Panel panel)
        {
            RadButton btn = new RadButton();
            btn.Content = buttonText;
            btn.Click += buttonEventHandler;
            btn.Width = 150;
            btn.Height = 20;
            btn.Margin = new Thickness(5);
            panel.Children.Add(btn);
        }

        void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.window.Close();
        }

        void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            var printSettings = new PrintSettings();
            printSettings.PrintMode = PrintMode.Native;
            this.radRichTextBox.Print(printSettings);
        }
    }
}
