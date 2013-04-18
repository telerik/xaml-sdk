using System;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace CustomColumnFilterDescriptor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
			InitializeComponent();

			// We create a generic flags enum column. You can use it for any flags enum type.
			var flagsEnumColumn = new FlagsEnumColumn<Days>()
			{
				DataMemberBinding = new Binding("WorkingDays")
			};
			this.radGridView.Columns.Add(flagsEnumColumn);

			// Bind the grid to some sample data.
			this.radGridView.ItemsSource = Person.GetSampleData();
		}

		private void OnDistinctValuesLoading(object sender, Telerik.Windows.Controls.GridView.GridViewDistinctValuesLoadingEventArgs e)
		{
			// Here we override the distinct values by supplying all individual flags enum members.
			// If we do not do this you will see something like this in the distinct values list:
			// -- "Monday, Tuesday, Wednesday"
			// -- "Tuesday, Wednesday, Thursday"
			// Overriding e.ItemsSource makes the distinct values of the form:
			// -- "Monday"
			// -- "Tuesday"
			// -- ...
			if (e.Column == this.radGridView.Columns["WorkingDays"])
			{
				e.ItemsSource = typeof(Days).GetFields()
					.Where(f => f.IsLiteral)
					.Select(f => f.GetValue(typeof(Days)));
			}
		}
    }
}
