using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace HighlightCustomColumn
{
    class HightlightColumn : GridViewBoundColumnBase
    {
        public override System.Windows.FrameworkElement CreateCellElement(Telerik.Windows.Controls.GridView.GridViewCell cell, object dataItem)
        {
            StackPanel mainContainer = new StackPanel();
            mainContainer.Orientation = Orientation.Horizontal;
            
            //Add an additional element in the cell
            if (cell.DataContext != null)
            {
                Image image = new Image();
                image.Source = new BitmapImage(new Uri(string.Format("/Images/{0}.png", (cell.DataContext as Club).Name), UriKind.RelativeOrAbsolute));
                mainContainer.Children.Add(image);
            }

            //Add HighlightTextBlock to keep the SearchPanel functionality
            HighlightTextBlock htb = new HighlightTextBlock();
            htb.DataType = this.DataType;
            htb.SetBinding(HighlightTextBlock.HighlightTextProperty, new Binding(this.DataMemberBinding.Path.Path));
            cell.SetBinding(GridViewCell.IsHighlightedProperty, new Binding("ContainsMatch") { Source = htb, Mode = BindingMode.TwoWay });
            SetHighlightTextBlockTextProperty(dataItem, htb);
            mainContainer.Children.Add(htb);

            return mainContainer;
        }

        public void SetHighlightTextBlockTextProperty(object dataItem, TextBlock textBlock)
        {
            if (this.DataMemberBinding != null)
            {
                var content = this.GetCellContent(dataItem);
                if (content != null)
                {
                    textBlock.SetValue(TextBlock.TextProperty, string.Format("{0}", content));
                }
            }
            else
            {
                textBlock.ClearValue(TextBlock.TextProperty);
            }
        }

        public override System.Windows.FrameworkElement CreateCellEditElement(GridViewCell cell, object dataItem)
        {
            var textbox = new TextBox();
            textbox.SetBinding(TextBox.TextProperty, new Binding(this.DataMemberBinding.Path.Path));
            return textbox;
        }
    }
}
