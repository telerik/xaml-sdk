##Dynamic Format String##
Users often need to type doubles like 1.234, 56.78, 123.9 and they do NOT need to see these values to be completed with zeros like:

* 1.23400
* 56.7800
* 123.90000

To achieve this effect users can users can use No-Mask (Mask="") and update the FormatString property runtime. Furthermore, users need delete and backspace keys to not produce zeros.
For example pressing delete here 12.3|45 to produce 12.35 but not 12.305. This is demonstrated in the sample with overriding the HandleBackKeyNomask and HandleDeleteKeyNoMask methods of the RadMaskedNumericInput.
Finally, it is good idea to limit the input somehow both on the left and on the right of  the decimal point because double loses precision after 15 digits.

<keywords: dynamicformatstring, maskedinputextensions, maskednumericinput, custom, maximumdigitsonright>