using System.Collections.ObjectModel;
using System.Linq;
using Telerik.Windows.Controls;
using System.Collections.Generic;
using System;
using System.Windows.Data;
using Telerik.Windows.Data;
using System.Xml.Linq;
using System.Xml;
using System.Data;

namespace VariousDataSources
{
    public class MyModel : ViewModelBase
    {
        public MyModel()
        {
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
                        return Club.GetClubs();
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
                case BindingType.DataTable:
                    {
                        return GetDataTable().DefaultView;
                    }
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

        private static DataTable GetDataTable()
        {
            // Here we create a DataTable with four columns.
            DataTable table = new DataTable();
            table.Columns.Add("Dosage", typeof(int));
            table.Columns.Add("Drug", typeof(string));
            table.Columns.Add("Patient", typeof(string));
            table.Columns.Add("Date", typeof(DateTime));

            // Here we add five DataRows.
            table.Rows.Add(25, "Indocin", "David", DateTime.Now);
            table.Rows.Add(50, "Enebrel", "Sam", DateTime.Now);
            table.Rows.Add(10, "Hydralazine", "Christoff", DateTime.Now);
            table.Rows.Add(21, "Combivent", "Janet", DateTime.Now);
            table.Rows.Add(100, "Dilantin", "Melanie", DateTime.Now);
            return table;
        }
    }
}
