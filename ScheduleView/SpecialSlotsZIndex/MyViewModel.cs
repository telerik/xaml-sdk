using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Controls.ScheduleView;
using System.Collections.ObjectModel;
using Telerik.Windows.Controls;
using System.Linq;

namespace SpecialSlotsZIndex
{
    // This is the singleton view-model
    public class MyViewModel : ViewModelBase
    {
        private ICommand visibleRangeChanged;

        private ObservableCollection<ResourceType> resourceType;
        private ObservableCollection<Slot> specialSlots;

        private ObservableCollection<Appointment> appointments;

        private int zValue = 1;

        public int ZValue
        {
            get
            {
                return zValue;
            }
            set
            {
                if (this.zValue != value)
                {
                    zValue = value;
                    var nonWorkingSlots = this.SpecialSlots.Where(s => s is NonWorkingSlot);
                    foreach (NonWorkingSlot item in nonWorkingSlots)
                    {
                        item.ZValueIndex = zValue;
                    }
                }
                this.OnPropertyChanged(() => this.ZValue);
            }
        }

        private int zValueReadOnly = 50;

        public int ZValueReadOnly
        {
            get
            {
                return zValueReadOnly;
            }
            set
            {
                if (this.zValueReadOnly != value)
                {
                    zValueReadOnly = value;
                }

                this.OnPropertyChanged(() => this.ZValueReadOnly);
            }
        }


        private ObservableCollection<Resource> resources;
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


        public ObservableCollection<ResourceType> ResourceType
        {
            get
            {
                return this.resourceType;
            }
            set
            {
                this.resourceType = value;
            }
        }
        public MyViewModel()
        {
            resources = new ObservableCollection<Resource>();
            resourceType = new ObservableCollection<ResourceType>();
            this.appointments = new ObservableCollection<Appointment>();
            this.specialSlots = new ObservableCollection<Slot>();


            DateTime start = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 14, 0, 0);
            ResourceType type = new ResourceType() { Name = "Level" };
            this.resourceType.Add(type);
            for (int i = 1; i <= 5; i++)
            {
                resources.Add(new Resource() { ResourceType = "Level", ResourceName = string.Format("Level {0}", i) });
            }
            type.Resources.AddRange(resources);


            this.specialSlots.Add(this.CreateReadOnlySlot(resources));

            this.specialSlots.Add(this.CreateNonWorkingSlot(resources, start, start.AddHours(3), RecurrenceDays.EveryDay));
        }

        public ICommand VisibleRangeChanged
        {
            get
            {
                return this.visibleRangeChanged;
            }
            set
            {
                this.visibleRangeChanged = value;
            }
        }

        public ObservableCollection<Slot> SpecialSlots
        {
            get
            {
                return this.specialSlots;
            }
            set
            {
                this.specialSlots = value;
            }
        }

        private Slot CreateNonWorkingSlot(ObservableCollection<Resource> resources, DateTime start, DateTime end, RecurrenceDays days)
        {
            Slot slot = new NonWorkingSlot(start, end);
            slot.RecurrencePattern = new RecurrencePattern(null, days, RecurrenceFrequency.Weekly, 1, null, null);
            slot.Resources.AddRange(resources);
            return slot;
        }

        private Slot CreateReadOnlySlot(ObservableCollection<Resource> resources)
        {
            Slot slot = new Slot() { Start = DateTime.Today, End = DateTime.Today.AddDays(2), IsReadOnly = true };
            slot.Resources.Add(resources[1]);
            slot.Resources.Add(resources[3]);

            return slot;
        }
    }
}
