using System;
using System.Linq;

namespace GroupingAndFilteringWithTreeView
{
    public class Process
    {
        private string name;

        public bool IsExpanded { get; set; }

        public Process(string speakerName)
        {
            this.name = speakerName;
            this.IsExpanded = false;
        }

        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (this.name != value)
                {
                    this.name = value;
                }
            }
        }
    }
}