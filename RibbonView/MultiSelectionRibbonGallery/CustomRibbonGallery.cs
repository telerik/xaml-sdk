using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Telerik.Windows.Controls;

namespace MultiSelectionRibbonGallery
{
    public class CustomRibbonGallery : RadRibbonGallery
    {
        private bool closeDropDownOnSelection = true;

        public bool CloseDropDownOnSelection
        {
            get { return this.closeDropDownOnSelection; }
            set { this.closeDropDownOnSelection = value; }
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            if (this.CloseDropDownOnSelection)
            {
                base.OnSelectionChanged(e);               
            }
        }    

        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            var galleryItem = GetGalleryItemFromEventSource((DependencyObject)e.Source, (DependencyObject)e.OriginalSource);
            if (galleryItem != null)
            {
                galleryItem.IsSelected ^= true;
                if (this.CloseDropDownOnSelection == false)
                {
                    e.Handled = true;
                }                
            }
            base.OnPreviewMouseLeftButtonDown(e);
        }

        private RadGalleryItem GetGalleryItemFromEventSource(DependencyObject source, DependencyObject originalSource)
        {
            if (source is RadGalleryItem)
            {
                return (RadGalleryItem)source;
            }
            else
            {
                if (originalSource is RadGalleryItem)
                {
                    return (RadGalleryItem)originalSource;
                }
                else
                {
                    return originalSource.ParentOfType<RadGalleryItem>();
                }
            }
        }
    }
}
