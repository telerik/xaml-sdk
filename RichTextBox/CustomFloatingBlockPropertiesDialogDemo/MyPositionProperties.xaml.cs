using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Documents.Layout;
using Telerik.Windows.Documents.Model;
using Telerik.Windows.Documents.Model.FloatingBlocks;
using Tuples = System;

namespace CustomFloatingBlockPropertiesDialogDemo
{
    public partial class MyPositionProperties : UserControl
    {
        #region Fields

        private bool isLoaded;

        #endregion

        #region Constructros

        public MyPositionProperties()
        {
            InitializeComponent();
            this.FillComboBoxesWithItems();

            this.isLoaded = true;
            this.UpdateRadioPositionTypes();
        }

        private void FillComboBoxesWithItems()
        {
            // horizontal
            this.horizontalAlignmentComboBox.ItemsSource = new List<Tuples.Tuple<RadHorizontalAlignment, string>>()
            {
                new Tuples.Tuple<RadHorizontalAlignment, string>(RadHorizontalAlignment.Left, LocalizationManager.GetString("Documents_FloatingBlockPropertiesDialog_PositionProperties_RadHorizontalAlignment_Left")),
                new Tuples.Tuple<RadHorizontalAlignment, string>(RadHorizontalAlignment.Center, LocalizationManager.GetString("Documents_FloatingBlockPropertiesDialog_PositionProperties_RadHorizontalAlignment_Center")),
                new Tuples.Tuple<RadHorizontalAlignment, string>(RadHorizontalAlignment.Right, LocalizationManager.GetString("Documents_FloatingBlockPropertiesDialog_PositionProperties_RadHorizontalAlignment_Right"))
            };
            this.Dispatcher.BeginInvoke(new Action(() => this.horizontalAlignmentComboBox.SelectedIndex = 0));

            this.horizontalAlignmentRelativeToComboBox.ItemsSource = this.GetHorizontalRelativeFromOptionsInTuple();
            this.Dispatcher.BeginInvoke(new Action(() => this.horizontalAlignmentRelativeToComboBox.SelectedIndex = 0));

            this.horizontalAbsoluteRelativeToComboBox.ItemsSource = this.GetHorizontalRelativeFromOptionsInTuple();
            this.Dispatcher.BeginInvoke(new Action(() => this.horizontalAbsoluteRelativeToComboBox.SelectedIndex = 0));

            // vertical
            this.verticalAlignmentComboBox.ItemsSource = new List<Tuples.Tuple<RadVerticalAlignment, string>>()
            {
                new Tuples.Tuple<RadVerticalAlignment, string>(RadVerticalAlignment.Top, LocalizationManager.GetString("Documents_FloatingBlockPropertiesDialog_PositionProperties_RadVerticalAlignment_Top")),
                new Tuples.Tuple<RadVerticalAlignment, string>(RadVerticalAlignment.Center, LocalizationManager.GetString("Documents_FloatingBlockPropertiesDialog_PositionProperties_RadVerticalAlignment_Center")),
                new Tuples.Tuple<RadVerticalAlignment, string>(RadVerticalAlignment.Bottom, LocalizationManager.GetString("Documents_FloatingBlockPropertiesDialog_PositionProperties_RadVerticalAlignment_Bottom"))
            };
            this.Dispatcher.BeginInvoke(new Action(() => this.verticalAlignmentComboBox.SelectedIndex = 0));

            this.verticalAlignmentRelativeToComboBox.ItemsSource = this.GetVerticalRelativeFromOptionsInTuple();
            this.Dispatcher.BeginInvoke(new Action(() => this.verticalAlignmentRelativeToComboBox.SelectedIndex = 0));

            this.verticalAbsoluteRelativeToComboBox.ItemsSource = this.GetVerticalRelativeFromOptionsInTuple();
            this.Dispatcher.BeginInvoke(new Action(() => this.verticalAbsoluteRelativeToComboBox.SelectedIndex = 0));
        }

        private List<Tuples.Tuple<VerticalRelativeFrom, string>> GetVerticalRelativeFromOptionsInTuple()
        {
            List<Tuples.Tuple<VerticalRelativeFrom, string>> tuples = new List<Tuples.Tuple<VerticalRelativeFrom, string>>()
            {
                new Tuples.Tuple<VerticalRelativeFrom, string>(VerticalRelativeFrom.Paragraph, LocalizationManager.GetString("Documents_FloatingBlockPropertiesDialog_PositionProperties_VerticalRelativeFrom_Paragraph")),
                new Tuples.Tuple<VerticalRelativeFrom, string>(VerticalRelativeFrom.Page, LocalizationManager.GetString("Documents_FloatingBlockPropertiesDialog_PositionProperties_VerticalRelativeFrom_Page")),
                new Tuples.Tuple<VerticalRelativeFrom, string>(VerticalRelativeFrom.Margin, LocalizationManager.GetString("Documents_FloatingBlockPropertiesDialog_PositionProperties_VerticalRelativeFrom_Margin")),
                new Tuples.Tuple<VerticalRelativeFrom, string>(VerticalRelativeFrom.TopMargin, LocalizationManager.GetString("Documents_FloatingBlockPropertiesDialog_PositionProperties_VerticalRelativeFrom_TopMargin")),
                new Tuples.Tuple<VerticalRelativeFrom, string>(VerticalRelativeFrom.BottomMargin, LocalizationManager.GetString("Documents_FloatingBlockPropertiesDialog_PositionProperties_VerticalRelativeFrom_BottomMargin")),
            };

            return tuples;
        }

