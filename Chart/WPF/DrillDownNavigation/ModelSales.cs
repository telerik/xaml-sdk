namespace DrillDownNavigation
{
    public class ModelSales
    {
        public string Model
        {
            get;
            set;
        }
        public double Amount
        {
            get;
            set;
        }
        public ModelSales(string model, double amount)
        {
            this.Model = model;
            this.Amount = amount;
        }
    }
}
