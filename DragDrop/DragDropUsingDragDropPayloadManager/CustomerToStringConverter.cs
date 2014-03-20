using Telerik.Windows.DragDrop.Behaviors;

namespace DragDropUsingDragDropPayloadManager
{
    public class CustomerToStringConverter : DataConverter
    {
        public override object ConvertTo(object data, string format)
        {
            if (format == typeof(string).FullName && DataObjectHelper.GetDataPresent(data, "DragData", false))
            {
                var customer = DataObjectHelper.GetData(data, "DragData", false) as Customer;
                var fullInfoString = "Name: " + customer.FirstName + " " + customer.LastName + ", Age: " + customer.Age;
                return fullInfoString;
            }

            return null;
        }

        public override string[] GetConvertToFormats()
        {
            return new string[] { typeof(string).FullName };
        }
    }
}
