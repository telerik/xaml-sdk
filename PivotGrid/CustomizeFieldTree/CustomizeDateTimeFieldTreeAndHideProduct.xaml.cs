using System;
using System.Linq;
using System.Windows.Controls;
using Telerik.Pivot.Core.Fields;

namespace CustomizeFieldTree
{
    /// <summary>
    /// Interaction logic for CustomizeDateTimeFieldTreeAndHideProduct.xaml
    /// </summary>
    public partial class CustomizeDateTimeFieldTreeAndHideProduct : UserControl
    {
        public CustomizeDateTimeFieldTreeAndHideProduct()
        {
            InitializeComponent();
        }

        private void LocalDataSourceFieldDescriptionsProvider_ContainerNodeAdded_1(object sender, ContainerNodeEventArgs e)
        {
            if (e.ContainerNode.Name == "Date")
            {
                foreach (var containerNode in e.ContainerNode.Children)
                {
                    FieldInfoNode fin = containerNode as FieldInfoNode;
                    if (containerNode.Name == "Date.Month")
                    {
                        //hide Month from the DateTime tree
                        (fin.FieldInfo as PropertyFieldInfo).AutoGenerateField = false;
                    }
                    else if (containerNode.Name == "Date.Hour")
                    {
                        //Show Hour from the DateTime tree
                        (fin.FieldInfo as PropertyFieldInfo).AutoGenerateField = true;
                    }
                    else if (containerNode.Name == "Date")
                    {
                        //Hide Date from the DateTime tree
                        (fin.FieldInfo as PropertyFieldInfo).AutoGenerateField = false;
                    }
                }
            }

            if (e.ContainerNode.Name == "Product")
            {
                e.Cancel = true;
            }
        }
    }
}
