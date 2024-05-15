using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Telerik.Windows.Controls;

namespace NestedDockingWithPanesSource
{
    public class ViewModel : ViewModelBase
    {
        private ObservableCollection<RadPane> outerDockPanes;
        private ObservableCollection<RadPane> innerDockPanes1;
        private ObservableCollection<RadPane> innerDockPanes2;
        private ObservableCollection<RadPane> innerDockPanes3;
        private DelegateCommand newInnerDocumentCommand;
        private DelegateCommand newOuterDocumentCommand;
        private int innerDockingNumber = 1;

        public ViewModel()
        {
            this.OuterDockPanes = new ObservableCollection<RadPane>();
            this.InnerDockPanes1 = new ObservableCollection<RadPane>() 
            { 
                new RadPane() { Header = "Inner Pane 1", Tag = "DocumentHost" }, 
                new RadPane() { Header = "Inner Pane 2", Tag = "DocumentHost" }, 
                new RadPane() { Header = "Inner Pane 3", Tag = "DocumentHost" }
            };
            var radPaneGroup = new RadPaneGroup() { Name = "InnerDocumentHostPane" + innerDockingNumber };
            innerDockingNumber++;
            var radSplitContainer = new RadSplitContainer();
            radSplitContainer.Items.Add(radPaneGroup);
            var tempInnerDock = new RadDocking() { DockingPanesFactory = new CustomDockingPanesFactory(), DocumentHost = radSplitContainer };

            // Set correct PanesSource for hte inner RadDocking
            tempInnerDock.PanesSource = this.InnerDockPanes1;
            this.OuterDockPanes.Add(new RadPane()
            {
                Header = "Outer Pane 1",
                Content = tempInnerDock,
                Tag = "DocumentHost"
            });
        }
        public ICommand NewOuterDocumentCommand
        {
            get
            {
                if (newOuterDocumentCommand == null)
                    newOuterDocumentCommand = new DelegateCommand(this.CreateNewOuterDocument);

                return newOuterDocumentCommand;
            }
        }

        public ICommand NewInnerDocumentCommand
        {
            get
            {
                if (newInnerDocumentCommand == null)
                    newInnerDocumentCommand = new DelegateCommand(this.CreateNewInnerDocument);

                return newInnerDocumentCommand;
            }
        }

        public ObservableCollection<RadPane> InnerDockPanes3
        {
            get
            {
                return this.innerDockPanes3;
            }

            set
            {
                if (this.innerDockPanes3 != value)
                {
                    this.innerDockPanes3 = value;
                    this.OnPropertyChanged(() => this.InnerDockPanes3);
                }
            }
        }

        public ObservableCollection<RadPane> InnerDockPanes2
        {
            get
            {
                return this.innerDockPanes2;
            }

            set
            {
                if (this.innerDockPanes2 != value)
                {
                    this.innerDockPanes2 = value;
                    this.OnPropertyChanged(() => this.InnerDockPanes2);
                }
            }
        }

        public ObservableCollection<RadPane> InnerDockPanes1
        {
            get
            {
                return this.innerDockPanes1;
            }

            set
            {
                if (this.innerDockPanes1 != value)
                {
                    this.innerDockPanes1 = value;
                    this.OnPropertyChanged(() => this.InnerDockPanes1);
                }
            }
        }

        public ObservableCollection<RadPane> OuterDockPanes
        {
            get
            {
                return this.outerDockPanes;
            }

            set
            {
                if (this.outerDockPanes != value)
                {
                    this.outerDockPanes = value;
                    this.OnPropertyChanged(() => this.OuterDockPanes);
                }
            }
        }

        private void CreateNewInnerDocument(object param)
        {
            this.InnerDockPanes1.Add(new RadPane
            {
                Header = "New Inner Pane " + Guid.NewGuid(),
                Tag = "DocumentHost"
            });
        }


        private void CreateNewOuterDocument(object param)
        {
            var tempInnerDockPanes = new ObservableCollection<RadPane>() 
            { 
                new RadPane() 
                {
                    Header = "Inner Pane", 
                    Tag = "DocumentHost" 
                }, 
            };
            var radPaneGroup = new RadPaneGroup() { Name = "DocumentHostPane" + innerDockingNumber };
            innerDockingNumber++;
            var radSplitContainer = new RadSplitContainer();
            radSplitContainer.Items.Add(radPaneGroup);
            var tempInnerDock = new RadDocking() { DockingPanesFactory = new CustomDockingPanesFactory(), DocumentHost = radSplitContainer };

            // Set correct PanesSource for hte inner RadDocking
            tempInnerDock.PanesSource = tempInnerDockPanes;
            this.OuterDockPanes.Add(new RadPane()
            {
                Header = "New Outer Pane " + Guid.NewGuid(),
                Content = tempInnerDock,
                Tag = "DocumentHost"
            });
        }
    }
}
