using System;
using System.Linq;
using System.Windows.Controls;
using Telerik.Windows.Controls;

namespace Localization
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            LocalizationManager.Manager = new LocalizationManager()
            {
                ResourceManager = RadRichTextBoxResources.ResourceManager
            };

            InitializeComponent();
        }
    }
}
