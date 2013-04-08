using System.Collections.Generic;
using System.Linq;

namespace DrillDownNavigation
{
    public class ModelSalesCollection : List<ModelSales>
    {
        public double TotalAmount
        {
            get
            {
                return this.Sum(modelSales => modelSales.Amount);
            }
        }
    }
}
