This example illustrates how to work-around an exception thrown when dragging a Pane.
The issue is caused because the Application.Current is Specific for WPF Application.
Therefore when using WPF controls in WinForms Application it needs to be initialized an instance of WPF Application. This shoud be done in the WinForms Application.