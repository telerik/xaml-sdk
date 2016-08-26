using System.Collections.ObjectModel;

namespace ShowTooltipWhenNodeIsClipped
{
    public class TreeViewModel
    {
        public ObservableCollection<TreeNode> Data { get; set; }
        public TreeViewModel()
        {
            Data = new ObservableCollection<TreeNode>();
            GetData();
        }

        private void GetData()
        {
            for (int i = 0; i < 5; i++)
            {
                TreeNode parentNode = new TreeNode();
                if (i % 2 == 0)
                {
                    parentNode.Header = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.";
                }
                else
                {
                    parentNode.Header = "Lorem ipsum dolor";
                }

                for (int p = 0; p < 2; p++)
                {
                    TreeNode child = new TreeNode();
                    if (p % 2 == 0)
                    {
                        child.Header = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.";
                    }
                    else
                    {
                        child.Header = "Lorem ipsum dolor";
                    }
                    parentNode.Children.Add(child);
                }
                Data.Add(parentNode);
            }
        }
    }
}
