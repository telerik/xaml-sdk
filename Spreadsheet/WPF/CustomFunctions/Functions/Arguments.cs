using System;
using System.Collections.Generic;
using Telerik.Windows.Documents.Spreadsheet.Expressions;
using Telerik.Windows.Documents.Spreadsheet.Expressions.Functions;

namespace CustomFunctions.Functions
{
    public class Arguments : FunctionBase
    {
        public static readonly string FunctionName = "ARGUMENTS";
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

        static Arguments()
        {
            string description = "Returns number of used arguments.";

            IEnumerable<ArgumentInfo> requiredArguments = new ArgumentInfo[]
		    {
			    new ArgumentInfo("First", "First argument.", ArgumentType.Any),
			    new ArgumentInfo("Second", "Second argument.", ArgumentType.Any),
			    new ArgumentInfo("Third", "Third argument.", ArgumentType.Any),
		    };

            IEnumerable<ArgumentInfo> optionalArguments = new ArgumentInfo[]
		    {
			    new ArgumentInfo("First", "First argument.", ArgumentType.Any),
			    new ArgumentInfo("Second", "Second argument.", ArgumentType.Any),
			    new ArgumentInfo("Third", "Third argument.", ArgumentType.Any),
		    };

            Info = new FunctionInfo(FunctionName, FunctionCategory.MathTrig, description, requiredArguments, optionalArguments, optionalArgumentsRepeatCount: 3);
        }

        protected override RadExpression EvaluateOverride(FunctionEvaluationContext<RadExpression> context)
        {
            return new NumberExpression(context.Arguments.Length);
        }
    }
}
