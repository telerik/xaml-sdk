using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Docking;

namespace VisualStudioDocking
{
    [DataContract]
    public class PaneViewModel : INotifyPropertyChanged
    {
        private string header;
        private DockState initialPosition;
        private bool isActive;
        private bool isHidden;
        private bool isDocument;

        public PaneViewModel()
        {
        }

        public PaneViewModel(Type contentType)
        {
            this.ContentType = contentType;
        }

        [DataMember]
        public string Header
        {
            get
            {
                return this.header;
            }
            set
            {
                if (this.header != value)
                {
                    this.header = value;
                    this.OnPropertyChanged("Header");
                }
            }
        }

        [DataMember]
        public DockState InitialPosition
        {
            get
            {
                return this.initialPosition;
            }
            set
            {
                if (this.initialPosition != value)
                {
                    this.initialPosition = value;
                    this.OnPropertyChanged("InitialPosition");
                }
            }
        }

        [DataMember]
        public bool IsActive
        {
            get
            {
                return this.isActive;
            }
            set
            {
                if (this.isActive != value)
                {
                    this.isActive = value;
                    this.OnPropertyChanged("IsActive");
                }
            }
        }

        [DataMember]
        public bool IsHidden
        {
            get
            {
                return this.isHidden;
            }
            set
            {
                if (this.isHidden != value)
                {
                    this.isHidden = value;
                    this.OnPropertyChanged("IsHidden");
                }
            }
        }

        [DataMember]
        public bool IsDocument
        {
            get
            {
                return this.isDocument;
            }
            set
            {
                if (this.isDocument != value)
                {
                    this.isDocument = value;
                    this.OnPropertyChanged("IsDocument");
                }
            }
        }

        public Type ContentType
        {
            get;
            private set;
        }

        [DataMember]
        public string TypeName
        {
            get
            {
                return this.ContentType == null ? string.Empty : this.ContentType.AssemblyQualifiedName;
            }
            set
            {
                this.ContentType = value == null ? null : Type.GetType(value);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}