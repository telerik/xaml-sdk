## Numeric Indicator Number Positions
By default the Numeric Indicator won't display its values. You have to define the number positions to display the value's digits. This can be done by adding a number position objects to the Positions collection of the NumericIndicator. You can use the following types of number position:

  * SevenSegsNumberPosition: The digit get displayed via 7 rectangular segments.
  * HexagonalNumberPosition: The digit get displayed via 7 hexagonal segments.
  * FontNumberPosition: The digit is displayed as characters. In this case the values of the various font properties, like FontFamily, FontSize, FontWeight etc., get applied. The font properties can be set to the indicator itself. In this case they will get applied to all of the FontNumberPosition controls in it. You can apply it directly to the FontNumberPosition control as well, in order to have different appearance for each position.

[//]: <keywords: numericscale, numericindicator, fontnumberposition, cornerradius>
