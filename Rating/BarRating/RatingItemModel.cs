using Telerik.Windows.Controls;

namespace BarRating
{
    public class RatingItemModel : ViewModelBase
    {
        private int votesCount;

        public int Value { get; set; }

        public int VotesCount
        {
            get { return votesCount; }
            set
            {
                if (this.votesCount != value)
                {
                    this.votesCount = value;
                    this.OnPropertyChanged("VotesCount");
                }
            }
        }
    }
}
