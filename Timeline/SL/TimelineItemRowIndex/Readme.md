##Timeline Item Row Index##
The purpose of this example is to demonstrate how you can customize the default vertical positioning logic of RadTimeline. There are three approaches:

1. The first approach is to specify a larger minimum gap between items using the MinimumItemGap property. This value is taken into account when calculating whether the span of an item overlaps the span of an already positioned item.
2. The second approach is to simply turn off the ordering by StartDate and Duration using the AutoSort property.
3. The third approach includes specifying your own custom vertical positioning logic using the ItemRowIndexGenerator property.

<keywords: itemrowindexgenerator, minimumitemgap, autosort, databinding, mvvm>