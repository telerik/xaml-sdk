##WrapAround and Virtualization##
Example shows how you can combine the two features - UI Virtualization in VisualizationLayer and WrapAround.
When you pan horizontally, portions of the map are requested - you normalize the given portions which are outside the longitude range [-180;180], then you find the objects you need to display in this portions.
Last step is to shift the normal (usual) Locations of the objects in order to display them in the requested areas.

<keywords:visualizationlayer, itemtemplateselector, openstreet, xml, mvvm>