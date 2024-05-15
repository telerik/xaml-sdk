using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Telerik.Windows.Controls;

namespace CustomDockingPanesFactory
{
    public class ViewModel : ViewModelBase
    {
        private ObservableCollection<RadPane> panes;

        public ViewModel()
        {
            this.Panes = new ObservableCollection<RadPane>()
            {
                new RadPane() { Header = "Bottom Pane 1", Tag = "Bottom" },
                new RadPane() { Header = "Bottom Pane 2", Tag = "Bottom" },
                new RadPane() { Header = "Left Pane", Tag = "Left" },
                new RadDocumentPane() { Header = "DocumentHost Pane 1", Tag = "DocumentHost" },
                new RadDocumentPane() { Header = "DocumentHost Pane 2", Tag = "DocumentHost" },
                new RadDocumentPane() { Header = "DocumentHost Pane 3", Tag = "DocumentHost" },
                new RadDocumentPane() { Header = "DocumentHost Pane 4", Tag = "DocumentHost" },
                new RadDocumentPane() { Header = "DocumentHost Pane 5", Tag = "DocumentHost" }
            };
        }

        /// <summary>
        /// Gets or sets Panes and notifies for changes
        /// </summary>
        public ObservableCollection<RadPane> Panes
        {
            get
            {
                return this.panes;
            }

            set
            {
                if (this.panes != value)
                {
                    this.panes = value;
                    this.OnPropertyChanged(() => this.Panes);
                }
            }
        }
    }
}
