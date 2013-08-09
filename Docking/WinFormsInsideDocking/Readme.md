This example illustrates how to work-around the airspace problem of WPF and WinForms working together. More information about the problem you could find <a href="http://msdn.microsoft.com/en-us/library/aa970688.aspx">here<a/>.

In order to be work-arounded all Popups, including the Windows, should have AllowTransparency property set to False.
This leads to some glitches in the Docking control, which fixes are provided in the project.