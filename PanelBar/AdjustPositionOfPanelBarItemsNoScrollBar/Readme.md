## Adjust Position Of PanelBarItems when PanelBar has no ScrollBar

The example demonstrates an approach to customizing the RadPanelBarItems to include a ScrollBar when the RadPanelBar's
vertical ScrollBar is Disabled.

Triggers are used to set the proper ContentTemplate for the RadPanelBarItem, depending on the level they are on.

Margin of Header of SecondLevel items is removed (from 5 0 to 0), which requires a custom ControlTemplate

[//]: <keywords: panelbaritem, scrollbar, listbox>