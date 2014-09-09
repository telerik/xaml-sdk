using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace CreateCustomDateTimePickerColumn
{
	public class Club
	{
		public Club(string name, DateTime established, int stadiumCapacity)
		{
			this.Name = name;
			this.Established = established;
			this.StadiumCapacity = stadiumCapacity;
		}

		public String Name { get; set; }

		public DateTime? Established { get; set; }

		public int StadiumCapacity { get; set; }

		public static IEnumerable<Club> GetClubs()
		{
			ObservableCollection<Club> clubs = new ObservableCollection<Club>();
			clubs.Add(new Club("Liverpool", new DateTime(1892, 1, 1, 13, 35, 15), 45362));
			clubs.Add(new Club("Manchester Utd.", new DateTime(1878, 1, 1, 18, 45, 25), 76212));
			clubs.Add(new Club("Chelsea", new DateTime(1905, 1, 1, 23, 45, 35), 42055));
			clubs.Add(new Club("Arsenal", new DateTime(1886, 1, 1, 4, 55, 45), 60355));
			return clubs;
		}
	}
}