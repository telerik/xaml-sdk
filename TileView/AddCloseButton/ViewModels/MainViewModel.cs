using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddCloseButton
{
    public class MainViewModel
    {
        public ObservableCollection<DataItem> Items { get; set; }

        public MainViewModel()
        {
            this.Items = new ObservableCollection<DataItem>();

            for (int i = 0; i < 5; i++)
            {
                var dataItem = new DataItem()
                {
                    Header = "Item " + i,
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit." +
                                "Nulla euismod dictum sapien dictum tempus." +
                                "In hac habitasse platea dictumst. Curabitur ut consectetur risus." +
                                "In neque magna, feugiat vitae ligula sit amet, condimentum tempus nulla." +
                                "Vestibulum condimentum est quis neque tincidunt interdum." +
                                "Nunc pellentesque orci id elit pharetra, at cursus odio tincidunt." +
                                "Quisque vel blandit est. Vivamus in purus eget eros iaculis pulvinar." +
                                "Aliquam erat volutpat. Nulla mi augue, molestie sit amet neque non, convallis fermentum enim." +
                                "Mauris faucibus ante sed orci sagittis vehicula. Cras ac felis ac est imperdiet gravida non id eros." +
                                "Vivamus odio eros, imperdiet vel lectus id, scelerisque pellentesque magna." +
                                "Vestibulum eleifend pharetra vestibulum." +
                                "Maecenas quis suscipit eros, nec dictum dui. In hac habitasse platea dictumst."
                };

                this.Items.Add(dataItem);
            }
        }
    }
}
