using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using Telerik.Windows.Controls;

namespace ShuttleControl
{
    public class ShuttleTreeViewModel : ViewModelBase
    {
        public ObservableCollection<NodeViewModel> TreeViewData { get; set; }
        public ObservableCollection<NodeViewModel> SelectedTreeNodes { get; set; }
        public NodeViewModel TableNode { get; set; }
        public NodeViewModel ViewNode { get; set; }
        public ICommand SelectionChangedCommand { get; set; }

        public ShuttleTreeViewModel()
        {
            TreeViewData = new ObservableCollection<NodeViewModel>();
            SelectedTreeNodes = new ObservableCollection<NodeViewModel>();
            this.SelectionChangedCommand = new DelegateCommand(OnSelectionChanged);
        }

        private void OnSelectionChanged(object parameter)
        {
            var args = (SelectionChangedEventArgs)parameter;
            if (args.RemovedItems.Count > 0)
            {
                foreach (var oldItem in args.RemovedItems)
                {
                    this.SelectedTreeNodes.Remove((NodeViewModel)oldItem);
                }
            }

            if (args.AddedItems.Count > 0)
            {
                foreach (var item in args.AddedItems)
                {
                    var newItem = (NodeViewModel)item;
                    if (newItem.IsSelectable)
                    {
                        this.SelectedTreeNodes.Add(newItem);
                    }
                }
            }
        }
    }
}
