using System.Collections.Generic;
using System.Linq;
using Telerik.Windows.DragDrop.Behaviors;

namespace DragDropDataConversion
{
    public class ProjectToCustomerConverter : DataConverter
    {
        public override object ConvertTo(object data, string format)
        {
            var result = new List<Customer>();
            var draggedProjects = ((List<object>)DataObjectHelper.GetData(data, typeof(Project), false)).OfType<Project>();

            foreach (var project in draggedProjects)
            {
                result.Add(new Customer()
                {
                    Name = project.Person,
                    Id = project.Id,
                    Project = project.Name
                });
            }

            return result;
        }

        public override string[] GetConvertToFormats()
        {
            return new string[] { typeof(Customer).Name };
        }
    }
}
