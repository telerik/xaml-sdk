This examples shows how to create and register custom functions in RadSpreadsheet.
The example shows several functions inheriting from different members from RadSpreadsheet functions inheritance tree.
 - Arguments and E functions are inheriting from FunctionBase abstract class.
 - RepeatString function is inheriting from FunctionWithArguments abstract class.
 - Indirect function is inheriting from FunctionWithArguments abstract class and is overriding the definition of the default RadSpreadsheet "INDIRECT" function. It also demonstrates the usage of CellReferenceRangeExpression.
 - Add and GeoMean functions are inheriting from NumbersInFunction abstract class and are using their own ArgumentConversionRules.
 - Nand function is inheriting from BooleansInFunction abstract class.
 - Upper function is inheriting from StringsInFunction abstract class and is overriding the definition of the default RadSpreadsheet "UPPER" function.
 
You should additionally notice that all this function classes are registered through the FunctionManager class, so that they can be used in RadSpreadsheet.