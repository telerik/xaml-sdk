using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EditorAttribute
{
    public class ViewModel
    {
        private Player captain;
        public Player Captain
        {
            get 
            {
                if (this.captain == null)
                {
                    this.captain = new Player("Pepe Reina", 25, "Spain") { PhoneNumber = new PhoneNumber() { CountryCode = "359", RegionCode = "885", Number = "434343" } };
                }
                return this.captain; 
            }
        }
    }
}
