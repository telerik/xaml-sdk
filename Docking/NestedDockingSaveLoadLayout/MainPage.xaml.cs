using System.IO;
using System.IO.IsolatedStorage;
using System.Windows;
using System.Windows.Controls;

namespace NestedDockingSaveLoadLayout
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void SaveLayoutToFileButtonClick(object sender, RoutedEventArgs e)
        {
            this.SaveInnerLayoutToFile();
            this.SaveOuterLayoutToFile();
            this.LoadLayoutFromStrButton.IsEnabled = true;
        }

        private void LoadLayoutFromFileButtonClick(object sender, RoutedEventArgs e)
        {
            this.LoadInnerLayoutFromFile();
            this.LoadOuterLayoutFromFile();
        }

        private void SaveOuterLayoutToFile()
        {
            using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (var isoStream = storage.OpenFile("RadDocking_Layout.xml", FileMode.Create))
                {
                    this.OuterDocking.SaveLayout(isoStream);
                    isoStream.Seek(0, SeekOrigin.Begin);
                    StreamReader reader2 = new StreamReader(isoStream);
                }
            }
        }

        private void LoadOuterLayoutFromFile()
        {
            using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                var isoStream = storage.OpenFile("RadDocking_Layout.xml", FileMode.Open);
                using (isoStream)
                {
                    this.OuterDocking.LoadLayout(isoStream);
                }
            }
        }

        private void SaveInnerLayoutToFile()
        {
            using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (var isoStream = storage.OpenFile("RadDocking_Inner_Layout.xml", FileMode.Create))
                {
                    this.InnerDocking.SaveLayout(isoStream);
                    isoStream.Seek(0, SeekOrigin.Begin);
                    StreamReader reader2 = new StreamReader(isoStream);
                }
            }
        }

        private void LoadInnerLayoutFromFile()
        {
            using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                var isoStream = storage.OpenFile("RadDocking_Inner_Layout.xml", FileMode.Open);
                using (isoStream)
                {
                    this.InnerDocking.LoadLayout(isoStream);
                }
            }
        }
    }
}
