using System;
using System.Collections.Generic;
using System.Linq;

namespace DragDropTreeViewToControls.ViewModels
{
    public class ProductViewModel
    {
        // Data generation.
        private string _name;
        private string _description;
        private decimal _unitPrice;

        private static readonly Random generator = new Random(1676545846);
        private static readonly string[] adjectives = "Fabulous,Amazing,New,Classic,Modern,Durable,Outstanding,Excellent,Premium".Split(',');
        private static readonly string[] owner = "Alaska,Jonhn,Ray,Ruby,Stone,Lilly,Scott,Barney,Dorian,Neo,Sarah".Split(',');
        private static readonly string[] objects = "Pen,Manual,Bicycle,Umbrella,Mouse,Vase,Keyboard".Split(',');
        private static readonly decimal[] prices = { 12.99M, 13.15M, 24.99M, 33.99M, 9.99M, 15.99M, 16.99M, 12.50M };

        public static IEnumerable<ProductViewModel> Generate(int count)
        {
            for (int i = 0; i < count; i++)
            {
                string product = objects[generator.Next(objects.Length)];
                ProductViewModel result = new ProductViewModel();
                result.Name = String.Format("{0}'s {1} {2}",
                        owner[generator.Next(owner.Length)],
                        adjectives[generator.Next(adjectives.Length)],
                        product);
                result.UnitPrice = prices[generator.Next(prices.Length)];
                result.Description = String.Format("Exquisite handcrafted {0}.", product.ToLower());
                yield return result;
            }
        }

        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }
        public string Description
        {
            get { return this._description; }
            set { this._description = value; }
        }
        public decimal UnitPrice
        {
            get { return this._unitPrice; }
            set { this._unitPrice = value; }
        }
    }
}
