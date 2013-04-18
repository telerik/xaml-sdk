using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Documents.FormatProviders.Txt;

namespace SearchAndHighlight
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void editor_Loaded(object sender, RoutedEventArgs e)
        {
            this.editor.Document = new TxtFormatProvider().Import(@"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque diam felis, accumsan vitae fringilla a, dignissim at dui.  Integer imperdiet feugiat dui. Pellentesque faucibus interdum urna, eget iaculis turpis dapibus id. Proin sapien metus, mollis eu porttitor in, bibendum quis odio. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque diam felis, accumsan vitae fringilla a, dignissim at dui. Vestibulum commodo ligula leo. In hac habitasse platea dictumst. Pellentesque faucibus interdum urna, eget iaculis turpis dapibus id. Integer imperdiet feugiat dui. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque diam felis, accumsan vitae fringilla a, dignissim at dui.  Integer imperdiet feugiat dui. Pellentesque faucibus interdum urna, eget iaculis turpis dapibus id. Proin sapien metus, mollis eu porttitor in, bibendum quis odio. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque diam felis, accumsan vitae fringilla a, dignissim at dui. 
Vestibulum commodo ligula leo. In hac habitasse platea dictumst. Pellentesque faucibus interdum urna, eget iaculis turpis dapibus id. Integer imperdiet feugiat dui. 
Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque diam felis, accumsan vitae fringilla a, dignissim at dui. Vestibulum commodo ligula leo. In hac habitasse platea dictumst. Pellentesque faucibus interdum urna, eget iaculis turpis dapibus id. Integer imperdiet feugiat dui. Lorem ipsum dolor sit amet, consectetur adipiscing elit. 
Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque diam felis, accumsan vitae fringilla a, dignissim at dui.  Integer imperdiet feugiat dui. Pellentesque faucibus interdum urna, eget iaculis turpis dapibus id. Proin sapien metus, mollis eu porttitor in, bibendum quis odio. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque diam felis, accumsan vitae fringilla a, dignissim at dui. Vestibulum commodo ligula leo. In hac habitasse platea dictumst. Pellentesque faucibus interdum urna, eget iaculis turpis dapibus id. Integer imperdiet feugiat dui. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque diam felis, accumsan vitae fringilla a, dignissim at dui.  Integer imperdiet feugiat dui. Pellentesque faucibus interdum urna, eget iaculis turpis dapibus id. Proin sapien metus, mollis eu porttitor in, bibendum quis odio. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque diam felis, accumsan vitae fringilla a, dignissim at dui. 
Vestibulum commodo ligula leo. In hac habitasse platea dictumst. Pellentesque faucibus interdum urna, eget iaculis turpis dapibus id. Integer imperdiet feugiat dui. 
Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque diam felis, accumsan vitae fringilla a, dignissim at dui. Vestibulum commodo ligula leo. In hac habitasse platea dictumst. Pellentesque faucibus interdum urna, eget iaculis turpis dapibus id. Integer imperdiet feugiat dui. Lorem ipsum dolor sit amet, consectetur adipiscing elit. 
Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque diam felis, accumsan vitae fringilla a, dignissim at dui.  Integer imperdiet feugiat dui. Pellentesque faucibus interdum urna, eget iaculis turpis dapibus id. Proin sapien metus, mollis eu porttitor in, bibendum quis odio. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque diam felis, accumsan vitae fringilla a, dignissim at dui. Vestibulum commodo ligula leo. In hac habitasse platea dictumst. Pellentesque faucibus interdum urna, eget iaculis turpis dapibus id. Integer imperdiet feugiat dui. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque diam felis, accumsan vitae fringilla a, dignissim at dui.  Integer imperdiet feugiat dui. Pellentesque faucibus interdum urna, eget iaculis turpis dapibus id. Proin sapien metus, mollis eu porttitor in, bibendum quis odio. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque diam felis, accumsan vitae fringilla a, dignissim at dui. 
Vestibulum commodo ligula leo. In hac habitasse platea dictumst. Pellentesque faucibus interdum urna, eget iaculis turpis dapibus id. Integer imperdiet feugiat dui. 
Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque diam felis, accumsan vitae fringilla a, dignissim at dui. Vestibulum commodo ligula leo. In hac habitasse platea dictumst. Pellentesque faucibus interdum urna, eget iaculis turpis dapibus id. Integer imperdiet feugiat dui. Lorem ipsum dolor sit amet, consectetur adipiscing elit. 
Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque diam felis, accumsan vitae fringilla a, dignissim at dui.  Integer imperdiet feugiat dui. Pellentesque faucibus interdum urna, eget iaculis turpis dapibus id. Proin sapien metus, mollis eu porttitor in, bibendum quis odio. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque diam felis, accumsan vitae fringilla a, dignissim at dui. Vestibulum commodo ligula leo. In hac habitasse platea dictumst. Pellentesque faucibus interdum urna, eget iaculis turpis dapibus id. Integer imperdiet feugiat dui. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque diam felis, accumsan vitae fringilla a, dignissim at dui.  Integer imperdiet feugiat dui. Pellentesque faucibus interdum urna, eget iaculis turpis dapibus id. Proin sapien metus, mollis eu porttitor in, bibendum quis odio. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque diam felis, accumsan vitae fringilla a, dignissim at dui. 
Vestibulum commodo ligula leo. In hac habitasse platea dictumst. Pellentesque faucibus interdum urna, eget iaculis turpis dapibus id. Integer imperdiet feugiat dui. 
Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque diam felis, accumsan vitae fringilla a, dignissim at dui. Vestibulum commodo ligula leo. In hac habitasse platea dictumst. Pellentesque faucibus interdum urna, eget iaculis turpis dapibus id. Integer imperdiet feugiat dui. Lorem ipsum dolor sit amet, consectetur adipiscing elit. ");
        }

        private void textToSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            HighlightSearchedWordLayer.Word = ((TextBox)sender).Text;
            this.editor.UpdateEditorLayout();
        }
    }

}
