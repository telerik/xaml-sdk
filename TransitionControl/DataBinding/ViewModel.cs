using System.Collections.ObjectModel;

namespace DataBinding
{
    public class ViewModel
    {
        public ObservableCollection<Photo> Photos { get; set; }
        
        public ViewModel()
        {
            this.Photos = new ObservableCollection<Photo>();
            var names = new string[] { "John", "Sarah", "Ivan", "Mark", "Anthony" };

            for (int i = 1; i <= 5; i++)
            {
                string directory = string.Format("../../images/image{0}.png", i);
                this.Photos.Add(new Photo() { Name = names[i - 1], ImageUrl = directory });
            }
        }
    }
}
