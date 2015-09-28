The purpose of the MultipleRootsInSingleTreeLayout demot is to simulate TreeDown Layout which supports more than 1 root in one independent tree component in RadDiagram.
How it works ?
You choose multiple roots and add it in the Roots collection of the TreeLayoutSettings.
On a Button Click, a dummy root shape is created and dummy connections are created which connect the new root to the roots chosen by the user. The Layout is performed with the new root. After Layout is complete, the dummy connections and the dummy root are removed from the diagram.
This way, the root shapes user has selected are positioned with equal Y values (equal vertically) and they are on top of the rest of the diagram shapes.
