namespace ShuttleControl
{
    public class Agency
    {
        public Agency(string name, string phone, string zip)
        {
            Name = name;
            Phone = phone;
            Zip = zip;
        }

        public string Name { get; set; }          
        public string Phone { get; set; }
        public string Zip { get; set; }
    }
}
