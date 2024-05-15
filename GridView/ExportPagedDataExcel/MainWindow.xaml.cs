using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using Telerik.Windows.Controls;

namespace ExportPagedDataExcel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = Club.GetClubs();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            var pageSize = this.pager.PageSize;
            var pageIndex = this.pager.PageIndex;

            this.pager.PageIndex = 0;
            this.pager.PageSize = 0;

            string extension = "xlsx";

            SaveFileDialog dialog = new SaveFileDialog()
            {
                DefaultExt = extension,
                Filter = String.Format("{1} files (*.{0})|*.{0}|All files (*.*)|*.*", extension, "Excel"),
                FilterIndex = 1
            };

            if (dialog.ShowDialog() == true)
            {
                using (Stream stream = dialog.OpenFile())
                {
                    this.clubsGrid.ExportToXlsx(stream);
                }
            }

            this.pager.PageSize = pageSize;
            this.pager.PageIndex = pageIndex;
        }
    }
    public class Club
    {
        public Club(string name, DateTime established, int stadiumCapacity)
        {
            this.Name = name;
            this.Established = established;
            this.StadiumCapacity = stadiumCapacity;
        }
        public string Name
        {
            get;
            set;
        }
        public DateTime Established
        {
            get;
            set;
        }
        public int StadiumCapacity
        {
            get;
            set;
        }
        public static IEnumerable<Club> GetClubs()
        {
            ObservableCollection<Club> clubs = new ObservableCollection<Club>();
            clubs.Add(new Club("Liverpool", new DateTime(1892, 1, 1), 45362));
            clubs.Add(new Club("Manchester Utd.", new DateTime(1878, 1, 1), 76212));
            clubs.Add(new Club("Chelsea", new DateTime(1905, 1, 1), 42055));
            clubs.Add(new Club("Arsenal", new DateTime(1886, 1, 1), 60355));
            return clubs;
        }
    }
}
