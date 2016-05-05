##Sorting##
The RadChart allows you to programmatically sort its data. This is achieved via the SortDescriptors property of the RadChart or the SortDescriptors property of the SeriesMapping. This collection allows you to use descriptors which define the sorting member and the sorting direction for the data to which the RadChart is bound. As this is a collection, you are able not only to add, but to remove or clear the entries in it, too.

The items in this collection are of type ChartSortDescriptor. It exposes two important properties:

  - Member - defines the property of the data object, by which the data will be sorted.
  - SortDirection - defines the sorting direction.