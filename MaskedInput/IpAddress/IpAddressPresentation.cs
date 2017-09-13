namespace IpAddress
{
    public class IpAddressPresentation
    {
        public IpAddressPresentation(byte a, byte b, byte c, byte d)
        {
            this.PartA = a;
            this.PartB = b;
            this.PartC = c;
            this.PartD = d;
        }

        public byte PartA
        {
            get;
            set;
        }

        public byte PartB
        {
            get;
            set;
        }
        public byte PartC
        {
            get;
            set;
        }

        public byte PartD
        {
            get;
            set;
        }

        public string IpFullString
        {
            get
            {
                return this.CompletePart(this.PartA.ToString()) + "." +
                       this.CompletePart(this.PartB.ToString()) + "." +
                       this.CompletePart(this.PartC.ToString()) + "." +
                       this.CompletePart(this.PartD.ToString());
            }
        }

        public bool IsValid
        {
            get;
            set;
        }

        public override string ToString()
        {
            return this.IpFullString;
        }

        private string CompletePart(string part)
        {
            if (part.Length == 3)
            {
                return part;
            }
            else if (part.Length == 2)
            {
                return "0" + part;
            }
            else if (part.Length == 1)
            {
                return "00" + part;
            }
            return string.Empty;
        }
    }
}
