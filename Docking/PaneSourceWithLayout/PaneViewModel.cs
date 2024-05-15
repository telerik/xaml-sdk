using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls;

namespace PaneSourceWithLayout
{
    public class PaneViewModel : ViewModelBase
    {
        private string headerText;

        public string SerializationTag { get; set; }

        /// <summary>
        /// Gets or sets HeaderText and notifies for changes
        /// </summary>
        public string HeaderText
        {
            get
            {
                return this.headerText;
            }

            set
            {
                if (this.headerText != value)
                {
                    this.headerText = value;
                    this.OnPropertyChanged(() => this.HeaderText);
                }
            }
        }
    }
}
