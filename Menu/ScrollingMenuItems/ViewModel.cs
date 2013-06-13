using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ScrollingMenuItems
{
    public class ViewModel
    {
        public ObservableCollection<MenuItem> Images { get; set; }

        public ViewModel()
        {
            this.Images = CreateImagesMenuItems();
        }

        private ObservableCollection<MenuItem> CreateImagesMenuItems()
        {
            return new ObservableCollection<MenuItem>()
            {
                new MenuItem { Header = "Koala.jpg", IconURL = "Images/Koala.jpg" },
                new MenuItem { Header = "Desert.jpg", IconURL = "Images/Desert.jpg" },
                new MenuItem { Header = "Chrysanthemum.jpg", IconURL = "Images/Chrysanthemum.jpg" },
                new MenuItem { Header = "Jellyfish.jpg", IconURL = "Images/Jellyfish.jpg" },
                new MenuItem { Header = "Lighthouse.jpg", IconURL = "Images/Lighthouse.jpg" },
                new MenuItem { Header = "Tulips.jpg", IconURL = "Images/Tulips.jpg" },
                new MenuItem { Header = "Koala.jpg", IconURL = "Images/Koala.jpg" },
                new MenuItem { Header = "Desert.jpg", IconURL = "Images/Desert.jpg" },
                new MenuItem { Header = "Chrysanthemum.jpg", IconURL = "Images/Chrysanthemum.jpg" },
                new MenuItem { Header = "Jellyfish.jpg", IconURL = "Images/Jellyfish.jpg" },
                new MenuItem { Header = "Lighthouse.jpg", IconURL = "Images/Lighthouse.jpg" },
                new MenuItem { Header = "Tulips.jpg", IconURL = "Images/Tulips.jpg" },
                new MenuItem { Header = "Koala.jpg", IconURL = "Images/Koala.jpg" },
                new MenuItem { Header = "Desert.jpg", IconURL = "Images/Desert.jpg" },
                new MenuItem { Header = "Chrysanthemum.jpg", IconURL = "Images/Chrysanthemum.jpg" },
                new MenuItem { Header = "Jellyfish.jpg", IconURL = "Images/Jellyfish.jpg" },
                new MenuItem { Header = "Lighthouse.jpg", IconURL = "Images/Lighthouse.jpg" },
                new MenuItem { Header = "Tulips.jpg", IconURL = "Images/Tulips.jpg" },
                new MenuItem { Header = "Koala.jpg", IconURL = "Images/Koala.jpg" },
                new MenuItem { Header = "Desert.jpg", IconURL = "Images/Desert.jpg" },
                new MenuItem { Header = "Chrysanthemum.jpg", IconURL = "Images/Chrysanthemum.jpg" },
                new MenuItem { Header = "Jellyfish.jpg", IconURL = "Images/Jellyfish.jpg" },
                new MenuItem { Header = "Lighthouse.jpg", IconURL = "Images/Lighthouse.jpg" },
                new MenuItem { Header = "Tulips.jpg", IconURL = "Images/Tulips.jpg" },
                new MenuItem { Header = "Koala.jpg", IconURL = "Images/Koala.jpg" },
                new MenuItem { Header = "Desert.jpg", IconURL = "Images/Desert.jpg" },
                new MenuItem { Header = "Chrysanthemum.jpg", IconURL = "Images/Chrysanthemum.jpg" },
                new MenuItem { Header = "Jellyfish.jpg", IconURL = "Images/Jellyfish.jpg" },
                new MenuItem { Header = "Lighthouse.jpg", IconURL = "Images/Lighthouse.jpg" },
                new MenuItem { Header = "Tulips.jpg", IconURL = "Images/Tulips.jpg" },
                new MenuItem { Header = "Koala.jpg", IconURL = "Images/Koala.jpg" },
                new MenuItem { Header = "Desert.jpg", IconURL = "Images/Desert.jpg" },
                new MenuItem { Header = "Chrysanthemum.jpg", IconURL = "Images/Chrysanthemum.jpg" },
                new MenuItem { Header = "Jellyfish.jpg", IconURL = "Images/Jellyfish.jpg" },
                new MenuItem { Header = "Lighthouse.jpg", IconURL = "Images/Lighthouse.jpg" },
                new MenuItem { Header = "Tulips.jpg", IconURL = "Images/Tulips.jpg" },
                new MenuItem { Header = "Koala.jpg", IconURL = "Images/Koala.jpg" },
                new MenuItem { Header = "Desert.jpg", IconURL = "Images/Desert.jpg" },
                new MenuItem { Header = "Chrysanthemum.jpg", IconURL = "Images/Chrysanthemum.jpg" },
                new MenuItem { Header = "Jellyfish.jpg", IconURL = "Images/Jellyfish.jpg" },
                new MenuItem { Header = "Lighthouse.jpg", IconURL = "Images/Lighthouse.jpg" },
                new MenuItem { Header = "Tulips.jpg", IconURL = "Images/Tulips.jpg" },
            };
        }

    }
}
