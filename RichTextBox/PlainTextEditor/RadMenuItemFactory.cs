using System;
using Telerik.Windows.Controls;
using Telerik.Windows.Documents.RichTextBoxCommands;

namespace PlainTextEditor
{
    internal static class RadMenuItemFactory
    {
        private const string BaseImagePath = "/Telerik.Windows.Controls.RichTextBoxUI;component/Images/MSOffice/";

        public static RadMenuItem CreateMenuItem(string text, string relativePath)
        {
            return CreateMenuItem(text, relativePath, null, null);
        }

        public static RadMenuItem CreateMenuItem(string text, string imageRelativePath, RichTextBoxCommandBase command, object commandParameter = null)
        {
            RadMenuItem menuItem = new RadMenuItem();
            menuItem.Header = text;

            if (imageRelativePath != null)
            {
                menuItem.Icon = new System.Windows.Controls.Image() { Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(BaseImagePath + imageRelativePath, UriKind.Relative)), Stretch = System.Windows.Media.Stretch.None };
            }

            menuItem.Command = command;

            if (commandParameter != null)
            {
                menuItem.CommandParameter = commandParameter;
            }

            return menuItem;
        }
    }
}
