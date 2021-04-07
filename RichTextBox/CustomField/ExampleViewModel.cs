using System.Windows;
using System.Windows.Input;
using Telerik.Windows.Controls;
using Telerik.Windows.Documents.Model;

namespace CustomField
{
    public class ExampleViewModel : ViewModelBase
    {
        private ICommand insertCustomFieldCommand = null;
        private static RadRichTextBox associatedRichTextBox;

        public ICommand InsertCustomFieldCommand
        {
            get
            {
                return this.insertCustomFieldCommand;
            }
            set
            {
                if (this.insertCustomFieldCommand != value)
                {
                    this.insertCustomFieldCommand = value;
                    this.OnPropertyChanged("InsertCustomFieldCommand");
                }
            }
        }

        public static RadRichTextBox GetRadRichTextBox(DependencyObject obj)
        {
            return (RadRichTextBox)obj.GetValue(RadRichTextBoxProperty);
        }

        public static void SetRadRichTextBox(DependencyObject obj, RadRichTextBox value)
        {
            obj.SetValue(RadRichTextBoxProperty, value);
        }

        // Using a DependencyProperty as the backing store for RadRichTextBox.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RadRichTextBoxProperty =
            DependencyProperty.RegisterAttached("RadRichTextBox", typeof(RadRichTextBox), typeof(ExampleViewModel), new PropertyMetadata(RadRichTextBoxChanged));

        private static void RadRichTextBoxChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            associatedRichTextBox = e.NewValue as RadRichTextBox;
        }

        public ExampleViewModel()
        {
            this.InsertCustomFieldCommand = new DelegateCommand(this.Insert);
        }

        private void Insert(object param)
        {
            if (associatedRichTextBox == null)
            {
                return;
            }

            switch (param as string)
            {
                case "Header":
                    this.InsertCustomFieldInHeader();
                    break;
                case "Footer":
                    this.InsertCustomFieldInFooter();
                    break;
                case "Document":
                    this.InsertCustomFieldInDocument();
                    break;
                default:
                    break;
            }
        }

        private void InsertCustomFieldInDocument()
        {
            associatedRichTextBox.InsertField(new CustomPageField(), FieldDisplayMode.Code);
        }

        private void InsertCustomFieldInFooter()
        {
            RadDocument document = new RadDocument();
            RadDocumentEditor editor = new RadDocumentEditor(document);

            Footer footer = associatedRichTextBox.Document.Sections.First.Footers.Default;
            footer.Body = document;

            CustomPageField customPageField = new CustomPageField
            {
                NumberFormattingType = NumberFormattingTypes.Latin
            };

            editor.InsertField(customPageField, FieldDisplayMode.Result);
        }

        private void InsertCustomFieldInHeader()
        {
            RadDocument document = new RadDocument();
            RadDocumentEditor editor = new RadDocumentEditor(document);

            Header header = associatedRichTextBox.Document.Sections.First.Headers.Default;
            header.Body = document;

            CustomPageField customPageField = new CustomPageField
            {
                NumberFormattingType = NumberFormattingTypes.Roman,
                DocumentName = "RadDocument",
            };

            editor.InsertField(customPageField, FieldDisplayMode.Result);
        }
    }
}
