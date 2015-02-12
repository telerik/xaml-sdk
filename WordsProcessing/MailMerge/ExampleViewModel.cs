using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Telerik.Windows.Controls;
using Telerik.Windows.Documents.Flow.FormatProviders.Docx;
using Telerik.Windows.Documents.Flow.Model;
using Telerik.Windows.Documents.Flow.Model.Editing;
using Telerik.Windows.Documents.Flow.Model.Fields;

namespace MailMerge
{
    public class ExampleViewModel : ViewModelBase
    {
        public ExampleViewModel()
        {
            this.MailMergeWithDynamicDataObjectCommand = new DelegateCommand(this.MailMergeWithDynamicDataObject);
            this.MailMergeWithConcreateDataObjectCommand = new DelegateCommand(this.MailMergeWithConcreateDataObject);
        }

        private ICommand mailMergeWithDynamicDataObjectCommand = null;
        public ICommand MailMergeWithDynamicDataObjectCommand
        {
            get
            {
                return this.mailMergeWithDynamicDataObjectCommand;
            }
            set
            {
                if (this.mailMergeWithDynamicDataObjectCommand != value)
                {
                    this.mailMergeWithDynamicDataObjectCommand = value;
                    this.OnPropertyChanged("MailMergeWithDynamicDataObjectCommand");
                }
            }
        }

        private ICommand mailMergeWithConcreateDataObjectCommand = null;
        public ICommand MailMergeWithConcreateDataObjectCommand
        {
            get
            {
                return this.mailMergeWithConcreateDataObjectCommand;
            }
            set
            {
                if (this.mailMergeWithConcreateDataObjectCommand != value)
                {
                    this.mailMergeWithConcreateDataObjectCommand = value;
                    this.OnPropertyChanged("MailMergeWithConcreateDataObjectCommand");
                }
            }
        }

        private void MailMergeWithDynamicDataObject(object obj)
        {
            IEnumerable mailMergeSource = this.GetDynamicMailMergeDataSource();
            RadFlowDocument mergedDocument = this.MergeTemplateWithData(mailMergeSource);
            this.SaveFile(mergedDocument);
        }

        private void MailMergeWithConcreateDataObject(object obj)
        {
            IEnumerable mailMergeSource = this.GetConcreateMailMergeDataSouce();
            RadFlowDocument mergedDocument = this.MergeTemplateWithData(mailMergeSource);
            this.SaveFile(mergedDocument);
        }

        private RadFlowDocument MergeTemplateWithData(IEnumerable mailMergeSource)
        {
            RadFlowDocument template = this.CreateMailMergeDocumentTemplate();
            RadFlowDocument mergedDocument = template.MailMerge(mailMergeSource);

            return mergedDocument;
        }

        private void SaveFile(RadFlowDocument document)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Docx File (*.docx)|*.docx";

            if (dialog.ShowDialog() == true)
            {
                using (Stream stream = dialog.OpenFile())
                {
                    DocxFormatProvider formatProvder = new DocxFormatProvider();
                    formatProvder.Export(document, stream);
                }
            }
        }

        private RadFlowDocument CreateMailMergeDocumentTemplate()
        {
            RadFlowDocument document = new RadFlowDocument();
            RadFlowDocumentEditor editor = new RadFlowDocumentEditor(document);

            editor.InsertText("Hello ");
            editor.InsertField("MERGEFIELD FirstName ", "«FirstName»");
            editor.InsertText(" ");
            editor.InsertField("MERGEFIELD LastName ", "«LastName»");
            editor.InsertText(",");

            editor.InsertParagraph();
            editor.InsertParagraph();
            editor.InsertText("On behalf of ");
            editor.InsertField("MERGEFIELD CompanyName ", "«CompanyName»");
            editor.InsertText(", ");
            editor.InsertText("I would like to thank you for the purchase of ");
            editor.InsertField("MERGEFIELD PurchasedItemsCount ", "«PurchasedItemsCount»");
            editor.InsertText(" ");
            editor.InsertField("MERGEFIELD ProductName ", "«ProductName»");
            editor.InsertText(" done by you from us.");

            editor.InsertParagraph();
            editor.InsertText("We are committed to provide you with the highest level of customer satisfaction possible. ");
            editor.InsertText("If for any reasons you have questions or comments please call ");
            editor.InsertField("MERGEFIELD ProductSupportPhone ", "«ProductSupportPhone»");
            editor.InsertText(" ");
            editor.InsertField("MERGEFIELD ProductSupportPhoneAvailability ", "«ProductSupportPhoneAvailability»");
            editor.InsertText(", or email us at ");
            editor.InsertField("MERGEFIELD ProductSupportEmail ", "«ProductSupportEmail»");
            editor.InsertText(".");

            editor.InsertParagraph();
            editor.InsertText("Once again thank you for choosing ");
            editor.InsertField("MERGEFIELD CompanyName ", "«CompanyName»");
            editor.InsertText(".");

            editor.InsertParagraph();
            editor.InsertParagraph();
            editor.InsertText("Sincerely yours,");
            editor.InsertParagraph();
            editor.InsertField("MERGEFIELD SalesRepFirstName ", "«SalesRepFirstName»");
            editor.InsertText(" ");
            editor.InsertField("MERGEFIELD SalesRepLastName ", "«SalesRepLastName»");
            editor.InsertText(",");

            Paragraph paragraph = editor.InsertParagraph();

            FieldInfo fieldInfo = new FieldInfo(document);
            paragraph.Inlines.Add(fieldInfo.Start);
            paragraph.Inlines.AddRun("MERGEFIELD SalesRepTitle ");
            paragraph.Inlines.Add(fieldInfo.Separator);
            paragraph.Inlines.AddRun("«SalesRepTitle» ");
            paragraph.Inlines.Add(fieldInfo.End);

            return document;
        }

