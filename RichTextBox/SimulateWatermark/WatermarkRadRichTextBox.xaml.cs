using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Telerik.Windows.Documents;
using Telerik.Windows.Documents.FormatProviders.Txt;
using Telerik.Windows.Documents.RichTextBoxCommands;

namespace SimulateWatermark
{
    public partial class WatermarkRadRichTextBox : UserControl
    {
        #region Fields

        private readonly TxtFormatProvider provider;
        private bool setTextSilentlyEnabled;

        #endregion


        #region Properties

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(WatermarkRadRichTextBox), new PropertyMetadata(OnTextChanged));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty WatermarkTextProperty =
            DependencyProperty.Register("WatermarkText", typeof(string), typeof(WatermarkRadRichTextBox), null);

        public string WatermarkText
        {
            get { return (string)GetValue(WatermarkTextProperty); }
            set { SetValue(WatermarkTextProperty, value); }
        }

        public static readonly DependencyProperty WatermarkForegroundProperty =
            DependencyProperty.Register("WatermarkForeground", typeof(Brush), typeof(WatermarkRadRichTextBox), null);

        public Brush WatermarkForeground
        {
            get { return (Brush)GetValue(WatermarkForegroundProperty); }
            set { SetValue(WatermarkForegroundProperty, value); }
        }

        #endregion


        #region Constructors

        public WatermarkRadRichTextBox()
        {
            InitializeComponent();

            this.AttachToEvents();
            this.FillInputBindingsCollection();

            this.provider = new TxtFormatProvider();
        }

        #endregion


        #region Methods

        private void AttachToEvents()
        {
            this.radRichTextBox.GotFocus += this.OnGotFocus;
            this.radRichTextBox.LostFocus += this.OnLostFocus;
            this.radRichTextBox.DocumentContentChanged += this.OnDocumentContentChanged;
        }

        private void FillInputBindingsCollection()
        {
            this.radRichTextBox.RegisteredApplicationCommands.Clear();
            this.radRichTextBox.RegisteredApplicationCommands.Add(ApplicationCommands.Cut);
            this.radRichTextBox.RegisteredApplicationCommands.Add(ApplicationCommands.Copy);
            this.radRichTextBox.RegisteredApplicationCommands.Add(ApplicationCommands.Paste);
            this.radRichTextBox.RegisteredApplicationCommands.Add(ApplicationCommands.SelectAll);
            this.radRichTextBox.RegisteredApplicationCommands.Add(ApplicationCommands.Undo);
            this.radRichTextBox.RegisteredApplicationCommands.Add(ApplicationCommands.Redo);

            RadRichTextBox.DefaultInputBindings.Clear();

            this.RegisterCommand(RichTextBoxCommands.InsertText, Key.Tab, ModifierKeys.Control, "\t");
            this.RegisterCommand(RichTextBoxCommands.Delete, Key.Delete, ModifierKeys.None, false);
            this.RegisterCommand(RichTextBoxCommands.Delete, Key.Back, ModifierKeys.None, true);
            this.RegisterCommand(RichTextBoxCommands.Delete, Key.Back, ModifierKeys.Shift, true);
            this.RegisterCommandOrShift(RichTextBoxCommands.MoveCaret, Key.Left, ModifierKeys.None, MoveCaretDirections.Previous);
            this.RegisterCommandOrShift(RichTextBoxCommands.MoveCaret, Key.Left, ModifierKeys.Control, MoveCaretDirections.PreviousWord);
            this.RegisterCommandOrShift(RichTextBoxCommands.MoveCaret, Key.Right, ModifierKeys.None, MoveCaretDirections.Next);
            this.RegisterCommandOrShift(RichTextBoxCommands.MoveCaret, Key.Right, ModifierKeys.Control, MoveCaretDirections.NextWord);
            this.RegisterCommandOrShift(RichTextBoxCommands.MoveCaret, Key.Up, ModifierKeys.None, MoveCaretDirections.Up);
            this.RegisterCommandOrShift(RichTextBoxCommands.MoveCaret, Key.Down, ModifierKeys.None, MoveCaretDirections.Down);
            this.RegisterCommandOrShift(RichTextBoxCommands.MoveCaret, Key.Up, ModifierKeys.Control, MoveCaretDirections.ParagraphStart);
            this.RegisterCommandOrShift(RichTextBoxCommands.MoveCaret, Key.Down, ModifierKeys.Control, MoveCaretDirections.ParagraphEnd);
            this.RegisterCommandOrShift(RichTextBoxCommands.MoveCaret, Key.Home, ModifierKeys.None, MoveCaretDirections.Home);
            this.RegisterCommandOrShift(RichTextBoxCommands.MoveCaret, Key.Home, ModifierKeys.Control, MoveCaretDirections.DocumentStart);
            this.RegisterCommandOrShift(RichTextBoxCommands.MoveCaret, Key.End, ModifierKeys.None, MoveCaretDirections.End);
            this.RegisterCommandOrShift(RichTextBoxCommands.MoveCaret, Key.End, ModifierKeys.Control, MoveCaretDirections.DocumentEnd);
            this.RegisterCommandOrShift(RichTextBoxCommands.MoveCaret, Key.PageUp, ModifierKeys.None, MoveCaretDirections.PageUp);
            this.RegisterCommandOrShift(RichTextBoxCommands.MoveCaret, Key.PageDown, ModifierKeys.None, MoveCaretDirections.PageDown);
        }

