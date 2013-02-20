using System;
using System.Linq;

namespace CustomIPagedCollectionView
{
    public class MyDataContext
    {
        MyPagedCollectionView view;
        public MyPagedCollectionView View
        {
            get
            {
                if (view == null)
                {
                    view = new MyPagedCollectionView() { PageSize = 10 };
                    view.SetCount(25000000);
                }

                return view;
            }
        }
    }
}
