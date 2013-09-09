using System;
using System.Windows.Input;
using Telerik.Windows.Controls;

namespace ClearButton
{
    public class ViewModel : ViewModelBase
    {
        private DateTime? selectedDate;

        public ViewModel()
        {
            this.selectedDate = DateTime.Now;
            this.ClearSelectedDateCommand = new DelegateCommand(this.ClearSelectedDate);
        }

        public ICommand ClearSelectedDateCommand { get; private set; }

        /// <summary>
        /// Gets or sets SelectedDate and notifies for changes.
        /// </summary>
        public DateTime? SelectedDate
        {
            get
            {
                return this.selectedDate;
            }

            set
            {
                if (this.selectedDate != value)
                {
                    this.selectedDate = value;
                    this.OnPropertyChanged(() => this.SelectedDate);
                }
            }
        }

        private void ClearSelectedDate(object obj)
        {
            this.SelectedDate = null;
        }
    }
}
