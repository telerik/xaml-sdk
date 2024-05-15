using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
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
using Telerik.Windows.Controls;

namespace SaveNormalSizeAndPosition
{
    /// <summary>
    /// Interaction logic for MyRadWindow.xaml
    /// </summary>
    public partial class MyRadWindow
    {
        LayoutData layoutData;

        public MyRadWindow()
        {
            InitializeComponent();
            this.layoutData = new LayoutData();
        }

        private void RadWindow_WindowStateChanged(object sender, EventArgs e)
        {
            var state = ((Telerik.Windows.Controls.RadWindow)(sender)).WindowState;
            using (IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForAssembly())
            {
                string item;
                string[] settings = new string[] { };
                if (state == System.Windows.WindowState.Normal)
                {
                    using (StreamReader srReader = new StreamReader(new IsolatedStorageFileStream("isotest", FileMode.OpenOrCreate, isolatedStorage)))
                    {

                        if (srReader == null)
                        {
                            MessageBox.Show("No Data stored!");
                        }
                        else
                        {
                            while (!srReader.EndOfStream)
                            {
                                item = srReader.ReadLine();
                                settings = item.Split(',');
                            }

                            if (settings.Any())
                            {
                                this.Dispatcher.BeginInvoke(new Action(() =>
                                           {
                                               this.Left = Double.Parse(settings[0]);
                                               this.Top = Double.Parse(settings[1]);
                                               this.Width = Double.Parse(settings[2]);
                                               this.Height = Double.Parse(settings[3]);
                                           }));
                            }
                        }
                    }
                }
            }
        }

        private void RadWindow_Closed(object sender, WindowClosedEventArgs e)
        {
            using (IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForAssembly())
            {
                using (StreamWriter srWriter = new StreamWriter(new IsolatedStorageFileStream("isotest", FileMode.Create, isolatedStorage)))
                {
                    srWriter.WriteLine(this.GetNormalSizeAndPosition().ToString());

                    srWriter.Flush();
                }
            }
        }
    }
}

