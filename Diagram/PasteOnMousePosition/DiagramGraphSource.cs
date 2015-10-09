using System;
using System.Linq;
using System.Windows;
using Telerik.Windows.Controls.Diagrams.Extensions.ViewModels;
using Telerik.Windows.Diagrams.Core;

namespace DiagramCustomPaste
{
    public class DiagramGraphSource : SerializableGraphSourceBase<NodeViewModelBase, StartEndLink>
    {
        public DiagramGraphSource()
        {
            var first = new NodeViewModelBase()
            {
                Position = new Point(200, 200),
                Content = "First item",
                Height = 60,
                Width = 80
            };

            var second = new NodeViewModelBase()
            {
                Position = new Point(600, 200),
                Content = "Second item",
                Height = 60,
                Width = 80
            };

            var third = new ContainerNodeViewModelBase<NodeViewModelBase>
            {
                Position = new Point(800, 200),
                Content = "Third item",
            };

            var connection = new StartEndLink()
            {
                StartPointVM = new Point(300, 225),
                EndPointVM = new Point(550, 225)
            };

            AddNode(first);
            AddNode(second);
            AddNode(third);
            AddLink(connection);
        }

        public override void SerializeLink(StartEndLink link, SerializationInfo info)
        {
            info["StartPoint"] = null;
            info["Start"] = link.StartPointVM.ToInvariant();

            info["EndPoint"] = null;
            info["End"] = link.EndPointVM.ToInvariant();

            base.SerializeLink(link, info);
        }

        public override StartEndLink DeserializeLink(IConnection connection, SerializationInfo info)
        {
            StartEndLink link = new StartEndLink();

            if (info["Start"] != null)
            {
                link.StartPointVM = Utils.ToPoint(info["Start"].ToString()).Value;
            }

            if (info["End"] != null)
            {
                link.EndPointVM = Utils.ToPoint(info["End"].ToString()).Value;
            }

            return link;
        }

        /// <summary>
        /// GetNodeUniqueId
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public override string GetNodeUniqueId(NodeViewModelBase node)
        {
            if (node != null)
                return node.GetHashCode().ToString();

            return string.Empty;
        }     
        
        /// <summary>
        /// SerializeNode
        /// </summary>
        /// <param name="node"></param>
        /// <param name="info"></param>
        public override void SerializeNode(NodeViewModelBase baseNode, SerializationInfo info)
        {
            base.SerializeNode(baseNode, info);
            if (baseNode.Content != null)
                info["Content"] = baseNode.Content.ToString();
            else
                info["Content"] = string.Empty;
            info["Position"] = null;
            info["NodePosition"] = baseNode.Position.ToInvariant();
        }

        /// <summary>
        /// Deserialize Node
        /// </summary>
        /// <param name="shape"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public override NodeViewModelBase DeserializeNode(IShape shape, SerializationInfo info)
        {
            NodeViewModelBase node = null;
            if (shape is IContainerShape)
            {
                node = new ContainerNodeViewModelBase<NodeViewModelBase>();
            }
            else
            {
                node = new NodeViewModelBase();
            }
 
            if (info["Content"] != null)
                node.Content = info["Content"].ToString();
            if (info["NodePosition"] != null)
            {
                var position = Utils.ToPoint(info["NodePosition"].ToString());
                if (position.HasValue) node.Position = position.Value;
            }
            if (info[this.NodeUniqueIdKey] != null)
            {
                var nodeUniquekey = info[this.NodeUniqueIdKey].ToString();
                this.CachedNodes[nodeUniquekey] = node;
            }

            return node;
        }
    }

    public class StartEndLink : LinkViewModelBase<NodeViewModelBase>
    {
        public override string ToString()
        {
            return string.Empty;
        }

        private Point startPointVM;
        public Point StartPointVM
        {
            get { return this.startPointVM; }
            set
            {
                if (this.startPointVM != value)
                {
                    this.startPointVM = value;
                    this.OnPropertyChanged("StartPointVM");
                }
            }
        }

        private Point endPointVM;
        public Point EndPointVM
        {
            get { return this.endPointVM; }
            set
            {
                if (this.endPointVM != value)
                {
                    this.endPointVM = value;
                    this.OnPropertyChanged("EndPointVM");
                }
            }
        }      
    }
}
