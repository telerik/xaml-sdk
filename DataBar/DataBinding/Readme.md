## Data Binding
Databinding for the RadDataBar control involves the correlation between the business logic/data, and the visualization of the control.

The DataBinding involves the following three properties:
* ItemsSource (a property of RadStackedDataBar and RadStacked100Databar) - gets or sets the data source used to generate the content of the databar control. Elements can be bound to data from a variety of data sources in the form of common language runtime (CLR) objects and XML - see the list of the supported data sources bellow.
* Value (a property of RadDataBar) - expects a value which will be used to determine the size of the bar.
* ValuePath (a property of RadStackedDataBar and RadStacked100DataBar) - expects the name of the property from the underlying datasource, which will determine the value of each bar in the stack.

[//]: <keywords: databinding, mvvm, stackeddatabar>