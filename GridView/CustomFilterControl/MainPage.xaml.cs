using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace CustomFilteringControl
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            this.DataContext = CustomFilteringControl.Club.GetClubs();
        }
      
    }
    public class Club
    {
        public Club(string name, DateTime established, int stadiumCapacity)
        {
            this.Name = name;
            this.Established = established;
            this.StadiumCapacity = stadiumCapacity;
        }
        public string Name
        {
            get;
            set;
        }
        public DateTime? Established
        {
            get;
            set;
        }
        public int StadiumCapacity
        {
            get;
            set;
        }
        public static IEnumerable<Club> GetClubs()
        {
            ObservableCollection<Club> clubs = new ObservableCollection<Club>();
            clubs.Add(new Club("Liverpool", new DateTime(1892, 1, 1), 45362));
            clubs.Add(new Club("Manchester Utd.", new DateTime(1878, 1, 1), 76212));
            clubs.Add(new Club("Chelsea", new DateTime(1905, 1, 1), 42055));
            clubs.Add(new Club("Arsenal", new DateTime(1886, 1, 1), 60355));
            return clubs;
        }
    }
}
