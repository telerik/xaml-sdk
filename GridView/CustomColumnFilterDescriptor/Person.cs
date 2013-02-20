using System;
using System.Collections.Generic;

namespace CustomColumnFilterDescriptor
{
	public class Person
	{
		public string Name { get; set; }
		public Days WorkingDays { get; set; }
		
		public static IEnumerable<Person> GetSampleData()
		{
			yield return new Person() { Name = "John", WorkingDays = Days.Monday | Days.Tuesday | Days.Wednesday};
			yield return new Person() { Name = "Ringo", WorkingDays = Days.Tuesday | Days.Wednesday | Days.Thursday };
			yield return new Person() { Name = "Paul", WorkingDays = Days.Wednesday | Days.Thursday | Days.Friday };
			yield return new Person() { Name = "George", WorkingDays = Days.Friday | Days.Saturday | Days.Sunday };
		}
	}
}
