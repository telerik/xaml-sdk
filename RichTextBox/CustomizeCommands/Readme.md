##  Customize Commands 

This example demonstrates how you can use the CommandExecuting and CommandExecuted events to stop the execution of a command or change its default behavior. 
The following customizations are shown:
 - The SaveCommand is cancelled and a different logic is implememted to change the default properties of the Save File dialog; 
 - The InsertTableCommand is cancelled and tables cannot be inserted;
 - The PasteCommand is altered just before its execution so it only allows to paste plain text;
 - After executing InsertPictureCommand, the image is resized so it doesn't exceed 200px width. 

[//]: <keywords: behavior, save, implement>