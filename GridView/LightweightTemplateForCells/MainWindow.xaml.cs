using System;
using System.Linq;
using System.Windows;
using LightweightTemplateForCells;
using Telerik.Windows.Controls;
using System.Windows.Data;

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

            var converter = new MyConverter();

            for (var i = 0; i < 100; i++)
            {
                RadGridView1.Columns.Add(new GridViewDataColumn()
                {
                    DataMemberBinding = new Binding("ID") { Converter = converter, ConverterParameter = i },
                    Header = string.Format("Column{0}", i)
                });
            }

            DataContext = new MyDataContext();
        }
    }
}
