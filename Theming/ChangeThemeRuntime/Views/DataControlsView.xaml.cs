using ChangeThemeRuntime.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;
using Telerik.Windows.Controls.MultiColumnComboBox;

namespace ChangeThemeRuntime
{
    /// <summary>
    /// Interaction logic for DataControlsView.xaml
    /// </summary>
    public partial class DataControlsView : UserControl
    {
        public DataControlsView()
        {
            InitializeComponent();

            var employees = EmployeeService.GetEmployees();

            this.multiColumnComboBox.ItemsSourceProvider = new GridViewItemsSourceProvider() { ItemsSource = employees };

            this.propertyGrid.Item = new Employee()
            {
                FirstName = "Maria",
                LastName = "Anders",
                IsMarried = true,
                Title = EmployeeService.OccupationPositions.SalesRepresentative,
                City = "Seattle",
                Country = "USA",
                Phone = "(206) 555-9857",
                Age = 24
            };

            this.collectionNavigator.Source = employees;

            var stream = Application.GetResourceStream(new Uri("/Resources/Folders.xml", UriKind.RelativeOrAbsolute)).Stream;
            var reader = XmlReader.Create(stream);
            var document = XDocument.Load(reader);
            var data = from f in document.Element("folders").Elements("folder")
                       select new TreeListViewModel(f.Attribute("Name").Value,
                                                    bool.Parse(f.Attribute("IsEmpty").Value),
                                                    DateTime.Parse(f.Attribute("CreationTime").Value, System.Globalization.CultureInfo.InvariantCulture),
                                                    f);
            this.treeListView.ItemsSource = new ObservableCollection<TreeListViewModel>(data);

            this.taskBoard.DataContext = new TaskBoardViewModel();
        }
    }
}
