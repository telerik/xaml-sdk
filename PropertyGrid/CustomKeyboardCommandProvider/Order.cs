using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KeyboardCommandProvider
{
   public class Order
    {
        public string ShipAddress { get; set; }
        public string ShipCountry { get; set; }
        public string ShipName { get; set; }
        public string ShipPostalCode { get; set; }
        public Employee Employee { get; set; }

    }
}
