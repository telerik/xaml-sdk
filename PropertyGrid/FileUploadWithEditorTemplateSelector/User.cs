using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUploadWithEditorTemplateSelector
{
    public class User : INotifyPropertyChanged
    {
        private string avatarPath;
        private string username;
        private int age;

        public string AvatarPath
        {
            get { return this.avatarPath; }
            set
            {
                if (value != this.avatarPath)
                {
                    this.avatarPath = value;
                    this.OnPropertyChanged("AvatarPath");
                }
            }
        }

        public string Username
        {
            get { return this.username; }
            set
            {
                if (value != this.username)
                {
                    this.username = value;
                    this.OnPropertyChanged("Username");
                }
            }
        }

        public int Age
        {
            get { return this.age; }
            set
            {
                if (value != this.age)
                {
                    this.age = value;
                    this.OnPropertyChanged("Age");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, args);
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
    }
}
