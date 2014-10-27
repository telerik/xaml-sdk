using System;
using System.Linq;
using Telerik.Windows.Documents.UI;

namespace CustomCaret
{
    public class SogouCaretFactory : ICaretFactory
    {
        public Caret CreateCaret()
        {
            return new SogouCaret();
        }
    }
}