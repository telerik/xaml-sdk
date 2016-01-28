MinusKeyCustomizations SDK sample shows how to use the negative sign symbol in custom, non- default way.
Please note that although the code shows RadMaskedNumericInput and its custom subclasses, the same approach can be used for RadMaskedCurrencyInput.
* The first control in the demo is RadMaskedNumericInput with AllowMinusOnNullValues set to true. This allows you to press OemMinus / Subtract key on the keyboard when the value is null resulting in entering negative sign symbol before entering any digits. 

* The second control in the demo is custom RadMaskedNumericInput which allows you to select all and press OemMinus / Subtract key resulting in clearing the value and entering negative sign. This is achieved with overriding HandleSubtractkey method of RadMaskedNumericInput. 

* The third control in the demo is custom RadMaskedNumericInput which allows you to delete the negative sign symbol with Backspace / Delete keys on the keyboard. This is achieved with overriding CanModifyChar, HandleBackKeyNoMask and HandleDeleteKeyNoMaks methods of the RadMaskedNumericInput.
