using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace DeferredSearching
{
    public class MyViewModel
    {

        private ObservableCollection<Player> players;

        public ObservableCollection<Player> Players
        {
            get
            {
                if (this.players == null)
                {
                    this.players = Player.GetPlayers();
                }

                return this.players;
            }
        }
    }
}
