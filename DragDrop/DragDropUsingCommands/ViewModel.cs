using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Telerik.Windows.Controls;

namespace DragDropUsingCommands
{
    public class ViewModel
    {
        public ViewModel()
        {
            this.GenerateData(5);
            this.DropCommand = new DelegateCommand(OnDropExecute);
            this.DragCommand = new DelegateCommand(OnDragExecute);
        }

        public ICommand DropCommand { get; private set; }
        public ICommand DragCommand { get; private set; }

        public ObservableCollection<User> SourceData { get; set; }
        public ObservableCollection<User> DestinationData { get; set; }

        private void OnDropExecute(object obj)
        {
            var param = obj as DragDropParameter;

            ((IList)param.ItemsSource).Add(param.DraggedItem);
        }

        private void OnDragExecute(object obj)
        {
            var param = obj as DragDropParameter;

            ((IList)param.ItemsSource).Remove(param.DraggedItem);
        }

        private void GenerateData(int count)
        {
            this.SourceData = new ObservableCollection<User>((
                from p in Enumerable.Range(0, count)
                select new User
                {
                    UserName = "User " + p
                }).ToList());

            this.DestinationData = new ObservableCollection<User>((
                from p in Enumerable.Range(0, count)
                select new User
                {
                    UserName = "Destination User " + p
                }).ToList());
        }
    }
}
