using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ExportUIElement
{
    internal class FrameworkElementRenderer : UIElementRendererBase
    {
        private Type[] types;

        internal FrameworkElementRenderer(params Type[] types)
        {
            this.types = types;
        }

        internal override bool Render(UIElement element, PdfRenderContext context)
        {
            FrameworkElement frameworkElement = element as FrameworkElement;
            if (frameworkElement == null)
            {
                return false;
            }

            bool hasMatchingType = false;
            Type elementType = frameworkElement.GetType();
            foreach (Type type in this.types)
            {
                if (elementType == type || elementType.IsSubclassOf(type))
                {
                    hasMatchingType = true;
                    break;
                }
            }

            if (!hasMatchingType)
            {
                return false;
            }

            List<UIElement> uiElements = new List<UIElement>();
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(frameworkElement); i++)
            {
                UIElement child = VisualTreeHelper.GetChild(frameworkElement, i) as UIElement;
                uiElements.Add(child);
            }

            uiElements = uiElements.OrderBy(el => Canvas.GetZIndex(el)).ToList();

            foreach (UIElement child in uiElements)
            {
                context.facade.Render(child, context);
            }

            return true;
        }
    }
}
