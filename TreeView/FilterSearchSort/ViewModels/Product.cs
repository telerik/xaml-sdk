using System;
using System.Linq;

namespace FilterSearchSort.ViewModels
{
    public class Product
    {
        public Product(string name)
        {
            this.Name = name;
        }
        public string Name { get; set; }

        public string Path
        {
            get
            {
                return this.Name.ToString();
            }
        }
    }
}
