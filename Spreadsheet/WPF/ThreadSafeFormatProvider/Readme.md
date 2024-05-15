##  Thread Safe Format Provider 

This WPF example shows how RadSpreadsheet provides an easy solution for making import and export of files asynchronous and thread safe.
The example UserControl provides four buttons:
 - "Open file synchronously" button opens big file synchronously. When pressing this button you may see how the UI thread is unresponsive during the time-consuming import.
 - "Open file asynchronously" button opens the same big file asynchronously. When pressing this button you may notice that the UI thread is responsive as it successfully shows the loading RadBusyIndicator during the import.
 - "Save file synchronously" button saves current Workbook to file synchronously. When pressing this button you may see how the UI thread is unresponsive during the export (this is easier to be noticed when saving big files with time-consuming export).
 - "Save file asynchronously" button saves current Workbook to file asynchronously. When pressing this button you may notice that the UI thread is responsive as you may interact freely with the UserControl during the export.

 [//]: <keywords: open,save,synchronously,asynchronously,bigfile,xlsx,csv,save,import,export>