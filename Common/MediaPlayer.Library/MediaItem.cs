using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;

namespace MediaPlayer.Library
{
    public class MediaItem
    {
        private static string[] videoExtensions = { ".AVI", ".MP4", ".DIVX", ".WMV", ".MKV" };

        private Uri source;
        private string mediaName;

        public Uri Source
        {
            get { return source; }
            set
            {
                if (this.source != value)
                {
                    this.source = value;
                    this.OnPropertyChanged("Source");
                }
            }
        }       

        public string MediaName
        {
            get { return mediaName; }
            set
            {
                if (this.mediaName != value)
                {
                    this.mediaName = value;
                    this.OnPropertyChanged("MediaName");
                }
            }
        }
                
        public bool IsVideo
        {
            get
            {                
                return IsVideoFile(this.source.AbsoluteUri);
            }
        }

        private static bool IsVideoFile(string path)
        {
            return -1 != Array.IndexOf(videoExtensions, Path.GetExtension(path).ToUpperInvariant());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
