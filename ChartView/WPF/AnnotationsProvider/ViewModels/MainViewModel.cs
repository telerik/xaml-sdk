using System.Collections.ObjectModel;

namespace AnnotationsProvider
{
    public class MainViewModel
    {
        public ObservableCollection<object> Limitations { get; set; }

        public MainViewModel()
        {
            this.Limitations = new ObservableCollection<object>();
            this.PopulateLimitations();
        }

        private void PopulateLimitations()
        {
            this.Limitations.Add(new DailyLimitationViewModel() { StartValue = 4.4 });
            this.Limitations.Add(new MonthlyLimitationViewModel() { StartValue = 10, EndValue = 25, StartMonth = "March", EndMonth = "April" });
        }
    }
}
