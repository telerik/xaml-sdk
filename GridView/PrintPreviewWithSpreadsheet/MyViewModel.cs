using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace PrintPreviewWithSpreadsheet
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
