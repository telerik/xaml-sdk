using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Input;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Data.PropertyGrid;

namespace BindPropertyDefinitionsViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ICommand AddSettingCommand { get; set; }

        public MainWindowViewModel()
        {
            this.AddSettingCommand = new DelegateCommand(AddSetting);
        }

        ObservableCollection<PropertyDefinition> propertyDefinitions;
        public ObservableCollection<PropertyDefinition> PropertyDefinitions
        {
            get
            {
                if (propertyDefinitions == null)
                {
                    propertyDefinitions = new ObservableCollection<PropertyDefinition>();
                }

                return propertyDefinitions;
            }
        }

        private ObservableCollection<Setting> settings;

        public ObservableCollection<Setting> Settings
        {
            get
            {
                if (this.settings == null)
                {
                    this.settings = new ObservableCollection<Setting>();
                    this.settings.CollectionChanged += Settings_CollectionChanged;
                    settings.Add(new Setting() { Key = "setting1", Value = "value1" });
                    settings.Add(new Setting() { Key = "setting2", Value = "value2" });
                    settings.Add(new Setting() { Key = "setting3", Value = "value3" });
                    settings.Add(new Setting() { Key = "setting4", Value = "value4" });
                }

                return this.settings;
            }
        }

        private void AddSetting(object obj)
        {
            this.Settings.Add(new Setting() { Key = "setting" + (this.Settings.Count + 1).ToString(), Value = "value" + (this.Settings.Count + 1).ToString() });
        }

        private void Settings_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Setting item in e.NewItems)
                {
                    this.PropertyDefinitions.Add(new PropertyDefinition() { DisplayName = item.Key, Binding = new Binding("Value") { Source = item } });
                }
            }
        }
    }
}
