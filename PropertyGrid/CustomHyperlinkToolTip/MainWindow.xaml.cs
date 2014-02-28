using System;
using System.Linq;
using System.Windows;
using CustomHyperlinkToolTip;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            rpg.Item = new Employee()
                {
                    FirstName = "Nancy",
                    Salary = 3500,
                    BirthDate = new DateTime(1878, 1, 1)
                };
        }
    }
}
