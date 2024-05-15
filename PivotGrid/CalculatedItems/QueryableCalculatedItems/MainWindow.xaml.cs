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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Pivot.Core;
using Telerik.Pivot.Queryable;

namespace QueryableCalculatedItems
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                (this.Resources["QueryableProvider"] as Telerik.Pivot.Queryable.QueryableDataProvider).Source = new NorthwindDBEntities().Orders;
            }
            catch
            {

            }
        }

        private void QueryableDataProvider_PrepareDescriptionForField(object sender, Telerik.Pivot.Core.PrepareDescriptionForFieldEventArgs e)
        {
            if (e.DescriptionType == DataProviderDescriptionType.Group && e.FieldInfo.DisplayName == "ShipCountry")
            {
                var description = e.Description as QueryablePropertyGroupDescription;

                if (description != null)
                {
                    var americanOrdersCalculatedItem = new AmericanOrdersSum();
                    americanOrdersCalculatedItem.GroupName = "American Orders Sum";
                    description.CalculatedItems.Add(americanOrdersCalculatedItem);

                    var europeanOrdersCalculatedItem = new EuropeanOrdersSum();
                    europeanOrdersCalculatedItem.GroupName = "European Orders Sum";
                    description.CalculatedItems.Add(europeanOrdersCalculatedItem);
                }
            }
            else if (e.DescriptionType == DataProviderDescriptionType.Group && e.FieldInfo.DisplayName == "OrderDate - Quarter")
            {
                var description = e.Description as QueryableDateTimeGroupDescription;

                if (description != null)
                {
                    var firstHalfYearCalculatedItem = new OrdersFirstHalfYear();
                    firstHalfYearCalculatedItem.GroupName = "First Half Year";
                    description.CalculatedItems.Add(firstHalfYearCalculatedItem);

                    var secondHalfYearCalculatedItem = new OrdersSecondHalfYear();
                    secondHalfYearCalculatedItem.GroupName = "Second Half Year";
                    description.CalculatedItems.Add(secondHalfYearCalculatedItem);
                }
            }
        }
    }
}
