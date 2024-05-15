namespace MailMerge
{
    public class Product
    {
        public Product(string productName, int purchasedItemsCount)
        {
            this.ProductName = productName;
            this.PurchasedItemsCount = purchasedItemsCount;
        }

        public string ProductName { get; set; }

        public int PurchasedItemsCount { get; set; }
    }
}
