using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Data;
using ValidationSummaryOutsideDataForm;

namespace ValidationSummaryOutsideDataForm_SL
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();
			this.clubsGrid.RowValidating += ClubsGrid_RowValidating;
		}

		private void ClubsGrid_RowValidating(object sender, GridViewRowValidatingEventArgs e)
		{
			this.ValidationSummary.Errors.Clear();

			var club = e.Row.DataContext as Club;
			if (string.IsNullOrEmpty(club.Name) || club.Name.Length < 5)
			{
				this.ValidationSummary.Errors.Add(new ErrorInfo()
				{
					SourceFieldDisplayName = "Name",
					ErrorContent = "Name is required and must be at least five characters!"
				});
				e.IsValid = false;
			}

			if (club.StadiumCapacity < 0)
			{
				this.ValidationSummary.Errors.Add(new ErrorInfo()
				{
					SourceFieldDisplayName = "StadiumCapacity",
					ErrorContent = "StadiumCapacity must be positive!"
				});
				e.IsValid = false;
			}
		}

		private void RadButton_Click(object sender, RoutedEventArgs e)
		{
			var vm = this.clubsGrid.DataContext as MyViewModel;
			var item = vm.Clubs[0];
			this.clubsGrid.BeginEdit();
			item.Name = "Liv";
			item.StadiumCapacity = -1000;
		}
	}
}
