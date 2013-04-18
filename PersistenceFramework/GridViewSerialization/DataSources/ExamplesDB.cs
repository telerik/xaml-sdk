using System;
using System.Windows.Interop;
using System.Windows.Resources;
using System.Windows;
using System.Reflection;
using System.IO;
using System.ComponentModel;
using Telerik.Windows.Controls;
using System.Xml.Linq;
using System.Linq;
using System.Collections.ObjectModel;
using System.Xml;
using System.Collections.Generic;

namespace GridViewSerialization
{
	public static class ExamplesDB
	{
        public static object GetCustomers()
        {
            return LoadCustomers();
        }

        public static ObservableCollection<Customer> GetCustomersCollection()
        {
            return LoadCustomers();
        }

        private static Uri GetResourceUri(string resource)
        {
            AssemblyName assemblyName = new AssemblyName(typeof(ExamplesDB).Assembly.FullName);
            string resourcePath = "/" + assemblyName.Name + ";component/" + resource;
            Uri resourceUri = new Uri(resourcePath, UriKind.Relative);

            return resourceUri;
        }

        private static ObservableCollection<Customer> LoadCustomers()
        {
            StreamResourceInfo resourceInfo = Application.GetResourceStream(GetResourceUri("DataSources/Customers.xml"));
            XmlReader reader = XmlReader.Create(resourceInfo.Stream);

            XElement el = XElement.Load(reader);
            XName elementName = XName.Get("Customers", "http://tempuri.org/NWindDataSet.xsd");

            ObservableCollection<Customer> customers = new ObservableCollection<Customer>();

            foreach (XElement customerElement in el.Elements(elementName))
                customers.Add(CreateCustomer(customerElement));

            return customers;
        }

        private static Customer CreateCustomer(XContainer customerElement)
        {
            Customer newCustomer = new Customer();
            XName customerID = XName.Get("CustomerID", "http://tempuri.org/NWindDataSet.xsd");
            XName companyName = XName.Get("CompanyName", "http://tempuri.org/NWindDataSet.xsd");
            XName country = XName.Get("Country", "http://tempuri.org/NWindDataSet.xsd");
            XName city = XName.Get("City", "http://tempuri.org/NWindDataSet.xsd");
            XName contactName = XName.Get("ContactName", "http://tempuri.org/NWindDataSet.xsd");
            XName boolean = XName.Get("Bool", "http://tempuri.org/NWindDataSet.xsd");

            newCustomer.CustomerID = customerElement.Element(customerID).Value;
            newCustomer.CompanyName = customerElement.Element(companyName).Value;
            newCustomer.Country = customerElement.Element(country).Value;
            newCustomer.City = customerElement.Element(city).Value;
            newCustomer.ContactName = customerElement.Element(contactName).Value;
            newCustomer.Bool = bool.Parse(customerElement.Element(boolean).Value);

            return newCustomer;
        }
	}
}
