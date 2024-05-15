using System; 
using System.Linq; 
using System.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Documents.Fixed.Search; 

namespace CustomFindDialog.Commands
{
    public class FindNextCommand : FindCommandBase
    {
        public FindNextCommand(FixedDocumentViewerBase viewer, TextSearchOptions textSearchOptions)
            : base(viewer, textSearchOptions)
        {
        }

        public override void Execute(object parameter)
        {
            string text = parameter as string;
            SearchResult result = this.Viewer.Find(text, this.TextSearchOptions);
            this.HandlerSearchResult(result);
        }

        protected override void HandleNotFoundResult()
        {
            MessageBox.Show("End of document reached");
        }
    }
}
