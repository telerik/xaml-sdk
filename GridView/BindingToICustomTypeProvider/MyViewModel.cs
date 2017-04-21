using BindingToICustomTypeProvider.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Telerik.Windows.Controls;

namespace BindingToICustomTypeProvider
{
    public class MyViewModel: ViewModelBase
    {
        private ObservableCollection<Club> clubs;

        public ObservableCollection<Club> Clubs 
        {
            get 
            {
                if (this.clubs == null)
                {
                    this.clubs = this.GenerateClubs();
                }

                return this.clubs;
            }
        }

        private ObservableCollection<Club> GenerateClubs() 
        {
            ObservableCollection<Club> clubs = new ObservableCollection<Club>();

            Club.AddProperty("Name", typeof(string));
            Club.AddProperty("StadiumCapacity", typeof(int));
            Club.AddProperty("IsChampion", typeof(bool));

            Club club = new Club();
            club.SetPropertyValue("Name", "Liverpool");
            club.SetPropertyValue("StadiumCapacity", 45362);
            club.SetPropertyValue("IsChampion", false);
            clubs.Add(club);

            club = new Club();
            club.SetPropertyValue("Name", "Chelsea");
            club.SetPropertyValue("StadiumCapacity", 42055);
            club.SetPropertyValue("IsChampion", false);

            clubs.Add(club);

            return clubs;
        }
    }
}
