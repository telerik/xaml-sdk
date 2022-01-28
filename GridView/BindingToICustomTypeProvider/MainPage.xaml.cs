using System.Windows.Controls;
using System.Windows.Data;
using Telerik.Windows.Controls;

namespace BindingToICustomTypeProvider
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();

            this.radGridView.Columns.Add(
                new GridViewDataColumn()
                {
                    DataMemberBinding = new Binding("Name"),
                    Header = "Name"
                });

            this.radGridView.Columns.Add(
                new GridViewDataColumn()
                {
                    DataType = typeof(string),
                    DataMemberBinding = new Binding("Stadium.Name"),
                    Header = "Stadium"
                });

            this.radGridView.Columns.Add(
                new GridViewDataColumn()
                {
                    DataType = typeof(string),
                    DataMemberBinding = new Binding("Players[GK][0].Name"),
                    Header = "First Goalkeeper"
                });
        }
    }
}
