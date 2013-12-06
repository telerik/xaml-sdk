using System;
using System.IO;
using System.Linq;
using Telerik.Windows.Documents.FormatProviders;

namespace ThreadSafeDocumentConversion
{
    public class ThreadParameters
    {
        public FileInfo FileInfo { get; set; }
        public IDocumentFormatProvider FromFormatProvider { get; set; }
        public IDocumentFormatProvider ToFormatProvider { get; set; }
        
        public ThreadParameters(FileInfo fileInfo, IDocumentFormatProvider fromFormatProvider, IDocumentFormatProvider toFormatProvider)
        {
            this.FileInfo = fileInfo;
            this.FromFormatProvider = fromFormatProvider;
            this.ToFormatProvider = toFormatProvider;
        }
    }
}
