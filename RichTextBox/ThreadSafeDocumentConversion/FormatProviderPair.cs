using System;
using System.Linq;
using Telerik.Windows.Documents.FormatProviders;

namespace ThreadSafeDocumentConversion
{
    public class FormatProviderPair
    {
        public IDocumentFormatProvider FormatProvider { get; set; }
        public string Title { get; set; }

        public FormatProviderPair(IDocumentFormatProvider formatProvider, string extention)
        {
            this.FormatProvider = formatProvider;
            this.Title = extention;
        }
    }
}
