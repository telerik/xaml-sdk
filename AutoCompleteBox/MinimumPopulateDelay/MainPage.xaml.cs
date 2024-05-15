using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Telerik.Windows.Controls;

namespace MinimumPopulateDelay
{
    public partial class MainPage : UserControl
    {
        private DispatcherTimer timer = new DispatcherTimer();
        private bool isHandled = true;
        private bool isDeleting;

        public MainPage()
        {
            InitializeComponent();
            this.timer.Interval = TimeSpan.FromSeconds((this.DataContext as ViewModel).SelectedDelay);
            this.timer.Tick += OnTimerTick;
            this.AutoCompleteBox.AddHandler(Control.KeyDownEvent, new KeyEventHandler(OnAutoCompleteBoxKeyDown), true);
        }

        private void OnAutoCompleteBoxSearchTextChanged(object sender, EventArgs e)
        {
            if (this.isDeleting || string.IsNullOrEmpty(this.AutoCompleteBox.SearchText))
            {
                this.AutoCompleteBox.Populate(string.Empty);
            }
            else
            {
                if (this.timer != null && this.timer.IsEnabled)
                {
                    this.timer.Stop();
                    this.timer.Start();
                }
                else
                {
                    if (this.timer != null)
                    {
                        this.timer.Start();
                    }
                }

                this.SetStatusBusyIndicator(true);
                this.DisabledOverlay.Visibility = System.Windows.Visibility.Visible;
            }

            this.isHandled = true;
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            this.timer.Stop();
            this.SetStatusBusyIndicator(false);
            this.DisabledOverlay.Visibility = System.Windows.Visibility.Collapsed;
            this.isHandled = false;
            if (!this.isDeleting)
            {
                this.AutoCompleteBox.Populate(this.AutoCompleteBox.SearchText);
            }
        }

        private void OnAutoCompleteBoxPopulating(object sender, CancelRoutedEventArgs e)
        {
            e.Cancel = this.isHandled;
        }

        private void OnAutoCompleteBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back || e.Key == Key.Delete || e.Key == Key.Escape || (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.X))
            {
                this.isDeleting = true;
                this.AutoCompleteBox.IsDropDownOpen = false;
                this.SetStatusBusyIndicator(false);
                this.DisabledOverlay.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                if (e.Key != Key.Up && e.Key != Key.Down)
                {
                    this.AutoCompleteBox.IsDropDownOpen = false;
                    if (e.Key == Key.Enter && this.isDeleting)
                    {
                        this.isHandled = false;
                        this.AutoCompleteBox.Populate(this.AutoCompleteBox.SearchText);
                    }
                }

                this.isDeleting = false;
            }
        }

        private void SetStatusBusyIndicator(bool isBusy)
        {
            if (this.StatusRadBusyIndicator != null)
            {
                this.StatusRadBusyIndicator.IsBusy = isBusy;
            }
        }

        private void OnDelaysComboBoxSelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangedEventArgs e)
        {
            this.RefreshDelayTimer(int.Parse((sender as RadComboBox).SelectedItem.ToString()));
        }

        private void RefreshDelayTimer(int delay)
        {
            this.timer.Interval = TimeSpan.FromSeconds(delay);
        }
    }
}
