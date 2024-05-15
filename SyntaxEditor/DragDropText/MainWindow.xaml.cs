using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.SyntaxEditor.Taggers;
using Telerik.Windows.Controls.SyntaxEditor.Tagging.Taggers;
using Telerik.Windows.Controls.SyntaxEditor.UI;
using Telerik.Windows.DragDrop;

namespace DragDropText
{
    public partial class MainWindow : Window
    {
        private CaretPosition dropPos;

        public MainWindow()
        {
            InitializeComponent();
            DragDropManager.AddDragInitializeHandler(this.treeview, OnDragInitialize);
            this.treeview.ItemsSource = Snippet.GetSnippets();
            this.syntaxEditor.TaggersRegistry.RegisterTagger(new CSharpTagger(this.syntaxEditor));
            this.syntaxEditor.TaggersRegistry.RegisterTagger(new CSharpFoldingTagger(this.syntaxEditor));
        }

        private void OnDragInitialize(object sender, DragInitializeEventArgs e)
        {
            e.AllowedEffects = DragDropEffects.All;
            string text = (e.OriginalSource as RadTreeViewItem).DataContext.ToString();
            e.DragVisual = new ContentControl { Content = text };
            e.Data = text;
        }

        private void OnEditorLoaded(object sender, RoutedEventArgs e)
        {
            this.syntaxEditor.EditorPresenter.AllowDrop = true;
            DragDropManager.AddDragOverHandler(this.syntaxEditor.EditorPresenter, OnDragOver);

            DragDropManager.AddDropHandler(this.syntaxEditor.EditorPresenter, OnDrop);
        }

        private void OnDrop(object sender, Telerik.Windows.DragDrop.DragEventArgs e)
        {
            string text = ((DataObject)e.Data).GetData(typeof(string)) as string;
            this.syntaxEditor.Insert(text);
        }

        private void OnDragOver(object sender, Telerik.Windows.DragDrop.DragEventArgs e)
        {
            Point mousepos = e.GetPosition(this.syntaxEditor.EditorPresenter);
            dropPos = this.syntaxEditor.EditorPresenter.GetPositionFromViewPoint(mousepos);
            this.syntaxEditor.CaretPosition.MoveToPosition(dropPos);
            this.syntaxEditor.Focus();
        }
    }
}
