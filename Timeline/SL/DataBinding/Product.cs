using System;
using System.Collections.Generic;

namespace DataBinding
{

	public class Product
	{
		public IEnumerable<Item> Data { get; set; }

		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
	}
}
