This help example will demonstrate how you can make your charts more interactive by adding a selection behavior. Here are the main properties that ChartSelectionBehavior exposes:

  - DataPointSelectionMode - to control single / multiple data point selection.
  - HitTestMargin - to increase the hit-testable area around the data point (especially useful when visualizing small scatter points that can be selected).
  - SelectionPoints - to detect the selection. You may also use the SelectionChanged event of the RadChartView for the same purpose.
  - SelectionPalette - to control the selected element appearance. 