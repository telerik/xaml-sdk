using System;
using System.Linq;
using System.Windows;
using Telerik.Windows.Data;
using System.Collections;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Xml.Linq;
using System.Collections.Generic;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IEnumerable<Book> Books;

        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            Uri uri = new Uri("books.xml", UriKind.Relative);
            System.Windows.Resources.StreamResourceInfo info = Application.GetResourceStream(uri);

            XDocument xdoc = XDocument.Load(info.Stream);

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
