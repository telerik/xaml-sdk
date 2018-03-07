using System;
using System.Collections.Generic;
using System.Text;
using Telerik.Windows.Documents.Spreadsheet.Expressions;
using Telerik.Windows.Documents.Spreadsheet.Expressions.Functions;

namespace CustomFunctions.Functions
{
    public class Upper : StringsInFunction
    {
        public static readonly string FunctionName = "UPPER";
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
        
        static Upper()
        {
            string description = "Converts text to uppercase.";

            IEnumerable<ArgumentInfo> requiredArguments = new ArgumentInfo[]
            {
                new ArgumentInfo("Text", "Text is the text you want converted to uppercase. Text can be a reference or text string.", ArgumentType.Text)
            };

            Info = new FunctionInfo(FunctionName, FunctionCategory.Text, description, requiredArguments);
        }

        protected override RadExpression EvaluateOverride(FunctionEvaluationContext<string> context)
        {
            string text = context.Arguments[0];

            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < text.Length; i++)
            {
                char c = char.ToUpperInvariant(text[i]);
                builder.Append(c);
            }

            return new StringExpression(builder.ToString());
        }
    }
}