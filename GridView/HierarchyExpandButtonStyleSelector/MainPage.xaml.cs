using System;
using System.Linq;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;

namespace SilverlightApplication1
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();

			this.clubsGrid.TableDefinition.ChildTableDefinitions.Add(
				new GridViewTableDefinition
				{
					Relation = new PropertyRelation("Players")
				});
        }
    }
}