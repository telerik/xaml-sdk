using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Telerik.Windows.Controls;
using Telerik.Windows.Documents.Fixed.Model;
using Telerik.Windows.Documents.Fixed.Model.Editing;
using Telerik.Windows.Documents.Fixed.UI.Layers;
using Telerik.Windows.Documents.Fixed.Utilities.Rendering;

namespace AddDocumentContent
{
    public class AddTextUILayer : IUILayer
    {
        private static readonly string LayerName = "AddTextUILayer";
        private readonly Canvas canvas;
        private bool hasActiveTextBox;
        private UILayerInitializeContext context;
        private Point mouseLocation;

        static AddTextUILayer()
        {
            IsEditModeEnabled = true;
        }

        public AddTextUILayer()
        {
            this.canvas = new Canvas();
            this.canvas.MouseLeftButtonDown += this.Canvas_MouseLeftButtonDown;
            this.canvas.MouseLeftButtonUp += this.Canvas_MouseLeftButtonUp;
        }

        public Canvas UIElement
        {
            get
            {
                return this.canvas;
            }
        }

        public string Name
        {
            get
            {
                return LayerName;
            }
        }

        public static bool IsEditModeEnabled { get; set; }

        public void Initialize(UILayerInitializeContext context)
        {
            this.context = context;

            double width = PageLayoutHelper.GetActualWidth(context.Page);
            double height = PageLayoutHelper.GetActualHeight(context.Page);

            this.canvas.Width = width;
            this.canvas.Height = height;
            this.canvas.Background = Brushes.Transparent;
        }

        public void Update(UILayerUpdateContext context)
        {
        }

        public void Clear()
        {
            this.canvas.Children.Clear();
        }

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!this.hasActiveTextBox && IsEditModeEnabled)
            {
                e.Handled = true;
            }
        }

        private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (this.hasActiveTextBox)
            {
                return;
            }

            e.Handled = true;

            this.mouseLocation = e.GetPosition(this.canvas);

            TextBox textBox = new TextBox();
            this.context.AddFocusableElement(textBox);

            textBox.MinWidth = 200;
            this.canvas.Children.Add(textBox);

            Canvas.SetLeft(textBox, this.mouseLocation.X);
            Canvas.SetTop(textBox, this.mouseLocation.Y);

            textBox.LostFocus += this.TextBox_LostFocus;
            textBox.KeyDown += this.TextBox_KeyDown;

            Dispatcher.CurrentDispatcher.BeginInvoke(new Action(() => textBox.Focus()), DispatcherPriority.Input);

            this.hasActiveTextBox = true;
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.SubmitChangesAndRemoveTextBox(sender);
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            this.SubmitChangesAndRemoveTextBox(sender);
        }

        private void SubmitChangesAndRemoveTextBox(object sender)
        {
            TextBox textBox = (TextBox)sender;

            textBox.LostFocus -= this.TextBox_LostFocus;
            textBox.KeyDown -= this.TextBox_KeyDown;

            this.canvas.Children.Remove(textBox);

            string text = textBox.Text;

            RadFixedPage page = this.context.Page;

            FixedContentEditor editor = new FixedContentEditor(page);
            editor.Position.Translate(this.mouseLocation.X, this.mouseLocation.Y);
            editor.DrawText(text);

            RadPdfViewer viewer = this.context.Presenter.Owner as RadPdfViewer;
            if (viewer != null)
            {
                viewer.InvalidatePageUI(page);
            }

            this.hasActiveTextBox = false;
        }
    }
}
