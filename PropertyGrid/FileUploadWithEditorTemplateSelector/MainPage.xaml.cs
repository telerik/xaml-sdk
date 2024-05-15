using FileUploadWithEditorTemplateSelector;
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

namespace FileUploadWithEditorTemplateSelector_SL
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            this.PropertyGrid1.Item = new User()
            {
                AvatarPath = null,
                Age = 28,
                Username = "johndoe"
            };
        }

        private void SelectFileClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                var item = this.PropertyGrid1.Item as User;
                item.AvatarPath = openFileDialog.File.Name;
            }
        }
    }
}
