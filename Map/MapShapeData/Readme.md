## Map Shape Data
The VisualizationLayer of the RadMap control allows you to visualize a number of different visual elements on the map. This is done via different implementations of the MapShapeData class which represents shape data that can be created in a background thread. This data is then used to create visual elements on the UI thread. As these shape data classes are not dependency objects, this leads to improved rendering performance.

[//]: <keywords:map, shape, data, EllipseData, LineData, PathData, PolygonData, PolylineData, RectangleData>