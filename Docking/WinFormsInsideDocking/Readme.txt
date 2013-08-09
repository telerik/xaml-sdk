This example illustrates how to work-around the airspace problem of WPF and WinForms working together. More information about the problem you could find here: http://msdn.microsoft.com/en-us/library/aa970688.aspx
In order to be work-arounded all Popups, including the Windows, should have AllowTransparency property set to False.
This leads to some glitches in the Docking control, which fixes are provided in the project.

In order to trigger ActivePaneChanged and set properly ActivePane for the RadDocking when clicking on WinForms control, you should handle MouseDown WinForms event.

To work arond the problem with the AutoHide are a ClickFlyoutBehavior should be set to the Docking control.