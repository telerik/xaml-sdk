using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Telerik.Windows.Data;

namespace ColumnSettings
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

        private IEnumerable<EnumMemberViewModel> editTriggers;

        public IEnumerable<EnumMemberViewModel> EditTriggers
        {
            get
            {
                if (this.editTriggers == null)
                {
                    this.editTriggers = Telerik.Windows.Data.EnumDataSource.FromType<Telerik.Windows.Controls.GridView.GridViewEditTriggers>();
                }

                return this.editTriggers;
            }
        }
    }
}
