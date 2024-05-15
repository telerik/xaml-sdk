using System.ComponentModel;

namespace VariousDataSources
{
    public enum BindingType
    {
        [Description("Dynamic Data")]
        DynamicData,

        [Description("ObservableCollection")]
        ObservableCollection,

        [Description("Data Table")]
        DataTable,

        [Description("Xml")]
        Xml
    }
}
