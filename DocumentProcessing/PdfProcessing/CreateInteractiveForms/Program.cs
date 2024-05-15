using System;
using System.Diagnostics;
using System.IO;
#if NETCOREAPP
using Telerik.Documents.Primitives;
#else
using System.Windows;
#endif
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf;
using Telerik.Windows.Documents.Fixed.Model;
using Telerik.Windows.Documents.Fixed.Model.ColorSpaces;
using Telerik.Windows.Documents.Fixed.Model.Editing;
using Telerik.Windows.Documents.Fixed.Model.InteractiveForms;

namespace CreateInteractiveForms
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            RadFixedDocument document = CreateDocument();

            string fileName = "AllFieldTypes.pdf";

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            File.WriteAllBytes(fileName, new PdfFormatProvider().Export(document));

            Console.WriteLine("Document created.");

            ProcessStartInfo psi = new ProcessStartInfo()
            {
                FileName = fileName,
                UseShellExecute = true
            };
            Process.Start(psi);
        }

        private static RadFixedDocument CreateDocument()
        {
            RadFixedDocument document = new RadFixedDocument();
            CreateFields(document);
            DrawPageWithWidgets(document);

            return document;
        }

        private static void CreateFields(RadFixedDocument document)
        {
            CheckBoxField check = new CheckBoxField("checkBox");
            document.AcroForm.FormFields.Add(check);
            check.IsChecked = true;

            ComboBoxField combo = new ComboBoxField("combo");
            document.AcroForm.FormFields.Add(combo);
            combo.Options.Add(new ChoiceOption("Combo choice 1"));
            combo.Options.Add(new ChoiceOption("Combo choice 2"));
            combo.Options.Add(new ChoiceOption("Combo choice 3"));
            combo.Options.Add(new ChoiceOption("Combo choice 4"));
            combo.Options.Add(new ChoiceOption("Combo choice 5"));
            combo.Value = combo.Options[2];

            CombTextBoxField comb = new CombTextBoxField("comb");
            document.AcroForm.FormFields.Add(comb);
            comb.MaxLengthOfInputCharacters = 10;
            comb.Value = "0123456789";

            ListBoxField list = new ListBoxField("list");
            document.AcroForm.FormFields.Add(list);
            list.AllowMultiSelection = true;
            list.Options.Add(new ChoiceOption("List choice 1"));
            list.Options.Add(new ChoiceOption("List choice 2"));
            list.Options.Add(new ChoiceOption("List choice 3"));
            list.Options.Add(new ChoiceOption("List choice 4"));
            list.Options.Add(new ChoiceOption("List choice 5"));
            list.Options.Add(new ChoiceOption("List choice 6"));
            list.Options.Add(new ChoiceOption("List choice 7"));
            list.Value = new ChoiceOption[] { list.Options[0], list.Options[2] };

            PushButtonField push = new PushButtonField("push");
            document.AcroForm.FormFields.Add(push);

            RadioButtonField radio = new RadioButtonField("radio");
            document.AcroForm.FormFields.Add(radio);
            radio.Options.Add(new RadioOption("Radio option 1"));
            radio.Options.Add(new RadioOption("Radio option 2"));
            radio.Value = radio.Options[1];

            SignatureField signature = new SignatureField("signature");
            document.AcroForm.FormFields.Add(signature);

            TextBoxField textBox = new TextBoxField("textBox");
            document.AcroForm.FormFields.Add(textBox);
            textBox.Value = "Sample text...";
        }

        private static void DrawPageWithWidgets(RadFixedDocument document)
        {
            RadFixedPage page = document.Pages.AddPage();

            FixedContentEditor editor = new FixedContentEditor(page);
            using (editor.SaveGraphicProperties())
            {
                editor.GraphicProperties.IsFilled = true;
                editor.GraphicProperties.IsStroked = false;
                editor.GraphicProperties.StrokeThickness = 0;
                editor.GraphicProperties.FillColor = new RgbColor(209, 178, 234);
                editor.DrawRectangle(new Rect(50, 50, editor.Root.Size.Width - 100, editor.Root.Size.Height - 100));
            }

            editor.Position.Translate(100, 100);
            Size widgetDimensions = new Size(200, 30);

            foreach (FormField field in document.AcroForm.FormFields)
            {
                switch (field.FieldType)
                {
                    case FormFieldType.CheckBox:
                        CheckBoxField check = (CheckBoxField)field;
                        DrawNextWidgetWithDescription(editor, "CheckBox", (e) => e.DrawWidget(check, widgetDimensions));
                        break;
                    case FormFieldType.ComboBox:
                        ComboBoxField combo = (ComboBoxField)field;
                        DrawNextWidgetWithDescription(editor, "ComboBox", (e) => e.DrawWidget(combo, widgetDimensions));
                        break;
                    case FormFieldType.CombTextBox:
                        CombTextBoxField comb = (CombTextBoxField)field;
                        DrawNextWidgetWithDescription(editor, "Comb TextBox", (e) => e.DrawWidget(comb, widgetDimensions));
                        break;
                    case FormFieldType.ListBox:
                        ListBoxField list = (ListBoxField)field;
                        DrawNextWidgetWithDescription(editor, "ListBox", (e) => e.DrawWidget(list, new Size(widgetDimensions.Width, widgetDimensions.Width)));
                        break;
                    case FormFieldType.PushButton:
                        PushButtonField push = (PushButtonField)field;
                        DrawNextWidgetWithDescription(editor, "Button", (e) => e.DrawWidget(push, widgetDimensions));
                        break;
                    case FormFieldType.RadioButton:
                        RadioButtonField radio = (RadioButtonField)field;
                        foreach (RadioOption option in radio.Options)
                        {
                            DrawNextWidgetWithDescription(editor, option.Value, (e) => e.DrawWidget(radio, option, widgetDimensions));
                        }
                        break;
                    case FormFieldType.Signature:
                        SignatureField signature = (SignatureField)field;
                        DrawNextWidgetWithDescription(editor, "Signature", (e) => e.DrawWidget(signature, widgetDimensions));
                        break;
                    case FormFieldType.TextBox:
                        TextBoxField textBox = (TextBoxField)field;
                        DrawNextWidgetWithDescription(editor, "TextBox", (e) => e.DrawWidget(textBox, widgetDimensions));
                        break;
                }
            }
        }

        private static void DrawNextWidgetWithDescription(FixedContentEditor editor, string description, Action<FixedContentEditor> drawWidgetWithEditor)
        {
            double padding = 20;
            drawWidgetWithEditor(editor);

            Size annotationSize = editor.Root.Annotations[editor.Root.Annotations.Count - 1].Rect.Size;
            double x = editor.Position.Matrix.OffsetX;
            double y = editor.Position.Matrix.OffsetY;

            Block block = new Block();
            block.TextProperties.FontSize = 20;
            block.VerticalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.VerticalAlignment.Center;
            block.InsertText(description);
            editor.Position.Translate(x + annotationSize.Width + padding, y);
            editor.DrawBlock(block, new Size(editor.Root.Size.Width, annotationSize.Height));

            editor.Position.Translate(x, y + annotationSize.Height + padding);
        }
    }
}
