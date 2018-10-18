using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using Telerik.Windows.DragDrop;

namespace DragDropUsingCommands
{
    public class CommandDragDropBehavior : Behavior<ItemsControl>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            DragDropManager.AddDragInitializeHandler(this.AssociatedObject, OnDragInitialize);
            DragDropManager.AddGiveFeedbackHandler(this.AssociatedObject, OnGiveFeedback);
            DragDropManager.AddDragDropCompletedHandler(this.AssociatedObject, OnDragDropCompleted);
            DragDropManager.AddDropHandler(this.AssociatedObject, OnDrop);
        }

        private void OnDragInitialize(object sender, DragInitializeEventArgs args)
        {
            args.AllowedEffects = DragDropEffects.All;
            var payload = DragDropPayloadManager.GeneratePayload(null);
            var data = ((FrameworkElement)args.OriginalSource).DataContext;
            payload.SetData("DragData", data);
            args.Data = payload;
            args.DragVisual = new ContentControl { Content = data, ContentTemplate = this.DragVisualTemplate };
            args.DragVisualOffset = args.RelativeStartPoint;
        }

        private void OnGiveFeedback(object sender, Telerik.Windows.DragDrop.GiveFeedbackEventArgs args)
        {
            args.SetCursor(Cursors.Arrow);
            args.Handled = true;
        }

        private void OnDragDropCompleted(object sender, DragDropCompletedEventArgs args)
        {
            var data = DragDropPayloadManager.GetDataFromObject(args.Data, "DragData");

            var param = new DragDropParameter
            {
                DraggedItem = data,
                ItemsSource = this.AssociatedObject.ItemsSource
            };

            if (this.DragCommand != null && this.DragCommand.CanExecute(param))
            {
                this.DragCommand.Execute(param);
            }
        }

        private void OnDrop(object sender, Telerik.Windows.DragDrop.DragEventArgs args)
        {
            var data = ((DataObject)args.Data).GetData("DragData");

            var param = new DragDropParameter
            {
                DraggedItem = data,
                ItemsSource = this.AssociatedObject.ItemsSource
            };

            if (this.DropCommand != null && this.DropCommand.CanExecute(param))
            {
                this.DropCommand.Execute(param);
            }
        }

        public ICommand DragCommand
        {
            get { return (ICommand)GetValue(DragCommandProperty); }
            set { SetValue(DragCommandProperty, value); }
        }
        
        public static readonly DependencyProperty DragCommandProperty =
            DependencyProperty.Register("DragCommand", typeof(ICommand), typeof(CommandDragDropBehavior), new PropertyMetadata(null));

        public ICommand DropCommand
        {
            get { return (ICommand)GetValue(DropCommandProperty); }
            set { SetValue(DropCommandProperty, value); }
        }
        
        public static readonly DependencyProperty DropCommandProperty =
            DependencyProperty.Register("DropCommand", typeof(ICommand), typeof(CommandDragDropBehavior), new PropertyMetadata(null));

        public DataTemplate DragVisualTemplate
        {
            get { return (DataTemplate)GetValue(DragVisualTemplateProperty); }
            set { SetValue(DragVisualTemplateProperty, value); }
        }

        public static readonly DependencyProperty DragVisualTemplateProperty =
            DependencyProperty.Register("DragVisualTemplate", typeof(DataTemplate), typeof(CommandDragDropBehavior), new PropertyMetadata(null));

    }
}
