##KML map shape reader##
The RadMap provides support for stunning map overlays through its KML-import feature. Once you have the desired set of features (place marks, images, polygons, textual descriptions, etc.) encoded in KML, you can easily import the data and visualize it through the RadMap control. In this way you can easily visualize complex shapes like country's borders on the map and fill the separate shapes with different colors in order to achieve a sort of grouping.
To read your data you have to use the MapShapeReader class. To pass the desired KML file you have to use the Source property of the MapShapeReader and pass the Uri to the desired .kml file to it. This will automatically generate shapes according to the data inside the file.

To see and run the example, please use the 'Open in VS' button and execute the project inside Visual Studio.
