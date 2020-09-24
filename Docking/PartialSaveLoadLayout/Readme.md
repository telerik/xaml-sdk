## Save/Load a part of RadDocking layout
This example demonstrates how to save/load only one part of the RadDocking layout. Using the ElementLayoutSaving event we cancel the saving of the undesired elements.
With the ElementLayoutCleaning event we cancel the cleaning of all elements that weren't saved in the ElementLayoutSaving event handler.

[//]: <keywords:docking, partial, persist, cancel, serializationtag>