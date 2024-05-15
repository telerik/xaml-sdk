using System;
using System.Linq;
using System.Collections.ObjectModel;

namespace ForegroundColorSelectedHoveredRow
{
    public class MyViewModel
    {
        private ObservableCollection<Club> clubs;

        public ObservableCollection<Club> Clubs
        {
            get
            {
                if (this.clubs == null)
                {
                    this.clubs = Club.GetClubs();
                }

                return this.clubs;
            }
        }
    }
}
