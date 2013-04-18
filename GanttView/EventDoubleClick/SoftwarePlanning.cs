using System;
using System.Collections.ObjectModel;
using Telerik.Windows.Controls.GanttView;

namespace EventDoubleClick
{
	public class SoftwarePlanning : ObservableCollection<IGanttTask>
	{
		public SoftwarePlanning()
		{
			var scopeTaskChild1 = new GanttTask(new DateTime(2012, 1, 3, 8, 0, 0), new DateTime(2012, 1, 5, 23, 0, 0), "Determine project scope") { Description = "Description: Determine project scope" };

			var scopeTaskChild2 = new GanttTask(new DateTime(2012, 1, 3, 13, 0, 0), new DateTime(2012, 1, 4, 12, 0, 0), "Secure project sponsorship") { Description = "Description: Secure project sponsorship" };
			var scopeTaskChild3 = new GanttTask(new DateTime(2012, 1, 4, 13, 0, 0), new DateTime(2012, 1, 5, 12, 0, 0), "Define preliminary resources") { Description = "Description: Define preliminary resources" };
			var scopeTaskChild4 = new GanttTask(new DateTime(2012, 1, 5, 13, 0, 0), new DateTime(2012, 1, 6, 0, 0, 0), "Secure core resources") { Description = "Description: Secure core resources" };
			var scopeTaskChild5 = new GanttTask(new DateTime(2012, 1, 6, 0, 0, 0), new DateTime(2012, 1, 6, 0, 0, 0), "Scope complete") { Description = "Description: Scope complete" };

			scopeTaskChild2.Dependencies.Add(new Dependency { FromTask = scopeTaskChild1 });
			scopeTaskChild3.Dependencies.Add(new Dependency { FromTask = scopeTaskChild2 });
			scopeTaskChild4.Dependencies.Add(new Dependency { FromTask = scopeTaskChild3 });
			scopeTaskChild5.Dependencies.Add(new Dependency { FromTask = scopeTaskChild4 });

			var scopeTask = new GanttTask(new DateTime(2012, 1, 3, 8, 0, 0), new DateTime(2012, 1, 6), "Scope")
			{
				Children = { scopeTaskChild1, scopeTaskChild2, scopeTaskChild3, scopeTaskChild4, scopeTaskChild5 },
				Description = "Description: Determine project scope"
			};
			this.Add(scopeTask);

			var softwareRequirementsTaskChild1 = new GanttTask(new DateTime(2012, 1, 6, 13, 0, 0), new DateTime(2012, 1, 12, 0, 0, 0), "Conduct needs analysis");
			var softwareRequirementsTaskChild2 = new GanttTask(new DateTime(2012, 1, 13, 13, 0, 0), new DateTime(2012, 1, 18, 0, 0, 0), "Draft preliminary software specifications");
			var softwareRequirementsTaskChild3 = new GanttTask(new DateTime(2012, 1, 18, 13, 0, 0), new DateTime(2012, 1, 20, 0, 0, 0), "Develop preliminary budget");
			var softwareRequirementsTaskChild4 = new GanttTask(new DateTime(2012, 1, 21, 8, 0, 0), new DateTime(2012, 1, 21, 17, 0, 0), "Review software specifications/budget with team");
			var softwareRequirementsTaskChild5 = new GanttTask(new DateTime(2012, 1, 21, 8, 0, 0), new DateTime(2012, 1, 21, 17, 0, 0), "Incorporate feedback on software specifications");
			var softwareRequirementsTaskChild6 = new GanttTask(new DateTime(2012, 1, 24, 8, 0, 0), new DateTime(2012, 1, 24, 17, 0, 0), "Develop delivery timeline");
			var softwareRequirementsTaskChild7 = new GanttTask(new DateTime(2012, 1, 25, 8, 0, 0), new DateTime(2012, 1, 25, 12, 0, 0), "Obtain approvals to proceed (concept, timeline, budget)");
			var softwareRequirementsTaskChild8 = new GanttTask(new DateTime(2012, 1, 25, 13, 0, 0), new DateTime(2012, 1, 28, 0, 0, 0), "Secure required resources");
			var softwareRequirementsTaskChild9 = new GanttTask(new DateTime(2012, 1, 26, 0, 0, 0), new DateTime(2012, 1, 26, 0, 0, 0), "Analysis complete");

			softwareRequirementsTaskChild2.Dependencies.Add(new Dependency { FromTask = softwareRequirementsTaskChild1 });
			softwareRequirementsTaskChild3.Dependencies.Add(new Dependency { FromTask = softwareRequirementsTaskChild2 });
			softwareRequirementsTaskChild4.Dependencies.Add(new Dependency { FromTask = softwareRequirementsTaskChild3 });
			softwareRequirementsTaskChild5.Dependencies.Add(new Dependency { FromTask = softwareRequirementsTaskChild4 });
			softwareRequirementsTaskChild6.Dependencies.Add(new Dependency { FromTask = softwareRequirementsTaskChild5 });
			softwareRequirementsTaskChild7.Dependencies.Add(new Dependency { FromTask = softwareRequirementsTaskChild6 });
			softwareRequirementsTaskChild8.Dependencies.Add(new Dependency { FromTask = softwareRequirementsTaskChild7 });
			softwareRequirementsTaskChild9.Dependencies.Add(new Dependency { FromTask = softwareRequirementsTaskChild8 });

			var softwareRequirementsTask = new GanttTask(new DateTime(2012, 1, 6, 13, 0, 0), new DateTime(2012, 1, 26), "Analysis/Software Requirements")
			{
				Children = { softwareRequirementsTaskChild1, softwareRequirementsTaskChild2, softwareRequirementsTaskChild3, softwareRequirementsTaskChild4, softwareRequirementsTaskChild5, softwareRequirementsTaskChild6, softwareRequirementsTaskChild7, softwareRequirementsTaskChild8, softwareRequirementsTaskChild9 }
			};

			this.Add(softwareRequirementsTask);
		}
	}
}
