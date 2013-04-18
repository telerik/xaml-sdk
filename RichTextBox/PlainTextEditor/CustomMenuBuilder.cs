using System;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.RichTextBoxUI.Menus;

namespace PlainTextEditor
{
    public class CustomMenuBuilder : ContextMenuContentBuilder
    {
        private RadRichTextBox radRichTextBox;

        public CustomMenuBuilder(RadRichTextBox radRichTextBox)
            : base(radRichTextBox)
        {
            this.radRichTextBox = radRichTextBox;
        }

        protected override ContextMenuGroup CreateTextEditCommands()
        {
            return new ContextMenuGroup(ContextMenuGroupType.TextEditCommands);
        }

        protected override ContextMenuGroup CreateHyperlinkCommands(bool forExistingHyperlink)
        {
            return new ContextMenuGroup(ContextMenuGroupType.HyperlinkCommands);
        }
    }
}
