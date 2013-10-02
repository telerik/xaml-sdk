using System;
using System.Linq;
using System.Windows;

namespace Diagrams.PascalTriangle
{
    public class PascalNode
    {
        public Point Position
        {
            get;
            set;
        }
        public int PascalNumber
        {
            get;
            set;
        }

        public bool IsTextBoxType
        {
            get;
            set;
        }
    }
}
