using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EditableTextBoxTemplate
{
    public class Company
    {
        public Company(string name, string address, string phone, string image)
        {
            this.Name = name;
            this.Address = address;
            this.Phone = phone;
            this.Image = image;
        }

        public string Name { get; set;}
        public string Phone { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }
    }
}
