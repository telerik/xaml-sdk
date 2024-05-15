using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AddDocumentContent
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsEditModeEnabled
        {
            get
            {
                return AddTextUILayer.IsEditModeEnabled;
            }
            set
            {
                if (AddTextUILayer.IsEditModeEnabled != value)
                {
                    AddTextUILayer.IsEditModeEnabled = value;
                    this.OnProperyChanged();
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