        private List<Tuples.Tuple<HorizontalRelativeFrom, string>> GetHorizontalRelativeFromOptionsInTuple()
        {
            List<Tuples.Tuple<HorizontalRelativeFrom, string>> tuples = new List<Tuples.Tuple<HorizontalRelativeFrom, string>>()
            {
                new Tuples.Tuple<HorizontalRelativeFrom, string>(HorizontalRelativeFrom.Paragraph, LocalizationManager.GetString("Documents_FloatingBlockPropertiesDialog_PositionProperties_HorizontalRelativeFrom_Paragraph")),
                new Tuples.Tuple<HorizontalRelativeFrom, string>(HorizontalRelativeFrom.Page, LocalizationManager.GetString("Documents_FloatingBlockPropertiesDialog_PositionProperties_HorizontalRelativeFrom_Page")),
                new Tuples.Tuple<HorizontalRelativeFrom, string>(HorizontalRelativeFrom.Margin, LocalizationManager.GetString("Documents_FloatingBlockPropertiesDialog_PositionProperties_HorizontalRelativeFrom_Margin")),
                new Tuples.Tuple<HorizontalRelativeFrom, string>(HorizontalRelativeFrom.LeftMargin, LocalizationManager.GetString("Documents_FloatingBlockPropertiesDialog_PositionProperties_HorizontalRelativeFrom_LeftMargin")),
                new Tuples.Tuple<HorizontalRelativeFrom, string>(HorizontalRelativeFrom.RightMargin, LocalizationManager.GetString("Documents_FloatingBlockPropertiesDialog_PositionProperties_HorizontalRelativeFrom_RightMargin")),
            };

            return tuples;
        }

        #endregion

        #region Methods

        internal void FillUI(Inline targetInline)
        {
            FloatingBlock flaotingBlock = targetInline as FloatingBlock;
            if (flaotingBlock != null)
            {
                this.FillUIFromFloatingBlock(flaotingBlock);
                this.UpdateRadioPositionTypes();
            }
            else
            {
                ImageInline image = targetInline as ImageInline;
                Debug.Assert(image != null, "Unexpected inline type");
                if (image != null)
                {
                    this.FillUIFromImage(image);
                }
            }

        }

        private void FillUIFromFloatingBlock(FloatingBlock flaotingBlock)
        {
            this.SetVerticalPosition(flaotingBlock.VerticalPosition);
            this.SetHorizontalPosition(flaotingBlock.HorizontalPosition);
            this.SetAllowOverlap(flaotingBlock.AllowOverlap);
        }

        private void FillUIFromImage(ImageInline image)
        {
            this.SetVerticalPosition(new FloatingBlockVerticalPosition(VerticalRelativeFrom.Paragraph, 0d));
            this.SetHorizontalPosition(new FloatingBlockHorizontalPosition(HorizontalRelativeFrom.Paragraph, 0d));
            this.SetAllowOverlap(false);
            this.SetIsEnabled(false);
        }

        internal void WrappingModeUpdated(WrappingStyle? wrappingStyle)
        {
            if (wrappingStyle != null)
            {
                this.SetIsEnabled(true);
                this.UpdateRadioPositionTypes();
            }
            else
            {
                this.SetIsEnabled(false);
            }
        }

        private void SetVerticalPosition(FloatingBlockVerticalPosition vPos)
        {
            this.verticalAlignmentComboBox.SelectedValue = vPos.Alignment;
            this.verticalAbsoluteOffset.Value = vPos.Offset;
            this.verticalAlignmentRelativeToComboBox.SelectedValue = vPos.RelativeFrom;
            this.verticalAbsoluteRelativeToComboBox.SelectedValue = vPos.RelativeFrom;

            if (vPos.ValueType == PositionValueType.Alignment)
            {
                this.verticalAlignmentRadio.IsChecked = true;
            }
            else
            {
                this.verticalAbsoluteRadio.IsChecked = true;
            }
        }

