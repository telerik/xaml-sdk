using System;
using System.Collections.Generic;
using System.Text;
using Telerik.Windows.Documents.Spreadsheet.Expressions;
using Telerik.Windows.Documents.Spreadsheet.Expressions.Functions;

namespace CustomFunctions.Functions
{
    public class RepeatString : FunctionWithArguments
    {
        public static readonly string FunctionName = "REPEAT.STRING";
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

        static RepeatString()
        {
            IEnumerable<ArgumentInfo> requiredArguments = new ArgumentInfo[]
            {
                 new ArgumentInfo("Text", "Text is the text you want to repeat.", ArgumentType.Text)
            };

            IEnumerable<ArgumentInfo> optionalArguments = new ArgumentInfo[]
            {
                new ArgumentInfo("NumberTimes", "NumberTimes is a positive number specifying the number of times to repeat text.", ArgumentType.Number)
            };

            string description = "REPEAT.STRING repeats some text a desired number of times.";

            Info = new FunctionInfo(FunctionName, FunctionCategory.Text, description, requiredArguments, optionalArguments, 1, false);
        }

        protected override RadExpression EvaluateOverride(FunctionEvaluationContext<object> context)
        {
            object[] arguments = context.Arguments;
            string text = arguments[0].ToString();

            double numberTimes = 1;
            if (arguments.Length > 1)
            {
                numberTimes = (double)arguments[1];
            }

            StringBuilder builder = new StringBuilder();

            if (numberTimes < 0)
            {
                return ErrorExpressions.ValueError;
            }

            for (int i = 0; i < numberTimes; i++)
            {
                builder.Append(text);
            }

            return new StringExpression(builder.ToString());
        }
    }
}
