using System.Windows.Controls;

namespace IpAddress
{
    /// <summary>
    /// Interaction logic for Example.xaml
    /// </summary>
    public partial class Example : UserControl
    {
        public Example()
        {
            InitializeComponent();
            this.firstInput.DataContext = new ViewModel()
            {
                IpValue = new IpAddressPresentation(255, 255, 255, 255) { IsValid = true }
            };
        }
    }
}
