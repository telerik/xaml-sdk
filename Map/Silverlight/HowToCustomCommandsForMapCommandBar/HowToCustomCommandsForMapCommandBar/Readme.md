## Custom commands for Map command bar
It is now possible to customize the default set of commands in the MapCommandBar UI for every map provider. Every custom command should be described by a CommandDescription instance that you need to add to the respective MapProvider.Commands collection (if your custom command is RoutedCommand / RoutedUICommand, you can add the associated CommandBinding to the MapProvider.CommandBindingCollection).

To see and run the example, please use the 'Open in VS' button and execute the project inside Visual Studio.

[//]: <keywords:MapProvider, CommandDescription, CommandBinding>