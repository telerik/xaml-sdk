using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace RadUploadIntegration
{
    public partial class MainPage : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private MySaveCommand mySaveCommand;
        public MySaveCommand NewSaveCommand
        {
            get
            {
                return mySaveCommand;
            }
            set
            {
                if (mySaveCommand != value)
                {
                    mySaveCommand = value;
                    this.NotifyPropertyChanged(this, "NewSaveCommand");
                }
            }
        }

        public MainPage()
        {
            InitializeComponent();
            this.DataContext = this;
            this.ImageEditorUI.ImageEditorLoaded += ImageEditorUI_ImageEditorLoaded;
        }

        void ImageEditorUI_ImageEditorLoaded(object sender, EventArgs e)
        {
            this.NewSaveCommand = new MySaveCommand(this.ImageEditorUI.ImageEditor);
        }

        private void NotifyPropertyChanged(object sender, string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(sender, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}