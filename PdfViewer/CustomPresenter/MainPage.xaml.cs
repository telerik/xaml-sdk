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

namespace CustomPresenter
{
    public partial class MainPage : UserControl
    {
        private const string CustomPresenter = "CustomPresenter";
        private const double DefaultScaleFactor = 0.3;

        public MainPage()
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
