using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls;

namespace PaneSourceWithLayout
{
    public class ViewModel : ViewModelBase
    {
        private ObservableCollection<PaneViewModel> paneViewModels;
        
        public ViewModel()
        {
            this.PaneViewModels = GetSerializedRadPanes();
        }

        /// <summary>
        /// Gets or sets Panes and notifies for changes
        /// </summary>
        public ObservableCollection<PaneViewModel> PaneViewModels
        {
            get
            {
                return this.paneViewModels;
            }

            set
            {
                if (this.paneViewModels != value)
                {
                    this.paneViewModels = value;
                    this.OnPropertyChanged(() => this.PaneViewModels);
                }
            }
        }

        private static ObservableCollection<PaneViewModel> GetSerializedRadPanes()
        {
            var pane1 = new PaneViewModel() { HeaderText = "Saved Pane 1", SerializationTag = "SavedPane1" };
            var pane2 = new PaneViewModel() { HeaderText = "Saved Pane 2", SerializationTag = "SavedPane2" };
            var notSavedPane = new PaneViewModel() { HeaderText = "Not Saved Pane", SerializationTag = "NotSavedPane" };

            return new ObservableCollection<PaneViewModel> { pane1, pane2, notSavedPane };
        }
    }
}
