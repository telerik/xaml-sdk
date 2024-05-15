using System;
using System.Collections.Generic;

namespace FilteringCollectionProperties
{
	public class Person
	{
		private readonly string name;
		private readonly List<string> workingDays = new List<string>();

		public Person(string name, IEnumerable<string> workingDays)
		{
			this.name = name;
			this.workingDays.AddRange(workingDays);
		}
		
		public string Name 
		{
			get { return this.name; }
		}
		
		public IList<string> WorkingDays 
		{ 
			get { return this.workingDays; }	 
		}
		
		public static IEnumerable<Person> GetSampleData()
		{
			yield return new Person("John", new List<string>() { "Monday", "Tuesday", "Wednesday"});
			yield return new Person("Ringo", new List<string>() { "Tuesday", "Wednesday", "Thursday" });
			yield return new Person("Paul", new List<string>() { "Wednesday", "Thursday", "Friday" });
			yield return new Person("George", new List<string>() { "Thursday", "Friday", "Saturday" }); 
		}
	}
}
