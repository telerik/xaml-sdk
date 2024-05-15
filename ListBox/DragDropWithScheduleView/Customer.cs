using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DragDropWithScheduleView
{
	public class Customer
	{
		public string Name { get; set; }
		public int ID { get; set; }

		public Customer Copy()
		{
			return new Customer
			{
				Name = this.Name,
				ID = this.ID
			};
		}
	}
}
