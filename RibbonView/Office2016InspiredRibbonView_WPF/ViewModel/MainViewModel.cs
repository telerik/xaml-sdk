using Office2016InspiredRibbonView_WPF.Appearance;
using Telerik.Windows.Controls;

namespace Office2016InspiredRibbonView_WPF.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            MainViewModel.SelectedColorVariation = Office2016ColorVariations.Colorful;
            AppearanceManager.ChangeAppearance(MainViewModel.SelectedColorVariation);
            this.ChangeAppearanceCommand = new DelegateCommand(this.ChangeColorVariation);
        }

        private void ChangeColorVariation(object parameter)
        {
            var commandParameter = parameter as AppearanceCommandParameter;
            if (this.ColorVariation == commandParameter.ColorVariation)
            {
                return;
            }

            this.CurrentColorVariation = commandParameter.ColorVariation;
            MainViewModel.SelectedColorVariation = this.CurrentColorVariation;

            this.ColorVariation = commandParameter.ColorVariation;

            AppearanceManager.ChangeAppearance(this.ColorVariation);
        }

        public DelegateCommand ChangeAppearanceCommand { get; private set; }

        public Office2016ColorVariations ColorVariation { get; private set; }

        public Office2016ColorVariations CurrentColorVariation { get; private set; }

        public static Office2016ColorVariations SelectedColorVariation {get; set; } 
    }
}
