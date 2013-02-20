using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace ImageColumnFiltering
{
	/// <summary>
	/// A football club.
	/// </summary>
	public class Club
	{
		private readonly string name;

		public string Name
		{
			get 
			{ 
				return this.name; 
			}
		}

		public string Image
		{
			get 
			{ 
				return string.Format("Images/{0}.png", this.Name.ToLower()); 
			}
		}
		
		public Club(string name)
		{
			this.name = name;
		}

		public static ObservableCollection<Club> GetClubs()
		{
			ObservableCollection<Club> clubs = new ObservableCollection<Club>();

			clubs.Add(new Club("Liverpool"));
			clubs.Add(new Club("ManUtd"));
			clubs.Add(new Club("Chelsea"));
			clubs.Add(new Club("Arsenal"));

			return clubs;
		}
	}
}
