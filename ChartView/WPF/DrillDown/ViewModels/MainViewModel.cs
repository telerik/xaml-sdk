using System.Collections.Generic;
using Telerik.Windows.Controls;

namespace DrillDown.ChartUtilities
{
    public class MainViewModel : ViewModelBase
    {
        private List<DrillDownHelper> drillersList = new List<DrillDownHelper>();
        private int drillDownLevel;

        public List<Company> Data { get; set; }

        private DrillDownHelper drillDownHelper;
        public DrillDownHelper DrillDownHelper
        {
            get { return this.drillDownHelper; }
            set
            {
                if (this.drillDownHelper != value)
                {
                    this.drillDownHelper = value;
                    this.OnPropertyChanged(() => this.DrillDownHelper);
                }
            }
        }

        private string breadCrumb;
        public string BreadCrumb
        {
            get { return this.breadCrumb; }
            set
            {
                this.breadCrumb = value;
                this.OnPropertyChanged(() => this.BreadCrumb);
            }
        }

        public MainViewModel()
        {
            Company company1 = CompanyDataProvider.GetCompany1Data();
            Company company2 = CompanyDataProvider.GetCompany2Data();
            Company company3 = CompanyDataProvider.GetCompany3Data();

            this.Data = new List<Company> { company1, company2, company3 };

            var driller = new DrillDownHelper(this.Data, "CompanyName", "Revenue", "BarSeries", "");

            this.DrillDownHelper = driller;
            this.drillersList.Add(driller);
            this.drillDownLevel = 0;
        }

        internal void HandleItemClicked(object item)
        {
            DrillDownHelper driller = null;

            Company company = item as Company;
            if (company != null)
            {
                driller = new DrillDownHelper(company.Products, "ProductName", "CurrentPrice", "BarSeries", company.CompanyName);
            }

            Product product = item as Product;
            if (product != null)
            {
                driller = new DrillDownHelper(product.PricesInfo, "Year", "Price", "LineSeries", product.ProductName);
            }

            if (driller != null)
            {
                this.DrillDownHelper = driller;
                this.drillDownLevel++;
                this.SyncDrillersListWithIndex();
                this.drillersList.Add(driller);
                this.UpdateDrillLevel();
            }
        }

        private void SyncDrillersListWithIndex()
        {
            for (int i = this.drillersList.Count - 1; i >= this.drillDownLevel; i--)
            {
                this.drillersList.RemoveAt(i);
            }
        }

        internal void DrillUp()
        {
            if (this.drillDownLevel > 0)
            {
                this.drillDownLevel--;
                var driller = this.drillersList[this.drillDownLevel];
                this.DrillDownHelper = driller;
                this.UpdateDrillLevel();
            }
        }

        internal void DrillDown()
        {
            if (this.drillDownLevel < this.drillersList.Count - 1)
            {
                this.drillDownLevel++;
                var driller = this.drillersList[this.drillDownLevel];
                this.DrillDownHelper = driller;
                this.UpdateDrillLevel();
            }
        }

        private void UpdateDrillLevel()
        {
            string newBreadCrumb = "";
            for (int i = 0; i <= this.drillDownLevel; i++)
            {
                if (!string.IsNullOrEmpty(newBreadCrumb)) newBreadCrumb += " -> ";
                newBreadCrumb += this.drillersList[i].FriendlyName;
            }

            this.BreadCrumb = newBreadCrumb;
        }
    }
}
