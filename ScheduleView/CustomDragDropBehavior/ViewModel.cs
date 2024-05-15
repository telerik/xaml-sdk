using System;
using System.Collections.ObjectModel;
using System.Linq;
using Telerik.Windows.Controls;

namespace CustomDragDropBehavior
{
    public class ViewModel : ViewModelBase
    {
        public ResourceTypeCollection ResourceTypeSource { get; set; }
        public ObservableCollection<CustomAppointment> Appointments { get; set; }
        public ObservableCollection<Meeting> Meetings { get; set; }
        public GroupDescriptionCollection GroupDescriptionsSource { get; set; }

        public string Readme { get; set; }
        public ViewModel()
        {
            this.ResourceTypeSource = GenerateResourceTypeCollection();
            this.GroupDescriptionsSource = new GroupDescriptionCollection
            {
                new DateGroupDescription(),
                new ResourceGroupDescription{ ResourceType="Location"}
            };
            this.Appointments = GenerateAppointments();
            this.Meetings = new ObservableCollection<Meeting> 
            {
                new Meeting { ID = 1, Name = "Meeting 1" },
                new Meeting { ID = 2, Name = "Meeting 2" },
                new Meeting { ID = 3, Name = "Meeting 3" },
                new Meeting { ID = 4, Name = "Meeting 4" },
                new Meeting { ID = 5, Name = "Meeting 5" } 
            };

            this.Readme = GetText();
        }

        private string GetText()
        {
            return @"This example shows how to implement and use a custom ScheduleViewDragDropBehavior. The functionality of example is explained below and demonstrates the use of all the methods available for overriding:
                    -	Drag and drop between ScheduleView and ListBox
                    -	Custom Appointment with IsReadOnly property which cannot be moved or resized and has different Background color (Red in the example)
                    -	The Appointments cannot be resized to a duration below 30 minutes and above 2 hours
                    -	All of the non ReadOnly Appointments in with same Resource should move together when single Appointment is dragged
                    -	Appointments cannot be moved from one Resource to another
                    -	Dragging Appointment with Control key pressed doesn't copy the Appointment";
        }

        private ResourceTypeCollection GenerateResourceTypeCollection()
        {
            var resourceType = new ResourceType("Location");

            var room1 = new Resource("Room 1");
            var room2 = new Resource("Room 2");
            var room3 = new Resource("Room 3");

            resourceType.Resources.Add(room1);
            resourceType.Resources.Add(room2);
            resourceType.Resources.Add(room3);

            var resourceTypeCollection = new ResourceTypeCollection { resourceType };
            return resourceTypeCollection;
        }

        private ObservableCollection<CustomAppointment> GenerateAppointments()
        {
            var app1 = new CustomAppointment { Start = DateTime.Today.AddHours(8), End = DateTime.Today.AddHours(9), Subject = "This Appointment cannot be dragged and resized as it is ReadOnly.", IsReadOnly = true };
            app1.Resources.Add(this.ResourceTypeSource.First().Resources[1] as Resource);
            return new ObservableCollection<CustomAppointment> { app1 };
        }
    }
}
