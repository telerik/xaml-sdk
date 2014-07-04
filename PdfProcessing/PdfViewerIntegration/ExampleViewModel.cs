using System;
using System.IO;
using System.Linq;
using Telerik.Windows.Controls;

namespace PdfViewerIntegration
{
    public class ExampleViewModel : ViewModelBase
    {
        private Stream documentStream;

        public Stream DocumentStream
        {
            get
            {
                return this.documentStream;
            }
            set
            {
                if (this.documentStream != value)
                {
                    this.documentStream = value;
                    this.OnPropertyChanged("DocumentStream");
                }
            }
        }
    }
}
