using System;
using System.Linq;
using System.Windows;
using Telerik.Windows.Controls;
using ComboboxColumn;
using Telerik.Windows.Controls.GridView;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
private int maxID = 0;
        public MainWindow()
        {
            InitializeComponent();

            ((GridViewComboBoxColumn)this.pilotsGrid.Columns["Country"]).ItemsSource = Country.GetCountries();
            maxID = Country.GetCountries().Count - 1;

            this.pilotsGrid.DistinctValuesLoading += PilotsGridDistinctValuesLoading;
            this.pilotsGrid.CellEditEnded += new EventHandler<GridViewCellEditEndedEventArgs>(RadGridView1_CellEditEnded);
		}

		void RadGridView1_CellEditEnded(object sender, GridViewCellEditEndedEventArgs e)
		{
			RadComboBox combo = e.Cell.ChildrenOfType<RadComboBox>().FirstOrDefault();
            if (combo != null)
            {
                ObservableCollection<Country> comboItems = combo.ItemsSource as ObservableCollection<Country>;

		        string  textEntered = e.Cell.ChildrenOfType<RadComboBox>().First().Text;

                bool result = comboItems.Contains(comboItems.Where(x=>x.Name == textEntered).FirstOrDefault());
                if (!result)
		        {
                    comboItems.Add(new Country(textEntered, ++maxID));

                    ((Pilot)e.Cell.DataContext).CountryId = maxID;
		        }
            }
		}

        void PilotsGridDistinctValuesLoading(object sender, Telerik.Windows.Controls.GridView.GridViewDistinctValuesLoadingEventArgs e)
        {
            e.ItemsSource = Country.GetCountries().Select(x=>x.Id);
        }
    }
}
