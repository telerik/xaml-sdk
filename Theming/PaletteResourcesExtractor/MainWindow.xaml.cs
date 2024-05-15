using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Telerik.Windows.Controls;

namespace PaletteResourcesExtractor
{
    public partial class MainWindow : Window
    {
        private string lastGeneratedPaletteThemeName;

        public MainWindow()
        {
            StyleManager.ApplicationTheme = new Office2019Theme();
            InitializeComponent();

            if (!TelerikAssemblyHelper.CheckIfSupported())
            {
                MessageBox.Show(
                    "PaletteResourcesExtractor is supported with Telerik UI for WPF version 2023.2.703 or later. " +
                    "Please replace the Telerik.Windows.Controls.dll assembly reference with a newer version.\n\n" +
                    "The application is now closing.");
                this.Close();                
            }

            var themes = PaletteResourcesManager.GetThemesWithPalettes();
            this.themesComboBox.ItemsSource = themes;
            this.themesComboBox.SelectedItem = themes.FirstOrDefault(x => x.Name.Contains("Windows11"));
        }

        private void OnThemesComboBoxSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0 && !string.IsNullOrEmpty(lastGeneratedPaletteThemeName))
            {
                var newThemeType = (Type)e.AddedItems[0];
                this.generatePaletteButton.IsEnabled = !newThemeType.Name.Equals(lastGeneratedPaletteThemeName);
            }
        }

        private void OnGeneratePaletteResources(object sender, RoutedEventArgs e)
        {
            var selectedThemeType = (Type)this.themesComboBox.SelectedItem;
            this.tabControl.ItemsSource = PaletteResourcesManager.ExtractPaletteResourcesForTheme(selectedThemeType);
            ;
            this.tabControl.SelectedIndex = 0;
            this.generatePaletteButton.IsEnabled = false;
            lastGeneratedPaletteThemeName = selectedThemeType.Name;
        }

        private void OnAddToClipboardButtonClick(object sender, RoutedEventArgs e)
        {
            var selectedPaletteItem = (PaletteResourceInfo)this.tabControl.SelectedItem;
            Clipboard.SetText(selectedPaletteItem.Content);
        }

        private void OnSaveSelectedTabButtonClick(object sender, RoutedEventArgs e)
        {
            var selectedPaletteItem = (PaletteResourceInfo)this.tabControl.SelectedItem;
            var saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            saveFileDialog.Filter = "Xaml files (.xaml)|*.xaml";
            saveFileDialog.FileName = selectedPaletteItem.Name;
            if (saveFileDialog.ShowDialog() == true)
            {
                PaletteResourcesManager.SavePaletteResourcesToFile(selectedPaletteItem.Resources.ToResourceDictionary(), saveFileDialog.FileName);
            }
        }

        private void OnSaveAllTabsButtonClick(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    var palettes = (List<PaletteResourceInfo>)tabControl.ItemsSource;
                    foreach (var palette in palettes)
                    {
                        PaletteResourcesManager.SavePaletteResourcesToFile(palette.Resources.ToResourceDictionary(), dialog.SelectedPath + "\\" + palette.Name + ".xaml");
                    }
                }
            }
        }

        private void OnGenerateAllResourceFilesClick(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    PaletteResourcesManager.SavePaletteResourcesToFiles(dialog.SelectedPath + "\\");
                }
            }
        }
    }
}
