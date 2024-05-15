using System.Collections.Generic;
using System.Linq;
using Telerik.Windows.DragDrop.Behaviors;

namespace DragDropDataConversion
{
    public class CustomerToProjectConverter : DataConverter
    {
        public override object ConvertTo(object data, string format)
        {
            var result = new List<Project>();
            var draggedCustomers = ((List<object>)DataObjectHelper.GetData(data, typeof(Customer), false)).OfType<Customer>();

            foreach (var customer in draggedCustomers)
            {
                result.Add(new Project()
                {
                    Name = customer.Project,
                    Id = customer.Id,
                    Person = customer.Name
                });
            }

            return result;
        }

        public override string[] GetConvertToFormats()
        {
            return new string[] { typeof(Project).Name };
        }
    }
}
