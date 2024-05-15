## Drag Drop to RadDiagram

This example demonstrates how to implement custom DragDrop behavior from RadGridView to RadDiagram control using DragDropManager. Using the DragDropManager we are subscribing to the DragInitiliaze event for every RadGridView row in a custom attached property. In this event handler is created the drag visual element. Then in the PreviewDrop event handler of the RadDiagram control, we are creating a custom NodeViewModeBase object which represents the diagram shape. 

[//]: <keywords: dragdrop, radgridview, raddiagram, dragdropmanager>
