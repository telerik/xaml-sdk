using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Telerik.Windows.Controls;

namespace ExcelEditing
{
    public class ExcelEditingBehavior
    {
        public static bool GetIsEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsEnabledProperty);
        }

        public static void SetIsEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(IsEnabledProperty, value);
        }

        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(ExcelEditingBehavior), new PropertyMetadata(OnIsEnabledChanged));

        private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var gridView = d as RadGridView;
            if ((bool)e.NewValue)
            {
                gridView.PreviewKeyDown += OnGridViewPreviewKeyDown;
            }
            else
            {
                gridView.PreviewKeyDown -= OnGridViewPreviewKeyDown;
            }
        }

        private static void OnGridViewPreviewKeyDown(object sender, KeyEventArgs e)
        {
            var gridView = sender as RadGridView;

            var pendingCommands = new List<ICommand>();

            switch (e.Key)
            {
                case Key.Right:
                    pendingCommands.Add(RadGridViewCommands.MoveRight);
                    break;
                case Key.Left:
                    pendingCommands.Add(RadGridViewCommands.MoveLeft);
                    break;
                case Key.Up:
                    pendingCommands.Add(RadGridViewCommands.MoveUp);
                    break;
                case Key.Down:
                    pendingCommands.Add(RadGridViewCommands.MoveDown);
                    break;
                default:
                    return;
            }

            var editBox = gridView.CurrentCell.ChildrenOfType<TextBox>().FirstOrDefault();

            if (!gridView.Items.IsEditingItem ||
                (e.Key == Key.Right && editBox.CaretIndex != editBox.Text.Length) ||
                (e.Key == Key.Left && (editBox.CaretIndex != 0 || editBox.SelectionLength > 0)))
            {
                return;
            }

            pendingCommands.Add(RadGridViewCommands.SelectCurrentUnit);
            pendingCommands.Add(RadGridViewCommands.BeginEdit);

            gridView.PendingCommands.AddRange(pendingCommands);
            gridView.ExecutePendingCommand();

            e.Handled = true;
        }
    }
}
