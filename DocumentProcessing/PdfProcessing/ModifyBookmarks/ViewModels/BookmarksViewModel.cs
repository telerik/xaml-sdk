using System;
using System.Collections.Generic;
using System.Windows.Input;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Data;
using Telerik.Windows.Documents.Fixed.Model;
using Telerik.Windows.Documents.Fixed.Model.Collections;
using Telerik.Windows.Documents.Fixed.Model.Navigation;

namespace ModifyBookmarks.ViewModels
{
    public class BookmarksViewModel : ViewModelBase
    {
        private readonly RadFixedDocument document;
        private IEnumerable<object> itemsToDelete;
        private IEnumerable<EnumMemberViewModel> bookmarkItemStyles;

        public BookmarksViewModel()
        {
            this.document = ImportExportDocument.ImportDocument();
            this.Bookmarks = new ObservableItemCollection<BookmarkViewModel>();
            this.ExportCommand = new DelegateCommand(OnExportCommandExecuted);
            this.RowEditEndedCommand = new DelegateCommand(OnRowEditEndedExecuted);
            this.DeleteComamnd = new DelegateCommand(OnDeleteExecuted);

            this.LoadBookmarksFromPdfDocument();

            this.Bookmarks.ItemChanged += Bookmarks_ItemChanged;
        }

        public ICommand ExportCommand { get; private set; }

        public ICommand RowEditEndedCommand { get; private set; }

        public ICommand DeleteComamnd { get; private set; }

        public ObservableItemCollection<BookmarkViewModel> Bookmarks { get; private set; }

        public IEnumerable<EnumMemberViewModel> BookmarkItemStyles
        {
            get
            {
                if (this.bookmarkItemStyles == null)
                {
                    this.bookmarkItemStyles = EnumDataSource.FromType(typeof(BookmarkItemStyles));
                }

                return this.bookmarkItemStyles;
            }
        }

        private void OnExportCommandExecuted(object obj)
        {
            ImportExportDocument.ExportDocument(document);
        }

        private void OnRowEditEndedExecuted(object obj)
        {
            GridViewRowEditEndedEventArgs e = obj as GridViewRowEditEndedEventArgs;

            BookmarkViewModel bookmarkViewModel = e.NewData as BookmarkViewModel;

            if (e.EditAction == GridViewEditAction.Cancel)
            {
                return;
            }
            else if (e.EditOperationType == GridViewEditOperationType.Insert)
            {
                RadFixedPage page = this.document.Pages[bookmarkViewModel.PageNumber - 1];
                bookmarkViewModel.UpdateBookmarkItem(page);

                this.document.Bookmarks.Add(bookmarkViewModel.BookmarkItem);
            }
            else if (e.EditOperationType == GridViewEditOperationType.Edit)
            {
                RadFixedPage page = this.document.Pages[bookmarkViewModel.PageNumber - 1];

                BookmarkViewModel viewModel = e.EditedItem as BookmarkViewModel;
                viewModel.UpdateBookmarkItem(page);
            }
        }

        private void OnDeleteExecuted(object obj)
        {
            GridViewDeletingEventArgs e = obj as GridViewDeletingEventArgs;

            e.Cancel = true;

            this.itemsToDelete = e.Items;

            RadWindow.Confirm("Are you sure?", this.OnDeleteConfirmation);
        }

        private void OnDeleteConfirmation(object sender, WindowClosedEventArgs args)
        {
            bool shouldDelete = args.DialogResult.HasValue ? args.DialogResult.Value : false;
            if (shouldDelete)
            {
                this.DeleteBookmarks();
            }
        }

        private void DeleteBookmarks()
        {
            foreach (object bookmark in this.itemsToDelete)
            {
                BookmarksCollection bookmarks = this.document.Bookmarks;
                BookmarkViewModel bookmarkViewModel = (BookmarkViewModel)bookmark;
                BookmarkItem bookmarkToRemove = bookmarkViewModel.BookmarkItem;

                this.IterateAndRemoveChildren(bookmarkToRemove, bookmarks);
            }

            this.LoadBookmarksFromPdfDocument();
        }

        private void IterateAndRemoveChildren(BookmarkItem bookmarkToRemove, BookmarksCollection bookmarks)
        {
            foreach (BookmarkItem bookmarkItem in bookmarks)
            {
                if (bookmarkItem == bookmarkToRemove)
                {
                    bookmarks.Remove(bookmarkItem);
                    break;
                }

                this.IterateAndRemoveChildren(bookmarkToRemove, bookmarkItem.Children);
            }
        }

        private void Bookmarks_ItemChanged(object sender, ItemChangedEventArgs<BookmarkViewModel> e)
        {
            int pageNumber = Math.Min(e.Item.PageNumber, this.document.Pages.Count);
            pageNumber = Math.Max(1, pageNumber);

            e.Item.PageNumber = pageNumber;
        }

        private void LoadBookmarksFromPdfDocument()
        {
            this.Bookmarks.Clear();
            this.LoadChildBookmarks(this.document.Bookmarks);
        }

        private void LoadChildBookmarks(BookmarksCollection bookmarks)
        {
            foreach (BookmarkItem bookmarkItem in bookmarks)
            {
                this.Bookmarks.Add(this.ToBookmarkViewModel(bookmarkItem));

                this.LoadChildBookmarks(bookmarkItem.Children);
            }
        }

        private BookmarkViewModel ToBookmarkViewModel(BookmarkItem bookmark)
        {
            int pageNumber = this.document.Pages.IndexOf(bookmark.Destination.Page) + 1;
            BookmarkViewModel bookmarkViewModel = new BookmarkViewModel(pageNumber, bookmark);

            return bookmarkViewModel;
        }
    }
}
