using System.Collections.Generic;

namespace MailMerge
{
    public class ConcreteDataObject
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string CompanyName { get; set; }

        public IList<Product> Products { get; internal set; }

        public string ProductSupportPhone { get; set; }

        public string ProductSupportPhoneAvailability { get; set; }

        public string ProductSupportEmail { get; set; }

        public string SalesRepFirstName { get; set; }

        public string SalesRepLastName { get; set; }

        public string SalesRepTitle { get; set; }
    }
}
