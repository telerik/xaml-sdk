using System.Windows.Input;
using Telerik.Windows.Controls;

namespace MDILayout
{
    public class WindowViewModel : ViewModelBase
    {
        #region Fields
        private string header;
        private bool isMinimized;
        private bool isHidden;
        #endregion

        #region Properties
        public bool IsMinimized
        {
            get
            {
                return this.isMinimized;
            }
            set
            {
                if (this.isMinimized != value && this.IsClosedClicked)
                {
                    this.isMinimized = value;
                    this.OnPropertyChanged("IsMinimized");
                    this.IsClosedClicked = false;
                    this.IsHidden = value;
                }
            }
        }

        public bool IsHidden
        {
            get
            {
                return this.isHidden;
            }
            set
            {
                if (this.isHidden != value)
                {
                    this.isHidden = value;
                    this.OnPropertyChanged("IsHidden");
                }
            }
        }
        public bool IsClosedClicked { get; set; }
        public bool IsMaximized { get; set; }
        public double NormalVerticalOffSet { get; set; }
        public double NormalHorizontalOffSet { get; set; }
        public double NormalWidth { get; set; }
        public double NormalHeight { get; set; }
        public string Header
        {
            get
            {
                return this.header;
            }
            set
            {
                if (this.header != value)
                {
                    this.header = value;
                    this.OnPropertyChanged("Header");
                }
            }
        }
        public ICommand IsMinimizedCommand { get; set; }
        #endregion

        #region Ctor
        public WindowViewModel()
        {
            this.IsMinimizedCommand = new DelegateCommand(OnIsMinimizedCommand, (param) => { return true; });
            this.IsMaximized = false;
        }
        #endregion

        #region Methods
        public void OnIsMinimizedCommand(object param)
        {
            this.IsClosedClicked = true;
            this.IsMinimized = false;
        }
        #endregion
    }
}
