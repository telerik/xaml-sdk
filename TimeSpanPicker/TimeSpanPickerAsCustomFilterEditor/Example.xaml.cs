using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using Telerik.Windows.Controls;

namespace TimeSpanPickerAsCustomFilterEditor
{
    /// <summary>
    /// Interaction logic for Example.xaml
    /// </summary>
    public partial class Example : UserControl
    {
        public Example()
        {
            InitializeComponent();

            ObservableCollection<Employee> employees = new ObservableCollection<Employee>();
            employees.Add(new Employee("Maria Anders", "Alfreds Futterkiste", "Sales Representative", new TimeSpan(2, 10, 20)));
            employees.Add(new Employee("Ana Trujillo", "Ana Trujillo Emparedados y helados", "Owner", new TimeSpan(3, 10, 20)));
            employees.Add(new Employee("Antonio Moreno", "Antonio Moreno Taqueria", "Owner", new TimeSpan(4, 10, 20)));
            employees.Add(new Employee("Thomas Hardy", "Around the Horn", "Sales Representative", new TimeSpan(5, 10, 20)));
            employees.Add(new Employee("Hanna Moos", "Blauer See Delikatessen", "Sales Representative", new TimeSpan(6, 10, 20)));
            employees.Add(new Employee("Frederique Citeaux", "Blondesddsl pere et fils", "Marketing Manager", new TimeSpan(7, 10, 20)));
            employees.Add(new Employee("Martin Sommer", "Bolido Comidas preparadas", "Owner", new TimeSpan(8, 10, 20)));
            employees.Add(new Employee("Laurence Lebihan", "Bon app'", "Owner", new TimeSpan(9, 10, 20)));
            employees.Add(new Employee("Elizabeth Lincoln", "Bottom-Dollar Markets", "Accounting manager", new TimeSpan(10, 10, 20)));
            employees.Add(new Employee("Victoria Ashworth", "B's Beverages", "Sales representative", new TimeSpan(11, 10, 20)));

            this.radDataFilter.Source = employees;
        }

        private void radDataFilter_EditorCreated(object sender, Telerik.Windows.Controls.Data.DataFilter.EditorCreatedEventArgs e)
        {
            switch (e.ItemPropertyDefinition.PropertyName)
            {
                case "Title":
                    ((RadComboBox)e.Editor).ItemsSource = this.GetTitles();
                    break;
            }
        }

        public List<string> GetTitles()
        {
            return new List<string>() { "Owner", "Sales Representative", "Sales Associate", "Sales Agent", "Marketing Assistent" };
        }
    }
}
