using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Telerik.Windows.Controls;

namespace ShuttleControl
{
    public class ViewModel
    {
        public ViewModel()
        {
            this.AvailableItems = new ObservableCollection<Agency>()
            {
               new Agency("Exotic Liquids", "(171) 555-2222", "EC1 4SD"),
               new Agency("New Orleans Cajun Delights", "(100) 555-4822", "70117"),
               new Agency("Grandma Kelly's Homestead", "(313) 555-5735", "48104"),
               new Agency("Tokyo Traders", "(03) 3555-5011", "100"),
               new Agency("Cooperativa de Quesos 'Las Cabras'", "(98) 598 76 54", "33007"),
               new Agency("Mayumi's", "(06) 431-7877", "545"),
               new Agency("Pavlova, Ltd.", "(03) 444-2343", "3058"),
               new Agency("Specialty Biscuits, Ltd.", "(161) 555-4448", "M14 GSD"),
               new Agency("PB Knäckebröd AB", "031-987 65 43", "S-345 67"),
               new Agency("Refrescos Americanas LTDA", "(11) 555 4640", "5442")
            };

            this.MovedItems = new ObservableCollection<Agency>();
            this.SourceSelectedItems = new ObservableCollection<Agency>();
            this.TargetSelectedItems = new ObservableCollection<Agency>();

            this.SourceSelectedItems.CollectionChanged += SourceSelectedItems_CollectionChanged;
            this.TargetSelectedItems.CollectionChanged += TargetSelectedItems_CollectionChanged;
            this.AvailableItems.CollectionChanged += AvailableItems_CollectionChanged;
            this.MovedItems.CollectionChanged += MovedItems_CollectionChanged;

            this.AddCommand = new DelegateCommand(this.AddCommandExecuted, this.CanAddCommandExecute);
            this.RemoveCommand = new DelegateCommand(this.RemoveCommandExecuted, this.CanRemoveCommandExecute);
            this.AddAllCommand = new DelegateCommand(this.AddAllCommandExecuted, this.CanAddAllCommandExecute);
            this.RemoveAllCommand = new DelegateCommand(this.RemoveAllCommandExecuted, this.CanRemoveAllCommandExecute);

        }

        public ObservableCollection<Agency> AvailableItems { get; set; }
        public ObservableCollection<Agency> MovedItems { get; set; }

        public ObservableCollection<Agency> SourceSelectedItems { get; set; }
        public ObservableCollection<Agency> TargetSelectedItems { get; set; }

        public DelegateCommand AddCommand { get; set; }
        public DelegateCommand RemoveCommand { get; set; }
        public DelegateCommand AddAllCommand { get; set; }
        public DelegateCommand RemoveAllCommand { get; set; }
    

        protected bool CanAddCommandExecute(object param)
        {
            return this.SourceSelectedItems.Count > 0;
        }

        protected void AddCommandExecuted(object param)
        {
            this.TargetSelectedItems.Clear();
            
            var sourceSelectedItemsCopy = new ObservableCollection<Agency>(this.SourceSelectedItems);
            foreach (var item in sourceSelectedItemsCopy)
            {
                this.AvailableItems.Remove(item);
                this.MovedItems.Add(item);
            }
        }

        protected bool CanRemoveCommandExecute(object param)
        {
           return this.TargetSelectedItems.Count > 0;          
        }

        protected void RemoveCommandExecuted(object param)
        {
            this.SourceSelectedItems.Clear();

            var targetSelectedItemsCopy = new ObservableCollection<Agency>(this.TargetSelectedItems);
            foreach (var item in targetSelectedItemsCopy)
            {               
                this.MovedItems.Remove(item);
                this.AvailableItems.Add(item);           
            }
        }

        protected bool CanAddAllCommandExecute(object param)
        {
            return this.AvailableItems.Count() > 0;
        }

        protected void AddAllCommandExecuted(object param)
        {
            var initialCount = this.AvailableItems.Count();
            for (int i = 0; i < initialCount; i++)
            {
                var itemToRemove = this.AvailableItems.ElementAt(0);
                this.AvailableItems.Remove(itemToRemove);
                this.MovedItems.Add(itemToRemove);
            }
        }

        protected bool CanRemoveAllCommandExecute(object param)
        {
            return this.MovedItems.Count() > 0;
        }

        protected void RemoveAllCommandExecuted(object param)
        {
            var initialCount = this.MovedItems.Count();
            for (int i = 0; i < initialCount; i++)
            {
                var itemToRemove = this.MovedItems.ElementAt(0);
                this.MovedItems.Remove(itemToRemove);
                this.AvailableItems.Add(itemToRemove);
            }
        }

        private void SourceSelectedItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.AddCommand.InvalidateCanExecute();
        }

        private void TargetSelectedItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.RemoveCommand.InvalidateCanExecute();
        }

        private void AvailableItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.AddAllCommand.InvalidateCanExecute();
        }

        private void MovedItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.RemoveAllCommand.InvalidateCanExecute();
        }        
    }
}
