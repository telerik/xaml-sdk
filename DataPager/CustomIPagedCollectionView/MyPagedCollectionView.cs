using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using Telerik.Windows.Data;

namespace CustomIPagedCollectionView
{
    public class MyPagedCollectionView : IPagedCollectionView, IEnumerable, INotifyPropertyChanged, INotifyCollectionChanged
    {
        public void SetCount(int count)
        {
            this.count = count;
        }

        #region IPagedCollectionView Members

        public bool CanChangePage
        {
            get { return true; }
        }

        readonly bool isPageChanging = false;
        public bool IsPageChanging
        {
            get { return isPageChanging; }
        }

        int count;
        public int ItemCount
        {
            get { return count; }
        }

        public bool MoveToFirstPage()
        {
            return MoveToPage(0);
        }

        public bool MoveToLastPage()
        {
            var page = PageCount - 1;
            return MoveToPage(page);
        }

        public bool MoveToNextPage()
        {
            var page = pageIndex + 1;
            return MoveToPage(page);
        }

        public bool MoveToPage(int pageIndex)
        {
            if ((pageIndex <= (PageCount - 1)) && pageIndex >= 0)
            {
                this.pageIndex = pageIndex;

                OnPropertyChanged("PageIndex");

                if (CollectionChanged != null)
                {
                    CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                }

                return true;
            }

            return false;
        }

        public bool MoveToPreviousPage()
        {
            var page = pageIndex - 1;
            return MoveToPage(page);
        }

        public event EventHandler<EventArgs> PageChanged;

        public event EventHandler<PageChangingEventArgs> PageChanging;

        int pageIndex;
        public int PageIndex
        {
            get { return pageIndex; }
        }

        int pageSize;
        public int PageSize
        {
            get
            {
                return pageSize;
            }
            set
            {
                pageSize = value;
            }
        }

        public int TotalItemCount
        {
            get { return count; }
        }

        #endregion


        public int PageCount
        {
            get { return TotalItemCount / PageSize; }
        }


        #region IEnumerable Members

        public IEnumerator GetEnumerator()
        {
            return (from i in Enumerable.Range(PageIndex * PageSize, PageSize) select i).GetEnumerator();
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #region INotifyCollectionChanged Members

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        #endregion
    }
}
