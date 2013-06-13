using System;

namespace CustomSettingsPane.ViewModels
{
#if !WPF
	public class HierarchicalDataTemplate : Telerik.Windows.Controls.HierarchicalDataTemplate
#else
	public class HierarchicalDataTemplate : System.Windows.HierarchicalDataTemplate
#endif
	{


	}
}