        private void ShowWatermark()
        {
            this.watermarkTextBlock.Visibility = Visibility.Visible;
        }

        private void HideWatermark()
        {
            this.watermarkTextBlock.Visibility = Visibility.Collapsed;
        }

        private bool IsTextEmpty(RadRichTextBox rrtb)
        {
            return string.IsNullOrEmpty(this.GetText(rrtb));
        }

        private string GetText(RadRichTextBox rrtb)
        {
            return this.provider.Export(rrtb.Document);
        }

        private void SetText(RadRichTextBox rrtb, string newText)
        {
            rrtb.Document = this.provider.Import(newText);
            if (!string.IsNullOrEmpty(newText))
            {
                this.HideWatermark();
            }
        }

        private static void OnTextChanged(DependencyObject dObj, DependencyPropertyChangedEventArgs args)
        {
            WatermarkRadRichTextBox watermarkRrtb = dObj as WatermarkRadRichTextBox;

            if (args.NewValue != null && !watermarkRrtb.setTextSilentlyEnabled)
            {
                string newTextValue = args.NewValue.ToString();
                watermarkRrtb.SetText(watermarkRrtb.radRichTextBox, newTextValue);
            }
        }

        public void RegisterCommand(RoutedCommand command, Key key, ModifierKeys modifierKeys = ModifierKeys.None, object commandParameter = null)
        {
            RadRichTextBox.RegisterCommand(command, key, modifierKeys, commandParameter);
        }

        private void RegisterCommandOrShift(RoutedCommand command, Key key, ModifierKeys modifierKeys = ModifierKeys.None, object commandParameter = null)
        {
            RadRichTextBox.RegisterCommand(command, key, modifierKeys, commandParameter);
            RadRichTextBox.RegisterCommand(command, key, modifierKeys | ModifierKeys.Shift, commandParameter);
        }

        #endregion


        #region Event Handlers

        private void OnGotFocus(object sender, RoutedEventArgs e)
        {
            this.HideWatermark();
        }

        private void OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (this.IsTextEmpty(this.radRichTextBox))
            {
                this.ShowWatermark();
            }
        }

        private void OnDocumentContentChanged(object sender, EventArgs e)
        {
            RadRichTextBox radRichTextBox = sender as RadRichTextBox;
            this.setTextSilentlyEnabled = true;
            this.Text = this.GetText(radRichTextBox);
            this.setTextSilentlyEnabled = false;
        }

        #endregion
    }
}
