using System.Collections.Generic;
using System.Linq;
using Telerik.Windows.Documents.Spreadsheet.Expressions;
using Telerik.Windows.Documents.Spreadsheet.Expressions.Functions;
using Telerik.Windows.Documents.Spreadsheet.Model;
using Telerik.Windows.Documents.Spreadsheet.Utilities;

namespace CustomFunctions.Functions
{
    public class Indirect : FunctionWithArguments
    {
        public static readonly string FunctionName = "INDIRECT";
        private static readonly FunctionInfo Info;

        public override string Name
        {
            get
            {
                return FunctionName;
            }
        }

        public override FunctionInfo FunctionInfo
        {
            get
            {
                return Info;
            }
        }

        static Indirect()
        {
            string description = "Returns the reference of the cell specified by a text string.";

            IEnumerable<ArgumentInfo> requiredArguments = new ArgumentInfo[]
            {
                new ArgumentInfo("Text",
                    "A reference to a cell that contains an A1-style reference, a name defined as a reference, or a reference to a cell as a text string.",
                    ArgumentType.Text),
            };

            Info = new FunctionInfo(FunctionName,FunctionCategory.LookupReference, description, requiredArguments);
        }

        protected override RadExpression EvaluateOverride(FunctionEvaluationContext<object> context)
        {
            string reference = context.Arguments[0].ToString();

            if (string.IsNullOrEmpty(reference))
            {
                return ErrorExpressions.ReferenceError;
            }

            List<CellReferenceRange> cellReferenceRanges = new List<CellReferenceRange>();

            CellReferenceRangeExpression expression;
            if (NameConverter.TryConvertNamesToCellReferenceRangeExpression(reference, context.Worksheet, context.RowIndex, context.ColumnIndex, out expression))
            {
                cellReferenceRanges.AddRange(expression.CellReferenceRanges);
            }

            if (cellReferenceRanges.Count == 1)
            {
                CellReferenceRange cellReferenceRange = cellReferenceRanges.First();
                if (cellReferenceRange.Worksheet == context.Worksheet)
                {
                    CellRange cellRange = cellReferenceRange.ToCellRange();
                    if (cellRange.Contains(context.RowIndex, context.ColumnIndex))
                    {
                        return ErrorExpressions.CyclicReference;
                    }
                }
            }

            if (cellReferenceRanges.Count == 0)
            {
                return ErrorExpressions.ReferenceError;
            }

            return expression;
        }
    }
}
