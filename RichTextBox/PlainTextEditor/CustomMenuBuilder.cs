using System;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.RichTextBoxUI.Menus;

namespace PlainTextEditor
{
    public class CustomMenuBuilder : ContextMenuContentBuilder
    {
        public CustomMenuBuilder()
            : base() { }

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
