## Custom DragDropBehavior
This example shows how to implement and use a custom ScheduleViewDragDropBehavior. The functionality of example is explained below and demonstrates the use of all the methods available for overriding:
-	Drag and drop between ScheduleView and ListBox
-	Custom Appointment with IsReadOnly property which cannot be moved or resized and has different Background color (Red in the example)
-	The Appointments cannot be resized to a duration below 30 minutes and above 2 hours
-	All of the non ReadOnly Appointments in with same Resource should move together when single Appointment is dragged
-	Appointments cannot be moved from one Resource to another
-	Dragging Appointment with Control key pressed doesn't copy the Appointment

[//]: <keywords: scheduleviewdragdropbehavior, listbox, move, resize, readonly, appointments>
