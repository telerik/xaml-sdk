using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DrawOnDocument
{
    public class MainPageDrawViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsDrawModeEnabled
        {
            get
            {
                return DrawUILayer.IsDrawModeEnabled;
            }
            set
            {
                if (DrawUILayer.IsDrawModeEnabled != value)
                {
                    DrawUILayer.IsDrawModeEnabled = value;
                    DrawUILayer.UpdateCursor();
                    this.OnProperyChanged("IsDrawModeEnabled");
                }
            }
        }

        protected void OnProperyChanged([CallerMemberName] string propertyName = null)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
