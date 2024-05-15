using System;
using System.Linq;
using System.Windows.Controls;
using Telerik.Windows.Controls;

namespace CustomLocalization
{
    /// <summary>
    /// Interaction logic for Example.xaml
    /// </summary>
    public partial class Example : UserControl
    {
        public Example()
        {
            LocalizationManager.Manager = new CustomLocalizationManager();
            InitializeComponent();
        }
    }
}
