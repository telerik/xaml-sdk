using System.Collections.Generic;

namespace DataBinding
{
	public class Product
	{
		public int Value1 { get; set; }
		public int Value2 { get; set; }
		public IEnumerable<int> Ints { get; set; }
		public IEnumerable<Item> Items { get; set; }
	}
}
