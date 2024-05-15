using System;
using System.Linq;
using System.Windows;
using Telerik.Windows.Controls;

namespace Localization
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            LocalizationManager.Manager = new LocalizationManager()
            {
                ResourceManager = RadRichTextBoxResources.ResourceManager
            };

            InitializeComponent();
        }
    }
}
