using System;
using System.Linq;

namespace FastGridExportWithSpreadStreamProcessing
{
    public class CellRange
    {
        public CellRange(int fromRowIndex, int fromColumnIndex, int toRowIndex, int toColumnIndex)
        {
            this.FromRowIndex = fromRowIndex;
            this.FromColumnIndex = fromColumnIndex;
            this.ToRowIndex = toRowIndex;
            this.ToColumnIndex = toColumnIndex;
        }

        public int FromRowIndex { get; private set; }
        public int FromColumnIndex { get; private set; }
        public int ToRowIndex { get; private set; }
        public int ToColumnIndex { get; private set; }
    }
}