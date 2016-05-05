##Empty Values##
The Q1 2011 version of the Chart control includes code to support empty/null values. There are many scenarios, in which the property of an object, or a collection of objects, to which the Chart is data bound, is null.

Note: RadChart recognizes null values as "empty" and not double.NaN-s.

  - In unbound scenarios, you need to explicitly set the DataPoint.IsEmpty property to true as the YValue property is of type double (and not nullable double) for backwards compatibility reasons.
  - In bound scenarios you need to use nullable double type to achieve the desired functionality. 