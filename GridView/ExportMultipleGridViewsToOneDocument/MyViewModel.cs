using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Telerik.Windows.Controls;

namespace ExportMultipleGridViewsToOneDocument
{
    public class MyViewModel : ViewModelBase
    {
        private ObservableCollection<Club> clubs;
        private ObservableCollection<Player> players;

        IEnumerable<Telerik.Windows.Data.EnumMemberViewModel> _positions;

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

        public IEnumerable<Telerik.Windows.Data.EnumMemberViewModel> Positions
        {
            get
            {
                if (_positions == null)
                {
                    _positions = Telerik.Windows.Data.EnumDataSource.FromType<Position>();
                }

                return _positions;
            }
        }
    }
}
