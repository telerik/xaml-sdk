## Animations
When displayed, each chart is nicely animated using default animation settings. AnimationSettings allows you to control how the animation is applied to each series and/or series item:

  - ItemDelay - this property specifies the delay for each series item toward the previous one. For example, if you have bar chart, when a bar item animation is started, the next bar item will start its animation with the delay specified by this property.
  - TotalSeriesAnimationDuration - specifies how long will take the animation duration for a single series.
  - ItemAnimationDuration – specifies how long will take the animation duration of each series item.
  - DefaultSeriesDelay - controls when the rendering of the next series should start, compared to the previous one (i.e. the series index is taken into account). For example, if you have three series and DefaultSeriesDelay is set to 10 sec for each of them – the rendering of the 2nd series will begin 10 seconds after the first one is complete, respectively the rendering of the 3rd – 10 seconds after the second is complete.

Note:
If you set values to ItemDelay and TotalSeriesAnimationDuration, then ItemDelay is omitted and TotalSeriesAnimationDuration is used.