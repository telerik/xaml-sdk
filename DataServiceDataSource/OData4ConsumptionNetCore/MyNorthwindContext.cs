using ODataWebExperimental.Northwind.Model;
using System;

namespace OData4Consumption
{
	public class MyNorthwindContext : NorthwindEntities
	{
		public MyNorthwindContext() : 
			base(new Uri("http://services.odata.org/V4/Northwind/Northwind.svc/", UriKind.Absolute))
		{ }
    }
}
