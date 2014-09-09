using HelpFunctionality;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;

namespace HelpFunctionality_SL
{
    public partial class MainPage : RadRibbonWindow
    {
        public MainPage()
        {
            InitializeComponent(); 
            
            OpenHelpPageCommand = new DelegateCommand(ExecuteOpenHelpPage);
            this.DataContext = OpenHelpPageCommand;
            InputBindingCollection inputBindCollection = new InputBindingCollection();
            inputBindCollection.Add(new KeyBinding(this.OpenHelpPageCommand, new KeyGesture(Key.F1)));
            CommandManager.SetInputBindings(this, inputBindCollection);
        }

        private void ExecuteOpenHelpPage(object obj)
        {
            var helpWindow = MyHelpWindow.Instance;
            helpWindow.Show();
        }

        public DelegateCommand OpenHelpPageCommand { get; set; }
    }
}
