## Date Time Axes
RadCartesianChart supports two types of DateTime axes:
	- The DateTime continuous axis is like a numerical axis but the axis value range consists of DateTime values which are always sorted chronologically.
	- The DateTime categorical axis is a categorical axis with Date-Time values which are sorted chronologically and is represented by the DateTimeCategoricalAxis class. It also allows definition of categories based on specific date time components. For example if such an axis displays a range of one year, the data points can be plotted in categories for each month. If the range is one month, the values may be categorized by day and so on.

[//]: <keywords: lineseries, datetimecategoricalaxis, datetimecontinuousaxis>