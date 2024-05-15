using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace LocalDataSourceCalculatedItems
{
    /// <summary>
    /// Interaction logic for Example.xaml
    /// </summary>
    public partial class Example : UserControl
    {
        public Example()
        {
            InitializeComponent();
        }

        private void LocalDataSourceProvider_PrepareDescriptionForField(object sender, PrepareDescriptionForFieldEventArgs e)
        {
            if (e.DescriptionType == DataProviderDescriptionType.Group)
            {
                var description = e.Description as Telerik.Pivot.Core.PropertyGroupDescription;

                if (description != null)
                {
                    if (e.FieldInfo.DisplayName == "Salesperson")
                    {
                        var menCalculatedItem = new MenAverageSales();
                        menCalculatedItem.GroupName = "Average Sales (Men)";
                        menCalculatedItem.SolveOrder = 3;
                        description.CalculatedItems.Add(menCalculatedItem);

                        var womenCalculatedItem = new WomenAverageSales();
                        womenCalculatedItem.GroupName = "Average Sales (Women)";
                        womenCalculatedItem.SolveOrder = 1;
                        description.CalculatedItems.Add(womenCalculatedItem);
                    }
                    else if (e.FieldInfo.Name == "Country")
                    {
                        var caCalculatedItem = new CA();
                        caCalculatedItem.GroupName = "CA";
                        caCalculatedItem.SolveOrder = 2;
                        description.CalculatedItems.Add(caCalculatedItem);
                    }
                }
            }
        }
    }
}
