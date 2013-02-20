using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using LightweightTemplateForCells;
using Telerik.Windows.Controls;

namespace SilverlightApplication1
{
    public partial class MainPage : UserControl
    {
        public MainPage()
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
