using System;
using System.Linq;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using System.Windows.Controls.Primitives;
using Telerik.Windows.Controls.GridView;
using System.Windows;
using Telerik.Windows.Controls.Filtering.Editors;
using Telerik.Windows.Data;

namespace SilverlightApplication1
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void RadGridView_FilterOperatorsLoading(object sender, FilterOperatorsLoadingEventArgs e)
        {
            if (e.AvailableOperators.Contains(FilterOperator.Contains))
            {
                e.DefaultOperator1 = FilterOperator.Contains;
            }
        }

        private void RadGridView_FieldFilterEditorCreated(object sender, EditorCreatedEventArgs e)
        {
            var stringFilterEditor = e.Editor as StringFilterEditor;

            if (stringFilterEditor != null)
            {
                e.Editor.Loaded += (s1, e1) =>
                {
                    var textBox = e.Editor.ChildrenOfType<TextBox>().Single();
                    textBox.TextChanged += (s2, e2) =>
                    {
                        textBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                    };
                };
            }
        }
    }
}