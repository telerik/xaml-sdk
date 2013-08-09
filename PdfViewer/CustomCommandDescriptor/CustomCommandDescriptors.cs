using System;
using System.Linq;
using Telerik.Windows.Controls;
using Telerik.Windows.Documents.Commands.Descriptors;

namespace CustomCommandDescriptor
{
    public class CustomCommandDescriptors : DefaultCommandDescriptors
    {
        private readonly CommandDescriptor fitToWidthCommandDescriptor;

        public CommandDescriptor FitToWidthCommandDescriptor
        {
            get
            {
                return this.fitToWidthCommandDescriptor;
            }
        }

        public CustomCommandDescriptors(FixedDocumentViewerBase fixedDocumentViewerBase)
            : base(fixedDocumentViewerBase)
        {
            this.fitToWidthCommandDescriptor = new CommandDescriptor(new FitToWidthCommand(fixedDocumentViewerBase));
        }
    }
}
