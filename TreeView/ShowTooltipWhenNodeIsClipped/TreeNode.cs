using System.Collections.ObjectModel;

namespace ShowTooltipWhenNodeIsClipped
{
    public class TreeNode
    {
        public TreeNode()
        {
            Children = new ObservableCollection<TreeNode>();
        }

        public string Header { get; set; }
        public ObservableCollection<TreeNode> Children { get; set; }
    }
}
