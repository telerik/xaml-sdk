using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Windows.Input;
using Telerik.Windows.Controls;

namespace RestrictInputToOnlyExistingItems
{
    public class RestrictInputHelper
    {
        private static ObservableCollection<Country> countries;
        private static ObservableCollection<Country> selectedItems;

        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(RestrictInputHelper), new PropertyMetadata(false, OnIsHelperEnabledChanged));

        public static bool GetIsEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsEnabledProperty);
        }

        public static void SetIsEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(IsEnabledProperty, value);
        }

        [DllImport("user32.dll")]
        public static extern short GetKeyState(int keyCode);

        private static void OnIsHelperEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var autoComplete = d as RadAutoCompleteBox;
            if (autoComplete != null && e.NewValue != e.OldValue)
            {
                if ((bool)e.NewValue)
                {
                    autoComplete.KeyDown += OnKeyDown;
                }
                else
                {
                    autoComplete.KeyDown -= OnKeyDown;
                }
            }
        }

        private static void OnKeyDown(object sender, KeyEventArgs e)
        {
#if SILVERLIGHT
            if(Application.Current.HasElevatedPermissions)
            {
#endif
                var autoComplete = sender as RadAutoCompleteBox;

                var watermark = autoComplete.ChildrenOfType<TextBox>().FirstOrDefault();
                if (watermark != null)
                {
                    countries = (autoComplete.DataContext as ViewModel).Countries;
                }

                var textSearchMode = autoComplete.TextSearchMode;
                selectedItems = new ObservableCollection<Country>(autoComplete.SelectedItems.Cast<Country>());
#if SILVERLIGHT

                if (!(e.Key == Key.Shift || e.Key == Key.Space || e.Key == Key.Tab || e.Key == Key.Up
                   || e.Key == Key.Down || e.Key == Key.Left || e.Key == Key.Right))
#else
            if (!(e.Key == Key.LeftShift || e.Key == Key.RightShift || e.Key == Key.Space || e.Key == Key.Tab || e.Key == Key.Up
                || e.Key == Key.Down || e.Key == Key.Left || e.Key == Key.Right || e.Key == Key.Capital || e.Key == Key.Return))
#endif

                {
#if SILVERLIGHT
                    var pressedKey = (char)e.PlatformKeyCode;
#else
                    var pressedKey = (char)KeyInterop.VirtualKeyFromKey(e.Key);
#endif
                    var text = watermark.Text;
                    var caretIndex = watermark.SelectionStart;
                    bool CapsLock = (((ushort)GetKeyState(0x14)) & 0xffff) != 0;

                    if (watermark.SelectionLength != 0)
                    {
                        text = text.Substring(0, watermark.Text.Length - watermark.SelectionLength);
                    }

                    switch (textSearchMode)
                    {
                        case TextSearchMode.Contains:
                            e.Handled = IsEnteredTextContainedInsideExistingItems(text, caretIndex, pressedKey, null);
                            break;
                        case TextSearchMode.ContainsCaseSensitive:
                            if (CapsLock && Keyboard.Modifiers == ModifierKeys.Shift)
                            {
                                e.Handled = IsEnteredTextContainedInsideExistingItems(text, caretIndex, pressedKey, false);
                            }
                            else
                            {
                                if (CapsLock)
                                {
                                    e.Handled = IsEnteredTextContainedInsideExistingItems(text, caretIndex, pressedKey, true);
                                }
                                else
                                {
                                    if (Keyboard.Modifiers == ModifierKeys.Shift)
                                    {
                                        e.Handled = IsEnteredTextContainedInsideExistingItems(text, caretIndex, pressedKey, true);
                                    }
                                    else
                                    {
                                        e.Handled = IsEnteredTextContainedInsideExistingItems(text, caretIndex, pressedKey, false);
                                    }
                                }
                            }

                            break;
                        case TextSearchMode.StartsWith:
                            e.Handled = IsEnteredTextStartsWithExistingItems(text, caretIndex, pressedKey, null);
                            break;
                        case TextSearchMode.StartsWithCaseSensitive:
                            CapsLock = (((ushort)GetKeyState(0x14)) & 0xffff) != 0;

                            if (CapsLock && Keyboard.Modifiers == ModifierKeys.Shift)
                            {
                                e.Handled = IsEnteredTextStartsWithExistingItems(text, caretIndex, pressedKey, false);
                            }
                            else
                            {
                                if (CapsLock)
                                {
                                    e.Handled = IsEnteredTextStartsWithExistingItems(text, caretIndex, pressedKey, true);
                                }
                                else
                                {
                                    if (Keyboard.Modifiers == ModifierKeys.Shift)
                                    {
                                        e.Handled = IsEnteredTextStartsWithExistingItems(text, caretIndex, pressedKey, true);
                                    }
                                    else
                                    {
                                        e.Handled = IsEnteredTextStartsWithExistingItems(text, caretIndex, pressedKey, false);
                                    }
                                }
                            }

                            break;
                    }
                }
#if SILVERLIGHT
            }
            else
            {
                var autoComplete = sender as RadAutoCompleteBox;
                autoComplete.IsEnabled = false;
                MessageBox.Show("You need to enable trusted applications to run inside the browser! Please, check the description of the sample for some more detailed inforamtion how to achieve this.");
            }
#endif
        }

        private static bool IsEnteredTextContainedInsideExistingItems(string text, int caretIndex, char pressedKey, bool? isUpper)
        {
            var searchCollection = countries.Select(a => a).ToList().Except(selectedItems).ToList();
            if (isUpper == null)
            {
                text = text.Insert(caretIndex, pressedKey.ToString());
                if (!searchCollection.Any(a => a.Name.ToLower().Contains(text.ToLower())))
                {
                    return true;
                }
            }
            else
            {
                if (isUpper == true)
                {
                    text = text.Insert(caretIndex, pressedKey.ToString().ToUpper());
                    if (!searchCollection.Any(a => a.Name.Contains(text)))
                    {
                        return true;
                    }
                }
                else
                {
                    text = text.Insert(caretIndex, pressedKey.ToString().ToLower());
                    if (!searchCollection.Any(a => a.Name.Contains(text)))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static bool IsEnteredTextStartsWithExistingItems(string text, int caretIndex, char pressedKey, bool? isUpper)
        {
            var searchCollection = countries.Select(a => a).ToList().Except(selectedItems).ToList();

            if (isUpper == null)
            {
                text = text.Insert(caretIndex, pressedKey.ToString());
                if (!searchCollection.Any(a => a.Name.ToLower().StartsWith(text.ToLower())))
                {
                    return true;
                }
            }
            else
            {
                if (isUpper == true)
                {
                    text = text.Insert(caretIndex, pressedKey.ToString().ToUpper());
                    if (!searchCollection.Any(a => a.Name.StartsWith(text)))
                    {
                        return true;
                    }
                }
                else
                {
                    text = text.Insert(caretIndex, pressedKey.ToString().ToLower());
                    if (!searchCollection.Any(a => a.Name.StartsWith(text)))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
