using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace CustomHyperlinkToolTip
{
    public class InfoTipData
    {
        private InfoTipData(object content, string title, string navigateUri)
        {
            this.Content = content;
            this.Title = title;
            this.NavigateUri = navigateUri;
        }

        public object Content { get; set; }
        public string Title { get; set; }
        public string NavigateUri { get; set; }

        public static InfoTipData CreateInfoTipData(Type type)
        {
            if (type == typeof(string))
            {
                var title = "String";
                var content = new TextBlock() { Text = @"Represents text as a series of Unicode characters.", TextWrapping = System.Windows.TextWrapping.Wrap };
                var navigateUri = @"http://msdn.microsoft.com/en-us/library/system.string(v=vs.110).aspx";
                return new InfoTipData(content, title, navigateUri);
            }
            if (type == typeof(Int32))
            {
                var title = "Int32";
                var content = new TextBlock() { Text = @"Represents a 32-bit signed integer.", TextWrapping = System.Windows.TextWrapping.Wrap };
                var navigateUri = @"http://msdn.microsoft.com/en-us/library/System.Int32(v=vs.110).aspx";
                return new InfoTipData(content, title, navigateUri);
            }
            if (type == typeof(DateTime))
            {
                var title = "DateTime";
                var content = new TextBlock() { Text = @"Represents an instant in time, typically expressed as a date and time of day.", TextWrapping = System.Windows.TextWrapping.Wrap };
                var navigateUri = @"http://msdn.microsoft.com/en-us/library/system.datetime(v=vs.110).aspx";
                return new InfoTipData(content, title, navigateUri);
            }
            return null;
        }
    }
}
