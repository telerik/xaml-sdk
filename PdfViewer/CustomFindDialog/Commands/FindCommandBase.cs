using System; 
using System.Linq;
using System.Text;
using System.Windows.Input;
using Telerik.Windows.Controls;
using Telerik.Windows.Documents.Fixed.Search; 

namespace CustomFindDialog.Commands
{
    public abstract class FindCommandBase : ICommand
    {
        private readonly TextSearchOptions textSearchOptions; 
        private readonly FixedDocumentViewerBase viewer; 

        public FindCommandBase(FixedDocumentViewerBase viewer, TextSearchOptions textSearchOptions)
        {
            this.viewer = viewer;
            this.textSearchOptions = textSearchOptions;
        }
         
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public TextSearchOptions TextSearchOptions
        {
            get
            {
                return this.textSearchOptions;
            }
        }

        public FixedDocumentViewerBase Viewer
        {
            get
            {
                return this.viewer;
            }
        } 

        protected abstract void HandleNotFoundResult(); 

        public bool CanExecute(object parameter)
        {
            if (this.viewer == null) 
            {
                return false;
            }

            string text = parameter as string;
            return !string.IsNullOrEmpty(text);
        } 

        public abstract void Execute(object parameter);

        protected void HandlerSearchResult(SearchResult result)
        {
            if (this.Viewer == null ||
                this.Viewer.Document == null)
            {
                return;
            }

            this.Viewer.Document.Selection.Clear();
            
            if (result == null)
            {
                return;
            }
            
            if (result == SearchResult.NotFound)
            {
                this.HandleNotFoundResult();
                return;
            }

            this.Viewer.Document.CaretPosition.MoveToPosition(result.Range.StartPosition);
            this.Viewer.Select(result.Range);
        } 
    }
}
