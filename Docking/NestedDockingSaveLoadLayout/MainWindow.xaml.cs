using System.IO;
using System.IO.IsolatedStorage;
using System.Windows;
using System.Xml.Linq;

namespace NestedDockingSaveLoadLayout
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private XElement outerLayout;
        private XElement innerLayout;

        public MainWindow()
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
            using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForAssembly())
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
            using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForAssembly())
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
            using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForAssembly())
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
            using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForAssembly())
            {
                var isoStream = storage.OpenFile("RadDocking_Inner_Layout.xml", FileMode.Open);
                using (isoStream)
                {
                    this.InnerDocking.LoadLayout(isoStream);
                }
            }
        }

        private void SaveLayoutToXElementButtonClick(object sender, RoutedEventArgs e)
        {
            var innerDockingStream = new MemoryStream();
            this.InnerDocking.SaveLayout(innerDockingStream);
            innerDockingStream.Seek(0, SeekOrigin.Begin);
            this.innerLayout = XElement.Load(innerDockingStream);

            var outerDockingStream = new MemoryStream();
            this.OuterDocking.SaveLayout(outerDockingStream);
            outerDockingStream.Seek(0, SeekOrigin.Begin);
            this.outerLayout = XElement.Load(outerDockingStream);

            this.LoadLayoutFromXElementButton.IsEnabled = true;
        }

        private void LoadLayoutFromXElementButtonClick(object sender, RoutedEventArgs e)
        {
            MemoryStream innerDockingStream = new MemoryStream();
            this.innerLayout.Save(innerDockingStream);
            innerDockingStream.Seek(0, SeekOrigin.Begin);
            this.InnerDocking.LoadLayout(innerDockingStream);

            MemoryStream outerDockingStream = new MemoryStream();
            this.outerLayout.Save(outerDockingStream);
            outerDockingStream.Seek(0, SeekOrigin.Begin);
            this.OuterDocking.LoadLayout(outerDockingStream);
        }
    }
}