using System;
using System.Linq;
using System.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.RichTextBoxUI.Dialogs;
using Telerik.Windows.Documents.Model;
using Telerik.Windows.Documents.UI.Extensibility;

namespace CustomFloatingBlockPropertiesDialogDemo
{
    [CustomFloatingBlockPropertiesDialog]
    public partial class CustomFloatingBlockPropertiesDialogSL : RadRichTextBoxWindow, IFloatingBlockPropertiesDialog
    {
        #region Fields

        private Action<Inline, Inline> replaceCurrentInlineCallback;
        private Inline originalInline;
        private ImageInline originalImageInline;


        #endregion

        public CustomFloatingBlockPropertiesDialogSL()
        {
            InitializeComponent();
        }

        #region Methods

        public void ShowDialog(Inline targetInline, Action<Inline, Inline> replaceCurrentInlineCallback)
        {
            this.ShowDialogInternal(targetInline, replaceCurrentInlineCallback, null);
        }

        private void ShowDialogInternal(Inline targetInline, Action<Inline, Inline> replaceCallback, RadRichTextBox owner)
        {
            if (targetInline is ImageInline)
            {
                this.originalImageInline = (ImageInline)targetInline;
            }
            else if (targetInline is FloatingImageBlock)
            {
                this.originalImageInline = ((FloatingImageBlock)targetInline).ImageInline;
            }
            else
            {
                throw new InvalidOperationException("Unable to find image element.");
            }
            this.SetOwner(owner);
            this.replaceCurrentInlineCallback = replaceCallback;
            this.originalInline = targetInline;

            this.ShowDialog();

            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                this.FillUI(this.originalInline);
            }));
        }

        private void FillUI(Inline targetInline)
        {
            if (targetInline == null)
            {
                return;
            }

            this.positionProperties.FillUI(targetInline);
            this.textWrappingProperties.FillUI(targetInline);
        }

        private Inline CreateInlineToInsert()
        {
            Inline result;
            ImageInline imageInline = new ImageInline(this.originalImageInline);
            WrappingStyle? style = this.textWrappingProperties.GetWrappingStyle();

            if (style == null)
            {
                //create image inline
                result = imageInline;
            }
            else
            {
                FloatingImageBlock imageBlock = new FloatingImageBlock();
                imageBlock.ImageInline = imageInline;

                imageBlock.WrappingStyle = style.Value;
                imageBlock.TextWrap = this.textWrappingProperties.GetTextWrap();
                imageBlock.Margin = this.textWrappingProperties.GetMargin();
                imageBlock.VerticalPosition = this.positionProperties.GetVerticalPosition();
                imageBlock.HorizontalPosition = this.positionProperties.GetHorizontalPosition();
                imageBlock.AllowOverlap = this.positionProperties.GetAllowOverlap();

                result = imageBlock;
            }

            return result;
        }

        #endregion


        #region Event Handlers

        private void RadWindow_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Escape)
            {
                this.Close();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DefaultButton_Click(object sender, RoutedEventArgs e)
        {
            this.FillUI(this.originalInline);
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.replaceCurrentInlineCallback != null)
            {
                this.replaceCurrentInlineCallback(this.originalInline, this.CreateInlineToInsert());
            }
            this.Close();
        }

        private void RadWindow_Closed(object sender, WindowClosedEventArgs e)
        {
            this.Owner = null;
            this.originalImageInline = null;
            this.originalInline = null;
            this.replaceCurrentInlineCallback = null;

        }


        private void textWrappingProperties_WrappingStyleChanged(object sender, EventArgs e)
        {
            this.positionProperties.WrappingModeUpdated(this.textWrappingProperties.GetWrappingStyle());
        }
        #endregion
    }
}
