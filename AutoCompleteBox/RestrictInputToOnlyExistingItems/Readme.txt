#Restrict Input To Only Existing Items#
This example demonstrates how to restrict the input of the user to only the existing items inside the ItemsSource of RadAutoCompleteBox.

For Silverlight: The Silverlight application needs to be trusted and you need to enable trusted applications to run inside the browser. This needs be done in order to import the WinAPI function 'GetKeyState' - it is used to find if the 'Caps Lock' key is pressed. If the application is not trusted the attached behavior won't work. Please, follow the next steps:

1. Sign the .XAP file of the Silverlight application with code signing certificate. Right click on the Silverlight application and go to Properties. Choose ‘Signing' from the left hand side and check the checkbox 'Sign the .XAP file'.
2. Click on the button 'Create Test Certificate'. Enter the password and confirm password and click the 'OK' button.
3. Click on the 'More Details' button highlighted - this will show you a 'Certificate' window and click the 'Install Certificate' button.
4. Clicking on the button brings up the 'Certificate Import wizard'. Click on the 'Next' button and choose 'Place all certificates in the following store'. Click on the Browse button. This will show you a 'Certificate Store'. Choose 'Trusted Publisher' and finish the wizard. Now repeat the same step to install this certificate in 'Trusted Root Certification Authorities'.

For a more detailed information how to enable Trusted Applications to run inside the browser, please check the following article from MSDN:
https://msdn.microsoft.com/en-us/library/gg192793(v=vs.95)