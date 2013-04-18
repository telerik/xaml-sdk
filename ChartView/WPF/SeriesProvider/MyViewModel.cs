using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SeriesProvider
{
    public class MyViewModel : DependencyObject
    {
        /// <summary>
        /// Identifies the <see cref="Data"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty DataProperty = DependencyProperty.Register("Data",
            typeof(ObservableCollection<SeriesProviderDataItem>),
            typeof(MyViewModel),
            new PropertyMetadata(null));

        public ObservableCollection<SeriesProviderDataItem> Data
        {
            get
            {
                return (ObservableCollection<SeriesProviderDataItem>)this.GetValue(DataProperty);
            }
            set
            {
                this.SetValue(DataProperty, value);
            }
        }

        public MyViewModel()
        {
            this.Data = new ObservableCollection<SeriesProviderDataItem>() {
                new SeriesProviderDataItem() { Items = new ObservableCollection<MyDataObject>() {
                    new MyDataObject() { Category = "A", Value = 5},
                    new MyDataObject() { Category = "B", Value = 7},
                    new MyDataObject() { Category = "C", Value = 6},
                    new MyDataObject() { Category = "D", Value = 8}
                }
                },
                new SeriesProviderDataItem() { Items = new ObservableCollection<MyDataObject>() {
                    new MyDataObject() { Category = "A", Value = 15},
                    new MyDataObject() { Category = "B", Value = 18},
                    new MyDataObject() { Category = "C", Value = 19},
                    new MyDataObject() { Category = "D", Value = 23}
                }
                },
                new SeriesProviderDataItem() { Items = new ObservableCollection<MyDataObject>() {
                    new MyDataObject() { Category = "A", Value = 21},
                    new MyDataObject() { Category = "B", Value = 25},
                    new MyDataObject() { Category = "C", Value = 26},
                    new MyDataObject() { Category = "D", Value = 25}
                }
                }
            };
        }
    }
}
