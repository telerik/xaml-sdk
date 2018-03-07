using System;
using Telerik.Windows.Documents.Spreadsheet.Expressions;
using Telerik.Windows.Documents.Spreadsheet.Expressions.Functions;

namespace CustomFunctions.Functions
{
    public class E : FunctionBase
    {
        public static readonly string FunctionName = "E";
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

        static E()
        {
            string description = "Returns the Napier's constant.";

            Info = new FunctionInfo(FunctionName, FunctionCategory.MathTrig, description);
        }

        protected override RadExpression EvaluateOverride(FunctionEvaluationContext<RadExpression> context)
        {
            return NumberExpression.E;
        }
    }
}