using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using Telerik.Pivot.Core;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.FieldList;

namespace CustomContextMenuBehavior
{
    public class MyCustomContextMenuBehavior : FieldListContextMenuBehavior
    {
        private bool isCellTemplateSelectorSet;

        public MyCustomContextMenuBehavior()
            : base()
        {
            this.RemoveColorsCommand = new DelegateCommand(OnRemoveColoringExecute, canExecute => this.isCellTemplateSelectorSet);
        }

        public RadPivotGrid Pivot { get; set; }

        public ICommand RemoveColorsCommand { get; set; }

        protected override RadContextMenu CreateContextMenu(object dataContext)
        {
            var contextMenu = base.CreateContextMenu(dataContext);

            if (dataContext is Telerik.Pivot.Core.PropertyGroupDescription)
            {
                var itemToRemove = contextMenu.Items.Cast<RadMenuItem>().FirstOrDefault(i => (i as RadMenuItem).Header != null && (i as RadMenuItem).Header.Equals("Label Filter"));

                if (itemToRemove != null)
                {
                    contextMenu.Items.Remove(itemToRemove);
                }
            }

            if (dataContext is DoubleGroupDescription)
            {
                contextMenu.Items.Add(new RadMenuSeparatorItem());
                contextMenu.Items.Add(new RadMenuItem { Header = "Change the Step", Command = new DelegateCommand(OnChangeStepExecute), CommandParameter = dataContext });
            }

            if (dataContext is AggregateDescriptionBase)
            {
                contextMenu.Items.Add(new RadMenuSeparatorItem());
                contextMenu.Items.Add(new RadMenuItem { Header = "Color Cells", Command = new DelegateCommand(OnColorCellsExecute), CommandParameter = dataContext });
                contextMenu.Items.Add(new RadMenuItem { Header = "Remove Coloring", Command = this.RemoveColorsCommand });
            }

            return contextMenu;
        }

        private void OnColorCellsExecute(object obj)
        {
            var cellTemplateSettingsWindow = new CellTemplateSettingsWindow();           

            cellTemplateSettingsWindow.Closed += (s, e) =>
            {
                if (e.DialogResult == true)
                {
                    var selector = new CellTemplateSelector();
                    selector.LowerValueTemplate = this.CreateDataTemplate(cellTemplateSettingsWindow.LowerValueColorPicker.SelectedColor);
                    selector.HigherValueTemplate = this.CreateDataTemplate(cellTemplateSettingsWindow.HigherValueColorPicker.SelectedColor);

                    var groupDesc = obj as PropertyAggregateDescription;
                    selector.PropertyName = groupDesc.PropertyName;
                    selector.LimitValue = double.Parse(e.PromptResult);

                    this.Pivot.CellTemplateSelector = null;
                    this.Pivot.CellTemplateSelector = selector;
                    this.isCellTemplateSelectorSet = true;
                    (this.RemoveColorsCommand as DelegateCommand).InvalidateCanExecute();
                }
            };

            cellTemplateSettingsWindow.ShowDialog();
        }

        private void OnRemoveColoringExecute(object obj)
        {
            this.Pivot.CellTemplateSelector = null;
            this.isCellTemplateSelectorSet = false;
            (this.RemoveColorsCommand as DelegateCommand).InvalidateCanExecute();
        }

        private void OnChangeStepExecute(object obj)
        {
            var groupDesc = obj as DoubleGroupDescription;
            var settingsWindow = new StepSettingsWindow(groupDesc.Step);
            
            settingsWindow.Closed += (s, e) =>
            {
                if (e.DialogResult == true)
                {
                    groupDesc.Step = int.Parse(e.PromptResult);

                    if (!this.Pivot.DataProvider.DeferUpdates)
                    {
                        this.Pivot.DataProvider.Refresh();
                    }
                }
            };

            settingsWindow.ShowDialog();
        }

        private DataTemplate CreateDataTemplate(Color color)
        {
#if SILVERLIGHT
            return (DataTemplate)XamlReader.Load(
                @"<DataTemplate xmlns=""http://schemas.microsoft.com/client/2007"">
                     <Border BorderThickness=""1 1 0 0"" BorderBrush=""LightGray"" Background=""" + color.ToString() + @""">                        
                         <TextBlock Text=""{Binding Data, Mode=OneWay}"" Margin=""4"" VerticalAlignment=""Center"" HorizontalAlignment=""Right""/>                           
                     </Border> 
                 </DataTemplate>"
                 );
#else
            FrameworkElementFactory border = new FrameworkElementFactory(typeof(Border));
            border.SetValue(Border.BorderThicknessProperty, new Thickness(1, 1, 0, 0));
            border.SetValue(Border.BorderBrushProperty, Brushes.LightGray);
            border.SetValue(Border.BackgroundProperty, new SolidColorBrush(color));
            DataTemplate dataTemplate = new DataTemplate();
            dataTemplate.VisualTree = border;
            FrameworkElementFactory textBlock = new FrameworkElementFactory(typeof(TextBlock));
            textBlock.SetBinding(TextBlock.TextProperty, new Binding("Data"));
            textBlock.SetValue(TextBlock.MarginProperty, new Thickness(2));
            textBlock.SetValue(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center);
            textBlock.SetValue(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Right);
            border.AppendChild(textBlock);
            dataTemplate.Seal();

            return dataTemplate;
#endif            
        }
    }
}

