using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.MultiColumnComboBox;
using Telerik.Windows.Data;

namespace TreeViewInDropDown
{
    public class TreeViewDropDownContentManager : DropDownContentManager
    {
        private TreeViewComboBox owner;
        private RadTreeView treeViewDropDownElement;

        public TreeViewDropDownContentManager(TreeViewComboBox owner)
        {
            this.owner = owner;
        }

        public override FrameworkElement DropDownElement => this.treeViewDropDownElement;

        public override RadMultiColumnComboBox Owner => this.owner;

        public override ISelectionBridge InitializeSelectionBridge()
        {
            ISelectionBridge bridge = new TreeSelectionBridge(this.treeViewDropDownElement, this.owner);
            return bridge;
        }

        public override void OnCollectionViewCollectionChanged(QueryableCollectionView collectionView)
        {
        }

        public override void OnMouseDown(object sender, MouseButtonEventArgs args)
        {
        }

        public override void OnMouseUp(object sender, MouseButtonEventArgs args)
        {
            DependencyObject originalSource = null;

            var originalSourceUIElement = args.OriginalSource as UIElement;
            if (originalSourceUIElement == null)
            {
                // This is in case when user clicks over the System.Windows.Documents.Run element placed in the TextBlock.
                var originalSourceContentElement = args.OriginalSource as ContentElement;
                if (originalSourceContentElement != null)
                {
                    originalSource = originalSourceContentElement;
                }
            }
            else
            {
                originalSource = originalSourceUIElement;
            }

            if (originalSource != null)
            {
                var originalItemSource = originalSource as RadTreeViewItem ?? originalSource.ParentOfType<RadTreeViewItem>();

                if (originalItemSource != null && originalItemSource.IsSelected)
                {
                    var parentTreeView = originalItemSource.ParentOfType<RadTreeView>();

                    this.Owner.AutoCompleteProvider.ShouldSetMatchText = true;

                    if (parentTreeView.SelectionMode == System.Windows.Controls.SelectionMode.Single)
                    {
                        this.Owner.ItemsSourceProvider.CollectionView.MoveCurrentTo(null);
                    }                
                    
                    if (originalItemSource.Item != null)
                    {
                        this.owner.ClearText();
                        if (this.Owner.CloseDropDownAfterSelectionInput && this.Owner.SelectionMode == Telerik.Windows.Controls.Primitives.AutoCompleteSelectionMode.Single)
                        {
                            this.Owner.CloseDropDown();
                        }
                    }
                }
            }
        }

        public override void RefreshDropDownElement()
        {
        }

        public override void SetSelectionMode()
        {
            if (this.treeViewDropDownElement == null)
            {
                return;
            }

            this.treeViewDropDownElement.SelectionMode = this.Owner.SelectionMode == Telerik.Windows.Controls.Primitives.AutoCompleteSelectionMode.Single ? System.Windows.Controls.SelectionMode.Single : System.Windows.Controls.SelectionMode.Multiple;
        }

        public override void InitializeDropDownContent(Popup dropDownPopup)
        {
            if (this.Owner != null && this.Owner.ItemsSourceProvider != null && this.Owner.ItemsSourceProvider.CollectionView != null)
            {
                this.owner.ItemsSourceProvider.CollectionView.CurrentChanged += this.OnCollectionViewCurrentChanged;
            }

            var dropDownContent = dropDownPopup.Child.ChildrenOfType<RadTreeView>().FirstOrDefault();
            if (dropDownContent == null)
            {
                throw new ArgumentException("DropDownContent does not have any RadTreeView children.", "dropDownPopup");
            }

            this.treeViewDropDownElement = dropDownContent;

            if (this.Owner != null)
            {
                StyleManager.SetThemeFromParent(this.treeViewDropDownElement, this.Owner);
            }
            this.SetSelectionMode();
        }

        private void OnCollectionViewCurrentChanged(object sender, EventArgs e)
        {
            var qcv = sender as QueryableCollectionView;
            if (this.treeViewDropDownElement == null || qcv.CurrentItem == null)
            {
                return;
            }

            this.treeViewDropDownElement.BringItemIntoView(qcv.CurrentItem);
        }
    }
}