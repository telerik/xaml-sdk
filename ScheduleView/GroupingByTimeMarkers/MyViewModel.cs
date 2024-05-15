using System;
using System.Collections.ObjectModel;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ScheduleView;

namespace GroupingByTimeMarkers
{
    public class MyViewModel : ViewModelBase
    {
        private ObservableCollection<Appointment> appointments;
        private ObservableCollection<ResourceType> resourceTypes;
        private GroupDescriptionCollection groupDescriptions;

        public MyViewModel()
        {
            this.resourceTypes = this.GenerateResourceTypes();

            this.appointments = new ObservableCollection<Appointment>();
        }

        public GroupDescriptionCollection GroupDescriptions
        {
            get
            {
                if (this.groupDescriptions == null)
                {
                    this.groupDescriptions = new GroupDescriptionCollection() { new DateGroupDescription() };

                    ResourceGroupDescription speakerRes = new ResourceGroupDescription() { ResourceType = "Speaker" };
                    this.groupDescriptions.Add(speakerRes);
                }

                return this.groupDescriptions;
            }
        }

        public ObservableCollection<ResourceType> ResourcesTypes
        {
            get
            {
                return this.resourceTypes;
            }
            set
            {
                this.resourceTypes = value;
            }
        }

        public ObservableCollection<Appointment> Appointments
        {
            get
            {
                return this.appointments;
            }
            set
            {
                this.appointments = value;
            }
        }

        private ObservableCollection<ResourceType> GenerateResourceTypes()
        {
            ObservableCollection<ResourceType> result = new ObservableCollection<ResourceType>();

            ResourceType speakerType = new ResourceType("Speaker");
            Resource tomSpeaker = new Resource("Tom");
            Resource peterSpeaker = new Resource("Peter");
            speakerType.Resources.Add(tomSpeaker);
            speakerType.Resources.Add(peterSpeaker);

            result.Add(speakerType);
            return result;
        }

        public ObservableCollection<Appointment> GenerateAppointments()
        {
            ObservableCollection<Appointment> apps = new ObservableCollection<Appointment>();
            var today = DateTime.Today;
            int state = 0;

            for (int i = 0; i < 20; i++)
            {
                var newApp = new Appointment();

                newApp.Subject = "App " + i;
                newApp.Start = today.AddHours(i);
                newApp.End = today.AddHours(i + 2);

                switch (state)
                {
                    case 0:
                        newApp.Resources.Add(this.resourceTypes[1].Resources[state]);
                        newApp.Resources.Add(this.resourceTypes[0].Resources[1]);
                        state = 1;
                        break;
                    case 1:
                        newApp.Resources.Add(this.resourceTypes[1].Resources[state]);
                        newApp.Resources.Add(this.resourceTypes[0].Resources[1]);
                        state = 2;
                        break;
                    case 2:
                        newApp.Resources.Add(this.resourceTypes[1].Resources[state]);
                        newApp.Resources.Add(this.resourceTypes[0].Resources[0]);
                        state = 3;
                        break;
                    case 3:
                        newApp.Resources.Add(this.resourceTypes[1].Resources[state]);
                        newApp.Resources.Add(this.resourceTypes[0].Resources[0]);
                        state = 0;
                        break;
                }

                apps.Add(newApp);
            }

            return apps;
        }
    }
}
