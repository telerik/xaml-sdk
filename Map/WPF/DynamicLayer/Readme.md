## Dynamic Layer
The DynamicLayer allows you to display additional data on top of the displayed map. In contrast to InformationLayer which processes all items in a collection, the DynamicLayer requests items to process. It makes favour when thousands of items are available (pictures of POIs, for example). Your application can select what pictures are suitable for given location and zoom factor and return only these ones.

[//]: <keywords:ZoomGrid, ZoomGridList, LatitudesCount, LongitudesCount, IMapDynamicSource, ItemsRequest>