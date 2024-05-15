using System.Windows;

namespace TimeSpanPickerAsCustomFilterEditor
{
    public class EditorTemplateRule
    {
        private string propertyName;
        private DataTemplate dataTemplate;

        public string PropertyName
        {
            get
            {
                return this.propertyName;
            }
            set
            {
                this.propertyName = value;
            }
        }

        public DataTemplate DataTemplate
        {
            get
            {
                return this.dataTemplate;
            }
            set
            {
                this.dataTemplate = value;
            }
        }
    }
}
