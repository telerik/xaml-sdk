using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Telerik.Windows.Documents.Flow.FormatProviders.Docx;
using Telerik.Windows.Documents.Flow.Model;
using Telerik.Windows.Documents.Flow.Model.Editing;
using Telerik.Windows.Documents.Flow.Model.Fields;

namespace MailMerge
{
    public class DocumentProcessor
    {
        public void MailMergeWithDynamicDataObject()
        {
            IEnumerable mailMergeSource = this.GetDynamicMailMergeDataSource();
            RadFlowDocument mergedDocument = this.MergeTemplateWithData(mailMergeSource);
            this.SaveFile(mergedDocument);
        }

        public void MailMergeWithConcreteDataObject()
        {
            IEnumerable mailMergeSource = this.GetConcreteMailMergeDataSouce();
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
            string path = "Mail Merge Sample.docx";
            using (Stream stream = File.OpenWrite(path))
            {
                DocxFormatProvider formatProvder = new DocxFormatProvider();
                formatProvder.Export(document, stream);
            }

            ProcessStartInfo psi = new ProcessStartInfo()
            {
                FileName = path,
                UseShellExecute = true
            };
            Process.Start(psi);

            Console.Write("Mail merge finished - the document is saved.");
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

        private IEnumerable GetConcreteMailMergeDataSouce()
        {
            List<ConcreteDataObject> collection = new List<ConcreteDataObject>();
            ConcreteDataObject data = new ConcreteDataObject()
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

            data = new ConcreteDataObject()
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
