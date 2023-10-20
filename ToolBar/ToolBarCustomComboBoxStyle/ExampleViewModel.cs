using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows;
using Telerik.Windows.Controls;

namespace CustomRadComboBoxStyle
{
    public class ExampleViewModel : ViewModelBase
    {
        private string selectedTheme;

        public ExampleViewModel()
        {
            this.Themes = (List<string>)this.AddThemes();
            this.SelectedTheme = this.Themes[0];
            this.ThemeChangedCommand = new DelegateCommand(OnThemeChanged);
        }

        public string SelectedTheme
        {
            get { return selectedTheme; }
            set { selectedTheme = value; OnPropertyChanged("SelectedTheme"); }
        }
        public List<string> Themes { get; set; }
        public ICommand ThemeChangedCommand { get; set; }

        private ICollection<string> AddThemes()
        {
            var collection = new List<string>
            {
                "Office_Black",
                "Windows8",
                "Windows8Touch",
                "Office2013",
                "Office2013_LightGray",
                "Office2013_DarkGray",
                "VisualStudio2013",
                "VisualStudio2013_Blue",
                "VisualStudio2013_Dark",
                "Green_Light",
                "Green_Dark",
                "Office2016",
                "Office2016Touch",
                "Fluent",
                "Fluent_Dark",
                "Material",
                "Material_Dark",
                "Crystal",
                "Crystal_Dark",
                "VisualStudio2019",
                "Office2019_Light",
                "Office2019_Gray",
                "Office2019_Dark",
                "Office2019_HighContrast"
            };

            return collection;
        }

        private void OnThemeChanged(object obj)
        {
            this.ChangeThemeNoXaml(this.SelectedTheme);
        }

        private void ChangeThemeNoXaml(string themeName)
        {
            themeName = ChoseTheme(themeName);

            Application.Current.Resources.MergedDictionaries.Clear();

            foreach (string file in GlobalConstants.assembliesXamlFiles)
            {
                string assembly = string.Format(@"/Telerik.Windows.Themes.{0};component/Themes" + file, themeName);

                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri(assembly, UriKind.RelativeOrAbsolute)
                });
            }

            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri("CustomStyles/" + themeName + "CustomStyles.xaml", UriKind.RelativeOrAbsolute)
            });
        }

        private string ChoseTheme(string themeName)
        {
            if (themeName.Contains("Office2013"))
            {
                switch (themeName)
                {
                    case "Office2013_LightGray":
                        Office2013Palette.LoadPreset(Office2013Palette.ColorVariation.LightGray);
                        break;
                    case "Office2013_DarkGray":
                        Office2013Palette.LoadPreset(Office2013Palette.ColorVariation.DarkGray);
                        break;
                    default:
                        Office2013Palette.LoadPreset(Office2013Palette.ColorVariation.White);
                        break;
                }

                themeName = "Office2013";
            }
            else if (themeName.Contains("VisualStudio2013"))
            {
                switch (themeName)
                {
                    case "VisualStudio2013_Dark":
                        VisualStudio2013Palette.LoadPreset(VisualStudio2013Palette.ColorVariation.Dark);
                        break;
                    case "VisualStudio2013_Blue":
                        VisualStudio2013Palette.LoadPreset(VisualStudio2013Palette.ColorVariation.Blue);
                        break;
                    default:
                        VisualStudio2013Palette.LoadPreset(VisualStudio2013Palette.ColorVariation.Light);
                        break;
                }
                themeName = "VisualStudio2013";
            }
            else if (themeName.Contains("Green"))
            {
                switch (themeName)
                {
                    case "Green_Dark":
                        GreenPalette.LoadPreset(GreenPalette.ColorVariation.Dark);
                        break;
                    default:
                        GreenPalette.LoadPreset(GreenPalette.ColorVariation.Light);
                        break;
                }

                themeName = "Green";
            }
            else if (themeName.Contains("Fluent"))
            {
                switch (themeName)
                {
                    case "Fluent_Dark":
                        FluentPalette.LoadPreset(FluentPalette.ColorVariation.Dark);
                        break;
                    default:
                        FluentPalette.LoadPreset(FluentPalette.ColorVariation.Light);
                        break;
                }

                themeName = "Fluent";
            }
            else if (themeName.Contains("Crystal"))
            {
                switch (themeName)
                {
                    case "Crystal_Dark":
                        CrystalPalette.LoadPreset(CrystalPalette.ColorVariation.Dark);
                        break;
                    default:
                        CrystalPalette.LoadPreset(CrystalPalette.ColorVariation.Light);
                        break;
                }

                themeName = "Crystal";
            }
            else if (themeName.Contains("Office2019"))
            {
                switch (themeName)
                {
                    case "Office2019_Gray":
                        Office2019Palette.LoadPreset(Office2019Palette.ColorVariation.Gray);
                        break;
                    case "Office2019_Dark":
                        Office2019Palette.LoadPreset(Office2019Palette.ColorVariation.Dark);
                        break;
                    case "Office2019_HighContrast":
                        Office2019Palette.LoadPreset(Office2019Palette.ColorVariation.HighContrast);
                        break;
                    default:
                        Office2019Palette.LoadPreset(Office2019Palette.ColorVariation.Light);
                        break;
                }

                themeName = "Office2019";
            }
            else if (themeName.Contains("Material"))
            {
                switch (themeName)
                {
                    case "Material_Dark":
                        MaterialPalette.LoadPreset(MaterialPalette.ColorVariation.Dark);
                        break;

                    default:
                        MaterialPalette.LoadPreset(MaterialPalette.ColorVariation.Light);
                        break;
                }

                themeName = "Material";
            }
            else if (themeName.Contains("VisualStudio2019"))
            {
                switch (themeName)
                {
                    case "VisualStudio2019_Dark":
                        VisualStudio2019Palette.LoadPreset(VisualStudio2019Palette.ColorVariation.Dark);
                        break;
                    default:
                        VisualStudio2019Palette.LoadPreset(VisualStudio2019Palette.ColorVariation.Blue);
                        break;
                }

                themeName = "VisualStudio2019";
            }

            return themeName;
        }
    }
}
