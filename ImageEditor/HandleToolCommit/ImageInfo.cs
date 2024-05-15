using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace HandleToolCommit
{
    public class ImageInfo : INotifyPropertyChanged
    {
        private double widthBefore;
        private double heightBefore;
        private double widthCurrent;
        private double heightCurrent;

        private string executedTool;

        public double WidthBefore
        {
            get
            {
                return this.widthBefore;
            }
            set
            {
                if (this.widthBefore != value)
                {
                    this.widthBefore = value;
                    this.OnPropertyChanged("WidthBefore");
                }
            }
        }

        public double HeightBefore
        {
            get
            {
                return this.heightBefore;
            }
            set
            {
                if (this.heightBefore != value)
                {
                    this.heightBefore = value;
                    this.OnPropertyChanged("HeightBefore");
                }
            }
        }

        public double WidthCurrent
        {
            get
            {
                return this.widthCurrent;
            }
            set
            {
                if (this.widthCurrent != value)
                {
                    this.widthCurrent = value;
                    this.OnPropertyChanged("WidthCurrent");
                }
            }
        }

        public double HeightCurrent
        {
            get
            {
                return this.heightCurrent;
            }
            set
            {
                if (this.heightCurrent != value)
                {
                    this.heightCurrent = value;
                    this.OnPropertyChanged("HeightCurrent");
                }
            }
        }

        public string ExecutedTool
        {
            get
            {
                return this.executedTool;
            }
            set
            {
                if (this.executedTool != value)
                {
                    this.executedTool = value;
                    this.OnPropertyChanged("ExecutedTool");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}