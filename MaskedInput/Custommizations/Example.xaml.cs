using System;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using Telerik.Windows.Controls.MaskedInput.Tokens;

namespace MaskCustomizations
{
	/// <summary>
	/// Interaction logic for Example.xaml
	/// </summary>
	public partial class Example : UserControl
	{
		public Example()
		{
			InitializeCustomTokens();
			InitializeComponent();		
			bankInput.Culture = this.CreateBankCulture();
		}

		private CultureInfo CreateBankCulture()
		{
			CultureInfo bankCulture = new CultureInfo("en-US");
			bankCulture.NumberFormat.CurrencyGroupSeparator = ((char)'\x0964').ToString();
			bankCulture.NumberFormat.CurrencyGroupSizes = new int[] { 1 };

			return bankCulture;
		}

		private void InitializeCustomTokens()
		{
			FirstCharCapitalFormatToken fccFormatToken = new FirstCharCapitalFormatToken();
			if (TokenLocator.GetTokenRule(fccFormatToken.Token, TokenTypes.Modifier) == null)
			{
				TokenLocator.AddCustomValidationRule(new FirstCharCapitalFormatToken());
			}
		}
	}
}
