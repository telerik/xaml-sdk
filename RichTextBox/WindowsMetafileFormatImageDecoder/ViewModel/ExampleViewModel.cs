using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using Telerik.Windows.Controls;

namespace WindowsMetafileFormatDecoder.ViewModel
{
    public class ExampleViewModel : ViewModelBase
    {
        private static readonly string documentFolderPath = "SampleData/";

        private readonly ICommand loadCommand;
        private byte[] documentData;

        public ExampleViewModel()
        {
            this.loadCommand = new DelegateCommand(this.LoadCommandExecuted);
        }

        public ICommand LoadCommand
        {
            get
            {
                return this.loadCommand;
            }
        }

        public byte[] DocumentData
        {
            get
            {
                return this.documentData;
            }
            set
            {
                if (this.documentData != value)
                {
                    this.documentData = value;
                    this.OnPropertyChanged("DocumentData");
                }
            }
        }

        private void LoadCommandExecuted(object parameter)
        {
            string documentName = parameter as string;

            if (string.IsNullOrEmpty(documentName))
            {
                return;
            }

            Uri resourceUri = GetResourceUri(string.Concat(documentFolderPath, documentName));
            using (Stream stream = Application.GetResourceStream(resourceUri).Stream)
            {
                this.DocumentData = GetBytes(stream);
            }
        }

        private static byte[] GetBytes(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);

            return bytes;
        }

        private static Uri GetResourceUri(string resource)
        {
            AssemblyName assemblyName = new AssemblyName(typeof(ExampleViewModel).Assembly.FullName);
            string resourcePath = "/" + assemblyName.Name + ";component/" + resource;
            Uri resourceUri = new Uri(resourcePath, UriKind.RelativeOrAbsolute);

            return resourceUri;
        }
    }
}
