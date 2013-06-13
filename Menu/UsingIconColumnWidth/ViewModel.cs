using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace UsingIconColumnWidth
{
    public class ViewModel
    {
        public ObservableCollection<MenuItem> Images { get; set; }
        public ObservableCollection<MenuItem> ThumbButtons { get; set; }
        public ObservableCollection<MenuItem> ImageSizes { get; set; }

        public ViewModel()
        {
            this.Images = CreateImagesMenuItems();
            this.ThumbButtons = CreateThumbButtonsMenuItems();
            this.ImageSizes = CreateImageSizesMenuItems();
        }

        private ObservableCollection<MenuItem> CreateImageSizesMenuItems()
        {
            return new ObservableCollection<MenuItem>()
            {
                new MenuItem { Header = "Koala.jpg", IconURL = "Images/Koala16.jpg" },
                new MenuItem { Header = "Koala.jpg", IconURL = "Images/Koala32.jpg" },
                new MenuItem { Header = "Koala.jpg", IconURL = "Images/Koala.jpg" },
                new MenuItem { Header = "Koala.jpg", IconURL = "Images/Koala128.jpg" },
            };
        }

        private ObservableCollection<MenuItem> CreateThumbButtonsMenuItems()
        {
            return new ObservableCollection<MenuItem>()
            {
                new MenuItem { Header = "Images/Koala.jpg"},
                new MenuItem { Header = "Images/Desert.jpg"},
                new MenuItem { Header = "Images/Chrysanthemum.jpg"},
                new MenuItem { Header = "Images/Jellyfish.jpg"},
                new MenuItem { Header = "Images/Lighthouse.jpg"},
                new MenuItem { Header = "Images/Tulips.jpg"}
            };
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
                new MenuItem { Header = "Tulips.jpg", IconURL = "Images/Tulips.jpg" }
            };
        }
    }
}
