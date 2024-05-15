using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Data;

namespace AutoCompleteStringFilterEditor_SL
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();
		}

		private void Button1_Click(object sender, RoutedEventArgs e)
		{

		}

		private void Button2_Click(object sender, RoutedEventArgs e)
		{

		}
	}

    /// <summary>
    /// A string column, whose field filter editors have the auto-complete functionality.
    /// The ItemsSource of the RadAutoCompleteBox are the distinct values of the column.
    /// </summary>
    public class AutoCompleteFilterStringColumn : GridViewDataColumn
    {
        public override FrameworkElement CreateFieldFilterEditor()
        {
            return FilterEditorFactory.CreateAutoCompleteStringEditor();
        }
    }
}
