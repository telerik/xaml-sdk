using System;
using System.Linq;
using System.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.RichTextBoxUI.Dialogs;
using Telerik.Windows.Documents.Model;
using Telerik.Windows.Documents.UI.Extensibility;
using Telerik.Windows.Media.Imaging;
using Telerik.Windows.Media.Imaging.Tools;

namespace CustomImageEditorDialogDemo
{
    [CustomImageEditorDialog]
    public partial class MyCustomImageEditorDialog : RadRichTextBoxWindow, IImageEditorDialog
    {
        private Inline originalInline;
        private ImageInline originalImageInline;

        private bool isRotated;
        private Size originalAspect;
        private double originalRotateAngle;
        private Action<Inline, Inline> replaceCurrentImageCallback;

        public MyCustomImageEditorDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Shows the dialog. Specified insert image callback is applied on user confirmation.
        /// </summary>
        /// <param name="selectedImage">The selected image.</param>
        /// <param name="replaceCurrentInlineCallback">The replace image callback.</param>
        public void ShowDialog(Inline selectedImage, Action<Inline, Inline> replaceCurrentInlineCallback, string executeToolName)
        {
            this.ShowDialogInternal(selectedImage, replaceCurrentInlineCallback, executeToolName, null);
        }


        public void ShowDialogInternal(Inline orgInline, Action<Inline, Inline> replaceCurrentImageCallback, string executeToolName, RadRichTextBox owner)
        {
            this.SetOwner(owner);
            this.originalInline = orgInline;
            if (orgInline is ImageInline)
            {
                this.originalImageInline = (ImageInline)orgInline;
            }
            else if (orgInline is FloatingImageBlock)
            {
                this.originalImageInline = ((FloatingImageBlock)orgInline).ImageInline;
            }
            else
            {
                throw new InvalidOperationException("Unable to find image element.");
            }

            RadBitmap image = new RadBitmap(this.originalImageInline.ImageSource);
            this.isRotated = false;
            this.originalAspect = new Size(this.originalImageInline.Width / image.Width, this.originalImageInline.Height / image.Height);
            this.originalRotateAngle = this.originalImageInline.RotateAngle;


            this.replaceCurrentImageCallback = replaceCurrentImageCallback;
            this.ImageEditorUI.Image = image;

            this.ShowDialog();
            this.StartExecuteTool(executeToolName);
        }

        private void StartExecuteTool(string executeToolName)
        {
            if (!string.IsNullOrEmpty(executeToolName))
            {
                ITool toolToExecute = this.Resources[executeToolName] as ITool;
                if (toolToExecute != null)
                {
                    if (this.ImageEditorUI.ImageEditor == null)
                    {
                        this.ForceApplyTemplate();
                    }

                    this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            if (this.ImageEditorUI.ImageEditor != null)
                            {
                                this.ImageEditorUI.ImageEditor.ExecuteTool(toolToExecute);
                                this.ImageEditorUI.ImageEditor.ScaleFactor = 0;
                            }
                        }));
                }
            }
        }

        private void ForceApplyTemplate()
        {
            this.ImageEditorUI.ApplyTemplate();
            if (this.ImageEditorUI.ImageEditor != null)
            {
                this.ImageEditorUI.ImageEditor.ApplyTemplate();
            }
            this.ImageEditorUI.UpdateLayout();
        }

        void RadWindow_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Escape)
            {
                this.Close();
            }
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if (this.replaceCurrentImageCallback != null)
            {
                this.ImageEditorUI.ImageEditor.CommitTool();
                Inline reslut;
                ImageInline image = new ImageInline(this.ImageEditorUI.Image.Bitmap);
                image.CopyPropertiesFrom(this.originalImageInline);

                image.Size = new Size(image.Width * this.originalAspect.Width, image.Height * this.originalAspect.Height);
                if (this.isRotated)
                {
                    image.Size = new Size(image.Size.Height, image.Size.Width);
                }
                image.RotateAngle = this.originalRotateAngle;

                if (this.originalInline is FloatingImageBlock)
                {
                    FloatingImageBlock imageBlock = new FloatingImageBlock();
                    imageBlock.CopyPropertiesFrom(this.originalInline);
                    imageBlock.ImageInline = image;
                    reslut = imageBlock;
                }
                else
                {
                    reslut = image;
                }

                this.replaceCurrentImageCallback(this.originalInline, reslut);
            }

            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        protected override void OnClosed(WindowClosedEventArgs args)
        {
            base.OnClosed(args);

            this.replaceCurrentImageCallback = null;
            this.Owner = null;
        }

        private void Rotate_Click(object sender, RoutedEventArgs e)
        {
            this.isRotated = !this.isRotated;
        }
    }
}

