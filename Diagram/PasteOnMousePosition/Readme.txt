The Diagram Custom Paste Demonstrates thw following requested customizations / features:
	- Pasting on exact Mouse Position
	- Pasting connection on hovered Shape Connector attaches the coonnection to the connector
This is acheived with overriding the RadDiagram and its Paste() method. On paste the copied items are pasted on the Mouse Position. If the Mouse is above a Shape Connector, the copied Connections are attached to this connector 