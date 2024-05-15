using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Telerik.Windows.Documents.Flow.FormatProviders.Docx;
using Telerik.Windows.Documents.Flow.Model;
using Telerik.Windows.Documents.Flow.Model.Editing;
using Telerik.Windows.Documents.Flow.Model.Fields;
using Telerik.Windows.Documents.Flow.Model.Styles;
#if NETSTANDARD
using Telerik.Documents.Common.Model;
using Telerik.Documents.Media;
#else
using Telerik.Windows.Documents.Spreadsheet.Model;
using System.Windows.Media;
#endif

namespace MailMerge
{
    public static class DocumentProcessor
    {
        public static void MailMergeWithDynamicDataObject()
        {
            IEnumerable mailMergeSource = GetDynamicMailMergeDataSource();
            RadFlowDocument mergedDocument = MergeTemplateWithData(mailMergeSource);
            SaveFile(mergedDocument);
        }

        public static void MailMergeWithConcreteDataObject()
        {
            IEnumerable mailMergeSource = GetConcreteMailMergeDataSouce();
            RadFlowDocument mergedDocument = MergeTemplateWithData(mailMergeSource);
            SaveFile(mergedDocument);
        }

        private static void InsertTextOnHeaderRow(RadFlowDocumentEditor editor, TableCell cell, string text)
        {
            InsertParagraphAndPositionEditor(editor, cell);

            editor.InsertText(text);
        }

        private static void InsertTextOnTableRow(RadFlowDocumentEditor editor, TableCell cell, string[] fieldNames)
        {
            InsertParagraphAndPositionEditor(editor, cell);

            for (int i = 0; i < fieldNames.Length; i++)
            {
                InsertMergeField(editor, fieldNames[i]);
            }
        }

        private static void InsertParagraphAndPositionEditor(RadFlowDocumentEditor editor, TableCell cell)
        {
            Paragraph paragraph = cell.Blocks.AddParagraph();
            editor.MoveToParagraphStart(paragraph);
        }

        private static void InsertMergeField(RadFlowDocumentEditor editor, string mergeField)
        {
            editor.InsertField(string.Format("MERGEFIELD {0}", mergeField), string.Format("«{0}»", mergeField));
        }

        private static void UpdateTableProperties(Table table)
        {
            table.PreferredWidth = new TableWidthUnit(TableWidthUnitType.Percent, 100);
            Border border = new Border(1, BorderStyle.Single, new ThemableColor(Colors.Black));
            table.Borders = new TableBorders(border);
        }

        private static RadFlowDocument MergeTemplateWithData(IEnumerable mailMergeSource)
        {
            RadFlowDocument template = CreateMailMergeDocumentTemplate();
            RadFlowDocument mergedDocument = template.MailMerge(mailMergeSource);

            return mergedDocument;
        }

        private static void SaveFile(RadFlowDocument document)
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

        private static RadFlowDocument CreateMailMergeDocumentTemplate()
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
            editor.InsertText("I would like to thank you for the purchase of:");

            Table table = editor.InsertTable(2, 2);

            TableRow row = table.Rows[0];
            InsertTextOnHeaderRow(editor, row.Cells[0], "Product");
            InsertTextOnHeaderRow(editor, row.Cells[1], "Quantity");

            row = table.Rows[1];
            string[] fieldNames = new string[] { "GroupStart:Products", "ProductName" };
            InsertTextOnTableRow(editor, row.Cells[0], fieldNames);
            fieldNames = new string[] { "PurchasedItemsCount", "GroupEnd:Products" };
            InsertTextOnTableRow(editor, row.Cells[1], fieldNames);

            UpdateTableProperties(table);

            editor.MoveToTableEnd(table);

            editor.InsertParagraph();
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

