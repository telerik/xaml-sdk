using System;
using System.Collections;
using System.Collections.ObjectModel;

namespace DragDropWithLines
{
    public class MessageViewModel
    {
        public static IList Generate()
        {
            IList data = new ObservableCollection<MessageViewModel>();
            data.Add(new MessageViewModel("tom@hanna-barbera.com", "Cats are cool", 100));
            data.Add(new MessageViewModel("jerry@hanna-barbera.com", "Mice are cool", 100));
            data.Add(new MessageViewModel("spike@hanna-barbera.com", "Dogs are cool", 100));
            data.Add(new MessageViewModel("jerry2@hanna-barbera.com", "2Mice are cool", 200));
            data.Add(new MessageViewModel("spike2@hanna-barbera.com", "2Dogs are cool", 200));
            data.Add(new MessageViewModel("jerry3@hanna-barbera.com", "3Mice are cool", 300));
            data.Add(new MessageViewModel("spike3@hanna-barbera.com", "3Dogs are cool", 300));
            data.Add(new MessageViewModel("spike3@hanna-barbera.com", "3Dogs are cool", 300));
            data.Add(new MessageViewModel("spike3@hanna-barbera.com", "3Dogs are cool", 300));
            return data;
        }
        public MessageViewModel(string sender, string subject, int size)
        {
            this.Sender = sender;
            this.Subject = subject;
            this.Size = size;
        }
        public string Subject
        {
            get;
            set;
        }
        public string Sender
        {
            get;
            set;
        }
        public int Size
        {
            get;
            set;
        }
        public override string ToString()
        {
            return this.Sender;
        }
    }
}
