using System;
using System.Linq;
using Telerik.Windows.Documents.Spreadsheet.Utilities;

namespace CustomRowAndColumnHeadings.HeaderConverters
{
    public class NumberedColumnsHeaderNameRenderingConverter : HeaderNameRenderingConverterBase
    {
        protected override string ConvertColumnIndexToNameOverride(HeaderNameRenderingConverterContext context, int columnIndex)
        {
            return columnIndex.ToString();
        }
    }
}