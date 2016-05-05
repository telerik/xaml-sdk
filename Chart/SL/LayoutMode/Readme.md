##Layout Mode##
RadChart allows you to modify the layout of the X-axis to better fit the different chart types in the chart area. This will help you to improve the look and feel of your charts. Depending on the selected layout mode, the X-Axis's ticks and labels and also the charts are visualized differently. LayoutMode is property related only to the X-Axis, hence charts without X-Axis, like Pie and Doughnut, do not use it.

Each LayoutMode is best to be used with certain chart types:

  - Normal - in this mode, the ticks match labels. Best for Line charts and Area charts.
  - Inside - ticks match labels; a small margin on both sides is provided for better presentation. Use this for Bubble Charts, Stick and Candlestick charts.
  - Between - ticks are margins (labels are between two ticks); a small margin on both sides is provided. Useful for Bar charts.
  - Auto - depending on the chart type, RadChart willautomatically select the best LayoutMode. For multi-series charts, layout mode is prioritized in this order: Between, Inside, Normal.