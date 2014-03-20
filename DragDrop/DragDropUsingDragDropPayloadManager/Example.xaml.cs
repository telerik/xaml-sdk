using System.Collections;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.DragDrop;

namespace DragDropUsingDragDropPayloadManager
{
    /// <summary>
    /// Interaction logic for Example.xaml
    /// </summary>
    public partial class Example : UserControl
    {
        public Example()
        {
            InitializeComponent();

            DragDropManager.AddDragInitializeHandler(this.ListBox1, OnDragInitialize);
            DragDropManager.AddDragDropCompletedHandler(this.ListBox1, OnDragDropCompleted);

            DragDropManager.AddDropHandler(this.ListBox2, OnDrop);
            DragDropManager.AddDragOverHandler(this.ListBox2, OnDragOver);
        }

        private void OnDragOver(object sender, Telerik.Windows.DragDrop.DragEventArgs e)
        {
            var formats = DragDropPayloadManager.GetFormats(e.Data, true);

            if (formats.Contains(typeof(string).FullName))
            {
                e.Effects = DragDropEffects.Move;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }

            e.Handled = true;
        }

        private void OnDragDropCompleted(object sender, DragDropCompletedEventArgs e)
        {
            var isDropSuccessful = DragDropPayloadManager.GetDataFromObject(e.Data, "IsDropSuccessful");

            if (isDropSuccessful != null && (bool)isDropSuccessful)
            {
                var data = DragDropPayloadManager.GetDataFromObject(e.Data, "DragData");
                ((IList)(sender as RadListBox).ItemsSource).Remove(data);
            }

            e.Handled = true;
        }

        private void OnDrop(object sender, Telerik.Windows.DragDrop.DragEventArgs e)
        {
            var data = DragDropPayloadManager.GetDataFromObject(e.Data, typeof(string).FullName);
            ((IList)(sender as RadListBox).ItemsSource).Add(data);
            DragDropPayloadManager.SetData(e.Data, "IsDropSuccessful", true);
            e.Handled = true;
        }

        private void OnDragInitialize(object sender, DragInitializeEventArgs e)
        {
            e.AllowedEffects = DragDropEffects.All;
            var payload = DragDropPayloadManager.GeneratePayload(new CustomerToStringConverter());
            var data = ((FrameworkElement)e.OriginalSource).DataContext;
            payload.SetData("DragData", data);
            e.Data = payload;
            e.Handled = true;
        }
    }
}
