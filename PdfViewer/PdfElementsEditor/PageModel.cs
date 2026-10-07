using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfElementsEditor
{
    public class PageModel
    {
        public PageModel(int pageNumber)
        {
            this.PageNumber = pageNumber;
            this.Elements = new ObservableCollection<ElementModel>();
        }

        public int PageNumber { get; private set; }
        public string PageText
        {
            get
            {
                return "Page " + PageNumber;
            }
        }

        /// <summary>
        /// Gets the vector icon displayed next to the page node.
        /// </summary>
        public System.Windows.Media.Geometry PageIcon
        {
            get
            {
                return IconGeometries.Page;
            }
        }

        /// <summary>
        /// Gets the brush used to fill the <see cref="PageIcon"/>.
        /// </summary>
        public System.Windows.Media.Brush PageIconBrush
        {
            get
            {
                return IconGeometries.PageBrush;
            }
        }

        public ObservableCollection<ElementModel> Elements { get; set; }
    }
}
