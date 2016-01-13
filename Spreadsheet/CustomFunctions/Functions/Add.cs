using System;
using System.Collections.Generic;
using Telerik.Windows.Documents.Spreadsheet.Expressions;
using Telerik.Windows.Documents.Spreadsheet.Expressions.Functions;

namespace CustomFunctions.Functions
{
    public class Add : NumbersInFunction
    {
        public static readonly string FunctionName = "ADD";
        private static readonly FunctionInfo Info;
        
        public override ArgumentConversionRules ArgumentConversionRules
        {
            get
            {
                return ArgumentConversionRules.NaryIgnoreIndirectNumberFunctionConversion;
            }
        }

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

        static Add()
        {
            #region FunctionInfo

            string description = "Adds all the numbers in range of cells.";

            IEnumerable<ArgumentInfo> requiredArguments = new ArgumentInfo[]
	        {
		        new ArgumentInfo("Number", "number1, number2,... are the numbers to sum. Logical values and text are ignored in cells, included if typed as arguments.", ArgumentType.Number),
	        };

            IEnumerable<ArgumentInfo> optionalArguments = new ArgumentInfo[]
	        {
		        new ArgumentInfo("Number", "number1, number2,... are the numbers to sum. Logical values and text are ignored in cells, included if typed as arguments.", ArgumentType.Number),
	        };

            Info = new FunctionInfo(FunctionName, FunctionCategory.MathTrig, description, requiredArguments, optionalArguments, 254, true);

            #endregion
        }

        protected override RadExpression EvaluateOverride(FunctionEvaluationContext<double> context)
        {
            double result = 0;
            double[] arguments = context.Arguments;

            for (int i = 0; i < arguments.Length; i++)
            {
                result += arguments[i];
            }

            if (double.IsInfinity(result))
            {
                return ErrorExpressions.NumberError;
            }

            return new NumberExpression(result);
        }
    }
}
