using System.Windows.Media;
using Telerik.Windows.Documents.Model;

namespace CreateCustomMergeField
{
    public class OrdersMergeField : MergeField
    {
        private const string CustomFieldName = "OrdersField";

        static OrdersMergeField()
        {
            CodeBasedFieldFactory.RegisterFieldType(OrdersMergeField.CustomFieldName, () => { return new OrdersMergeField(); });
        }

        public override string FieldTypeName
        {
            get
            {
                return OrdersMergeField.CustomFieldName;
            }
        }

        public override Field CreateInstance()
        {
            return new OrdersMergeField();
        }

        protected override DocumentFragment GetResultFragment()
        {
            Customer customer = this.Document.MailMergeDataSource.CurrentItem as Customer;
            if (customer == null)
            {
                return null;
            }

            if (this.PropertyPath == "Orders")
            {
                Table table = new Table();
                var grayBorder1 = new Border(1, BorderStyle.Single, Colors.Gray);

                foreach (Order order in customer.Orders)
                {
                    Span span = new Span(order.ProductName);
                    Paragraph paragraph = new Paragraph();
                    paragraph.Inlines.Add(span);

                    TableCell cell = new TableCell();
                    cell.Borders = new TableCellBorders(grayBorder1);
                    cell.Blocks.Add(paragraph);

                    TableRow row = new TableRow();
                    row.Cells.Add(cell);

                    table.AddRow(row);
                }

                Section section = new Section();
                section.Blocks.Add(table);

                RadDocument document = new RadDocument();
                document.Sections.Add(section);

                document.MeasureAndArrangeInDefaultSize();
                return new DocumentFragment(document);
            }

            return null;
        }
    }

}
