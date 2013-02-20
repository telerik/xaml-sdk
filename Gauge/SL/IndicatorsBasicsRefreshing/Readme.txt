To implement this feature you have to use the RefreshRate and RefreshMode properties. The first one represents the interval of time between two updates. It is of type TimeSpan. The second one specifies the way the final value gets calculated. You can choose between the following:

  - None(default) - disables refreshing.
  - Average - displays the average of the values, that occurred in the interval.
  - Max - displays the greatest from the values, that occured in the interval.
  - Min - display the smallest of the values, that occured in the interval.