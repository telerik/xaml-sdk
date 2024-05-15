function load() {
    var div = document.getElementById("surface");
    var diagram;
    if (div) {
        diagram = new RadDiagram.Diagram(div);
        diagram.Clear();
        var importer = new RadDiagram.RDImporter(diagram);
        importer.LoadXML(diagramXML);
    } else
        alert('No DIV found.');
}