using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls;

namespace ShuttleControl
{
    public class ShuttleUtilities
    {
        internal static void MoveSelectedItems(ShuttleTreeViewModel sourceTreeDataContext, ShuttleTreeViewModel targetTreeDataContext)
        {
            var selectedNodes = sourceTreeDataContext.SelectedTreeNodes.ToList();

            foreach (var node in selectedNodes)
            {

                RemoveItem(node, node.Parent);

                if (node.Parent == sourceTreeDataContext.TableNode)
                {
                    node.Parent = targetTreeDataContext.TableNode;
                    targetTreeDataContext.TableNode.Children.Add(node);
                }
                else if (node.Parent == sourceTreeDataContext.ViewNode)
                {
                    node.Parent = targetTreeDataContext.ViewNode;
                    targetTreeDataContext.ViewNode.Children.Add(node);
                }

                if (node.Parent.IsExpanded == false)
                {
                    node.Parent.IsExpanded = true;
                }

                node.IsSelected = false;
            }
        }

        internal static void MoveAllItems(ShuttleTreeViewModel sourceTreeDataContext, ShuttleTreeViewModel targetTreeDataContext)
        {
            foreach (NodeViewModel node in sourceTreeDataContext.TableNode.Children.ToList())
            {
                RemoveItem(node, node.Parent);
                AddToNewParent(node, targetTreeDataContext.TableNode);
            }

            foreach (NodeViewModel node in sourceTreeDataContext.ViewNode.Children.ToList())
            {
                RemoveItem(node, node.Parent);
                AddToNewParent(node, targetTreeDataContext.ViewNode);
            }
        }

        private static void AddToNewParent(NodeViewModel node, NodeViewModel parent)
        {
            node.Parent = parent;
            parent.Children.Add(node);

            if (parent.IsExpanded == false)
            {
                parent.IsExpanded = true;
            }
        }

        private static void RemoveItem(NodeViewModel nodeToRemove, NodeViewModel parentNode)
        {
            parentNode.Children.Remove(nodeToRemove);
        }
    }
}
