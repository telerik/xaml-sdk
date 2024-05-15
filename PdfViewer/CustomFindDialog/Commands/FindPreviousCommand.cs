using System; 
using System.Linq; 
using System.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Documents.Fixed.Search; 

namespace CustomFindDialog.Commands
{
    class FindPreviousCommand : FindCommandBase
    {        
        public FindPreviousCommand(FixedDocumentViewerBase viewer, TextSearchOptions textSearchOptions)
            : base(viewer, textSearchOptions)
        {
        }

        public override void Execute(object parameter)
        {
            string text = parameter as string;
            SearchResult result = this.Viewer.FindPrevious(text, this.TextSearchOptions);
            this.HandlerSearchResult(result);
        }

        protected override void HandleNotFoundResult()
        {
            MessageBox.Show("Beginning of document reached.");
        }

    }
}
