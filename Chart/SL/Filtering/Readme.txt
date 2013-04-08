The RadChart allows you to programmatically filter its data. This is achieved via the FilterDescriptors property of the RadChart or the FilterDescriptors property of the SeriesMapping. This collection allows you to use descriptors which define the sorting member and the sorting direction for the data to which the RadChart is bound. As this is a collection you are able not only to add, but also to remove or clear the entries in it, too.

The items in this collection are of type ChartFilterDescriptor. It exposes a few important properties:

  - Member - defines the field of the data object, by which the data will be filtered.
  - Operator - defines the operator, which will be applied to the filtering criteria.
  - Value - defines the value that will be compared with the value of the Member property via the Operator property.
	
Note: This feature is available in data bound scenarios only!