        private void SetHorizontalPosition(FloatingBlockHorizontalPosition hPos)
        {
            this.horizontalAlignmentComboBox.SelectedValue = hPos.Alignment;
            this.horizontalAbsoluteOffset.Value = hPos.Offset;
            this.horizontalAlignmentRelativeToComboBox.SelectedValue = hPos.RelativeFrom;
            this.horizontalAbsoluteRelativeToComboBox.SelectedValue = hPos.RelativeFrom;

            if (hPos.ValueType == PositionValueType.Alignment)
            {
                this.horizontalAlignmentRadio.IsChecked = true;
            }
            else
            {
                this.horizontalAbsoluteRadio.IsChecked = true;
            }
        }

        public void SetAllowOverlap(bool value)
        {
            this.checkAllowOverlap.IsChecked = value;
        }

        public FloatingBlockHorizontalPosition GetHorizontalPosition()
        {
            if (this.horizontalAlignmentRadio.IsChecked.Value)
            {
                HorizontalRelativeFrom relative = (HorizontalRelativeFrom)this.horizontalAlignmentRelativeToComboBox.SelectedValue;
                RadHorizontalAlignment align = (RadHorizontalAlignment)this.horizontalAlignmentComboBox.SelectedValue;
                return new FloatingBlockHorizontalPosition(relative, align);
            }
            else
            {
                HorizontalRelativeFrom relative = (HorizontalRelativeFrom)this.horizontalAbsoluteRelativeToComboBox.SelectedValue;
                double offset = this.horizontalAbsoluteOffset.Value.Value;
                return new FloatingBlockHorizontalPosition(relative, offset);
            }
        }

        public FloatingBlockVerticalPosition GetVerticalPosition()
        {
            if (this.verticalAlignmentRadio.IsChecked.Value)
            {
                VerticalRelativeFrom relative = (VerticalRelativeFrom)this.verticalAlignmentRelativeToComboBox.SelectedValue;
                RadVerticalAlignment align = (RadVerticalAlignment)this.verticalAlignmentComboBox.SelectedValue;
                return new FloatingBlockVerticalPosition(relative, align);
            }
            else
            {
                VerticalRelativeFrom relative = (VerticalRelativeFrom)this.verticalAbsoluteRelativeToComboBox.SelectedValue;
                double offset = this.verticalAbsoluteOffset.Value.Value;
                return new FloatingBlockVerticalPosition(relative, offset);
            }
        }

        public bool GetAllowOverlap()
        {
            return this.checkAllowOverlap.IsChecked.Value;
        }

        private void SetIsEnabled(bool isEnabled)
        {
            this.verticalAlignmentRadio.IsEnabled = isEnabled;
            this.verticalAlignmentComboBox.IsEnabled = isEnabled;
            this.verticalAlignmentRelativeToComboBox.IsEnabled = isEnabled;
            this.verticalAbsoluteRadio.IsEnabled = isEnabled;
            this.verticalAbsoluteOffset.IsEnabled = isEnabled;
            this.verticalAbsoluteRelativeToComboBox.IsEnabled = isEnabled;

            this.horizontalAlignmentRadio.IsEnabled = isEnabled;
            this.horizontalAlignmentComboBox.IsEnabled = isEnabled;
            this.horizontalAlignmentRelativeToComboBox.IsEnabled = isEnabled;
            this.horizontalAbsoluteRadio.IsEnabled = isEnabled;
            this.horizontalAbsoluteOffset.IsEnabled = isEnabled;
            this.horizontalAbsoluteRelativeToComboBox.IsEnabled = isEnabled;

            this.checkAllowOverlap.IsEnabled = isEnabled;
        }

        private void UpdateRadioPositionTypes()
        {
            if (!this.isLoaded)
            {
                return;
            }

            bool absoluteVerticalIsChecked = this.verticalAbsoluteRadio.IsChecked.Value;

            this.verticalAlignmentComboBox.IsEnabled = !absoluteVerticalIsChecked;
            this.verticalAlignmentRelativeToComboBox.IsEnabled = !absoluteVerticalIsChecked;
            this.verticalAbsoluteOffset.IsEnabled = absoluteVerticalIsChecked;
            this.verticalAbsoluteRelativeToComboBox.IsEnabled = absoluteVerticalIsChecked;

            bool absoluteHorizontalIsChecked = this.horizontalAbsoluteRadio.IsChecked.Value;

            this.horizontalAlignmentComboBox.IsEnabled = !absoluteHorizontalIsChecked;
            this.horizontalAlignmentRelativeToComboBox.IsEnabled = !absoluteHorizontalIsChecked;
            this.horizontalAbsoluteOffset.IsEnabled = absoluteHorizontalIsChecked;
            this.horizontalAbsoluteRelativeToComboBox.IsEnabled = absoluteHorizontalIsChecked;
        }

        #endregion

        #region Event Handlers

        private void positionValueTypeRadio_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.UpdateRadioPositionTypes();
        }

        #endregion
    }
}

