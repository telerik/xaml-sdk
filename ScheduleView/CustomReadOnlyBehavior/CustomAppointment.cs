using Telerik.Windows.Controls.ScheduleView;

namespace CustomReadOnlyBehavior
{
    public class CustomAppointment : Appointment
    {
        private bool isDeletable = true;
        private bool isDraggable = true;
        private bool isResizable = true;
        private bool isEditable = true;
        
        public bool IsDeletable
        {
            get { return this.Storage<CustomAppointment>().isDeletable; }
            set
            {
                var storage = this.Storage<CustomAppointment>();
                if(storage.isDeletable != value)
                {
                    storage.isDeletable = value;
                    this.OnPropertyChanged(() => this.IsDeletable);
                }
            }
        }

        public bool IsDraggable
        {
            get { return this.Storage<CustomAppointment>().isDraggable; }
            set
            {
                var storage = this.Storage<CustomAppointment>();
                if(storage.isDraggable != value)
                {
                    storage.isDraggable = value;
                    this.OnPropertyChanged(() => this.IsDraggable);
                }
            }
        }

        public bool IsResizable
        {
            get { return this.Storage<CustomAppointment>().isResizable; }
            set
            {
                var storage = this.Storage<CustomAppointment>();
                if(storage.isResizable != value)
                {
                    storage.isResizable = value;
                    this.OnPropertyChanged(() => this.IsResizable);
                }
            }
        }

        public bool IsEditable
        {
            get { return this.Storage<CustomAppointment>().isEditable; }
            set
            {
                var storage = this.Storage<CustomAppointment>();
                if (storage.isEditable != value)
                {
                    storage.isEditable = value;
                    this.OnPropertyChanged(() => this.IsEditable);
                } 
            }

        }

        public override IAppointment Copy()
        {
            var newAppointment = new CustomAppointment();
            newAppointment.CopyFrom(this);
            return newAppointment;
        }

        public override void CopyFrom(IAppointment other)
        {
            var task = other as CustomAppointment;

            if (task != null)
            {
                this.IsDeletable = task.IsDeletable;
                this.IsEditable = task.IsEditable;
                this.IsDraggable = task.IsDraggable;
                this.IsResizable = task.IsResizable;
            }

            base.CopyFrom(other);
        }
    }
}
