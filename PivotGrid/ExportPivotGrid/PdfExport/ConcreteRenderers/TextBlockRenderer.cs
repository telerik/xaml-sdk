using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Telerik.Pivot.Core;
using Telerik.Windows.Controls.Pivot;

namespace ExportPivotGrid
{
    internal class TextBlockRenderer : UIElementRendererBase
    {
        internal override bool Render(UIElement element, PdfRenderContext context)
        {
            TextBlock textBlock = element as TextBlock;
            if (textBlock == null)
            {
                return false;
            }

            string text = textBlock.Text;
            Brush foreground = textBlock.Foreground;
            double width = textBlock.ActualWidth;
            double height = textBlock.ActualHeight;
            var fontFamily = textBlock.FontFamily;
            double fontSize = textBlock.FontSize;
            var fontWeight = textBlock.FontWeight;

            var groupData = textBlock.DataContext as GroupData;
            if (groupData != null)
            {
                var groupNode = groupData.Data as IGroup;

                switch (groupNode.Type)
                {
                    case GroupType.BottomLevel:
                        fontWeight = FontWeights.Normal;
                        break;
                    case GroupType.GrandTotal:
                    case GroupType.Subheading:
                    case GroupType.Subtotal:
                        fontWeight = FontWeights.Bold;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                var data = textBlock.DataContext as CellData;

                var groupNode = data.RowItem as IGroup;
                var shouldBeBold = (groupNode.Type == GroupType.GrandTotal || groupNode.Type == GroupType.Subtotal);

                groupNode = data.ColumnItem as IGroup;
                shouldBeBold = shouldBeBold || (groupNode.Type == GroupType.GrandTotal || groupNode.Type == GroupType.Subtotal);
                // Bold the TextBlocks that display SubTotal and GrandTotal
                if (shouldBeBold)
                {
                    fontWeight = FontWeights.Bold;
                }
            }
            TextRenderer.DrawTextBlock(text, context, foreground, width, height, fontFamily, fontSize, fontWeight);

            return true;
        }
    }
}
