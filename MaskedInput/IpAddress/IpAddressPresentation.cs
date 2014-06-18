using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public override string ToString()
        {
            return string.Format("{0}.{1}.{2}.{3}", PartA.ToString(), PartB.ToString(), PartC.ToString(), PartD.ToString());
        }
    }
}
