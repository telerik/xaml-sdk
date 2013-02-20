using System.Windows.Controls;
using Telerik.Windows.Controls.Map;

namespace ProvidersEmptyProvider
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();

            EmptyProvider provider = new EmptyProvider();
            this.radMap.Provider = provider;
        }
    }
}
