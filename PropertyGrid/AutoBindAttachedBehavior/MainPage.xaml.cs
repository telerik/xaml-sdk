using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using Telerik.Windows.Controls;

namespace SilverlightApplication1
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();  
           
            var button = new Button();
            button.Height = 50;
            button.Width = 200;
            rpg.Item = button;
        }
    }
}