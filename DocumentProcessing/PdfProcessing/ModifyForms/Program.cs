using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf;
using Telerik.Windows.Documents.Fixed.Model;
using Telerik.Windows.Documents.Fixed.Model.InteractiveForms;


namespace ModifyFormValues
{
    internal class Program
    {
        public static readonly string RootDirectory = AppDomain.CurrentDomain.BaseDirectory;
        public static readonly string InputFileWithInteractiveForms = RootDirectory + "SampleData\\InteractiveForms.pdf";

        private static void Main(string[] args)
        {
            PdfFormatProvider provider = new PdfFormatProvider();

            RadFixedDocument document = provider.Import(File.ReadAllBytes(InputFileWithInteractiveForms), null);

            ModifyFormFieldValues(document);

            string modifiedFileName = "Modified.pdf";

            if (File.Exists(modifiedFileName))
            {
                File.Delete(modifiedFileName);
            }

            File.WriteAllBytes(modifiedFileName, provider.Export(document, null));

            ProcessStartInfo psi = new ProcessStartInfo()
            {
                FileName = modifiedFileName,
                UseShellExecute = true
            };
            Process.Start(psi);
        }

        private static void ModifyFormFieldValues(RadFixedDocument document)
        {
            foreach (FormField field in document.AcroForm.FormFields)
            {
                switch (field.FieldType)
                {
                    case FormFieldType.TextBox:
                        ModifyTextBox((TextBoxField)field, "Modified text value...");
                        break;
                    case FormFieldType.ListBox:
                        ModifyListBox((ListBoxField)field, new string[] { "Modified choice 1", "Modified choice 2", "Modified choice 3" }, 1);
                        break;
                    case FormFieldType.RadioButton:
                        ModifyRadioButtons((RadioButtonField)field, "Option 3");
                        break;
                    case FormFieldType.CheckBox:
                        ModifyCheckBox((CheckBoxField)field, new string[] { "Check Box1", "Check Box3" });
                        break;
                }
            }
        }

        private static void ModifyTextBox(TextBoxField textBoxField, string newTextValue)
        {
            textBoxField.Value = newTextValue;
        }

        private static void ModifyListBox(ListBoxField listField, IEnumerable<string> options, int selectedIndex)
        {
            listField.Options.Clear();
            int index = 0;

            foreach (string text in options)
            {
                ChoiceOption option = new ChoiceOption(text);
                listField.Options.Add(option);

                if (index == selectedIndex)
                {
                    listField.Value = new ChoiceOption[] { option };
                }

                index++;
            }
        }

        private static void ModifyRadioButtons(RadioButtonField radioField, string selectedOption)
        {
            foreach (RadioOption option in radioField.Options)
            {
                if (option.Value == selectedOption)
                {
                    radioField.Value = option;
                }
            }
        }

        private static void ModifyCheckBox(CheckBoxField checkBoxField, string[] selectedCheckBoxes)
        {
            checkBoxField.IsChecked = selectedCheckBoxes.Contains(checkBoxField.Name);
        }
    }
}
