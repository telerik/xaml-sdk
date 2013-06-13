using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace EnableOnlyDropInsideItem.ViewModels
{
    public class RadTreeViewSampleData
    {
        public RadTreeViewSampleData()
        {
            this.InitializeLeaguesDataSource();
        }
        public ObservableCollection<League> LeaguesDataSource
        {
            get;
            set;
        }
        private void InitializeLeaguesDataSource()
        {
            this.LeaguesDataSource = new ObservableCollection<League>();
            League l;
            Division d;
            this.LeaguesDataSource.Add(l = new League("League A"));
            l.Divisions.Add((d = new Division("Division A")));
            d.Teams.Add(new Team("Team I"));
            d.Teams.Add(new Team("Team II"));
            d.Teams.Add(new Team("Team III"));
            d.Teams.Add(new Team("Team IV"));
            d.Teams.Add(new Team("Team V"));
            l.Divisions.Add((d = new Division("Division B")));
            d.Teams.Add(new Team("Team Blue"));
            d.Teams.Add(new Team("Team Red"));
            d.Teams.Add(new Team("Team Yellow"));
            d.Teams.Add(new Team("Team Green"));
            d.Teams.Add(new Team("Team Orange"));
            l.Divisions.Add((d = new Division("Division C")));
            d.Teams.Add(new Team("Team East"));
            d.Teams.Add(new Team("Team West"));
            d.Teams.Add(new Team("Team North"));
            d.Teams.Add(new Team("Team South"));
            this.LeaguesDataSource.Add(l = new League("League B"));
            l.Divisions.Add((d = new Division("Division A")));
            d.Teams.Add(new Team("Team 1"));
            d.Teams.Add(new Team("Team 2"));
            d.Teams.Add(new Team("Team 3"));
            d.Teams.Add(new Team("Team 4"));
            d.Teams.Add(new Team("Team 5"));
            l.Divisions.Add((d = new Division("Division B")));
            d.Teams.Add(new Team("Team Diamond"));
            d.Teams.Add(new Team("Team Heart"));
            d.Teams.Add(new Team("Team Club"));
            d.Teams.Add(new Team("Team Spade"));
            l.Divisions.Add((d = new Division("Division C")));
            d.Teams.Add(new Team("Team Alpha"));
            d.Teams.Add(new Team("Team Beta"));
            d.Teams.Add(new Team("Team Gamma"));
            d.Teams.Add(new Team("Team Delta"));
            d.Teams.Add(new Team("Team Epsilon"));
        }
    }
}
