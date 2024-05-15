using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;

namespace Thumbnails
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
        public static readonly DependencyProperty ColumnWidthProperty =
               DependencyProperty.Register("ColumnWidth", typeof(double), typeof(MainWindow), new PropertyMetadata(ColumnWidthChanged));
        public double ColumnWidth
        {
            get { return (double)GetValue(ColumnWidthProperty); }
            set { SetValue(ColumnWidthProperty, value); }
        }

        private void UncheckButton(RadToggleButton btn)
        {
            if (btn.IsChecked == true)
            {
                btn.IsChecked = false;
            }
        }

        private void TbCurrentPage_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
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

        private void ChangePageNavigationPanelVisibility(object sender, RoutedEventArgs e)
        {
            Storyboard expand = this.Resources["Expand"] as Storyboard;
            Storyboard collapse = this.Resources["Collapse"] as Storyboard;

            if (this.PageButton.IsChecked == true && expand != null)
            {
                expand.Begin();
            }
            if (this.PageButton.IsChecked == false && collapse != null)
            {
                collapse.Begin();
                UncheckButton(this.PageButton);
            }
        }

        private void CollapseButton_Click(object sender, RoutedEventArgs e)
        {
            Storyboard collapse = this.Resources["Collapse"] as Storyboard;

            if (this.PageButton.IsChecked == true && collapse != null)
            {
                collapse.Begin();
                UncheckButton(this.PageButton);
            }
        }

        private static void ColumnWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as MainWindow;
            control.containerColumn.Width = new GridLength((double)e.NewValue);
        }
    }
}
