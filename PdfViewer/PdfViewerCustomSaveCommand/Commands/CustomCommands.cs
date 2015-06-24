using System;
using Telerik.Windows.Controls;
using System.IO;

namespace PdfViewerCustomSaveCommand.Commands
{
    public class CustomCommands
    {
        private readonly Stream stream;

        public CustomCommands(RadPdfViewer viewer)
        {
            stream = new MemoryStream();
            this.openCommand = new CustomOpenCommand(viewer, stream);
            this.saveCommand = new CustomSaveCommand(viewer, stream);
        }

        private CustomOpenCommand openCommand;
        public CustomOpenCommand OpenCommand
        {
            get
            {
                return openCommand;
            }
            private set
            {
                openCommand = value;
            }
        }

        private CustomSaveCommand saveCommand;
        public CustomSaveCommand SaveCommand
        {
            get
            {
                return saveCommand;
            }
            private set
            {
                saveCommand = value;
            }
        }
    }
}
