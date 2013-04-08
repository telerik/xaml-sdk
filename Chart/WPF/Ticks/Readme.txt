The ticks are part of the chart axes and are typically used to mark a specific value on the axis. There are two types of ticks - minor and major. The major ticks always represent the primary axis values. The minor ticks are used for more readable visualization of the data values.

To control the visibility of the ticks you have to use one fo the following properties:

   - MajorTicksVisibility - specifies whether the major ticks should be visible.
   - MinorTicksVisibility - specifies whether the minor ticks should be visible.
   - MinorTickPointMultiplier - specifies the number of minor ticks per major tick, i.e the number of the minor ticks between two major ticks. The number of ticks is always equal to the (MinorTickPointMultiplier - 1) as the multiplier rerpesents the count of chuks defined by the minor ticks between two major ticks. For example in order to have 3 chunks the Axis will need 2 minor ticks to allocate them.