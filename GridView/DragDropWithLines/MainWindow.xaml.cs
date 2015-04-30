using System;
using System.Windows;

namespace DragDropWithLines
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isEnabled = true;
        public MainWindow()
        {
            InitializeComponent();
            this.radGridView.ItemsSource = MessageViewModel.Generate();
            RowReorderBehavior.SetIsEnabled(this.radGridView, isEnabled);
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            isEnabled = !isEnabled;
            RowReorderBehavior.SetIsEnabled(this.radGridView, isEnabled);
        }

      
    }
}
