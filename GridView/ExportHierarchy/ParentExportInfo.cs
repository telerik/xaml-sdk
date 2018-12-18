using System.Collections;

namespace ExportHierarchy
{
    public class ParentExportInfo
    {
        public int OriginalIndex { get; set; }

        public int ExportIndex { get; set; }

        public IList SubItems { get; set; }
    }
}
