## Binding to ICustomTypeDescriptor
This example demonstrates how an object implementing the ICustomTypeDescriptor interface can be defined and bound to RadGridView. The ValidatesOnDataErrors of the control can be set to InEditMode 
to avoid the ArgumentExceptions thrown by the DataAnnotation.Validator thrown  for ICustomTypeDescriptor implementations in debug mode.

[//]: <KeyWords: bind, icustomtypedescriptor, dynamic>