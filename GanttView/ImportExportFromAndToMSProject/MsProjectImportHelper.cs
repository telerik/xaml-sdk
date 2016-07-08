using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using Telerik.Windows.Controls.GanttView;

namespace ImportExportFromAndToMSProject
{
    public class MsProjectImportHelper
    {
        private readonly static Dictionary<string, IList<string>> taskToChildren = new Dictionary<string, IList<string>>();

        private readonly static Dictionary<string, IList<string>> taskToRelation = new Dictionary<string, IList<string>>();

        private readonly static Dictionary<string, Tuple<GanttTask, string>> taskIdToGanttTask = new Dictionary<string, Tuple<GanttTask, string>>();

        public static ObservableCollection<GanttTask> GetMsTasks(XDocument xDocument, XNamespace xnamespace)
        {
            var allTasks = new ObservableCollection<GanttTask>();
            var task = xDocument.Root.Elements(xnamespace + "Tasks");
            var tasks = task.Elements(xnamespace + "Task");
            tasks.FirstOrDefault().Remove();
            PrepareXMLTasksForConvertingToGanttTasks(xnamespace, tasks);
            GenerateAllGanttTasks(tasks, xnamespace);

            foreach (var t in taskIdToGanttTask)
            {
                var gtask = t.Value.Item1;

                SetUpRelationsToGanttTask(t.Key, t.Value, gtask);

                var oNumber = t.Value.Item2;
                if (oNumber != null)
                {
                    SetUpChildrenToGanttTask(t.Value, gtask);

                }
                if (IsRootTask(oNumber))
                {
                    allTasks.Add(gtask);
                }
            }
            return allTasks;
        }

        private static bool IsRootTask(string oNumber)
        {
            return oNumber == null || oNumber.Split('.').Length == 1;
        }

        private static void PrepareXMLTasksForConvertingToGanttTasks(XNamespace xnamespace, IEnumerable<XElement> tasks)
        {
            taskToChildren.Clear();
            taskToRelation.Clear();
            taskIdToGanttTask.Clear();
            ConnectEveryTaskIdWithItsSubTasks(tasks, xnamespace);
            ConnectEveryTaskIdWithItsRelations(tasks, xnamespace);
        }

        private static void SetUpChildrenToGanttTask(Tuple<GanttTask, string> t, GanttTask gtask)
        {
            var oNumber = t.Item2;
            var childCollection = taskToChildren[oNumber];
            foreach (var child in childCollection.ToList())
            {
                var childrenTask = taskIdToGanttTask[child].Item1;
                gtask.Children.Add(childrenTask);
            };
        }

        private static void SetUpRelationsToGanttTask(string uniqueID, Tuple<GanttTask, string> t, GanttTask gtask)
        {
            foreach (var rel in taskToRelation[uniqueID].ToList())
            {
                var relationTask = taskIdToGanttTask[rel].Item1;
                gtask.Dependencies.Add(new Dependency { FromTask = relationTask });
            }
        }

        private static void GenerateAllGanttTasks(IEnumerable<XElement> tasks, XNamespace xnamespace)
        {
            foreach (var t in tasks)
            {
                var ganttTask = new GanttTask();
                if (t.Element(xnamespace + "Name") != null)
                {
                    ganttTask.Title = t.Element(xnamespace + "Name").Value;
                }
                ganttTask.UniqueId = t.Element(xnamespace + "UID").Value;
                ganttTask.Start = DateTime.Parse(t.Element(xnamespace + "Start").Value);
                ganttTask.End = DateTime.Parse(t.Element(xnamespace + "Finish").Value);
                ganttTask.IsMilestone = t.Element(xnamespace + "Milestone").Value == "1";
                ganttTask.Progress = Double.Parse(t.Element(xnamespace + "PercentComplete").Value);
                string outlineNumber = null;
                if (t.Element(xnamespace + "OutlineNumber") != null)
                {
                    outlineNumber = t.Element(xnamespace + "OutlineNumber").Value;
                }
                var relations = t.Elements(xnamespace + "PredecessorLink");
                IList<string> relatonTaskIDs = new List<string>();
                foreach (var relation in relations)
                {
                    var relationTask = relation.Element(xnamespace + "PredecessorUID").Value;
                    relatonTaskIDs.Add(relationTask);
                }
                if (outlineNumber != null)
                {
                    SetAllChildren(outlineNumber, ganttTask.UniqueId);
                }
                taskIdToGanttTask[ganttTask.UniqueId] = new Tuple<GanttTask, string>(ganttTask, outlineNumber);
            }
        }

        private static void ConnectEveryTaskIdWithItsSubTasks(IEnumerable<XElement> tasks, XNamespace xnamespace)
        {
            foreach (var t in tasks)
            {
                var outlineNumber = t.Element(xnamespace + "OutlineNumber");
                if (outlineNumber != null)
                {
                    taskToChildren[outlineNumber.Value] = new List<string>();
                }

            }
        }

        private static void ConnectEveryTaskIdWithItsRelations(IEnumerable<XElement> tasks, XNamespace xnamespace)
        {
            foreach (var t in tasks)
            {
                var id = t.Element(xnamespace + "UID").Value;
                var rels = t.Elements(xnamespace + "PredecessorLink");
                IList<string> relatonTaskIDs = new List<string>();
                foreach (var relation in rels)
                {
                    var relationTask = relation.Element(xnamespace + "PredecessorUID").Value;
                    relatonTaskIDs.Add(relationTask);
                }
                taskToRelation[id] = relatonTaskIDs;
            }
        }

        private static void SetAllChildren(string outlineNumber, string taskID)
        {
            var parentOutlineNumber = outlineNumber;
            if (outlineNumber.Split('.').Length > 1)
            {
                var index = outlineNumber.LastIndexOf('.');
                parentOutlineNumber = outlineNumber.Substring(0, index);

                var parentTasks = taskToChildren.FirstOrDefault(task => task.Key == parentOutlineNumber).Value;
                if (parentTasks != null)
                {
                    parentTasks.Add(taskID);
                }
            }

        }
    }
}
