using System;
using System.IO;
using Telerik.Windows.Controls;
using Telerik.Windows.Persistence;
using Telerik.Windows.Zip;
using System.Windows;

namespace ZipIntegration
{
    public class ZipViewModel : ViewModelBase
    {
        private long uncompressedSize;
        private long compressedSize;
        private Stream rawStream;
        private Stream compressedStream;
		private FontStyle fontStyle;
		private FontWeight fontWeight;
		private string contactInformation;
		private DelegateCommand fontWeightCommand;
		private DelegateCommand fontStyleCommand;
		private DelegateCommand textAlignmentCommand;
		private TextAlignment textAlignment = TextAlignment.Left;

        public long UncompressedSize
        {
            get { return uncompressedSize; }
            set
            {
                uncompressedSize = value;
                this.OnPropertyChanged("UncompressedSize");
            }
        }

        public long CompressedSize
        {
            get { return compressedSize; }
            set
            {
                compressedSize = value;
                this.OnPropertyChanged("CompressedSize");
            }
        }

		public string ContactInformation
		{
			get { return contactInformation; }
			set
			{
				contactInformation = value;
				this.OnPropertyChanged("ContactInformation");
			}
		}

		public FontStyle FontStyle
		{
			get { return fontStyle; }
			set
			{
				fontStyle = value;
				this.OnPropertyChanged("FontStyle");
			}
		}

		public FontWeight FontWeight
		{
			get { return fontWeight; }
			set
			{
				fontWeight = value;
				this.OnPropertyChanged("FontWeight");
			}
		}

		public TextAlignment TextAlignment
		{
			get { return textAlignment; }
			set
			{
				textAlignment = value;
				this.OnPropertyChanged("TextAlignment");
			}
		}

		public DelegateCommand FontStyleCommand
		{
			get { return fontStyleCommand; }
			set
			{
				fontStyleCommand = value;
				this.OnPropertyChanged("FontStyleCommand");
			}
		}

		public DelegateCommand FontWeightCommand
		{
			get { return fontWeightCommand; }
			set
			{
				fontWeightCommand = value;
				this.OnPropertyChanged("FontWeightCommand");
			}
		}

		public DelegateCommand TextAlignmentCommand
		{
			get { return textAlignmentCommand; }
			set
			{
				textAlignmentCommand = value;
				this.OnPropertyChanged("TextAlignmentCommand");
			}
		}
		public ZipViewModel()
		{
			this.ContactInformation = "MARIA LARSSON" + Environment.NewLine + "SWEDEN" + Environment.NewLine + "Company Name: Folk och fä HB" + Environment.NewLine + "Bräcke, S-844 67 Åkergatan 24" + Environment.NewLine + "Tel.: 0695-34 67 21";
			this.FontWeightCommand = new DelegateCommand(new Action<object>(ChangeFontWeight));
			this.FontStyleCommand = new DelegateCommand(new Action<object>(ChangeFontStyle));
			this.TextAlignmentCommand = new DelegateCommand(new Action<object>(ChangeTextAlignment));
		}

		public void ChangeFontWeight(object parameter)
		{
			if(parameter == null)
				return;
			bool param = bool.Parse(parameter.ToString());
			if (param)
				this.FontWeight = FontWeights.Bold;
			else
				this.FontWeight = FontWeights.Normal;
		}

		public void ChangeFontStyle(object parameter)
		{
			if (parameter == null)
				return;
			bool param = bool.Parse(parameter.ToString());
			if (param)
				this.FontStyle = FontStyles.Italic;
			else
				this.FontStyle = FontStyles.Normal;
		}

		public void ChangeTextAlignment(object parameter)
		{
			if (parameter == null)
				return;
			TextAlignment textAlignment = (TextAlignment)Enum.Parse(typeof(TextAlignment), parameter.ToString(), true);
			this.TextAlignment = textAlignment;
		}

        public void Save(object parameter)
        {
            PersistenceManager manager = new PersistenceManager();
            this.compressedStream = new MemoryStream();
            ZipPackage zipPackage = ZipPackage.Create(this.compressedStream);
            this.rawStream = manager.Save(parameter);
            this.rawStream.Position = 0L;
            zipPackage.AddStream(ZipCompression.Default, "persistence", this.rawStream, DateTime.Now.Date);
            zipPackage.Close(false);
            this.UncompressedSize = this.rawStream.Length;
            this.CompressedSize = this.compressedStream.Length;
        }

        public void Load(object parameter)
        {
            ZipPackage zip = ZipPackage.Open(this.compressedStream, FileAccess.Read);
            Stream uncompressedStream = zip.ZipPackageEntries[0].OpenInputStream();
            PersistenceManager manager = new PersistenceManager();
            manager.Load(parameter, uncompressedStream);
        }

        public bool CanLoad(object parameter)
        {
            return parameter != null && this.compressedStream != null && this.compressedStream.Length > 0;
        }
    }
}
