using System.Collections.ObjectModel;
using System.Linq;
using Telerik.Windows.Controls;
using System.Collections.Generic;
using System;
using System.Windows.Data;
using Telerik.Windows.Data;
using System.ComponentModel;
using System.Xml.Linq;
using System.Xml;

namespace VariousDataSources
{
    public class MyModel : ViewModelBase
    {
        private readonly NorthwindEntities northwind;

        public MyModel()
        {
            this.northwind = new NorthwindEntities();
        }

        object _data;
        public object Data
        {
            get
            {
                if (_data == null)
                {
                    _data = GetData();
                }

                return _data;
            }
        }

        EnumMemberViewModel _type;
        public EnumMemberViewModel Type
        {
            get
            {
                return _type;
            }
            set
            {
                if (!object.Equals(_type, value))
                {
                    _type = value;

                    _data = null;

                    OnPropertyChanged("Type");
                    OnPropertyChanged("Data");
                }
            }
        }

        private object GetData()
        {
            if (Type == null)
                return null;

            switch ((BindingType)Type.Value)
            {
                case BindingType.ObservableCollection:
                    {
                        return this.northwind.Customers;
                    }
                case BindingType.ICollectionView:
                    {
                        var cvs = new CollectionViewSource();
                        cvs.Source = this.northwind.Order_Details;
                        return cvs.View;
                    }
                case BindingType.DynamicData:
                    {
                        var data = new ObservableCollection<MyDataRow>();
                        for (int i = 0; i < 100; i++)
                        {
                            var row = new MyDataRow();

                            for (int j = 0; j < 10; j++)
                            {
                                row[string.Format("Column{0}", j)] = string.Format("Cell {0} {1}", i, j);
                            }

                            data.Add(row);
                        }

                        return data;
                    }
#if !SILVERLIGHT
                case BindingType.DataTable:
                    {
                        return GetDataTable();
                    }
#endif
                case BindingType.Xml:
                    {
                        return GetXmlData();
                    }
            }

            return null;
        }

        IEnumerable<Telerik.Windows.Data.EnumMemberViewModel> _bindingTypes;
        public IEnumerable<Telerik.Windows.Data.EnumMemberViewModel> BindingTypes
        {
            get
            {
                if (_bindingTypes == null)
                {
                    _bindingTypes = Telerik.Windows.Data.EnumDataSource.FromType<BindingType>();

                    Type = _bindingTypes.FirstOrDefault();
                }

                return _bindingTypes;
            }
        }

#if !SILVERLIGHT
        private static System.Data.DataTable GetDataTable()
        {
            var tableCustomers = new System.Data.DataTable();
            tableCustomers.Columns.Add(new System.Data.DataColumn("CustomerID", typeof(string)));
            tableCustomers.Columns.Add(new System.Data.DataColumn("CompanyName", typeof(string)));
            tableCustomers.Columns.Add(new System.Data.DataColumn("ContactName", typeof(string)));
            tableCustomers.Columns.Add(new System.Data.DataColumn("City", typeof(string)));
            tableCustomers.Columns.Add(new System.Data.DataColumn("Country", typeof(string)));

            foreach (Customer c in new NorthwindEntities().Customers)
            {
                var row = tableCustomers.NewRow();
                row["CustomerID"] = c.CustomerID;
                row["CompanyName"] = c.CompanyName;
                row["ContactName"] = c.ContactName;
                row["City"] = c.City;
                row["Country"] = c.Country;

                tableCustomers.Rows.Add(row);
            }
            return tableCustomers;
        }
#endif
        private static object GetXmlData()
        {
            var doc = XDocument.Parse(@"<?xml version=""1.0"" encoding=""utf-8"" ?>
<Products>
  <Product>
    <ID>1</ID>
    <Name>ASP.NET</Name>
    <Url>http://www.telerik.com/products/aspnet-ajax.aspx</Url>
  </Product>
  <Product>
    <ID>2</ID>
    <Name>WinForms</Name>
    <Url>http://www.telerik.com/products/winforms.aspx</Url>
  </Product>
  <Product>
    <ID>3</ID>
    <Name>Silverlight</Name>
    <Url>http://www.telerik.com/products/silverlight.aspx</Url>
  </Product>
  <Product>
    <ID>4</ID>
    <Name>WPF</Name>
    <Url>http://www.telerik.com/products/wpf.aspx</Url>
  </Product>
  <Product>
    <ID>5</ID>
    <Name>Reporting</Name>
    <Url>http://www.telerik.com/products/reporting.aspx</Url>
  </Product>
  <Product>
    <ID>6</ID>
    <Name>Sitefinity ASP.NET CMS</Name>
    <Url>http://www.telerik.com/products/sitefinity.aspx</Url>
  </Product>
  <Product>
    <ID>7</ID>
    <Name>OpenAccess ORM</Name>
    <Url>http://www.telerik.com/products/orm.aspx</Url>
  </Product>
</Products>
");
            return new ObservableCollection<dynamic>(from element in doc.Descendants("Product") select new MyDataRow(ToDictionary(element)));
        }

        public static IDictionary<string, object> ToDictionary(XElement element)
        {
            var dict = new Dictionary<string, object>();
            foreach (var e in element.Elements())
            {
                dict.Add(e.Name.LocalName, e.Value);
            }

            return dict;
        }
    }

    public enum BindingType
    {
        [Description("Dynamic Data")]
        DynamicData,

        [Description("ObservableCollection")]
        ObservableCollection,

        [Description("ICollectionView")]
        ICollectionView,

        [Description("Xml")]
        Xml,
#if !SILVERLIGHT
        [Description("Data Table")]
        DataTable,
#endif
    }
}
