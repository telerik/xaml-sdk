using System.Windows;
using Telerik.Windows.Controls.Map;

namespace ProvidersEmptyProvider
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            EmptyProvider provider = new EmptyProvider();
            this.radMap.Provider = provider;
        }
    }
}
