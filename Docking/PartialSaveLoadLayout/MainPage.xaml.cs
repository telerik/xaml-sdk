using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;

namespace PartialSaveLoadLayout
{
    public partial class MainPage : UserControl
    {
        private string rightLayoutFileName = "RadDocking_RightLayout.xml";
        private string leftLayoutFileName = "RadDocking_LeftLayout.xml";

        private List<string> ignoredRightPanesSerializationTagsWhenCleaning = new List<string>();
        private List<string> ignoredLeftPanesSerializationTagsWhenCleaning = new List<string>();

        private RightScope rightScope;
        private LeftScope leftScope;

        public MainPage()
        {
            InitializeComponent();
        }

        private void SaveLayoutToFile(string fileName)
        {
            using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (var isoStream = storage.OpenFile(fileName, FileMode.Create))
                {
                    this.Docking.SaveLayout(isoStream);
                    isoStream.Seek(0, SeekOrigin.Begin);
                    StreamReader reader2 = new StreamReader(isoStream);
                    this.XmlTextBox.Text = reader2.ReadToEnd();
                }
            }
        }

        private void LoadLayoutFromFile(string fileName)
        {
            using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                var isoStream = storage.OpenFile(fileName, FileMode.Open);
                using (isoStream)
                {
                    this.Docking.LoadLayout(isoStream);
                }
            }
        }

        private void SaveRightLayoutToFileButtonClick(object sender, RoutedEventArgs e)
        {
            using (this.rightScope = new RightScope())
            {
                this.ignoredRightPanesSerializationTagsWhenCleaning.Clear();
                this.SaveLayoutToFile(this.rightLayoutFileName);
                this.LoadRightLayoutFromFileButton.IsEnabled = true;
            }
        }

        private void LoadRightLayoutFromFileButtonClick(object sender, RoutedEventArgs e)
        {
            using (this.rightScope = new RightScope())
            {
                this.LoadLayoutFromFile(this.rightLayoutFileName);
            }
        }

        private void SaveLeftLayoutToFileButtonClick(object sender, RoutedEventArgs e)
        {
            using (this.leftScope = new LeftScope())
            {
                this.ignoredLeftPanesSerializationTagsWhenCleaning.Clear();
                this.SaveLayoutToFile(this.leftLayoutFileName);
                this.LoadLeftLayoutFromFileButton.IsEnabled = true;
            }
        }

        private void LoadLeftLayoutFromFileButtonClick(object sender, RoutedEventArgs e)
        {
            using (this.leftScope = new LeftScope())
            {
                this.LoadLayoutFromFile(this.leftLayoutFileName);
            }
        }

        private void Docking_ElementLayoutSaving(object sender, Telerik.Windows.Controls.LayoutSerializationSavingEventArgs e)
        {
            var splitContainer = e.AffectedElement as RadSplitContainer;
            var pane = e.AffectedElement as RadPane;

            if (this.rightScope != null && this.rightScope.IsActive)
            {
                if (splitContainer != null && (RadDocking.GetDockState(splitContainer) != Telerik.Windows.Controls.Docking.DockState.DockedRight || splitContainer.IsInDocumentHost || splitContainer.IsInToolWindow))
                {
                    e.Cancel = true;
                }

                if (pane != null)
                {
                    var parentSplitContainer = pane.ParentOfType<RadSplitContainer>();
                    if (parentSplitContainer != null && RadDocking.GetDockState(parentSplitContainer) == Telerik.Windows.Controls.Docking.DockState.DockedRight && !parentSplitContainer.IsInDocumentHost && !parentSplitContainer.IsInToolWindow)
                    {
                        this.ignoredRightPanesSerializationTagsWhenCleaning.Add(e.AffectedElementSerializationTag);
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
            }
            else if (this.leftScope != null && this.leftScope.IsActive)
            {
                if (splitContainer != null && (RadDocking.GetDockState(splitContainer) != Telerik.Windows.Controls.Docking.DockState.DockedLeft || splitContainer.IsInDocumentHost || splitContainer.IsInToolWindow))
                {
                    e.Cancel = true;
                }

                if (pane != null)
                {
                    var parentSplitContainer = pane.ParentOfType<RadSplitContainer>();
                    if (parentSplitContainer != null && RadDocking.GetDockState(parentSplitContainer) == Telerik.Windows.Controls.Docking.DockState.DockedLeft && !parentSplitContainer.IsInDocumentHost && !parentSplitContainer.IsInToolWindow)
                    {
                        this.ignoredLeftPanesSerializationTagsWhenCleaning.Add(e.AffectedElementSerializationTag);
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        private void Docking_ElementLayoutCleaning(object sender, Telerik.Windows.Controls.LayoutSerializationCleaningEventArgs e)
        {
            if (this.rightScope != null && this.rightScope.IsActive)
            {
                if (!this.ignoredRightPanesSerializationTagsWhenCleaning.Contains(e.AffectedElementSerializationTag))
                {
                    e.Cancel = true;
                }
            }

            else if (this.leftScope != null && this.leftScope.IsActive)
            {
                if (!this.ignoredLeftPanesSerializationTagsWhenCleaning.Contains(e.AffectedElementSerializationTag))
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
