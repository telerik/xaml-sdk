using System.IO;
using System.IO.IsolatedStorage;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Docking;

namespace SaveLoadLayout
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private LayoutDataViewModel data;
        private XElement dockingLayout;

		public MainWindow()
		{
			InitializeComponent();
			this.XmlTextBox.DataContext = this.data = new LayoutDataViewModel();
		}

		private string SaveLayoutAsString()
		{
			MemoryStream stream = new MemoryStream();
			this.Docking.SaveLayout(stream);
			stream.Seek(0, SeekOrigin.Begin);
			StreamReader reader = new StreamReader(stream);

			return reader.ReadToEnd();
		}

		private void SaveLayoutToFile()
		{
			using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForAssembly())
			{
				using (var isoStream = storage.OpenFile("RadDocking_Layout.xml", FileMode.Create))
				{
					this.Docking.SaveLayout(isoStream);
					isoStream.Seek(0, SeekOrigin.Begin);
					StreamReader reader2 = new StreamReader(isoStream); 
				}
			}
		}

		private void LoadLayoutFromString(string xml)
		{
			using (Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
			{
				stream.Seek(0, SeekOrigin.Begin);
				this.Docking.LoadLayout(stream);
			}
		}

		private void LoadLayoutFromFile()
		{
			using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForAssembly())
			{
				var isoStream = storage.OpenFile("RadDocking_Layout.xml", FileMode.Open);
				using (isoStream)
				{
					this.Docking.LoadLayout(isoStream);
				}
			}
		}

		private void SaveLayoutToFileButtonClick(object sender, RoutedEventArgs e)
		{
			this.SaveLayoutToFile();
			this.LoadLayoutFromFileButton.IsEnabled = true;
		}

		private void LoadLayoutFromFileButtonClick(object sender, RoutedEventArgs e)
		{
			this.LoadLayoutFromFile();
		}

		private void LoadLayoutFromStrButtonClick(object sender, RoutedEventArgs e)
		{
			this.LoadLayoutFromString(this.data.Xml);
		}

		private void SaveLayoutToStrButtonClick(object sender, RoutedEventArgs e)
		{
			this.data.Xml = this.SaveLayoutAsString();
			this.LoadLayoutFromStrButton.IsEnabled = true;
		}

		private void AddPaneButtonClick(object sender, RoutedEventArgs e)
		{
			RadPane radPane = new RadPane() { Title = "New sample RadPane" };
			radPane.Content = new TextBox() { Text = "TextBox" };
			RadPaneGroup radPaneGroup = new RadPaneGroup();
			RadSplitContainer radSplitContainer = new RadSplitContainer() { InitialPosition = DockState.FloatingDockable };
			RadDocking.SetFloatingLocation(radSplitContainer, new Point(400, 400));
			RadDocking.SetFloatingSize(radSplitContainer, new Size(200, 100));
			radPaneGroup.Items.Add(radPane);
			radSplitContainer.Items.Add(radPaneGroup);
			this.Docking.Items.Add(radSplitContainer);
		}

		private void AddPaneWithSerializationTagButtonClick(object sender, RoutedEventArgs e)
		{
			RadPane radPane = new RadPane() { Title = "New RadPane with SerializationTag", Name = "NewRadPane" };
			radPane.Content = new TextBox() { Text = "TextBox" };
			RadDocking.SetSerializationTag(radPane, "NewRadPane");
			RadPaneGroup radPaneGroup = new RadPaneGroup();
			RadSplitContainer radSplitContainer = new RadSplitContainer() { InitialPosition = DockState.FloatingDockable };
			RadDocking.SetFloatingLocation(radSplitContainer, new Point(700, 130));
			RadDocking.SetFloatingSize(radSplitContainer, new Size(500, 200));
			radPaneGroup.Items.Add(radPane);
			radSplitContainer.Items.Add(radPaneGroup);
			this.Docking.Items.Add(radSplitContainer);
			DisableButton(sender);
		}

		private static void DisableButton(object sender)
		{
			var radButton = sender as RadButton;
			radButton.IsEnabled = false;
		}

		private void RemovePaneButtonClick(object sender, RoutedEventArgs e)
		{
			if (!this.FlaotingOnlyPane.IsHidden)
			{
				this.FlaotingOnlyPane.RemoveFromParent();
				DisableButton(sender);
			}
		}

		private void ToggleButtonClick(object sender, RoutedEventArgs e)
		{
			if (this.LayoutXml.IsHidden)
			{
				this.LayoutXml.IsHidden = false;
			}
			else
			{
				this.LayoutXml.IsHidden = true;
			}
		}

        private void SaveLayoutToXElementButtonClick(object sender, RoutedEventArgs e)
        {
            var destinationStream = new MemoryStream();
            this.Docking.SaveLayout(destinationStream);
            destinationStream.Seek(0, SeekOrigin.Begin);
            this.dockingLayout = XElement.Load(destinationStream);
            this.LoadLayoutFromXElementButton.IsEnabled = true;
        }

        private void LoadLayoutFromXElementButtonClick(object sender, RoutedEventArgs e)
        {
            MemoryStream sourceAsStream = new MemoryStream();
            this.dockingLayout.Save(sourceAsStream);
            sourceAsStream.Seek(0, SeekOrigin.Begin);
            this.Docking.LoadLayout(sourceAsStream);
        }
	}
}
