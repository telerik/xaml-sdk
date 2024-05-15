using System;
using System.Linq;
using System.Collections.ObjectModel;

namespace BringIntoView
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SampleViewModel : ObservableCollection<BusinessItem>
    {
        public BusinessItem GetItemByName(string name)
        {
            BusinessItem item = null;
            string[] parts = name == null ? new string[] { "0" } : name.Split('.');
            foreach (string s in parts)
            {
                try
                {
                    int index = Int32.Parse(s);

                    if (item == null)
                    {
                        item = (index >= 0 && index < this.Items.Count) ? this[index] : null;
                    }
                    else
                    {
                        item = (index >= 0 && index < item.Items.Count) ? item.Items[index] : null;
                    }
                }
                catch
                {
                    item = null;
                    break;
                }
            }

            return item;
        }

        public SampleViewModel()
        {
            for (int i = 0; i < 100; i++)
            {
                BusinessItem bi1 = new BusinessItem(null)
                {
                    Name = i.ToString(),
                    Level = 1,
                    IsSelected = false
                };

                for (int j = 0; j < 5; j++)
                {
                    BusinessItem bi2 = new BusinessItem(bi1)
                    {
                        Name = bi1.Name + "." + j,
                        Level = 2,
                        IsSelected = false
                    };

                    for (int k = 0; k < 5; k++)
                    {
                        BusinessItem bi3 = new BusinessItem(bi2)
                        {
                            Name = bi2.Name + "." + k,
                            Level = 3,
                            IsSelected = false
                        };
                        bi2.Items.Add(bi3);

                        for (int l = 0; l < 5; l++)
                        {
                            BusinessItem bi4 = new BusinessItem(bi3)
                            {
                                Name = bi3.Name + "." + l,
                                Level = 4,
                                IsSelected = false
                            };
                            bi3.Items.Add(bi4);
                        }
                    }

                    bi1.Items.Add(bi2);
                }

                this.Add(bi1);
            }
        }
    }
}
