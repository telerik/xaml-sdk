using System.Windows;
using Telerik.Windows.Controls;

namespace SelectAllIndentCell
{
    public partial class App : Application
    {
        private void PART_IndicatorPresenter_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var gridView = (sender as DependencyObject).ParentOfType<RadGridView>();

            gridView.SelectAll();
        }
    }
}
