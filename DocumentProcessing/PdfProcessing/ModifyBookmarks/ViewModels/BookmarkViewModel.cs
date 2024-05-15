using System;
using System.Collections.Generic;
using Telerik.Windows.Data;
using Telerik.Windows.Documents.Fixed.Model;
using Telerik.Windows.Documents.Fixed.Model.ColorSpaces;
using Telerik.Windows.Documents.Fixed.Model.Navigation;

namespace ModifyBookmarks.ViewModels
{
    public class BookmarkViewModel : ViewModelBase
    {
        private int pageNumber = 1;
        private bool isExpanded;
        private double? top;
        private double? left;
        private double? zoom;
        private BookmarkItemStyles textStyle;
        private string title = "New Bookmark";
        private int textColorR;
        private int textColorG;
        private int textColorB;

        public BookmarkViewModel()
        {
        }

        internal BookmarkViewModel(int pageNumber, BookmarkItem bookmark)
        {
            this.Initialize(pageNumber, bookmark);
        }

        public int PageNumber
        {
            get
            {
                return this.pageNumber;
            }
            set
            {
                if (this.pageNumber != value)
                {
                    this.pageNumber = value;
                    this.OnPropertyChanged("PageNumber");
                }
            }
        }

        public bool IsExpanded
        {
            get
            {
                return this.isExpanded;
            }
            set
            {
                if (this.isExpanded != value)
                {
                    this.isExpanded = value;
                    this.OnPropertyChanged("IsExpanded");
                }
            }
        }

        public double? Top
        {
            get
            {
                return this.top;
            }
            set
            {
                if (this.top != value)
                {
                    this.top = value;
                    this.OnPropertyChanged("Top");
                }
            }
        }

        public double? Left
        {
            get
            {
                return this.left;
            }
            set
            {
                if (this.left != value)
                {
                    this.left = value;
                    this.OnPropertyChanged("Left");
                }
            }
        }

        public double? Zoom
        {
            get
            {
                return this.zoom;
            }
            set
            {
                if (this.zoom != value)
                {
                    this.zoom = value;
                    this.OnPropertyChanged("Zoom");
                }
            }
        }

        public BookmarkItemStyles TextStyle
        {
            get
            {
                return this.textStyle;
            }
            set
            {
                if (this.textStyle != value)
                {
                    this.textStyle = value;
                    this.OnPropertyChanged("TextStyle");
                }
            }
        }

        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                if (this.title != value)
                {
                    this.title = value;
                    this.OnPropertyChanged("Title");
                }
            }
        }

        public int TextColorR
        {
            get
            {
                return this.textColorR;
            }
            set
            {
                if (this.textColorR != value)
                {
                    this.textColorR = Math.Min(255, value);
                    this.textColorR = Math.Max(0, this.textColorR);
                    this.OnPropertyChanged("TextColorR");
                }
            }
        }

        public int TextColorG
        {
            get
            {
                return this.textColorG;
            }
            set
            {
                if (this.textColorG != value)
                {
                    this.textColorG = Math.Min(255, value);
                    this.textColorG = Math.Max(0, this.textColorG);
                    this.OnPropertyChanged("TextColorG");
                }
            }
        }

        public int TextColorB
        {
            get
            {
                return this.textColorB;
            }
            set
            {
                if (this.textColorB != value)
                {
                    this.textColorB = Math.Min(255, value);
                    this.textColorB = Math.Max(0, this.textColorB);
                    this.OnPropertyChanged("TextColorB");
                }
            }
        }

        public BookmarkItem BookmarkItem { get; internal set; }

        internal void UpdateBookmarkItem(RadFixedPage page)
        {
            if (this.BookmarkItem == null)
            {
                this.BookmarkItem = new BookmarkItem(this.Title, new Location());
            }

            BookmarkItem currentBookmark = this.BookmarkItem;

            byte red = Convert.ToByte(this.TextColorR);
            byte green = Convert.ToByte(this.TextColorG);
            byte blue = Convert.ToByte(this.TextColorB);

            currentBookmark.TextColor = new RgbColor(red, green, blue);
            currentBookmark.Title = this.Title;
            currentBookmark.IsExpanded = this.IsExpanded;
            currentBookmark.TextStyle = this.TextStyle;

            Location destination = (Location)currentBookmark.Destination;

            destination.Top = this.Top;
            destination.Left = this.Left;
            destination.Zoom = this.Zoom;
            destination.Page = page;
        }

        private void Initialize(int pageNumber, BookmarkItem bookmark)
        {
            Location location = bookmark.Destination as Location;

            this.Top = location.Top;
            this.Left = location.Left;
            this.Zoom = location.Zoom;
            this.IsExpanded = bookmark.IsExpanded;
            this.PageNumber = pageNumber;
            this.TextColorR = bookmark.TextColor.R;
            this.TextColorG = bookmark.TextColor.G;
            this.TextColorB = bookmark.TextColor.B;
            this.TextStyle = bookmark.TextStyle;
            this.Title = bookmark.Title;
            this.BookmarkItem = bookmark;
        }
    }
}
