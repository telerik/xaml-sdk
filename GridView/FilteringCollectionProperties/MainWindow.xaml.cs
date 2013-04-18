using System;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Collections.Generic;
using Telerik.Windows.Controls.Filtering.Editors;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Data;

namespace FilteringCollectionProperties
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
			InitializeComponent();

			// Bind the grid to some sample data.
			this.radGridView.ItemsSource = Person.GetSampleData();
		}

		private void OnFilterOperatorsLoading(object sender, FilterOperatorsLoadingEventArgs e)
		{
			if (e.Column is CollectionPropertyColumn)
			{
				foreach (var op in e.AvailableOperators.ToList()) 
				{
					if (!(op == FilterOperator.Contains || op == FilterOperator.DoesNotContain))
					{
						e.AvailableOperators.Remove(op);
					}
				}

				e.DefaultOperator1 = FilterOperator.Contains;
				e.DefaultOperator2 = FilterOperator.Contains;
			}
		}

		private void OnDistinctValuesLoading(object sender, GridViewDistinctValuesLoadingEventArgs e)
		{
			if (e.Column is CollectionPropertyColumn)
			{
				e.ItemsSource = Enumerable.Empty<object>();
			}
		}

		private void OnFieldFilterEditorCreated(object sender, EditorCreatedEventArgs e)
		{
			if (e.Column is CollectionPropertyColumn)
			{
				StringFilterEditor sfe = (StringFilterEditor)e.Editor;
				sfe.MatchCaseVisibility = Visibility.Collapsed;
			}
		}
    }
}
