using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Xml.Linq;
using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Controls.Scheduling;
using Telerik.Windows.Core;

namespace ImportExportFromAndToMSProject
{
    public class ViewModel : PropertyChangedBase
    {
        private ObservableCollection<GanttTask> tasks;
        private VisibleRange visibleRange;

        public ViewModel()
        {
            this.tasks = new ObservableCollection<GanttTask>();

            var stream = Application.GetResourceStream(new Uri(@"/ImportExportFromAndToMSProject;component/XMLFilesToLoad/MarketSchedule.xml", UriKind.RelativeOrAbsolute)).Stream;
            this.ImportFromFile(stream);
        }

        public ObservableCollection<GanttTask> Tasks
        {
            get
            {
                return this.tasks;
            }
            set
            {
                if (this.tasks != value)
                {
                    this.tasks = value;
                    this.OnPropertyChanged(() => Tasks);
                }
            }
        }

        public VisibleRange VisibleRange
        {
            get
            {
                return this.visibleRange;
            }
            set
            {
                if (this.visibleRange != value)
                {
                    this.visibleRange = value;
                    this.OnPropertyChanged(() => VisibleRange);
                }
            }
        }

        internal void ImportFromFile(Stream stream)
        {
            XDocument xDocument = XDocument.Load(stream);
            XNamespace xnamespace = xDocument.Root.Name.Namespace;
            this.Tasks = MsProjectImportHelper.GetMsTasks(xDocument, xnamespace);
            this.SetUpVisibleRange(xDocument, xnamespace);
        }

        private void SetUpVisibleRange(XDocument xDocument, XNamespace xnamespace)
        {
            DateTime dateTimeStart = DateTime.Parse(xDocument.Root.Element(xnamespace + "StartDate").Value);
            DateTime dateTimeEnd = DateTime.Parse(xDocument.Root.Element(xnamespace + "FinishDate").Value);
            this.VisibleRange = new Telerik.Windows.Controls.Scheduling.VisibleRange() { Start = dateTimeStart.Date.AddDays(1), End = dateTimeEnd.Date.AddDays(1) };
        }
    }
}
