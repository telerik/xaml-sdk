using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace PlainTextEditor
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent(); 
            editor.PreviewEditorKeyDown += new Telerik.Windows.Documents.PreviewEditorKeyEventHandler(editor_PreviewEditorKeyDown);

            Telerik.Windows.Controls.RichTextBoxUI.ContextMenu contextMenu = (Telerik.Windows.Controls.RichTextBoxUI.ContextMenu)this.editor.ContextMenu;
            contextMenu.ContentBuilder = new CustomMenuBuilder(this.editor);
        }

        void editor_PreviewEditorKeyDown(object sender, Telerik.Windows.Documents.PreviewEditorKeyEventArgs e)
        {
            if (
                (Keyboard.Modifiers.HasFlag(ModifierKeys.Control) && e.Key == Key.B) ||
                (Keyboard.Modifiers.HasFlag(ModifierKeys.Control) && Keyboard.Modifiers.HasFlag(ModifierKeys.Shift) && e.Key == Key.B) ||
                (Keyboard.Modifiers.HasFlag(ModifierKeys.Control) && e.Key == Key.I) ||
                (Keyboard.Modifiers.HasFlag(ModifierKeys.Control) && Keyboard.Modifiers.HasFlag(ModifierKeys.Shift) && e.Key == Key.I) ||
                (Keyboard.Modifiers.HasFlag(ModifierKeys.Control) && e.Key == Key.U)
                )
            {
                e.SuppressDefaultAction = true;
                e.OriginalArgs.Handled = true;
            }
        }

    }
}
