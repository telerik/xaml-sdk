using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Windows.Documents.Spreadsheet.Expressions;
using Telerik.Windows.Documents.Spreadsheet.Expressions.Functions;

namespace CustomFunctions.Functions
{
    class GeoMean : NumbersInFunction
    {
        public static readonly string FunctionName = "GEOMEAN";
        private static readonly FunctionInfo info;
        private static readonly ArgumentConversionRules conversionRules = new ArgumentConversionRules(
            nonTextNumberDirectArgument: ArgumentInterpretation.TreatAsError, 
            nonTextNumberIndirectArgument: ArgumentInterpretation.TreatAsError,
            emptyIndirectArgument: ArgumentInterpretation.Ignore,
            emptyDirectArgument: ArgumentInterpretation.Ignore,
            arrayArgument: ArrayArgumentInterpretation.UseAllElements);

        public override ArgumentConversionRules ArgumentConversionRules
        {
            get
            {
                return GeoMean.conversionRules;
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
                return info;
            }
        }

        static GeoMean()
        {
            string description = "Returns the geometric mean of a sequance of numbers.";

            IEnumerable<ArgumentInfo> requiredArguments = new ArgumentInfo[]
            {
                new ArgumentInfo("Number", "Positive number", ArgumentType.Number),
            };

            IEnumerable<ArgumentInfo> optionalArguments = new ArgumentInfo[]
            {
                new ArgumentInfo("Number", "Positive number", ArgumentType.Number)
            };

            info = new FunctionInfo(FunctionName, FunctionCategory.Statistical, description, requiredArguments, optionalArguments, optionalArgumentsRepeatCount:255);
        }

        protected override RadExpression EvaluateOverride(FunctionEvaluationContext<double> context)
        {
            double product = 1;
            double[] arguments = context.Arguments;

            foreach (double argument in arguments)
            {
                if (argument <= 0)
                {
                    return ErrorExpressions.NumberError;
                }

                product *= argument;
            }

            double result = Math.Pow(product, 1.0 / arguments.Length);

            return new NumberExpression(result);
        }
    }
}
