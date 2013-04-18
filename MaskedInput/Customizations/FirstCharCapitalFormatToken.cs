using System;
using System.Linq;
using System.Text;
using Telerik.Windows.Controls.MaskedInput.Tokens;
using Telerik.Windows.Controls.MaskedInput.Tokens.Modifier;

namespace MaskCustomizations
{
	public class FirstCharCapitalFormatToken : IModifierTokenValidationRule
	{
		public string ApplyFormatTo(string textToFormat)
		{
			StringBuilder builder = new StringBuilder(textToFormat);
			for (int i = 0; i < builder.Length; i++)
			{
				if (i == 0)
				{
					builder[i] = char.ToUpper(builder[i]);
				}
				else
				{
					builder[i] = char.ToLower(builder[i]);
				}
			}
			return builder.ToString();
		}

		public bool IsRequired
		{
			get { return false; }
		}

		public bool IsValid(char ch)
		{
			return true;
		}

		public char Token
		{
			get { return '^'; }
		}

		public Telerik.Windows.Controls.MaskedInput.Tokens.TokenTypes Type
		{
			get { return TokenTypes.Modifier; }
		}

		public string ValidChars
		{
			get { return string.Empty; }
		}
	}
}