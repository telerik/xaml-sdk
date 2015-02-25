using System;
using System.Linq;
using System.Windows;
using Telerik.Windows.Data;
using System.Collections;
using System.ComponentModel;
using System.Collections.Specialized;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Controls;
using System.Windows.Controls.Primitives;
using Telerik.Windows.Controls.Filtering.Editors;
using System.Windows.Controls;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RadGridView_FilterOperatorsLoading(object sender, FilterOperatorsLoadingEventArgs e)
        {

            if (e.AvailableOperators.Contains(FilterOperator.Contains))
            {
                e.DefaultOperator1 = FilterOperator.Contains;
            }
            else if (e.AvailableOperators.Contains(FilterOperator.IsLessThanOrEqualTo))
            {
                e.DefaultOperator1 = FilterOperator.IsLessThanOrEqualTo;
            }
        }

        private void RadGridView_FieldFilterEditorCreated(object sender, EditorCreatedEventArgs e)
        {
            var stringFilterEditor = e.Editor as StringFilterEditor;

            if (stringFilterEditor != null)
            {
                // filtering with StringFilterEditor (when filtering string values)
                e.Editor.Loaded += (s1, e1) =>
                {
                    var textBox = e.Editor.ChildrenOfType<TextBox>().Single();
                    textBox.TextChanged += (s2, e2) =>
                    {
                        textBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                    };
                };
            }
            else
            {
                // Filtering with TextBox editor (when filtering numeric values)
                var textBox = e.Editor as TextBox;
                if (textBox != null)
                {
                    textBox.TextChanged += (s2, e2) =>
                    {
                        textBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                    };
                }
            }
        }
    }
}
