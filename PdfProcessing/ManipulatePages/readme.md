##Manipulate Pages##
This project demonstrates how to use PdfStreamWriter class in order to manipulate PDF pages. The project shows several use-case scenario examples in 4 separate methods as follows:
 - MergeDifferentDocumentsPages - this method merges all pages from several PDF documents with different PDF page content. All page content is preserved unmodified after the merge operation, even when the original file contains currently not supported by RadPdfProcessing model PDF features (such us sound, video and 3D interactive content).
 - SplitDocumentPages - this method splits the pages of a multi-paged PDF file and saves them as separate single-paged PDF files.
 - FitAndPositionMultiplePagesOnSinglePage - this method gets a multi-paged PDF file and creates a new PDF file from its pages by scaling and positioning each 4 consecutive pages from the original file to a single page in the result file.
 - PrependAndAppendPageContent - this method gets a multi-paged PDF file and creates new PDF file from its pages by adding newly generated page content below and above the existing page content.

In the end, the demo opens the result folder with the newly created PDF files.

<keywords: merge,split,prepend,append,add,page,content,generate,PDF,stream,processing>