using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.IsolatedStorage;
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

namespace SaveNormalSizeAndPosition
{
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
            using (IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
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
                                        this.Left = Double.Parse(settings[0], CultureInfo.InvariantCulture);
                                        this.Top = Double.Parse(settings[1], CultureInfo.InvariantCulture);
                                        this.Width = Double.Parse(settings[2], CultureInfo.InvariantCulture);
                                        this.Height = Double.Parse(settings[3], CultureInfo.InvariantCulture);
                                    }));
                            }
                        }
                    }
                }
            }
        }

        private void RadWindow_Closed(object sender, WindowClosedEventArgs e)
        {
            using (IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
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
