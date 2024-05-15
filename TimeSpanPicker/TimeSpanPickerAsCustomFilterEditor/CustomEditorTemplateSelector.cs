using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls.Data.DataFilter;

namespace TimeSpanPickerAsCustomFilterEditor
{
    public class CustomEditorTemplateSelector : DataTemplateSelector
    {
        private List<EditorTemplateRule> editorTemplateRules;

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            ItemPropertyDefinition propertyDefinition = (ItemPropertyDefinition)item;

            foreach (EditorTemplateRule rule in this.EditorTemplateRules)
            {
                if (rule.PropertyName == propertyDefinition.PropertyName)
                {
                    return rule.DataTemplate;
                }
            }

            return base.SelectTemplate(item, container);
        }

        public List<EditorTemplateRule> EditorTemplateRules
        {
            get
            {
                if (this.editorTemplateRules == null)
                {
                    this.editorTemplateRules = new List<EditorTemplateRule>();
                }

                return this.editorTemplateRules;
            }
        }
    }
}
