using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input; 

using Telerik.Windows.Documents.Fixed.Search;
using Telerik.Windows.Controls;
using Telerik.Windows.Documents.Fixed.UI.Dialogs;
using CustomFindDialog.Commands;
namespace CustomFindDialog
{
    public class FindDialogViewModel : INotifyPropertyChanged
    { 
        private readonly TextSearchOptions textSearchOptions;
        private readonly ICommand findNextCommand;
        private readonly ICommand findPreviousCommand;

        public FindDialogViewModel(FindDialogContext context)
        {
            this.textSearchOptions = new TextSearchOptions(false, false, false);
            this.findNextCommand = new FindNextCommand(context.FixedDocumentViewer, this.TextSearchOptions);
            this.findPreviousCommand = new FindPreviousCommand(context.FixedDocumentViewer, this.TextSearchOptions);
        }
         
        public ICommand FindNextCommand
        {
            get
            {
                return this.findNextCommand;
            }
        } 

        public ICommand FindPreviousCommand
        {
            get
            {
                return this.findPreviousCommand;
            }
        }

        public TextSearchOptions TextSearchOptions
        {
            get
            {
                return this.textSearchOptions;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
