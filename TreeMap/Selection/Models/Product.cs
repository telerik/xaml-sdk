namespace Selection
{
    public class Product
    {
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public double UnitPrice { get; set; }
        public int UnitsInStock { get; set; }

        public double TotalValue
        {
            get
            {
                return this.UnitsInStock * this.UnitPrice;
            }
        }
    }
}