        private IEnumerable GetDynamicMailMergeDataSource()
        {
            List<DynamicDataObject> mailMergeSource = new List<DynamicDataObject>();

            DynamicDataObject data = new DynamicDataObject();
            data.Set("FirstName", "Andrew");
            data.Set("LastName", "Fuller");
            data.Set("CompanyName", "Mobile Phone Factory");
            data.Set("PurchasedItemsCount", 500);
            data.Set("ProductName", "mobile phones");
            data.Set("ProductSupportPhone", "skype: MobilePhoneFactorySupport");
            data.Set("ProductSupportPhoneAvailability", "24/7");
            data.Set("ProductSupportEmail", "support@mobilephonefactory.org");
            data.Set("SalesRepFirstName", "Nancy");
            data.Set("SalesRepLastName", "Davolio");
            data.Set("SalesRepTitle", "Sales Associate");

            mailMergeSource.Add(data);

            data = new DynamicDataObject();
            data.Set("FirstName", "Margaret");
            data.Set("LastName", "Peacock");
            data.Set("CompanyName", "Mobile Phone Factory Japan");
            data.Set("PurchasedItemsCount", 1200);
            data.Set("ProductName", "mobile phones");
            data.Set("ProductSupportPhone", "skype: MobilePhoneFactorySupportJapan");
            data.Set("ProductSupportPhoneAvailability", "24/7");
            data.Set("ProductSupportEmail", "support@mobilephonefactory.org");
            data.Set("SalesRepFirstName", "Asako");
            data.Set("SalesRepLastName", "Hoshi");
            data.Set("SalesRepTitle", "Sales Associate");

            mailMergeSource.Add(data);

            return mailMergeSource;
        }

        private IEnumerable GetConcreateMailMergeDataSouce()
        {
            List<ConcreateDataObject> collection = new List<ConcreateDataObject>();
            var data = new ConcreateDataObject()
            {
                FirstName = "Andrew",
                LastName = "Fuller",
                CompanyName = "Mobile Phone Factory",
                PurchasedItemsCount = 50,
                ProductName = "mobile phones",
                ProductSupportPhone = "skype: MobilePhoneFactorySupport",
                ProductSupportPhoneAvailability = "24/7",
                ProductSupportEmail = "support@mobilephonefactory.org",
                SalesRepFirstName = "Nancy",
                SalesRepLastName = "Davolio",
                SalesRepTitle = "Sales Associate"
            };

            collection.Add(data);

            data = new ConcreateDataObject()
            {
                FirstName = "Margaret",
                LastName = "Peacock",
                CompanyName = "Mobile Phone Factory Japan",
                PurchasedItemsCount = 1200,
                ProductName = "mobile phones",
                ProductSupportPhone = "skype: MobilePhoneFactorySupportJapan",
                ProductSupportPhoneAvailability = "24/7",
                ProductSupportEmail = "support@mobilephonefactory.org",
                SalesRepFirstName = "Asako",
                SalesRepLastName = "Hoshi",
                SalesRepTitle = "Sales Associate"
            };

            collection.Add(data);
            return collection;
        }
    }
}
