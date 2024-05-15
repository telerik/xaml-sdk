using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Spreadsheet;
using Telerik.Windows.Controls.Spreadsheet.Worksheets.Layers;
using Telerik.Windows.Documents.Spreadsheet.Layout;
using Telerik.Windows.Documents.Spreadsheet.Model;


namespace CustomCellEditLayer
{
    public class CustomCellEditUILayer : WorksheetUILayerBase
    {
        #region Constants

        public const string CustomCellEditUILayerName = "CustomCellButtonUILayer";

        #endregion


        #region Properties

        public override string Name
        {
            get
            {
                return CustomCellEditUILayerName;
            }
        }

        #endregion


        #region Constructors

        public CustomCellEditUILayer(RadSpreadsheet sheet)
        {
            this.ContainerManager.UILayerContainer.IsHitTestVisible = true;
        }

        #endregion


        #region Methods

        private void CalculateCellEditorSize(RadButton button, CellLayoutBox activeCellBox)
        {
            if (activeCellBox == null)
            {
                return;
            }

            if (button != null)
            {
                button.Width = activeCellBox.Width;
                button.Height = activeCellBox.Height;

                Canvas.SetLeft(button, activeCellBox.Left);
                Canvas.SetTop(button, activeCellBox.Top);
            }
        }

        public override void UpdateUIOverride(WorksheetUIUpdateContextBase updateContext)
        {
            this.Clear();

            WorksheetUIUpdateContext worksheetUIUpdateContext = updateContext as WorksheetUIUpdateContext;

            if (worksheetUIUpdateContext == null || !worksheetUIUpdateContext.WorksheetEditor.Selection.IsCellSelection)
            {
                return;
            }

            SheetViewport viewport = updateContext.SheetViewport;

            for (int paneIndex = 0; paneIndex < viewport.ViewportPanesCount; paneIndex++)
            {
                var pane = viewport.ViewportPanes[paneIndex];
                CellRange visibleRange = pane.VisibleRange;

                for (int i = visibleRange.FromIndex.RowIndex; i <= visibleRange.ToIndex.RowIndex; i++)
                {
                    CellIndex cellIndex = new CellIndex(i, Constants.ButtonColumnIndex);
                    if (this.Worksheet.UsedCellRange.Contains(cellIndex))
                    {
                        RadButton button = this.GetElementFromPool<RadButton>(pane.ViewportPaneType);
                        button.Content = "Show age";
                        button.Click += this.Button_Click;
                        button.Tag = cellIndex;

                        this.CalculateCellEditorSize(button, updateContext.GetVisibleCellBox(cellIndex));
                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CellIndex cellIndex = (sender as Button).Tag as CellIndex;
            ICellValue cellValue = this.Owner.Worksheet.Cells[cellIndex].GetValue().Value;
            MessageBox.Show("Age: " + cellValue.RawValue);
        }

        protected override void TranslateAndScale(UIUpdateContext updateContext)
        {
            ScaleTransform scaleTransform = new ScaleTransform
            {
                ScaleX = updateContext.ScaleFactor.Width,
                ScaleY = updateContext.ScaleFactor.Height,
            };

            this.ContainerManager.SetRenderTransform(updateContext, scaleTransform);
        }

        protected override void ResetPooledElementProperties(object element)
        {
            RadButton button = element as RadButton;
            if (button != null)
            {
                button.Click -= this.Button_Click;
                button.Tag = null;
            }
        }

        #endregion
    }
}