        private static IEnumerable GetDynamicMailMergeDataSource()
        {
            List<DynamicDataObject> mailMergeSource = new List<DynamicDataObject>();

            List<DynamicDataObject> products = new List<DynamicDataObject>();

            DynamicDataObject product = new DynamicDataObject();
            product.Set("ProductName", "MobilePhonePro 128GB");
            product.Set("PurchasedItemsCount", 100);
            products.Add(product);

            product = new DynamicDataObject();
            product.Set("ProductName", "MobilePhonePro 64GB");
            product.Set("PurchasedItemsCount", 150);
            products.Add(product);

            product = new DynamicDataObject();
            product.Set("ProductName", "MobilePhoneLite 16GB");
            product.Set("PurchasedItemsCount", 250);
            products.Add(product);

            DynamicDataObject data = new DynamicDataObject();
            data.Set("FirstName", "Andrew");
            data.Set("LastName", "Fuller");
            data.Set("CompanyName", "Mobile Phone Factory");
            data.Set("Products", products);
            data.Set("ProductSupportPhone", "skype: MobilePhoneFactorySupport");
            data.Set("ProductSupportPhoneAvailability", "24/7");
            data.Set("ProductSupportEmail", "support@mobilephonefactory.org");
            data.Set("SalesRepFirstName", "Nancy");
            data.Set("SalesRepLastName", "Davolio");
            data.Set("SalesRepTitle", "Sales Associate");

            mailMergeSource.Add(data);

            products = new List<DynamicDataObject>();

            product = new DynamicDataObject();
            product.Set("ProductName", "MobilePhonePro 64GB");
            product.Set("PurchasedItemsCount", 200);
            products.Add(product);

            product = new DynamicDataObject();
            product.Set("ProductName", "MobilePhoneLite 32GB");
            product.Set("PurchasedItemsCount", 450);
            products.Add(product);

            product = new DynamicDataObject();
            product.Set("ProductName", "MobilePhoneLite 16GB");
            product.Set("PurchasedItemsCount", 550);
            products.Add(product);

            data = new DynamicDataObject();
            data.Set("FirstName", "Margaret");
            data.Set("LastName", "Peacock");
            data.Set("CompanyName", "Mobile Phone Factory Japan");
            data.Set("Products", products);
            data.Set("ProductSupportPhone", "skype: MobilePhoneFactorySupportJapan");
            data.Set("ProductSupportPhoneAvailability", "24/7");
            data.Set("ProductSupportEmail", "support@mobilephonefactory.org");
            data.Set("SalesRepFirstName", "Asako");
            data.Set("SalesRepLastName", "Hoshi");
            data.Set("SalesRepTitle", "Sales Associate");

            mailMergeSource.Add(data);

            return mailMergeSource;
        }

        private static IEnumerable GetConcreteMailMergeDataSouce()
        {
            List<ConcreteDataObject> collection = new List<ConcreteDataObject>();

            List<Product> products = new List<Product>()
            {
                new Product("MobilePhonePro 128GB", 100),
                new Product("MobilePhonePro 64GB", 150),
                new Product("MobilePhoneLite 16GB", 250)
            };

            ConcreteDataObject data = new ConcreteDataObject()
            {
                FirstName = "Andrew",
                LastName = "Fuller",
                CompanyName = "Mobile Phone Factory",
                Products = products,
                ProductSupportPhone = "skype: MobilePhoneFactorySupport",
                ProductSupportPhoneAvailability = "24/7",
                ProductSupportEmail = "support@mobilephonefactory.org",
                SalesRepFirstName = "Nancy",
                SalesRepLastName = "Davolio",
                SalesRepTitle = "Sales Associate"
            };

            collection.Add(data);

            products = new List<Product>()
            {
                new Product("MobilePhonePro 64GB", 200),
                new Product("MobilePhoneLite 32GB", 450),
                new Product("MobilePhoneLite 16GB", 550)
            };

            data = new ConcreteDataObject()
            {
                FirstName = "Margaret",
                LastName = "Peacock",
                CompanyName = "Mobile Phone Factory Japan",
                Products = products,
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
