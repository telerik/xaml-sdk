using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Documents.Model;

namespace CustomFloatingBlockPropertiesDialogDemo
{
    public partial class MyTextWrappingProperties : UserControl
    {
        #region Constructors

        public MyTextWrappingProperties()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        internal void FillUI(Inline targetInline)
        {
            FloatingBlock flaotingBlock = targetInline as FloatingBlock;
            if (flaotingBlock != null)
            {
                this.FillUIFromFloatingBlock(flaotingBlock);
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

        private void FillUIFromFloatingBlock(FloatingBlock floatingBlock)
        {
            this.SetWrappingStyle(floatingBlock.WrappingStyle);
            this.SetMargin(floatingBlock.Margin);
            this.SetTextWrap(floatingBlock.TextWrap);
        }

        private void FillUIFromImage(ImageInline image)
        {
            this.SetWrappingStyle(null);
            this.SetMargin(new Thickness());
            this.SetTextWrap(TextWrap.BothSides);
        }

        private void UpdateOnWrappingStyleChanged()
        {
            this.DisableAllControls();
            WrappingStyle? style = this.GetWrappingStyle();

            if (style == null)
            {
                return;
            }

            switch (style.Value)
            {
                case WrappingStyle.TopAndBottom:
                    this.numTop.IsEnabled = true;
                    this.numBottom.IsEnabled = true;
                    break;
                case WrappingStyle.Square:
                    this.EnableAllControls();
                    break;
                case WrappingStyle.BehindText:
                case WrappingStyle.InFrontOfText:
                    break;
                default:
                    throw new NotImplementedException("Unknown WrappingStyle: " + style.Value.ToString());
            }

        }

        public WrappingStyle? GetWrappingStyle()
        {
            if (this.wrappingStyleSquareButton.IsChecked.Value)
            {
                return WrappingStyle.Square;
            }
            if (this.wrappingStyleBehindTextButton.IsChecked.Value)
            {
                return WrappingStyle.BehindText;
            }
            if (this.wrappingStyleInFrontOfTextButton.IsChecked.Value)
            {
                return WrappingStyle.InFrontOfText;
            }
            if (this.wrappingStyleTopAndBottomButton.IsChecked.Value)
            {
                return WrappingStyle.TopAndBottom;
            }

            return null;
        }

        private void SetWrappingStyle(WrappingStyle? style)
        {
            this.UncheckWrappingStyleToggleButtons();
            if (style == null)
            {
                this.wrappingStyleInLineWithTextButton.IsChecked = true;
            }
            else
            {
                switch (style.Value)
                {
                    case WrappingStyle.TopAndBottom:
                        this.wrappingStyleTopAndBottomButton.IsChecked = true;
                        break;
                    case WrappingStyle.Square:
                        this.wrappingStyleSquareButton.IsChecked = true;
                        break;
                    case WrappingStyle.BehindText:
                        this.wrappingStyleBehindTextButton.IsChecked = true;
                        break;
                    case WrappingStyle.InFrontOfText:
                        this.wrappingStyleInFrontOfTextButton.IsChecked = true;
                        break;
                    default:
                        throw new NotImplementedException("Unknown WrappingStyle: " + style.Value.ToString());
                }

            }
        }

        private void SetMargin(Thickness margin)
        {
            this.numLeft.Value = margin.Left;
            this.numRight.Value = margin.Right;
            this.numTop.Value = margin.Top;
            this.numBottom.Value = margin.Bottom;

        }

        public Thickness GetMargin()
        {
            return new Thickness()
            {
                Left = this.numLeft.Value.Value,
                Right = this.numRight.Value.Value,
                Top = this.numTop.Value.Value,
                Bottom = this.numBottom.Value.Value,
            };
        }

        private void SetTextWrap(TextWrap textWrap)
        {
            switch (textWrap)
            {
                case TextWrap.BothSides:
                    this.radWrapTextBothSides.IsChecked = true;
                    break;
                case TextWrap.LeftOnly:
                    this.radWrapTextLeft.IsChecked = true;
                    break;
                case TextWrap.RightOnly:
                    this.radWrapTextRight.IsChecked = true;
                    break;
                default:
                    throw new NotImplementedException("Unknown TextWrap: " + textWrap.ToString());
            }
        }

        public TextWrap GetTextWrap()
        {
            TextWrap result = TextWrap.BothSides;

            if (this.radWrapTextBothSides.IsChecked.Value)
            {
                result = TextWrap.BothSides;
            }

            if (this.radWrapTextLeft.IsChecked.Value)
            {
                result = TextWrap.LeftOnly;
            }

            if (this.radWrapTextRight.IsChecked.Value)
            {
                result = TextWrap.RightOnly;
            }

            return result;
        }

        private void UncheckWrappingStyleToggleButtons()
        {
            this.wrappingStyleBehindTextButton.IsChecked = false;
            this.wrappingStyleInFrontOfTextButton.IsChecked = false;
            this.wrappingStyleInLineWithTextButton.IsChecked = false;
            this.wrappingStyleSquareButton.IsChecked = false;
            this.wrappingStyleTopAndBottomButton.IsChecked = false;
        }

        private void DisableAllControls()
        {
            this.numLeft.IsEnabled = false;
            this.numRight.IsEnabled = false;
            this.numTop.IsEnabled = false;
            this.numBottom.IsEnabled = false;

            this.radWrapTextBothSides.IsEnabled = false;
            this.radWrapTextLeft.IsEnabled = false;
            this.radWrapTextRight.IsEnabled = false;
        }

        private void EnableAllControls()
        {
            this.numLeft.IsEnabled = true;
            this.numRight.IsEnabled = true;
            this.numTop.IsEnabled = true;
            this.numBottom.IsEnabled = true;

            this.radWrapTextBothSides.IsEnabled = true;
            this.radWrapTextLeft.IsEnabled = true;
            this.radWrapTextRight.IsEnabled = true;
        }

        #endregion

        #region Event Handlers

        private void WrappingStyleRadToggleButton_PreviewClick(object sender, RoutedEventArgs e)
        {
            this.UncheckWrappingStyleToggleButtons();
        }

        private void WrappingStyleRadToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            this.UpdateOnWrappingStyleChanged();
            this.OnWrappingStyleChanged();
        }

        #endregion

        #region Events

        public event EventHandler WrappingStyleChanged;
        protected void OnWrappingStyleChanged()
        {
            if (this.WrappingStyleChanged != null)
            {
                this.WrappingStyleChanged(this, EventArgs.Empty);
            }
        }

        #endregion
    }
}
