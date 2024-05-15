using System;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;

namespace PreserveSelectedItemScrollPosition
{
    /// <summary>
    /// A football club.
    /// </summary>
    public class Club : ViewModelBase
    {
        private string name;
        private DateTime established;
        private int stadiumCapacity;

        public string Name
        {
            get { return this.name; }
            set
            {
                if (value != this.name)
                {
                    this.name = value;
                    this.OnPropertyChanged("Name");
                }
            }
        }

        public DateTime Established
        {
            get { return this.established; }
            set
            {
                if (value != this.established)
                {
                    this.established = value;
                    this.OnPropertyChanged("Established");
                }
            }
        }

        public int StadiumCapacity
        {
            get { return this.stadiumCapacity; }
            set
            {
                if (value != this.stadiumCapacity)
                {
                    this.stadiumCapacity = value;
                    this.OnPropertyChanged("StadiumCapacity");
                }
            }
        }
        

        public Club()
        {

        }

        public Club(string name, DateTime established, int stadiumCapacity)
        {
            this.name = name;
            this.established = established;
            this.stadiumCapacity = stadiumCapacity;
        }
        

        public override string ToString()
        {
            return this.Name;
        }

        public static RadObservableCollection<Club> GetClubs()
        {
            RadObservableCollection<Club> clubs = new RadObservableCollection<Club>();
            Club club;

            for (int i = 0; i < 500; i++)
            {
                club = new Club("Club " + i, new DateTime(1892, 1, 1), i);
                clubs.Add(club);
            }
           

         
            return clubs;
        }
    }
}
