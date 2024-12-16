## Tool Bar Custom Styled Elements
The ToolBarCustomStyledElements project demonstrates the correct approach for setting a custom style on element placed in RadToolbar.
The toolbar has predefined styles for radbutton, textblock, raddropdownbutton, etc. In order to edit and use successfully these styles / or to use custom styles for the elements in the toolbar you have to override the ToolBarContainerStyleSelector of the RadToolbar. Add a ContainerStyle targetting the specific control, add its predefined style and add/edit/override the properties you need.

[//]: <keywords: itemcontainerstyleselector, toolbarcontainerstyleselector>