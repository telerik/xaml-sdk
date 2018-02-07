using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Telerik.Windows.Controls;

namespace MVVM_VirtualGrid
{
    public class MyViewModel: ViewModelBase
    {
        private MyDataProvider dataProvider;

        public MyDataProvider DataProvider 
        {
            get 
            {
                if (this.dataProvider == null)
                {
                    this.dataProvider = new MyDataProvider(Club.GetClubs());
                }

                return this.dataProvider;
            }
        }
    }
}
