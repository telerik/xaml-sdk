using System.Collections.Generic;
using System.Windows.Media;
using Telerik.Windows.Controls;

namespace BindingItemsSource
{
    public class CustomMenuItem : ViewModelBase, IRadialMenuItem
    {
        private Brush contentSectorBackground;
        private object toolTipContent;
        private bool isSelected;
        private IEnumerable<IRadialMenuItem> itemsSource;

        public CustomMenuItem()
        {
            this.CanUserSelect = true;
        }

        public Brush ContentSectorBackground
        {
            get
            {
                return this.contentSectorBackground;
            }

            set
            {
                if (this.contentSectorBackground != value)
                {
                    this.contentSectorBackground = value;
                    this.OnPropertyChanged(() => this.ContentSectorBackground);
                }
            }
        }

        public object ToolTipContent
        {
            get
            {
                return this.toolTipContent;
            }

            set
            {
                if (this.toolTipContent != value)
                {
                    this.toolTipContent = value;
                    this.OnPropertyChanged(() => this.ToolTipContent);
                }
            }
        }

        public bool IsSelected
        {
            get
            {
                return this.isSelected;
            }

            set
            {
                if (this.isSelected != value)
                {
                    this.isSelected = value;
                    this.OnPropertyChanged(() => this.IsSelected);
                }
            }
        }

        public IEnumerable<IRadialMenuItem> ItemsSource
        {
            get
            {
                return this.itemsSource;
            }

            set
            {
                if (this.itemsSource != value)
                {
                    this.itemsSource = value;
                    this.OnPropertyChanged(() => this.ItemsSource);
                }
            }
        }

        public bool CanUserSelect { get; set; }
        public System.Windows.Input.ICommand Command { get; set; }
        public object CommandParameter { get; set; }
        public System.Windows.UIElement CommandTarget { get; set; }
        public string GroupName { get; set; }
        public object Header { get; set; }
        public object IconContent { get; set; }
    }
}
