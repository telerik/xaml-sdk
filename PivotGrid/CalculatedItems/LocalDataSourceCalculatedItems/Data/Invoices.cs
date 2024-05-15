using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LocalDataSourceCalculatedItems.Data
{
    public class Invoices : List<Invoice>
    {
        public Invoices() : base(Invoice.GetInvoices())
        {
        }
    }
}
