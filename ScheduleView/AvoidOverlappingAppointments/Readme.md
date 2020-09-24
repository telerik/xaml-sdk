## Avoid Overlapping Appointments
This example demonstrates how you could prevent overlapping appointments when drag-and-drop or resize an appointment. In order to achieve it you should create a custom DragDropBehavior and check in CanDrop and CanResize methods whether the destination slot contains any appointments. Additionally, you should customize the RecurrenceChoiceDragDropDialog to handle the case when a whole series of a recurrent appointment is dragged.

[//]: <keywords: dragdropbehavior, conflictchecking, recurrencechoicedragdropdialogstyle, recurrencechoiceresizedialogstyle, recurrent>
