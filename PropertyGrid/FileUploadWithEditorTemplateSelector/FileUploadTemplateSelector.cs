using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Data.PropertyGrid;

namespace FileUploadWithEditorTemplateSelector
{
    public class FileUploadTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var pd = item as PropertyDefinition;

            if (pd.SourceProperty.Name == "AvatarPath")
            {
                return OpenFileDialogTemplate;
            }

            if (pd.SourceProperty.PropertyType == typeof(int))
            {
                return NumericPropertyTemplate;
            }

            return null;
        }

        public DataTemplate OpenFileDialogTemplate { get; set; }

        public DataTemplate NumericPropertyTemplate { get; set; }
    }
}
