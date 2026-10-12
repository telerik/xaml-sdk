# PdfElementsEditor

`PdfElementsEditor` is a WPF sample application that shows how to inspect and edit PDF content elements with **Telerik RadPdfViewer**.

## What it does

- Loads `SamplePdf.pdf` on startup.
- Displays the document in `RadPdfViewer`.
- Builds a tree grouped by page and content elements.
- Highlights the selected element in the viewer.
- Provides context-menu actions for each element:
  - **Delete**
  - **Save Selection** (exports the selected element to a new PDF)
  - **EditText** (for text fragments)

## Tech stack

- .NET 10 (`net10.0-windows`)
- WPF
- Telerik UI for WPF (`Telerik.UI.for.Wpf.AllControls.Xaml`)
- Telerik Document Processing PDF APIs

## Project structure

- `MainWindow.xaml(.cs)` – main UI, PDF loading, tree population, selection synchronization.
- `ElementModel.cs` / `PageModel.cs` – tree view models and element commands.
- `CustomUILayersBuilder.cs` / `HiglightElementLayer.cs` – custom viewer layer for element highlighting.
- `TextEditDialog.xaml(.cs)` – editor dialog for text fragment content.
- `SamplePdf.pdf` – sample input document.

## Run

1. Open `PdfElementsEditor.sln` in Visual Studio.
2. Restore NuGet packages.
3. Build and run the `PdfElementsEditor` project.

Alternatively:

```powershell
dotnet build PdfElementsEditor/PdfElementsEditor.csproj
dotnet run --project PdfElementsEditor/PdfElementsEditor.csproj
```
