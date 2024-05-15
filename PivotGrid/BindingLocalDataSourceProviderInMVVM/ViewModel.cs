using Telerik.Pivot.Core;
using Telerik.Windows.Controls;

namespace BindingLocalDataSourceProviderInMVVM
{
    public class ViewModel : ViewModelBase
    {
        private IDataProvider dataProvider;

        public ViewModel()
        {
            var localProvider = new LocalDataSourceProvider();
            localProvider.ItemsSource = new AllOrders();
            localProvider.RowGroupDescriptions.Add(new PropertyGroupDescription { PropertyName = "Product" });
            localProvider.RowGroupDescriptions.Add(new DateTimeGroupDescription { PropertyName = "Date", Step = DateTimeStep.Month });
            localProvider.ColumnGroupDescriptions.Add(new PropertyGroupDescription { PropertyName = "Advertisement" });
            localProvider.ColumnGroupDescriptions.Add(new PropertyGroupDescription { PropertyName = "Promotion" });

            localProvider.AggregateDescriptions.Add(new PropertyAggregateDescription { PropertyName = "Net" });
            localProvider.AggregateDescriptions.Add(new PropertyAggregateDescription { PropertyName = "Quantity" });

            this.DataProvider = localProvider;
        }

        public IDataProvider DataProvider
        {
            get 
            { 
                return this.dataProvider; 
            }
            set
            { 
                if (this.dataProvider != value)
                {
                    this.dataProvider = value;
                    this.OnPropertyChanged("DataProvider");
                }
            }
        }
    }
}
