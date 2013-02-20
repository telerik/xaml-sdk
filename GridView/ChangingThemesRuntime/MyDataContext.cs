using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Telerik.Windows.Controls;

namespace ChangingThemesRuntime
{
    public class MyDataContext : ViewModelBase
    {
        ObservableCollection<MyObject> view;
        public ObservableCollection<MyObject> View
        {
            get
            {
                if (view == null)
                {
                    view = new ObservableCollection<MyObject>(from i in Enumerable.Range(0, 1000)
                                                              select new MyObject() { ID = i, Name = string.Format("Name{0}", i) });
                }

                return view;
            }
        }

        ObservableCollection<string> themes;
        public ObservableCollection<string> Themes
        {
            get
            {
                if (themes == null)
                {
                    themes = new ObservableCollection<string>(new string[] 
                    { 
                        "Office_Black", 
                        "Office_Silver", 
                        "Office_Blue",
                        "Expression_Dark",
                        "Transparent",
                        "Summer",
                        "Vista", 
                        "Windows7",
                        "Windows8",
                        "Windows8Touch",
                    });

                    SelectedTheme = themes.FirstOrDefault();
                }

                return themes;
            }
        }

        private string selectedTheme;

        public string SelectedTheme
        {
            get
            {
                return selectedTheme;
            }
            set
            {
                if (selectedTheme != value)
                {
                    selectedTheme = value;

                    this.RegisterTheme();

                    this.OnPropertyChanged("SelectedTheme");
                }
            }
        }

        private void RegisterTheme()
        {
            var themePath = string.Format("/Telerik.Windows.Themes.{0};component/Themes", this.SelectedTheme);

            var mergedDictionaries = Application.Current.Resources.MergedDictionaries;

            mergedDictionaries.Clear();

            mergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri(string.Format("{0}/System.Windows.xaml", themePath), UriKind.RelativeOrAbsolute)
            });

            mergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri(string.Format("{0}/Telerik.Windows.Controls.xaml", themePath), UriKind.RelativeOrAbsolute)
            });

            mergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri(string.Format("{0}/Telerik.Windows.Controls.Input.xaml", themePath), UriKind.RelativeOrAbsolute)
            });

            mergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri(string.Format("{0}/Telerik.Windows.Controls.GridView.xaml", themePath), UriKind.RelativeOrAbsolute)
            });
        }
    }

    public class MyObject
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
