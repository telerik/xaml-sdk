using System;
using System.Collections.Generic;
using Telerik.Windows.Documents.Spreadsheet.Expressions;
using Telerik.Windows.Documents.Spreadsheet.Expressions.Functions;

namespace CustomFunctions.Functions
{
    public class Nand : BooleansInFunction
    {
        public static readonly string FunctionName = "NAND";
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

        static Nand()
        {
            string description = "Returns the Sheffer stroke (or also known as the NAND operator) of two booleans.";

            IEnumerable<ArgumentInfo> requiredArguments = new ArgumentInfo[]
            {
                new ArgumentInfo("Logical", "The first logical argument.", ArgumentType.Logical),
                new ArgumentInfo("Logical", "The second logical argument.", ArgumentType.Logical)
            };

            Info = new FunctionInfo(FunctionName, FunctionCategory.Text, description, requiredArguments);
        }

        protected override RadExpression EvaluateOverride(FunctionEvaluationContext<bool> context)
        {
            bool[] arguments = context.Arguments;
            bool result = !(arguments[0] & arguments[1]);

            return result ? BooleanExpression.True : BooleanExpression.False;
        }
    }
}
