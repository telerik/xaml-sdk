using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Xml.Linq;

namespace SilverlightApplication1
{
    public partial class MainPage : UserControl
    {
        IEnumerable<Book> Books;
        public MainPage()
        {
            InitializeComponent();

            LoadData();
        }

        private void LoadData()
        {
            XDocument xdoc = XDocument.Load("books.xml");
            Books = from book in xdoc.Descendants("book")
                    select new Book
                    {
                        id = book.Attribute("id").Value,
                        Author = book.Descendants("author").First().Value,
                        Title = book.Descendants("title").First().Value,
                        Genre = book.Descendants("genre").First().Value,
                        Description = book.Descendants("description").First().Value
                    };

            this.DataContext = Books;
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            string searchText = this.SearchBox.Text;
            LocateItem(searchText);
        }

        private void LocateItem(string searchText)
        {
            var item = this.RadGridView.Items.Cast<Book>().FirstOrDefault(book =>
                book.Description.Contains(searchText) ||
                book.Author.Contains(searchText) ||
                book.Title.Contains(searchText) ||
                book.Genre.Contains(searchText)
                );

            if (item == null)
                return;

            this.RadGridView.CurrentItem = item;
            this.RadGridView.ScrollIntoView(item);
        }
    }

    public class Book
    {
        public string id { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
    }
}
