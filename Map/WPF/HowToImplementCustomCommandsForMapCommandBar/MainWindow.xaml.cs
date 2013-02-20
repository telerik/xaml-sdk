using System;
using System.Windows;
using System.Windows.Input;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Map;

namespace HowToImplementCustomCommandsForMapCommandBar
{
    public partial class MainWindow : Window
    {
        private const string ImagePathFormat = "/HowToImplementCustomCommandsForMapCommandBar;component/Images/{0}.png";

        public MainWindow()
        {
            InitializeComponent();

            OpenStreetMapProvider provider = new OpenStreetMapProvider();

            provider.Commands.Clear();

            this.AddCustomCommandAction(provider, "Restaurants");
            this.AddCustomCommandAction(provider, "Bars");
            this.AddCustomCommandAction(provider, "Museums");

            CommandDescription commandDescription = new CommandDescription()
            {
                Command = new DelegateCommand(this.ExecuteCommand),
                CommandParameter = "test",
                DataTemplate = this.Resources["CustomTemplate2"] as DataTemplate
            };

            provider.Commands.Add(commandDescription);

            this.RadMap1.Provider = provider;
        }

        private void AddCustomCommandAction(OpenStreetMapProvider provider, string poi)
        {
            string commandText = string.Format("Find {0}", poi);
            string commandName = string.Format("Find{0}Command", poi);

            CommandDescription commandDescription = new CommandDescription();
            commandDescription.Command = new RoutedUICommand(commandText, commandName, typeof(OpenStreetMapProvider));
            commandDescription.CommandParameter = poi;
            commandDescription.DataTemplate = this.LayoutRoot.Resources["CustomCommandDataTemplate"] as DataTemplate;

            string imagePath = string.Format(ImagePathFormat, poi);
            commandDescription.ImageUri = new Uri(imagePath, UriKind.RelativeOrAbsolute);

            CommandBinding commandBinding = new CommandBinding(commandDescription.Command, ExecuteCustomCommand);
            provider.Commands.Add(commandDescription);
            provider.CommandBindingCollection.Add(commandBinding);
        }

        private void ExecuteCustomCommand(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show(string.Format("My custom command: Find {0}", e.Parameter));
        }

        private void ExecuteCommand(object parameter)
        {
            MessageBox.Show(string.Format("My command: {0}", parameter));
        }
    }
}
