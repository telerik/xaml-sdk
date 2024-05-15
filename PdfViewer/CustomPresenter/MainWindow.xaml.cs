using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CustomPresenter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string CustomPresenter = "CustomPresenter";
        private const double DefaultScaleFactor = 0.3;

        public MainWindow()
        {
            InitializeComponent();

        }
         
        private void viewer_Loaded(object sender, RoutedEventArgs e)
        {
            this.viewer.DocumentChanged += viewer_DocumentChanged;
            this.viewer.RegisterPresenter(PresenterNames.CustomPresenter, new CustomSinglePagePresenter());  
        }

        void viewer_DocumentChanged(object sender, Telerik.Windows.Documents.Fixed.DocumentChangedEventArgs e)
        {
            this.viewer.ScaleFactor = DefaultScaleFactor;
        } 

        private void RadButton1_Click(object sender, RoutedEventArgs e)
        {
            this.viewer.FixedDocumentPresenter = this.viewer.GetRegisteredPresenter(PresenterNames.CustomPresenter);
        }

        private void RadButton2_Click(object sender, RoutedEventArgs e)
        {
            this.viewer.FixedDocumentPresenter = this.viewer.GetRegisteredPresenter(PresenterNames.FixedDocumentPagesPresenter);
        }
    }
}
