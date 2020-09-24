##  Customize Visual States 

This example shows how to customize the visual states (mouse over, pressed, focused, etc.) of the RadButton control. The sample shows how to customize the colors of the mouse over, pressed and the focused states, but you can apply the same approach for all other states listed in the ControlTemplate of the button.

The sample contains custom styles showing how to change the visual states for the Office_Black and Office2016 themes. They show the two basic template structures used accross the ControlTemplates for the different themes. You can use them as a base for customizing any theme.

The guidelines shown in this project can be used for customizing any other button from the RadButtons suite. You just need to extract the corresponding button Style.

In the RadButtonCustomOffice_BlackStyle.xaml and RadButtonCustomOffice2016Style.xaml files, you can find the default Styles for the RadButton control extracted from the Office_Black and Office2016 themes. The changes applied are marked with comments following pattern "Changed OldValue to NewValue". For example: "Changed Background from "{StaticResource ControlBackground_MouseOver}" to "#FF8080:""

[//]: <keywords: trigger, mouseover, pressed, office2016, officeblack>