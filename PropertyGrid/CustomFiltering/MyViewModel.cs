using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Telerik.Windows.Controls;

namespace CustomFiltering
{
    public class MyViewModel : ViewModelBase
    {
        private Club champion;
        public Club Champion
        {
            get
            {
                if (this.champion == null)
                {
                    this.champion = new Club("Liverpool", new DateTime(1892, 1, 1), 45362);

                }
                return this.champion;
            }
            set
            {
                if (this.champion != value)
                {
                    this.champion = value;
                    this.OnPropertyChanged("Champion");
                }
            }
        }
    }
}
