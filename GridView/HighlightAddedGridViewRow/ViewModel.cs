using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Threading;
using Telerik.Windows.Controls;

namespace HighlightAddedGridViewRow
{
    public class ViewModel : ViewModelBase
    {
        private int currentId = 1;

        public ViewModel()
        {
            this.Samples = new ObservableCollection<Sample>();
            
            this.AddSampleCommand = new DelegateCommand(OnAddSampleExecute);
        }

        public ICommand AddSampleCommand { get; set; }

        public ObservableCollection<Sample> Samples { get; set; }

        private void OnAddSampleExecute(object obj)
        {
            var newSample = new Sample() { ID = currentId, Data = "Test" + currentId, IsNew = true };
            
            var timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 3);
            timer.Tick += (s, e) =>
            {
                newSample.IsNew = false;
                timer.Stop();
            };
            timer.Start();

            this.Samples.Add(newSample);
            currentId++;
        }
    }
}
