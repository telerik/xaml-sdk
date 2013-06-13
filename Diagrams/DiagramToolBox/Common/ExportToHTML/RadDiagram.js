/**
* Copyright Telerik.
*
* Disclaimer:
* The TypeScript and SVG libraries allow a fully interactive diagramming
* experience, but are not released or supported as such yet.
* The only part supported for now is the export/import to/from XAML/XML.
*
*/
///<reference path='../RadSVG/RadSVG.ts' />
var svg = RadSVG;
var RadDiagram;
(function (RadDiagram) {
    /**
    * The SVG namespace (http://www.w3.org/2000/svg).
    */
    RadDiagram.NS = "http://www.w3.org/2000/svg";
    /**
    * The actual diagramming surface.
    */
    var Diagram = (function () {
        function Diagram(div) {
            var _this = this;
            this.currentPosition = new svg.Point(0, 0);
            this.isShiftPressed = false;
            this.pan = svg.Point.Empty;
            this.isPanning = false;
            this.zoomRate = 1.1;
            // Increase for faster zooming (i.e., less granularity).
            this.undoRedoService = new UndoRedoService();
            /**
            * The collection of items contained within this diagram.
            */
            this.shapes = [];
            this.connections = [];
            this.lastUsedShapeTemplate = null;
            this.hoveredItem = null;
            this.newItem = null;
            this.newConnection = null;
            this.selector = null;
            this.isManipulating = false;
            this.currentZoom = 1.0;
            // the hosting div element
            this.div = div;
            // the root SVG Canvas
            this.canvas = new svg.Canvas(div);
            // the main layer
            this.mainLayer = new svg.Group();
            this.mainLayer.Id = "mainLayer";
            this.canvas.Append(this.mainLayer);
            // the default theme
            this.theme = {
                background: "#fff",
                connection: "#000",
                selection: "#ff8822",
                connector: "#31456b",
                connectorBorder: "#fff",
                connectorHoverBorder: "#000",
                connectorHover: "#0c0"
            };
            // some switches
            this.isSafari = typeof navigator.userAgent.split("WebKit/")[1] != "undefined";
            this.isFirefox = navigator.appVersion.indexOf('Gecko/') >= 0 || ((navigator.userAgent.indexOf("Gecko") >= 0) && !this.isSafari && (typeof navigator.appVersion != "undefined"));
            this.MouseDownHandler = function (e) {
                _this.MouseDown(e);
            };
            this.MouseUpHandler = function (e) {
                _this.MouseUp(e);
            };
            this.MouseMoveHandler = function (e) {
                _this.MouseMove(e);
            };
            this._doubleClickHandler = function (e) {
                _this.doubleClick(e);
            };
            this._touchStartHandler = function (e) {
                _this.touchStart(e);
            };
            this._touchEndHandler = function (e) {
                _this.touchEnd(e);
            };
            this._touchMoveHandler = function (e) {
                _this.touchMove(e);
            };
            this.KeyDownHandler = function (e) {
                _this.KeyDown(e);
            };
            this.KeyPressHandler = function (e) {
                _this.KeyPress(e);
            };
            this.KeyUpHandler = function (e) {
                _this.keyUp(e);
            };
            this.canvas.MouseMove = this.MouseMoveHandler;
            this.canvas.MouseDown = this.MouseDownHandler;
            this.canvas.MouseUp = this.MouseUpHandler;
            this.canvas.KeyDown = this.KeyDownHandler;
            this.canvas.KeyPress = this.KeyPressHandler;
            //this.todelete.addEventListener("touchstart", this._touchStartHandler, false);
            //this.todelete.addEventListener("touchend", this._touchEndHandler, false);
            //this.todelete.addEventListener("touchmove", this._touchMoveHandler, false);
            //this.todelete.addEventListener("dblclick", this._doubleClickHandler, false);
            //this.todelete.addEventListener("keydown", this.KeyDownHandler, false);
            //this.todelete.addEventListener("KeyPress", this.KeyPressHandler, false);
            //this.todelete.addEventListener("keyup", this.KeyUpHandler, false);
            this.selector = new Selector(this);
            this.listToWheel(this);
        }
        Object.defineProperty(Diagram.prototype, "Shapes", {
            get: /**
            * The collection of items contained within this diagram.
            */
            function () {
                return this.shapes;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Diagram.prototype, "Connections", {
            get: function () {
                return this.connections;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Diagram.prototype, "Canvas", {
            get: //TODO: note to Swa: ensure you delete this after the importer is working!!
            function () {
                return this.canvas;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Diagram.prototype, "Zoom", {
            get: function () {
                return this.currentZoom;
            },
            set: function (v) {
                if (this.mainLayer == null) {
                    throw "The 'mainLayer' is not present.";
                }
                //around 0.5 something exponential happens...!?
                this.currentZoom = Math.min(Math.max(v, 0.55), 2.0);
                this.mainLayer.Native.setAttribute("transform", "translate(" + this.pan.X + "," + this.pan.Y + ")scale(" + this.currentZoom + "," + this.currentZoom + ")");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Diagram.prototype, "Pan", {
            get: function () {
                return this.pan;
            },
            set: function (v) {
                this.pan = v;
                this.mainLayer.Native.setAttribute("transform", "translate(" + this.pan.X + "," + this.pan.Y + ")scale(" + this.currentZoom + "," + this.currentZoom + ")");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Diagram.prototype, "MainLayer", {
            get: function () {
                return this.mainLayer;
            },
            enumerable: true,
            configurable: true
        });
        Diagram.prototype.listToWheel = function (self) {
            var mousewheelevt = (/Firefox/i.test(navigator.userAgent)) ? "DOMMouseScroll" : "mousewheel";//FF doesn't recognize mousewheel as of FF3.x

            var handler = function (e) {
                var evt = window.event || e;
                if (evt.preventDefault) {
                    evt.preventDefault();
                } else {
                    evt.returnValue = false;
                }
                self.zoomViaMouseWheel(evt, self);
                return;
            };
            if (self.div.attachEvent) {
                //if IE (and Opera depending on user setting)
                self.div.attachEvent("on" + mousewheelevt, handler);
            } else if (self.div.addEventListener) {
                //WC3 browsers
                self.div.addEventListener(mousewheelevt, handler, false);
            }
        };
        Diagram.prototype.zoomViaMouseWheel = function (mouseWheelEvent, diagram) {
            var evt = window.event || mouseWheelEvent;
            var delta = evt.detail ? evt.detail * (-120) : evt.wheelDelta;
            var z = diagram.Zoom;
            ;
            if (delta > 0) {
                z *= this.zoomRate;
            } else {
                z /= this.zoomRate;
            }
            diagram.Zoom = z;
            /* When the mouse is over the webpage, don't let the mouse wheel scroll the entire webpage: */
            mouseWheelEvent.cancelBubble = true;
            return false;
        };
        Diagram.prototype.Focus = function () {
            this.canvas.Focus();
        };
        Object.defineProperty(Diagram.prototype, "Theme", {
            get: function () {
                return this.theme;
            },
            set: function (value) {
                this.theme = value;
            },
            enumerable: true,
            configurable: true
        });
        Diagram.prototype.Delete = function (undoable) {
            if (typeof undoable === "undefined") { undoable = false; }
            this.DeleteCurrentSelection(undoable);
            this.Refresh();
            this.UpdateHoveredItem(this.currentPosition);
            this.UpdateCursor();
        };
        Object.defineProperty(Diagram.prototype, "Selection", {
            get: function () {
                return this.getCurrentSelection();
            },
            enumerable: true,
            configurable: true
        });
        Diagram.prototype.Clear = /**
        * Clears the current diagram and the undo-redo stack.
        */
        function () {
            this.currentZoom = 1.0;
            this.pan = svg.Point.Empty;
            this.shapes = [];
            this.connections = [];
            this.canvas.Clear();
            this.mainLayer = new svg.Group();
            this.mainLayer.Id = "mainLayer";
            this.canvas.Append(this.mainLayer);
            this.undoRedoService = new UndoRedoService();
        };
        Diagram.prototype.getCurrentSelection = function () {
            var selection = [];
            for (var i = 0; i < this.shapes.length; i++) {
                var shape = this.shapes[i];
                if (shape.IsSelected) {
                    selection.push(shape);
                }
                for (var j = 0; j < shape.Connectors.length; j++) {
                    var connector = shape.Connectors[j];
                    for (var k = 0; k < connector.Connections.length; k++) {
                        var connection = connector.Connections[k];
                        if (connection.IsSelected) {
                            selection.push(connection);
                        }
                    }
                }
            }
            return selection;
        };
        Object.defineProperty(Diagram.prototype, "elements", {
            get: function () {
                return this.shapes;
            },
            enumerable: true,
            configurable: true
        });
        Diagram.prototype.AddConnection = /**
        * Creates a connection between the given connectors.
        */
        function (item, sink) {
            var connection = null;
            var source = null;
            if (item instanceof Connection) {
                if (sink != null) {
                    throw "Connection and sink cannot be specified simultaneously.";
                }
                connection = item;
                source = connection.From;
                sink = connection.To;
            } else {
                if (item instanceof Connector) {
                    source = item;
                    connection = new Connection(source, sink);
                } else {
                    throw "Parameter combination.";
                }
            }
            source.Connections.push(connection);
            if (sink != null)// happens when drawing a new connection
            {
                sink.Connections.push(connection);
            }
            this.mainLayer.Append(connection.Visual);
            connection.Diagram = this;
            connection.Invalidate();
            this.connections.push(connection);
            return connection;
        };
        Diagram.prototype.AddShape = function (template) {
            this.lastUsedShapeTemplate = template;
            var item = new Shape(template, template.Position);
            return this.AddItem(item);
        };
        Diagram.prototype.AddItem = function (shape) {
            this.shapes.push(shape);
            shape.Diagram = this;
            this.mainLayer.Append(shape.Visual);
            return shape;
        };
        Diagram.prototype.AddMarker = function (marker) {
            this.canvas.AddMarker(marker);
        };
        Diagram.prototype.Undo = function () {
            this.undoRedoService.Undo();
            this.Refresh();
            this.UpdateHoveredItem(this.currentPosition);
            this.UpdateCursor();
        };
        Diagram.prototype.SelectAll = function () {
            this.undoRedoService.begin();
            var selectionUndoUnit = new SelectionUndoUnit();
            this.selectAll(selectionUndoUnit, null);
            this.undoRedoService.Add(selectionUndoUnit);
            this.Refresh();
            this.UpdateHoveredItem(this.currentPosition);
            this.UpdateCursor();
        };
        Diagram.prototype.Redo = function () {
            this.undoRedoService.Redo();
            this.Refresh();
            this.UpdateHoveredItem(this.currentPosition);
            this.UpdateCursor();
        };
        Diagram.prototype.RecreateLastUsedShape = function () {
            var shape = new Shape(this.lastUsedShapeTemplate, this.currentPosition);
            var unit = new AddShapeUnit(shape, this);
            this.undoRedoService.Add(unit);
        };
        Diagram.prototype.RemoveConnection = /**
        * Removes the given connection from the diagram.
        */
        function (con) {
            con.IsSelected = false;
            con.From.Connections.remove(con);
            if (con.To != null) {
                con.To.Connections.remove(con);
            }
            con.Diagram = null;
            this.Connections.remove(con);
            this.mainLayer.Remove(con.Visual);
        };
        Diagram.prototype.RemoveShape = function (shape) {
            shape.Diagram = null;
            shape.IsSelected = false;
            this.shapes.remove(shape);
            this.mainLayer.Remove(shape.Visual);
        };
        Diagram.prototype.setElementContent = function (element, content) {
            this.undoRedoService.Add(new ContentChangedUndoUnit(element, content));
            this.Refresh();
        };
        Diagram.prototype.DeleteCurrentSelection = function (undoable) {
            if (typeof undoable === "undefined") { undoable = true; }
            if (undoable) {
                this.undoRedoService.begin();
            }
            var deletedConnections = [];
            for (var i = 0; i < this.shapes.length; i++) {
                var shape = this.shapes[i];
                for (var j = 0; j < shape.Connectors.length; j++) {
                    var connector = shape.Connectors[j];
                    for (var k = 0; k < connector.Connections.length; k++) {
                        var connection = connector.Connections[k];
                        if ((shape.IsSelected || connection.IsSelected) && (!deletedConnections.contains(connection))) {
                            if (undoable) {
                                this.undoRedoService.AddCompositeItem(new DeleteConnectionUnit(connection));
                            }
                            deletedConnections.push(connection);
                        }
                    }
                }
            }
            //if not undoable; cannot alter the collection or the loop will be biased
            if (!undoable && deletedConnections.length > 0) {
                for (var i = 0; i < deletedConnections.length; i++) {
                    var connection = deletedConnections[i];
                    this.RemoveConnection(connection);
                }
            }
            for (var i = 0; i < this.shapes.length; i++) {
                var shape = this.shapes[i];
                if (shape.IsSelected) {
                    if (undoable) {
                        this.undoRedoService.AddCompositeItem(new DeleteShapeUnit(shape));
                    } else {
                        this.RemoveShape(shape);
                    }
                }
            }
            if (undoable) {
                this.undoRedoService.commit();
            }
        };
        Diagram.prototype.MouseDown = /**
        * The mouse down logic.
        */
        function (e) {
            this.Focus();
            e.preventDefault();
            this.UpdateCurrentPosition(e);
            if (e.button === 0) {
                // alt+click allows fast creation of element using the active template
                if ((this.newItem === null) && (e.altKey)) {
                    this.RecreateLastUsedShape();
                } else {
                    this.Down(e);
                }
            }
        };
        Diagram.prototype.MouseUp = /**
        * The mouse up logic.
        */
        function (e) {
            e.preventDefault();
            this.UpdateCurrentPosition(e);
            if (e.button === 0) {
                this.Up();
            }
        };
        Diagram.prototype.MouseMove = /**
        * The mouse MoveTo logic.
        */
        function (e) {
            e.preventDefault();
            this.UpdateCurrentPosition(e);
            this.Move();
        };
        Diagram.prototype.doubleClick = function (e) {
            e.preventDefault();
            this.UpdateCurrentPosition(e);
            if (e.button === 0)// left-click
            {
                var point = this.currentPosition;
                this.UpdateHoveredItem(point);
                if ((this.hoveredItem != null) && (this.hoveredItem instanceof Shape)) {
                    var item = this.hoveredItem;
                    if ((item.Template != null) && ("edit" in item.Template)) {
                        item.Template.Edit(item, this.canvas, point);
                        this.Refresh();
                    }
                }
            }
        };
        Diagram.prototype.touchStart = function (e) {
            if (e.touches.length == 1) {
                e.preventDefault();
                this.UpdateCurrentTouchPosition(e);
                this.Down(e);
            }
        };
        Diagram.prototype.touchEnd = function (e) {
            e.preventDefault();
            this.Up();
        };
        Diagram.prototype.touchMove = function (e) {
            if (e.touches.length == 1) {
                e.preventDefault();
                this.UpdateCurrentTouchPosition(e);
                this.Move();
            }
        };
        Diagram.prototype.Down = /**
        * The actual mouse down logic.
        */
        function (e) {
            var p = this.currentPosition;
            if (this.newItem != null) {
                this.undoRedoService.begin();
                this.newItem.Rectangle = new svg.Rect(p.X, p.Y, this.newItem.Rectangle.Width, this.newItem.Rectangle.Height);
                this.newItem.Invalidate();
                this.undoRedoService.AddCompositeItem(new AddShapeUnit(this.newItem, this));
                this.undoRedoService.commit();
                this.newItem = null;
            } else {
                this.selector.End();
                this.UpdateHoveredItem(p);
                if (this.hoveredItem === null) {
                    var ev = window.event || e;
                    if (ev.ctrlKey == true) {
                        //pan
                        this.isPanning = true;
                        this.panStart = this.Pan;
                        this.panOffset = p// new svg.Point(p.X - this.panStart.X, p.Y + this.panStart.Y);
                        ;
                        this.panDelta = svg.Point.Empty//relative to root
                        ;
                    } else {
                        // Start selection
                        this.selector.Start(p);
                    }
                } else {
                    // Start connection
                    if ((this.hoveredItem instanceof Connector) && (!this.isShiftPressed)) {
                        var connector = this.hoveredItem;
                        //console.log("Starting a new connection from " + connector.Template.Name);
                        if (connector.CanConnectTo(null)) {
                            this.newConnection = this.AddConnection(connector, null);
                            this.newConnection.UpdateEndPoint(p);
                        }
                    } else {
                        // select object
                        var item = this.hoveredItem;
                        if (!item.IsSelected) {
                            this.undoRedoService.begin();
                            var selectionUndoUnit = new SelectionUndoUnit();
                            if (!this.isShiftPressed) {
                                this.DeselectAll(selectionUndoUnit);
                            }
                            selectionUndoUnit.select(item);
                            this.undoRedoService.AddCompositeItem(selectionUndoUnit);
                            this.undoRedoService.commit();
                        } else if (this.isShiftPressed) {
                            this.undoRedoService.begin();
                            var deselectUndoUnit = new SelectionUndoUnit();
                            deselectUndoUnit.deselect(item);
                            this.undoRedoService.AddCompositeItem(deselectUndoUnit);
                            this.undoRedoService.commit();
                        }
                        // seems we are transforming things
                        var hit = new svg.Point(0, 0);
                        if (this.hoveredItem instanceof Shape) {
                            var element = this.hoveredItem;
                            hit = element.Adorner.HitTest(p);
                        }
                        for (var i = 0; i < this.shapes.length; i++) {
                            var shape = this.shapes[i];
                            if (shape.Adorner != null) {
                                shape.Adorner.Start(p, hit);
                            }
                        }
                        this.isManipulating = true;
                    }
                }
            }
            this.Refresh();
            this.UpdateCursor();
        };
        Diagram.prototype.Move = /**
        * The actual mouse MoveTo logic.
        */
        function () {
            var p = this.currentPosition;
            if (this.newItem != null) {
                // placing new element
                this.newItem.Rectangle = new svg.Rect(p.X, p.Y, this.newItem.Rectangle.Width, this.newItem.Rectangle.Height);
                this.newItem.Invalidate();
            }
            if (this.isPanning) {
                this.panDelta = new svg.Point(this.panDelta.X + p.X - this.panOffset.X, this.panDelta.Y + p.Y - this.panOffset.Y);
                this.Pan = new svg.Point(this.panStart.X + this.panDelta.X, this.panStart.Y + this.panDelta.Y);
                this.Canvas.Cursor = Cursors.MoveTo;
                return;
            }
            if (this.isManipulating) {
                // moving IsSelected elements
                for (var i = 0; i < this.shapes.length; i++) {
                    var shape = this.shapes[i];
                    if (shape.Adorner != null) {
                        shape.Adorner.MoveTo(p);
                        // this will also repaint the visual
                        shape.Rectangle = shape.Adorner.Rectangle;
                    }
                }
            }
            if (this.newConnection != null) {
                // connecting two connectors
                this.newConnection.UpdateEndPoint(p);
                this.newConnection.Invalidate();
            }
            if (this.selector != null) {
                this.selector.updateCurrentPoint(p);
            }
            this.UpdateHoveredItem(p);
            this.Refresh();
            this.UpdateCursor();
        };
        Diagram.prototype.Up = /**
        * The actual mouse up logic.
        */
        function () {
            var point = this.currentPosition;
            if (this.isPanning) {
                this.isPanning = false;
                this.Canvas.Cursor = Cursors.arrow;
                var unit = new PanUndoUnit(this.panStart, this.Pan, this);
                this.undoRedoService.Add(unit);
                return;
            }
            if (this.newConnection != null) {
                this.UpdateHoveredItem(point);
                this.newConnection.Invalidate();
                if ((this.hoveredItem != null) && (this.hoveredItem instanceof Connector)) {
                    var connector = this.hoveredItem;
                    if ((connector != this.newConnection.From) && (connector.CanConnectTo(this.newConnection.From))) {
                        this.newConnection.To = connector;
                        this.undoRedoService.Add(new AddConnectionUnit(this.newConnection, this.newConnection.From, connector));
                        console.log("Connection established.");
                    } else {
                        this.RemoveConnection(this.newConnection);
                    }//remove temp connection

                } else {
                    this.RemoveConnection(this.newConnection);
                }
                this.newConnection = null;
            }
            if (this.selector.IsActive) {
                this.undoRedoService.begin();
                var selectionUndoUnit = new SelectionUndoUnit();
                var rectangle = this.selector.Rectangle;
                var selectable = this.hoveredItem;
                if (((this.hoveredItem === null) || (!selectable.IsSelected)) && !this.isShiftPressed) {
                    this.DeselectAll(selectionUndoUnit);
                }
                if ((rectangle.Width != 0) || (rectangle.Height != 0)) {
                    this.selectAll(selectionUndoUnit, rectangle);
                }
                this.undoRedoService.AddCompositeItem(selectionUndoUnit);
                this.undoRedoService.commit();
                this.selector.End();
            }
            if (this.isManipulating) {
                this.undoRedoService.begin();
                for (var i = 0; i < this.shapes.length; i++) {
                    var shape = this.shapes[i];
                    if (shape.Adorner != null) {
                        shape.Adorner.Stop();
                        shape.Invalidate();
                        var r1 = shape.Adorner.InitialState;
                        var r2 = shape.Adorner.FinalState;
                        if ((r1.X != r2.X) || (r1.Y != r2.Y) || (r1.Width != r2.Width) || (r1.Height != r2.Height)) {
                            this.undoRedoService.AddCompositeItem(new TransformUnit(shape, r1, r2));
                        }
                    }
                }
                this.undoRedoService.commit();
                this.isManipulating = false;
                this.UpdateHoveredItem(point);
            }
            this.Refresh();
            this.UpdateCursor();
        };
        Diagram.prototype.KeyDown = function (e) {
            if (!this.isFirefox) {
                this.ProcessKey(e, e.keyCode);
            }
        };
        Diagram.prototype.KeyPress = function (e) {
            //if (this.isFirefox)
            {
                if (typeof this.keyCodeTable === "undefined") {
                    this.keyCodeTable = [];
                    var charCodeTable = {
                        32: ' ',
                        48: '0',
                        49: '1',
                        50: '2',
                        51: '3',
                        52: '4',
                        53: '5',
                        54: '6',
                        55: '7',
                        56: '8',
                        57: '9',
                        59: ';',
                        61: '=',
                        65: 'a',
                        66: 'b',
                        67: 'c',
                        68: 'd',
                        69: 'e',
                        70: 'f',
                        71: 'g',
                        72: 'h',
                        73: 'i',
                        74: 'j',
                        75: 'k',
                        76: 'l',
                        77: 'm',
                        78: 'n',
                        79: 'o',
                        80: 'p',
                        81: 'q',
                        82: 'r',
                        83: 's',
                        84: 't',
                        85: 'u',
                        86: 'v',
                        87: 'w',
                        88: 'x',
                        89: 'y',
                        90: 'z',
                        107: '+',
                        109: '-',
                        110: '.',
                        188: ',',
                        190: '.',
                        191: '/',
                        192: '`',
                        219: '[',
                        220: '\\',
                        221: ']',
                        222: '\"'
                    };
                    for (var keyCode in charCodeTable) {
                        var key = charCodeTable[keyCode];
                        this.keyCodeTable[key.charCodeAt(0)] = keyCode;
                        if (key.toUpperCase() != key) {
                            this.keyCodeTable[key.toUpperCase().charCodeAt(0)] = keyCode;
                        }
                    }
                }
                this.ProcessKey(e, (this.keyCodeTable[e.charCode] != null) ? this.keyCodeTable[e.charCode] : e.keyCode);
            }
        };
        Diagram.prototype.keyUp = function (e) {
            this.UpdateCursor();
        };
        Diagram.prototype.ProcessKey = function (e, keyCode) {
            if ((e.ctrlKey || e.metaKey) && !e.altKey)// ctrl or option
            {
                if (keyCode == 65)// A: select all
                {
                    this.SelectAll();
                    this.stopEvent(e);
                }
                if ((keyCode == 90) && (!e.shiftKey))// Z: undo
                {
                    this.Undo();
                    this.stopEvent(e);
                }
                if (((keyCode == 90) && (e.shiftKey)) || (keyCode == 89))// Y: redo
                {
                    this.Redo();
                    this.stopEvent(e);
                }
            }
            if ((keyCode == 46) || (keyCode == 8))// del: deletion
            {
                this.Delete(true);
                this.stopEvent(e);
            }
            if (keyCode == 27)// ESC: stop any action
            {
                this.newItem = null;
                if (this.newConnection != null) {
                    this.RemoveConnection(this.newConnection);
                    this.newConnection = null;
                }
                this.isManipulating = false;
                for (var i = 0; i < this.shapes.length; i++) {
                    var element = this.shapes[i];
                    if (element.Adorner != null) {
                        element.Adorner.Stop();
                    }
                }
                this.Refresh();
                this.UpdateHoveredItem(this.currentPosition);
                this.UpdateCursor();
                this.stopEvent(e);
            }
        };
        Diagram.prototype.stopEvent = function (e) {
            e.preventDefault();
            e.stopPropagation();
        };
        Diagram.prototype.selectAll = /**
        * Selects all items of the diagram.
        */
        function (selectionUndoUnit, r) {
            for (var i = 0; i < this.shapes.length; i++) {
                var element = this.shapes[i];
                if ((r === null) || (element.HitTest(r))) {
                    selectionUndoUnit.select(element);
                }
                for (var j = 0; j < element.Connectors.length; j++) {
                    var connector = element.Connectors[j];
                    for (var k = 0; k < connector.Connections.length; k++) {
                        var connection = connector.Connections[k];
                        if ((r === null) || (connection.HitTest(r))) {
                            selectionUndoUnit.select(connection);
                        }
                    }
                }
            }
        };
        Diagram.prototype.DeselectAll = /**
        * Unselects all items.
        */
        function (selectionUndoUnit) {
            for (var i = 0; i < this.shapes.length; i++) {
                var item = this.shapes[i];
                selectionUndoUnit.deselect(item);
                for (var j = 0; j < item.Connectors.length; j++) {
                    var connector = item.Connectors[j];
                    for (var k = 0; k < connector.Connections.length; k++) {
                        selectionUndoUnit.deselect(connector.Connections[k]);
                    }
                }
            }
        };
        Diagram.prototype.UpdateHoveredItem = /**
        * Refreshed the current hovered item given the current location of the cursor.
        */
        function (p) {
            var hitObject = this.HitTest(p);
            if (hitObject != this.hoveredItem) {
                if (this.hoveredItem != null) {
                    this.hoveredItem.IsHovered = false;
                }
                this.hoveredItem = hitObject;
                if (this.hoveredItem != null) {
                    this.hoveredItem.IsHovered = true;
                }
            }
            //if (this.hoveredItem != null)
            //    console.log("hoveredItem:" + this.hoveredItem.toString());
        };
        Diagram.prototype.HitTest = /**
        * Detects the item underneath the given location.
        */
        function (point) {
            var rectangle = new svg.Rect(point.X, point.Y, 0, 0);
            // connectors
            for (var i = 0; i < this.shapes.length; i++) {
                var item = this.shapes[i];
                for (var j = 0; j < item.Connectors.length; j++) {
                    var connector = item.Connectors[j];
                    if (connector.HitTest(rectangle)) {
                        return connector;
                    }
                }
            }
            // shapes
            for (var i = 0; i < this.shapes.length; i++) {
                var item = this.shapes[i];
                if (item.HitTest(rectangle)) {
                    return item;
                }
            }
            // connections
            for (var i = 0; i < this.shapes.length; i++) {
                var item = this.shapes[i];
                for (var j = 0; j < item.Connectors.length; j++) {
                    var connector = item.Connectors[j];
                    for (var k = 0; k < connector.Connections.length; k++) {
                        var connection = connector.Connections[k];
                        if (connection.HitTest(rectangle)) {
                            return connection;
                        }
                    }
                }
            }
            return null;
        };
        Diagram.prototype.UpdateCursor = /**
        * Sets the cursors in function of the currently hovered item.
        */
        function () {
            if (this.newConnection != null) {
                this.canvas.Cursor = ((this.hoveredItem != null) && (this.hoveredItem instanceof Connector)) ? this.hoveredItem.GetCursor(this.currentPosition) : Cursors.cross;
            } else {
                this.canvas.Cursor = (this.hoveredItem != null) ? this.hoveredItem.GetCursor(this.currentPosition) : Cursors.arrow;
            }
        };
        Diagram.prototype.UpdateCurrentPosition = /*
        * Update the current position of the mouse to the local coordinate system.
        */
        function (e) {
            this.isShiftPressed = e.shiftKey;
            this.currentPosition = new svg.Point(e.pageX - this.pan.X, e.pageY - this.pan.Y);
            var node = this.div;
            // wished there was an easier way to do this
            while (node != null) {
                this.currentPosition.X -= node.offsetLeft;
                this.currentPosition.Y -= node.offsetTop;
                node = node.offsetParent;
            }
            this.currentPosition.X /= this.Zoom;
            this.currentPosition.Y /= this.Zoom;
            //console.log(this.currentPosition.toString());
        };
        Diagram.prototype.UpdateCurrentTouchPosition = function (e) {
            this.isShiftPressed = false;
            this.currentPosition = new svg.Point(e.touches[0].pageX, e.touches[0].pageY);
            var node = this.div;
            while (node != null) {
                this.currentPosition.X -= node.offsetLeft;
                this.currentPosition.Y -= node.offsetTop;
                node = node.offsetParent;
            }
        };
        Diagram.prototype.Refresh = function () {
            var connections = [];
            for (var i = 0; i < this.shapes.length; i++) {
                var item = this.shapes[i];
                for (var j = 0; j < item.Connectors.length; j++) {
                    var connector = item.Connectors[j];
                    for (var k = 0; k < connector.Connections.length; k++) {
                        var connection = connector.Connections[k];
                        if (!connections.contains(connection)) {
                            connection.paint(this.canvas);
                            connections.push(connection);
                        }
                    }
                }
            }
            for (var i = 0; i < this.shapes.length; i++) {
                this.shapes[i].paint(this.canvas);
            }
            for (var i = 0; i < this.shapes.length; i++) {
                var item = this.shapes[i];
                for (var j = 0; j < item.Connectors.length; j++) {
                    var connector = item.Connectors[j];
                    var IsHovered = false;
                    for (var k = 0; k < connector.Connections.length; k++) {
                        if (connector.Connections[k].IsHovered) {
                            IsHovered = true;
                        }
                    }
                    if ((item.IsHovered) || (connector.IsHovered) || IsHovered) {
                        connector.Invalidate((this.newConnection != null) ? this.newConnection.From : null);
                    } else if ((this.newConnection != null) && (connector.CanConnectTo(this.newConnection.From))) {
                        connector.Invalidate(this.newConnection.From);
                    }
                }
            }
            if (this.newItem != null) {
                this.newItem.paint(this.canvas);
            }
            if (this.newConnection != null) {
                this.newConnection.paintAdorner(this.canvas);
            }
            if (this.selector.IsActive) {
                this.selector.paint(this.canvas);
            }
        };
        return Diagram;
    })();
    RadDiagram.Diagram = Diagram;
    /**
    * Mapping of logical cursors to actual cursors.
    */
    var Cursors = (function () {
        function Cursors() { }
        Cursors.arrow = "default";
        Cursors.grip = "pointer";
        Cursors.cross = "pointer";
        Cursors.add = "pointer";
        Cursors.MoveTo = "move";
        Cursors.select = "pointer";
        return Cursors;
    })();
    RadDiagram.Cursors = Cursors;
    /**
    * The diagramming connection.
    */
    var Connection = (function () {
        function Connection(from, to) {
            this.toPoint = null;
            this.fromConnector = from;
            this.toConnector = to;
            this.createVisual();
        }
        Object.defineProperty(Connection.prototype, "Visual", {
            get: function () {
                return this.visual;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Connection.prototype, "Content", {
            set: function (v) {
                if (v == null) {
                    this.removeContent();
                }
                var tb = new svg.TextBlock();
                tb.dy = -5;
                tb.Text = v.toString();
                this.contentVisual = tb;
                this.visual.Append(this.contentVisual);
                this.Invalidate();
            },
            enumerable: true,
            configurable: true
        });
        Connection.prototype.removeContent = function () {
            if (this.contentVisual == null) {
                return;
            }
            this.visual.Remove(this.contentVisual);
            this.contentVisual = null;
        };
        Connection.prototype.createVisual = function () {
            var g = new svg.Group();//the group contains the line and the label

            this.line = new svg.Line();
            this.line.Stroke = "Green";
            g.Append(this.line);
            this.visual = g;
            this.updateCoordinates();
            this.unselectedColor = this.line.Stroke;
            this.line.StrokeThickness = 1;
        };
        Connection.prototype.updateCoordinates = function () {
            if (this.toConnector == null) {
                // means we are dragging a new connection
                if (this.toPoint == null || isNaN(this.toPoint.X) || isNaN(this.toPoint.Y)) {
                    return;
                }
                var globalSourcePoint = this.fromConnector.Parent.GetConnectorPosition(this.fromConnector);
                var globalSinkPoint = this.toPoint;
                var bounds = svg.Rect.FromPoints(globalSourcePoint, globalSinkPoint);
                var localSourcePoint = globalSourcePoint.Minus(bounds.TopLeft);
                var localSinkPoint = globalSinkPoint.Minus(bounds.TopLeft);
                this.line.From = localSourcePoint//local coordinate!
                ;
                this.line.To = localSinkPoint//local coordinate!
                ;
                this.visual.Position = bounds.TopLeft//global coordinates!
                ;
                return;
            }
            var globalSourcePoint = this.fromConnector.Parent.GetConnectorPosition(this.fromConnector);
            var globalSinkPoint = this.toConnector.Parent.GetConnectorPosition(this.toConnector);
            var bounds = svg.Rect.FromPoints(globalSourcePoint, globalSinkPoint);
            var localSourcePoint = globalSourcePoint.Minus(bounds.TopLeft);
            var localSinkPoint = globalSinkPoint.Minus(bounds.TopLeft);
            this.line.From = localSourcePoint//local coordinate!
            ;
            this.line.To = localSinkPoint//local coordinate!
            ;
            this.visual.Position = bounds.TopLeft//global coordinates!
            ;
            if (this.contentVisual != null) {
                var m = svg.Point.MiddleOf(localSourcePoint, localSinkPoint);
                this.contentVisual.Position = m;
                var p = localSinkPoint.Minus(localSourcePoint);
                var tr = this.contentVisual.Native.ownerSVGElement.createSVGTransform();
                tr.setRotate(p.ToPolar(true).Angle, m.X, m.Y);
                var tb = this.contentVisual.Native;
                if (tb.transform.baseVal.numberOfItems == 0) {
                    tb.transform.baseVal.appendItem(tr);
                } else {
                    tb.transform.baseVal.replaceItem(tr, 0);
                }
            }
        };
        Connection.prototype.updateVisual = function () {
            this.updateCoordinates();
            if (this.isSelected) {
                this.line.Stroke = "Orange";
                this.line.StrokeThickness = 2;
                if (this.EndCap != null) {
                    this.EndCap.Color = "Orange";
                }
                if (this.StartCap != null) {
                    this.StartCap.Color = "Orange";
                }
            } else {
                this.line.Stroke = this.unselectedColor;
                this.line.StrokeThickness = 1;
                if (this.EndCap != null) {
                    this.EndCap.Color = this.unselectedColor;
                }
                if (this.StartCap != null) {
                    this.StartCap.Color = this.unselectedColor;
                }
            }
        };
        Connection.prototype.updateContent = function () {
        };
        Object.defineProperty(Connection.prototype, "EndCap", {
            get: function () {
                return this.endCap;
            },
            set: function (marker) {
                if (marker == null) {
                    throw "Given Marker is null.";
                }
                if (marker.Id == null) {
                    throw "Given Marker has no Id.";
                }
                marker.Color = this.Stroke;
                this.Diagram.AddMarker(marker);
                this.line.MarkerEnd = marker;
                this.endCap = marker;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Connection.prototype, "StartCap", {
            get: function () {
                return this.startCap;
            },
            set: function (marker) {
                if (marker == null) {
                    throw "Given Marker is null.";
                }
                if (marker.Id == null) {
                    throw "Given Marker has no Id.";
                }
                marker.Color = this.Stroke;
                this.Diagram.AddMarker(marker);
                this.line.MarkerStart = marker;
                this.startCap = marker;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Connection.prototype, "Stroke", {
            get: function () {
                return this.line.Stroke;
            },
            set: function (value) {
                this.line.Stroke = value;
                this.unselectedColor = value;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Connection.prototype, "StrokeDash", {
            get: function () {
                return this.line.StrokeDash;
            },
            set: function (value) {
                this.line.StrokeDash = value;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Connection.prototype, "From", {
            get: /**
            *  Gets the source connector.
            */
            function () {
                return this.fromConnector;
            },
            set: /**
            * Sets the source connector.
            */
            function (v) {
                this.fromConnector = v;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Connection.prototype, "To", {
            get: /**
            *  Gets the sink connector.
            */
            function () {
                return this.toConnector;
            },
            set: /**
            * Sets the target connector.
            */
            function (v) {
                this.toConnector = v;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Connection.prototype, "IsSelected", {
            get: /**
            *  Gets whether this connection is selected.
            */
            function () {
                return this.isSelected;
            },
            set: /**
            *  Sets whether this connection is selected.
            */
            function (value) {
                this.isSelected = value;
                this.Invalidate();
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Connection.prototype, "IsHovered", {
            get: /**
            *  Gets whether this connection is hovered.
            */
            function () {
                return this.isHovered;
            },
            set: /**
            *  Sets whether this connection is hovered.
            */
            function (value) {
                this.isHovered = value;
            },
            enumerable: true,
            configurable: true
        });
        Connection.prototype.UpdateEndPoint = function (toPoint) {
            this.toPoint = toPoint;
            this.updateCoordinates();
        };
        Connection.prototype.GetCursor = function (point) {
            return Cursors.select;
        };
        Connection.prototype.HitTest = function (rectangle) {
            if ((this.From != null) && (this.To != null)) {
                var p1 = this.From.Parent.GetConnectorPosition(this.From);
                var p2 = this.To.Parent.GetConnectorPosition(this.To);
                if ((rectangle.Width != 0) || (rectangle.Width != 0)) {
                    return (rectangle.Contains(p1) && rectangle.Contains(p2));
                }
                var p = rectangle.TopLeft;
                // p1 must be the leftmost point
                if (p1.X > p2.X) {
                    var temp = p2;
                    p2 = p1;
                    p1 = temp;
                }
                var r1 = new svg.Rect(p1.X, p1.Y, 0, 0);
                var r2 = new svg.Rect(p2.X, p2.Y, 0, 0);
                r1.Inflate(3, 3);
                r2.Inflate(3, 3);
                if (r1.Union(r2).Contains(p)) {
                    if ((p1.X == p2.X) || (p1.Y == p2.Y)) {
                        return true;
                    } else if (p1.Y < p2.Y) {
                        var o1 = r1.X + (((r2.X - r1.X) * (p.Y - (r1.Y + r1.Height))) / ((r2.Y + r2.Height) - (r1.Y + r1.Height)));
                        var u1 = (r1.X + r1.Width) + ((((r2.X + r2.Width) - (r1.X + r1.Width)) * (p.Y - r1.Y)) / (r2.Y - r1.Y));
                        return ((p.X > o1) && (p.X < u1));
                    } else {
                        var o2 = r1.X + (((r2.X - r1.X) * (p.Y - r1.Y)) / (r2.Y - r1.Y));
                        var u2 = (r1.X + r1.Width) + ((((r2.X + r2.Width) - (r1.X + r1.Width)) * (p.Y - (r1.Y + r1.Height))) / ((r2.Y + r2.Height) - (r1.Y + r1.Height)));
                        return ((p.X > o2) && (p.X < u2));
                    }
                }
            }
            return false;
        };
        Connection.prototype.Invalidate = function () {
            this.updateVisual();
        };
        Connection.prototype.paint = function (context) {
            //context.strokeStyle = this.From.Parent.graph.theme.connection;
            //context.lineWidth = (this.isHovered) ? 2 : 1;
            //this.paintLine(context, this.isSelected);
        };
        Connection.prototype.paintAdorner = function (context) {
            //context.strokeStyle = this.From.Parent.graph.theme.connection;
            //context.lineWidth = 1;
            this.paintLine(context, true);
        };
        Connection.prototype.paintLine = function (context, dashed) {
            if (this.From != null) {
                var Start = this.From.Parent.GetConnectorPosition(this.From);
                var end = (this.To != null) ? this.To.Parent.GetConnectorPosition(this.To) : this.toPoint;
                //if ((Start.X != end.X) || (Start.Y != end.Y))
                //{
                //    context.beginPath();
                //    if (dashed)
                //    {
                //        LineHelper.dashedLine(context, Start.X, Start.Y, end.X, end.Y);
                //    }
                //    else
                //    {
                //        context.moveTo(Start.X - 0.5, Start.Y - 0.5);
                //        context.lineTo(end.X - 0.5, end.Y - 0.5);
                //    }
                //    context.closePath();
                //    context.stroke();
                //}
            }
        };
        return Connection;
    })();
    RadDiagram.Connection = Connection;
    /**
    * The intermediate between a shape and a connection, aka port.
    */
    var Connector = (function () {
        function Connector(parent, template) {
            this.connections = [];
            this.isHovered = false;
            this.parent = parent;
            this.template = template;
        }
        Object.defineProperty(Connector.prototype, "Parent", {
            get: function () {
                return this.parent;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Connector.prototype, "Template", {
            get: /*
            * Gets the template of this connector
            */
            function () {
                return this.template;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Connector.prototype, "Connections", {
            get: function () {
                return this.connections;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Connector.prototype, "Background", {
            set: function (value) {
                this.Visual.Native.setAttribute("fill", value);
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Connector.prototype, "IsHovered", {
            get: function () {
                return this.isHovered;
            },
            set: function (value) {
                this.isHovered = value;
                this.IsVisible = value;
                this.Background = value ? "Green" : "Black";
            },
            enumerable: true,
            configurable: true
        });
        Connector.prototype.GetCursor = function (point) {
            return Cursors.grip;
        };
        Connector.prototype.HitTest = function (r) {
            if ((r.Width === 0) && (r.Height === 0)) {
                return this.Rectangle.Contains(r.TopLeft);
            }
            return r.Contains(this.Rectangle.TopLeft);
        };
        Object.defineProperty(Connector.prototype, "IsVisible", {
            get: function () {
                return (this.Visual.Native.attributes["visibility"] == null) ? true : this.Visual.Native.attributes["visibility"].value == "visible";
            },
            set: function (value) {
                if (value) {
                    this.Visual.Native.setAttribute("visibility", "visible");
                } else {
                    this.Visual.Native.setAttribute("visibility", "hidden");
                }
            },
            enumerable: true,
            configurable: true
        });
        Connector.prototype.Invalidate = function (other) {
            var r = this.Rectangle;
            var strokeStyle = this.parent.Diagram.Theme.connectorBorder;
            var fillStyle = this.parent.Diagram.Theme.connector;
            if (this.isHovered) {
                strokeStyle = this.parent.Diagram.Theme.connectorHoverBorder;
                fillStyle = this.parent.Diagram.Theme.connectorHover;
                if (other != null && !this.CanConnectTo(other)) {
                    fillStyle = "#f00";
                }
            }
            this.Visual.Native.setAttribute("fill", fillStyle);
        };
        Connector.prototype.CanConnectTo = function (other) {
            if (other === this) {
                return false;
            }
            if (other == null) {
                return true;
            }
            return this.Template.CanConnectTo(other);
            //var t1: string[] = this.template.Type.split(' ');
            //if (!t1.contains("[array]") && (this.connections.length == 1)) return false;
            //if (connector instanceof Connector)
            //{
            //    var t2: string[] = connector.template.Type.split(' ');
            //    if ((t1[0] != t2[0]) ||
            //        (this.parent == connector.parent) ||
            //        (t1.contains("[in]") && !t2.contains("[out]")) ||
            //        (t1.contains("[out]") && !t2.contains("[in]")) ||
            //        (!t2.contains("[array]") && (connector.connections.length == 1)))
            //    {
            //        return false;
            //    }
            //}
        };
        Connector.prototype.toString = function () {
            return "Connector";
        };
        Object.defineProperty(Connector.prototype, "Rectangle", {
            get: function () {
                var point = this.parent.GetConnectorPosition(this);
                var rectangle = new svg.Rect(point.X, point.Y, 0, 0);
                rectangle.Inflate(3, 3);
                return rectangle;
            },
            enumerable: true,
            configurable: true
        });
        return Connector;
    })();
    RadDiagram.Connector = Connector;
    var CompositeUnit = (function () {
        function CompositeUnit(unit) {
            if (typeof unit === "undefined") { unit = null; }
            this.units = [];
            if (unit != null) {
                this.units.push(unit);
            }
        }
        CompositeUnit.prototype.add = function (undoUnit) {
            this.units.push(undoUnit);
        };
        CompositeUnit.prototype.Undo = function () {
            for (var i = 0; i < this.units.length; i++) {
                this.units[i].Undo();
            }
        };
        CompositeUnit.prototype.Redo = function () {
            for (var i = 0; i < this.units.length; i++) {
                this.units[i].Redo();
            }
        };
        Object.defineProperty(CompositeUnit.prototype, "Title", {
            get: function () {
                return "Composite unit";
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(CompositeUnit.prototype, "IsEmpty", {
            get: function () {
                if (this.units.length > 0) {
                    for (var i = 0; i < this.units.length; i++) {
                        if (!this.units[i].IsEmpty) {
                            return false;
                        }
                    }
                }
                return true;
            },
            enumerable: true,
            configurable: true
        });
        return CompositeUnit;
    })();
    RadDiagram.CompositeUnit = CompositeUnit;
    ;
    var ContentChangedUndoUnit = (function () {
        function ContentChangedUndoUnit(element, content) {
            this.item = element;
            this._undoContent = element.Content;
            this._redoContent = content;
        }
        Object.defineProperty(ContentChangedUndoUnit.prototype, "Title", {
            get: function () {
                return "Content Editing";
            },
            enumerable: true,
            configurable: true
        });
        ContentChangedUndoUnit.prototype.Undo = function () {
            this.item.Content = this._undoContent;
        };
        ContentChangedUndoUnit.prototype.Redo = function () {
            this.item.Content = this._redoContent;
        };
        Object.defineProperty(ContentChangedUndoUnit.prototype, "IsEmpty", {
            get: function () {
                return false;
            },
            enumerable: true,
            configurable: true
        });
        return ContentChangedUndoUnit;
    })();
    RadDiagram.ContentChangedUndoUnit = ContentChangedUndoUnit;
    /**
    * An undo-redo unit handling the deletion of a connection.
    */
    var DeleteConnectionUnit = (function () {
        function DeleteConnectionUnit(connection) {
            this.connection = connection;
            this.diagram = connection.Diagram;
            this.from = connection.From;
            this.to = connection.To;
        }
        Object.defineProperty(DeleteConnectionUnit.prototype, "Title", {
            get: function () {
                return "Delete connection";
            },
            enumerable: true,
            configurable: true
        });
        DeleteConnectionUnit.prototype.Undo = function () {
            this.diagram.AddConnection(this.connection);
        };
        DeleteConnectionUnit.prototype.Redo = function () {
            this.diagram.RemoveConnection(this.connection);
        };
        Object.defineProperty(DeleteConnectionUnit.prototype, "IsEmpty", {
            get: function () {
                return false;
            },
            enumerable: true,
            configurable: true
        });
        return DeleteConnectionUnit;
    })();
    RadDiagram.DeleteConnectionUnit = DeleteConnectionUnit;
    /**
    * An undo-redo unit handling the deletion of a diagram element.
    */
    var DeleteShapeUnit = (function () {
        function DeleteShapeUnit(shape) {
            this.shape = shape;
            this.diagram = shape.Diagram;
        }
        Object.defineProperty(DeleteShapeUnit.prototype, "Title", {
            get: function () {
                return "Deletion";
            },
            enumerable: true,
            configurable: true
        });
        DeleteShapeUnit.prototype.Undo = function () {
            this.diagram.AddItem(this.shape);
            this.shape.IsSelected = false;
        };
        DeleteShapeUnit.prototype.Redo = function () {
            this.shape.IsSelected = false;
            this.diagram.RemoveShape(this.shape);
        };
        Object.defineProperty(DeleteShapeUnit.prototype, "IsEmpty", {
            get: function () {
                return false;
            },
            enumerable: true,
            configurable: true
        });
        return DeleteShapeUnit;
    })();
    RadDiagram.DeleteShapeUnit = DeleteShapeUnit;
    /**
    * An undo-redo unit handling the transformation of a diagram element.
    */
    var TransformUnit = (function () {
        function TransformUnit(shape, undoRectangle, redoRectangle) {
            this.shape = shape;
            this.undoRectangle = undoRectangle.Clone();
            this.redoRectangle = redoRectangle.Clone();
        }
        Object.defineProperty(TransformUnit.prototype, "Title", {
            get: function () {
                return "Transformation";
            },
            enumerable: true,
            configurable: true
        });
        TransformUnit.prototype.Undo = function () {
            // if (this.shape.IsSelected) this.shape.Adorner.Rectangle = this.undoRectangle;
            this.shape.Rectangle = this.undoRectangle;
            this.shape.Invalidate();
        };
        TransformUnit.prototype.Redo = function () {
            //if (this.shape.IsSelected)
            //{
            //    this.shape.Adorner.Rectangle = this.redoRectangle;
            //    this.shape.Adorner.paint
            //}
            this.shape.Rectangle = this.redoRectangle;
            this.shape.Invalidate();
        };
        Object.defineProperty(TransformUnit.prototype, "IsEmpty", {
            get: function () {
                return false;
            },
            enumerable: true,
            configurable: true
        });
        return TransformUnit;
    })();
    RadDiagram.TransformUnit = TransformUnit;
    /**
    * An undo-redo unit handling the addition of a connection.
    */
    var AddConnectionUnit = (function () {
        function AddConnectionUnit(connection, from, to) {
            this.connection = connection;
            this.diagram = connection.Diagram;
            this.from = from;
            this.to = to;
        }
        Object.defineProperty(AddConnectionUnit.prototype, "Title", {
            get: function () {
                return "New connection";
            },
            enumerable: true,
            configurable: true
        });
        AddConnectionUnit.prototype.Undo = function () {
            this.diagram.RemoveConnection(this.connection);
        };
        AddConnectionUnit.prototype.Redo = function () {
            this.diagram.AddConnection(this.connection);
        };
        Object.defineProperty(AddConnectionUnit.prototype, "IsEmpty", {
            get: function () {
                return false;
            },
            enumerable: true,
            configurable: true
        });
        return AddConnectionUnit;
    })();
    RadDiagram.AddConnectionUnit = AddConnectionUnit;
    /**
    * An undo-redo unit handling the addition of diagram item.
    */
    var AddShapeUnit = (function () {
        function AddShapeUnit(shape, diagram) {
            this.shape = shape;
            this.diagram = diagram;
        }
        Object.defineProperty(AddShapeUnit.prototype, "Title", {
            get: function () {
                return "Insert";
            },
            enumerable: true,
            configurable: true
        });
        AddShapeUnit.prototype.Undo = function () {
            this.diagram.RemoveShape(this.shape);
        };
        AddShapeUnit.prototype.Redo = function () {
            this.diagram.AddItem(this.shape);
        };
        Object.defineProperty(AddShapeUnit.prototype, "IsEmpty", {
            get: function () {
                return false;
            },
            enumerable: true,
            configurable: true
        });
        return AddShapeUnit;
    })();
    RadDiagram.AddShapeUnit = AddShapeUnit;
    /**
    * An undo-redo unit handling the selection of items.
    */
    var SelectionUndoUnit = (function () {
        function SelectionUndoUnit() {
            this.shapeStates = [];
        }
        Object.defineProperty(SelectionUndoUnit.prototype, "Title", {
            get: function () {
                return "Selection Unit";
            },
            enumerable: true,
            configurable: true
        });
        SelectionUndoUnit.prototype.Undo = function () {
            for (var i = 0; i < this.shapeStates.length; i++) {
                this.shapeStates[i].Item.IsSelected = this.shapeStates[i].undo;
            }
        };
        SelectionUndoUnit.prototype.Redo = function () {
            for (var i = 0; i < this.shapeStates.length; i++) {
                this.shapeStates[i].Item.IsSelected = this.shapeStates[i].redo;
            }
        };
        Object.defineProperty(SelectionUndoUnit.prototype, "IsEmpty", {
            get: function () {
                for (var i = 0; i < this.shapeStates.length; i++) {
                    if (this.shapeStates[i].undo != this.shapeStates[i].redo) {
                        return false;
                    }
                }
                return true;
            },
            enumerable: true,
            configurable: true
        });
        SelectionUndoUnit.prototype.select = function (item) {
            this.Refresh(item, item.IsSelected, true);
        };
        SelectionUndoUnit.prototype.deselect = function (Item) {
            this.Refresh(Item, Item.IsSelected, false);
        };
        SelectionUndoUnit.prototype.Refresh = function (item, undo, redo) {
            for (var i = 0; i < this.shapeStates.length; i++) {
                if (this.shapeStates[i].Item == item) {
                    this.shapeStates[i].redo = redo;
                    return;
                }
            }
            this.shapeStates.push({
                Item: item,
                undo: undo,
                redo: redo
            });
        };
        return SelectionUndoUnit;
    })();
    RadDiagram.SelectionUndoUnit = SelectionUndoUnit;
    /**
    * An undo-redo unit handling the selection of items.
    */
    var PanUndoUnit = (function () {
        function PanUndoUnit(initial, final, diagram) {
            this.initial = initial;
            this.final = final;
            this.diagram = diagram;
        }
        Object.defineProperty(PanUndoUnit.prototype, "Title", {
            get: function () {
                return "Pan Unit";
            },
            enumerable: true,
            configurable: true
        });
        PanUndoUnit.prototype.Undo = function () {
            this.diagram.Pan = this.initial;
        };
        PanUndoUnit.prototype.Redo = function () {
            this.diagram.Pan = this.final;
        };
        Object.defineProperty(PanUndoUnit.prototype, "IsEmpty", {
            get: function () {
                return false;
            },
            enumerable: true,
            configurable: true
        });
        return PanUndoUnit;
    })();
    RadDiagram.PanUndoUnit = PanUndoUnit;
    /*
    * The node or shape.
    */
    var Shape = (function () {
        /*
        * Instantiates a new Shape.
        */
        function Shape(template, point) {
            this.isHovered = false;
            this.isSelected = false;
            this.adorner = null;
            this.connectors = [];
            this.rotation = new svg.Rotation(0);
            this.translation = new svg.Translation(0, 0);
            this.template = template;
            this._content = template.DefaultContent;
            this.rectangle = svg.Rect.Create(point.X, point.Y, Math.floor(template.Width), Math.floor(template.Height));
            this.createVisual();
        }
        Object.defineProperty(Shape.prototype, "IsVisible", {
            get: /*
            * Gets whether this shape is visible.
            */
            function () {
                return (this.Visual.Native.attributes["visibility"] == null) ? true : this.Visual.Native.attributes["visibility"].value == "visible";
            },
            set: /*
            * Sets whether this shape is visible.
            */
            function (value) {
                if (value) {
                    this.Visual.Native.setAttribute("visibility", "visible");
                } else {
                    this.Visual.Native.setAttribute("visibility", "hidden");
                }
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Shape.prototype, "Visual", {
            get: /*
            * Gets SVG visual this shape represents.
            */
            function () {
                return this.visual;
                ;
            },
            enumerable: true,
            configurable: true
        });
        Shape.prototype.getTemplateVisual = function () {
            if (this.template == null) {
                throw "Template is not set.";
            }
            if (this.template.Geometry == null) {
                throw "Geometry is not set in the template.";
            }
            var v = null;
            if (this.template.Geometry.toLowerCase() == "rectangle") {
                v = new svg.Rectangle();
            } else {
                var path = new svg.Path();
                path.Data = this.template.Geometry;
                v = path;
            }
            v.Stroke = this.template.Stroke;
            v.StrokeThickness = this.template.StrokeThickness;
            v.Background = this.template.Background;
            v.Width = this.template.Width;
            v.Height = this.template.Height;
            if (this.template.Rotation != 0) {
                var r = this.template.Rotation;
                //                if(r == 90 || r == 270 || r == 83)
                //                {
                //                   v.Height = v.Width;
                //                   v.Width = v.Height;
                //                   console.log(v.Id);
                //                }
            }
            return v;
        };
        Object.defineProperty(Shape.prototype, "Width", {
            get: function () {
                return this.Rectangle.Width;
            },
            set: function (v) {
                this.Rectangle.Width = v;
                this.Invalidate();
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Shape.prototype, "Height", {
            get: function () {
                return this.Rectangle.Height;
            },
            set: function (v) {
                this.Rectangle.Height = v;
                this.Invalidate();
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Shape.prototype, "Class", {
            get: /**
            * Sets the CSS class of this shape.
            */
            function () {
                return this.visual.Class;
            },
            set: /**
            * Gets the CSS class of this shape.
            */
            function (v) {
                this.visual.Class = v;
            },
            enumerable: true,
            configurable: true
        });
        Shape.prototype.createVisual = /*
        * Creates the underlying SVG hierarchy for this shape on the basis of the set IShapeTemplate.
        */
        function () {
            var g = new svg.Group();
            g.Id = this.template.Id;
            g.Position = this.rectangle.TopLeft;
            var vis = this.getTemplateVisual();
            vis.Position = svg.Point.Empty;
            g.Append(vis);
            this.mainVisual = vis//in order to update
            ;
            g.Title = (g.Id == null || g.Id.length == 0) ? "Shape" : g.Id;
            if (this.template.ConnectorTemplates.length > 0) {
                for (var i = 0; i < this.template.ConnectorTemplates.length; i++) {
                    var ct = this.template.ConnectorTemplates[i];
                    var connector = new Connector(this, ct);
                    var c = new svg.Rectangle();
                    c.Width = 7;
                    c.Height = 7;
                    var relative = ct.GetConnectorPosition(this);
                    c.Position = new svg.Point(relative.X - 3, relative.Y - 3);
                    connector.Visual = c;
                    connector.IsVisible = false;
                    connector.Parent = this;
                    var text = (ct.Description == null || ct.Description.length == 0) ? ct.Name : ct.Description;
                    c.Title = text;
                    g.Append(c);
                    this.Connectors.push(connector);
                }
            }
            //if (this.template.Rotation != 0)
            //{
            //    var rot = new svg.Rotation(this.template.Rotation);
            //    g.PrePendTransform(rot);
            //}
            this.visual = g;
        };
        Object.defineProperty(Shape.prototype, "Title", {
            get: /**
            * Sets the title of this visual.
            */
            function () {
                return this.visual.Title;
            },
            set: /**
            * Gets the title of this visual.
            */
            function (v) {
                this.visual.Title = v;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Shape.prototype, "Id", {
            get: /*
            * Gets the identifier of this shape.
            */
            function () {
                return this.Template.Id;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Shape.prototype, "Rectangle", {
            get: /*
            * Gets the bounding rectangle of this shape.
            */
            function () {
                return (this.adorner != null) ? this.adorner.Rectangle : this.rectangle;//&& (this.adorner.IsManipulation)

            },
            set: /*
            * Sets the bounding rectangle of this shape.
            */
            function (r) {
                this.rectangle = r;
                if (this.adorner != null) {
                    this.adorner.UpdateRectangle(r);
                }
                this.Invalidate();
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Shape.prototype, "Template", {
            get: /*
            * Gets the shape template.
            */
            function () {
                return this.template;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Shape.prototype, "Connectors", {
            get: /*
            * Gets the connectors of this shape.
            */
            function () {
                return this.connectors;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Shape.prototype, "Adorner", {
            get: /*
            * Gets the resizing adorner of this shape.
            */
            function () {
                return this.adorner;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Shape.prototype, "IsSelected", {
            get: /*
            * Gets whether this shape is selected.
            */
            function () {
                return this.isSelected;
            },
            set: /*
            * Sets whether this shape is selected.
            */
            function (value) {
                if (this.isSelected != value) {
                    this.isSelected = value;
                    if (this.isSelected) {
                        this.adorner = new ResizingAdorner(this.Rectangle, this.template.IsResizable);
                        this.Diagram.MainLayer.Append(this.adorner.Visual);
                        this.Invalidate();
                    } else {
                        this.Invalidate();
                        this.Diagram.MainLayer.Remove(this.adorner.Visual);
                        this.adorner = null;
                    }
                }
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Shape.prototype, "IsHovered", {
            get: /*
            * Gets whether the mouse pointer is currently over this shape.
            */
            function () {
                return this.isHovered;
            },
            set: /*
            * Sets whether the mouse pointer is currently over this shape.
            */
            function (value) {
                this.isHovered = value;
                if (this.Connectors.length > 0) {
                    for (var i = 0; i < this.Connectors.length; i++) {
                        this.Connectors[i].IsVisible = value;
                    }
                }
            },
            enumerable: true,
            configurable: true
        });
        Shape.prototype.paint = function (context) {
            //this.template.paint(this, context);
            if (this.isSelected) {
                this.adorner.paint(context);
            }
        };
        Object.defineProperty(Shape.prototype, "Position", {
            set: function (value) {
                this.translation.X = value.X;
                this.translation.Y = value.Y;
                this.visual.Native.setAttribute("transform", this.translation.toString() + this.rotation.toString());
            },
            enumerable: true,
            configurable: true
        });
        Shape.prototype.Invalidate = function () {
            this.Position = this.Rectangle.TopLeft;
            this.mainVisual.Width = this.Rectangle.Width;
            this.mainVisual.Height = this.Rectangle.Height;
            if (this.Connectors.length > 0) {
                var cons = [];
                for (var i = 0; i < this.Connectors.length; i++) {
                    var c = this.Connectors[i];
                    var ct = this.Template.ConnectorTemplates[i];
                    var relative = ct.GetConnectorPosition(this);
                    c.Visual.Position = new svg.Point(relative.X - 3, relative.Y - 3);
                    if (c.Connections.length > 0) {
                        for (var j = 0; j < c.Connections.length; j++) {
                            if (!cons.contains(c.Connections[j])) {
                                cons.push(c.Connections[j]);
                            }
                        }
                    }
                }
                cons.forEach(function (con) {
                    return con.Invalidate();
                });
            }
        };
        Shape.prototype.toString = function () {
            return (this.Template == null) ? "Shape" : ("Shape '" + this.Template.Id) + "'";
        };
        Shape.prototype.HitTest = /**
        * Hit testing of this item with respect to the given rectangle.
        * @param r The rectangle to test.
        */
        function (r) {
            if ((r.Width === 0) && (r.Height === 0)) {
                if (this.Rectangle.Contains(r.TopLeft)) {
                    return true;
                }
                if ((this.adorner != null)) {
                    var h = this.adorner.HitTest(r.TopLeft);
                    if ((h.X >= -1) && (h.X <= +1) && (h.Y >= -1) && (h.Y <= +1)) {
                        return true;
                    }
                }
                for (var i = 0; i < this.connectors.length; i++) {
                    if (this.connectors[i].HitTest(r)) {
                        return true;
                    }
                }
                return false;
            }
            return r.Contains(this.Rectangle.TopLeft);
        };
        Shape.prototype.GetCursor = function (point) {
            if (this.adorner != null) {
                var cursor = this.adorner.GetCursor(point);
                if (cursor != null) {
                    return cursor;
                }
            }
            if (window.event.shiftKey) {
                return Cursors.add;
            }
            return Cursors.select;
        };
        Shape.prototype.GetConnector = function (name) {
            for (var i = 0; i < this.connectors.length; i++) {
                var connector = this.connectors[i];
                if (connector.Template.Name == name) {
                    return connector;
                }
            }
            return null;
        };
        Shape.prototype.GetConnectorPosition = function (connector) {
            var r = this.Rectangle;
            var point = connector.Template.GetConnectorPosition(this);
            point.X += r.X;
            point.Y += r.Y;
            return point;
        };
        Shape.prototype.setContent = function (content) {
            this.Diagram.setElementContent(this, content);
        };
        Object.defineProperty(Shape.prototype, "Content", {
            get: function () {
                return this._content;
            },
            set: function (value) {
                this._content = value;
            },
            enumerable: true,
            configurable: true
        });
        return Shape;
    })();
    RadDiagram.Shape = Shape;
    /**
    * The service handling the undo-redo stack.
    */
    var UndoRedoService = (function () {
        function UndoRedoService() {
            this.composite = null;
            this.stack = [];
            this.index = 0;
        }
        UndoRedoService.prototype.begin = /**
        * Starts a new composite unit which can be either cancelled or committed.
        */
        function () {
            this.composite = new CompositeUnit();
        };
        UndoRedoService.prototype.Cancel = function () {
            this.composite = null;
        };
        UndoRedoService.prototype.commit = function () {
            if (!this.composite.IsEmpty) {
                // throw away anything beyond this point if this is a new branch
                this.stack.splice(this.index, this.stack.length - this.index);
                this.stack.push(this.composite);
                this.Redo();
            }
            this.composite = null;
        };
        UndoRedoService.prototype.AddCompositeItem = /**
        * Adds the given undoable unit to the current composite. Use the simple add() method if you wish to do things in one swing.
        * @param undoUnit The undoable unit to add.
        */
        function (undoUnit) {
            if (this.composite == null) {
                throw "Use begin() to initiate and then add an undoable unit.";
            }
            this.composite.add(undoUnit);
        };
        UndoRedoService.prototype.Add = /**
        * Adds the given undoable unit to the stack and executes it.
        * @param undoUnit The undoable unit to add.
        */
        function (undoUnit) {
            if (undoUnit == null) {
                throw "No undoable unit supplied.";
            }
            // throw away anything beyond this point if this is a new branch
            this.stack.splice(this.index, this.stack.length - this.index);
            this.stack.push(new CompositeUnit(undoUnit));
            this.Redo();
        };
        UndoRedoService.prototype.count = /**
        * Returns the number of composite units in this undo-redo stack.
        */
        function () {
            return this.stack.length;
        };
        UndoRedoService.prototype.Undo = function () {
            if (this.index != 0) {
                this.index--;
                this.stack[this.index].Undo();
            }
        };
        UndoRedoService.prototype.Redo = function () {
            if ((this.stack.length != 0) && (this.index < this.stack.length)) {
                this.stack[this.index].Redo();
                this.index++;
            } else {
                throw "Reached the end of the undo-redo stack.";
            }
        };
        return UndoRedoService;
    })();
    RadDiagram.UndoRedoService = UndoRedoService;
    /**
    * The adorner supporting the scaling of items.
    */
    var ResizingAdorner = (function () {
        function ResizingAdorner(rectangle, resizable) {
            this.isManipulating = false;
            this.map = {
            };
            this.initialState = null;
            this.finalState = null;
            this.rectangle = rectangle.Clone();
            this.isresizable = resizable;
            this.createVisuals();
        }
        Object.defineProperty(ResizingAdorner.prototype, "Visual", {
            get: function () {
                return this.visual;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(ResizingAdorner.prototype, "InitialState", {
            get: function () {
                return this.initialState;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(ResizingAdorner.prototype, "FinalState", {
            get: function () {
                return this.finalState;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(ResizingAdorner.prototype, "Rectangle", {
            get: function () {
                return this.rectangle;
            },
            enumerable: true,
            configurable: true
        });
        ResizingAdorner.prototype.createVisuals = function () {
            var g = new svg.Group();
            for (var x = -1; x <= +1; x++) {
                for (var y = -1; y <= +1; y++) {
                    if ((x != 0) || (y != 0)) {
                        var r = this.GetHandleBounds(new svg.Point(x, y));
                        var visual = new svg.Rectangle();
                        visual.Position = r.TopLeft;
                        visual.Width = 7;
                        visual.Height = 7;
                        visual.Background = "DimGray";
                        this.map[x.toString() + y.toString()] = visual;
                        g.Append(visual);
                    }
                }
            }
            g.Position = this.rectangle.TopLeft;
            g.IsVisible = true;
            this.text = new svg.TextBlock();
            this.text.FontSize = 10;
            this.text.Position = new svg.Point(0, this.rectangle.Height + 20);
            this.text.Text = "Width: " + this.rectangle.Width + ", Height: " + this.rectangle.Height;
            g.Append(this.text);
            this.visual = g;
        };
        ResizingAdorner.prototype.updateVisual = function () {
            for (var x = -1; x <= +1; x++) {
                for (var y = -1; y <= +1; y++) {
                    if ((x != 0) || (y != 0)) {
                        var v = this.map[x.toString() + y.toString()];
                        var r = this.GetHandleBounds(new svg.Point(x, y));
                        v.Position = r.TopLeft;
                    }
                }
            }
            this.text.Position = new svg.Point(0, this.rectangle.Height + 20);
            this.text.Text = "Width: " + this.rectangle.Width + ", Height: " + this.rectangle.Height;
            this.visual.Position = this.rectangle.TopLeft;
        };
        ResizingAdorner.prototype.HitTest = function (point) {
            // (0, 0) element, (-1, -1) top-left, (+1, +1) bottom-right
            if (this.isresizable) {
                for (var x = -1; x <= +1; x++) {
                    for (var y = -1; y <= +1; y++) {
                        if ((x != 0) || (y != 0)) {
                            var hit = new svg.Point(x, y);
                            var r = this.GetHandleBounds(hit);//local coordinates

                            r.Offset(this.rectangle.X, this.rectangle.Y);
                            if (r.Contains(point)) {
                                return hit;
                            }
                        }
                    }
                }
            }
            if (this.rectangle.Contains(point)) {
                return new svg.Point(0, 0);
            }
            return new svg.Point(-2, -2);
        };
        ResizingAdorner.prototype.GetHandleBounds = function (p) {
            var r = new svg.Rect(0, 0, 7, 7);
            if (p.X < 0) {
                r.X = -7;
            }
            if (p.X === 0) {
                r.X = Math.floor(this.rectangle.Width / 2) - 3;
            }
            if (p.X > 0) {
                r.X = this.rectangle.Width + 1.0;
            }
            if (p.Y < 0) {
                r.Y = -7;
            }
            if (p.Y === 0) {
                r.Y = Math.floor(this.rectangle.Height / 2) - 3;
            }
            if (p.Y > 0) {
                r.Y = this.rectangle.Height + 1.0;
            }
            return r;
        };
        ResizingAdorner.prototype.GetCursor = function (point) {
            var hit = this.HitTest(point);
            if ((hit.X === 0) && (hit.Y === 0)) {
                return (this.isManipulating) ? Cursors.MoveTo : Cursors.select;
            }
            if ((hit.X >= -1) && (hit.X <= +1) && (hit.Y >= -1) && (hit.Y <= +1) && this.isresizable) {
                if (hit.X === -1 && hit.Y === -1) {
                    return "nw-resize";
                }
                if (hit.X === +1 && hit.Y === +1) {
                    return "se-resize";
                }
                if (hit.X === -1 && hit.Y === +1) {
                    return "sw-resize";
                }
                if (hit.X === +1 && hit.Y === -1) {
                    return "ne-resize";
                }
                if (hit.X === 0 && hit.Y === -1) {
                    return "n-resize";
                }
                if (hit.X === 0 && hit.Y === +1) {
                    return "s-resize";
                }
                if (hit.X === +1 && hit.Y === 0) {
                    return "e-resize";
                }
                if (hit.X === -1 && hit.Y === 0) {
                    return "w-resize";
                }
            }
            return null;
        };
        ResizingAdorner.prototype.Start = function (point, handle) {
            if ((handle.X >= -1) && (handle.X <= +1) && (handle.Y >= -1) && (handle.Y <= +1)) {
                this.currentHandle = handle;
                this.initialState = this.rectangle;
                this.finalState = null;
                this.currentPoint = point;
                this.isManipulating = true;
            }
        };
        ResizingAdorner.prototype.Stop = function () {
            this.finalState = this.rectangle;
            this.isManipulating = false;
        };
        Object.defineProperty(ResizingAdorner.prototype, "IsManipulation", {
            get: function () {
                return this.isManipulating;
            },
            enumerable: true,
            configurable: true
        });
        ResizingAdorner.prototype.MoveTo = function (p) {
            var h = this.currentHandle;
            var a = svg.Point.Empty;
            var b = svg.Point.Empty;
            if ((h.X == -1) || ((h.X === 0) && (h.Y === 0))) {
                a.X = p.X - this.currentPoint.X;
            }
            if ((h.Y == -1) || ((h.X === 0) && (h.Y === 0))) {
                a.Y = p.Y - this.currentPoint.Y;
            }
            if ((h.X == +1) || ((h.X === 0) && (h.Y === 0))) {
                b.X = p.X - this.currentPoint.X;
            }
            if ((h.Y == +1) || ((h.X === 0) && (h.Y === 0))) {
                b.Y = p.Y - this.currentPoint.Y;
            }
            var tl = this.rectangle.TopLeft;
            var br = new svg.Point(this.rectangle.X + this.rectangle.Width, this.rectangle.Y + this.rectangle.Height);
            tl.X += a.X;
            tl.Y += a.Y;
            br.X += b.X;
            br.Y += b.Y;
            //if (a.X != 0 || a.Y != 0) console.log("a: (" + a.X + "," + a.Y + ")");
            //if (b.X != 0 || b.Y != 0) console.log("b: (" + b.X + "," + b.Y + ")");
            //cut-off
            if (Math.abs(br.X - tl.X) <= 4 || Math.abs(br.Y - tl.Y) <= 4) {
                return;
            }
            this.rectangle.X = tl.X;
            this.rectangle.Y = tl.Y;
            this.rectangle.Width = Math.floor(br.X - tl.X);
            this.rectangle.Height = Math.floor(br.Y - tl.Y);
            this.currentPoint = p;
            this.updateVisual();
        };
        ResizingAdorner.prototype.UpdateRectangle = function (r) {
            this.rectangle = r.Clone();
            this.updateVisual();
        };
        ResizingAdorner.prototype.paint = function (context) {
        };
        return ResizingAdorner;
    })();
    RadDiagram.ResizingAdorner = ResizingAdorner;
    /**
    * The service handling the undo-redo stack.
    */
    var Selector = (function () {
        function Selector(diagram) {
            this.IsActive = false;
            this.visual = new svg.Rectangle();
            //this.visual.Background = "#778899";
            this.visual.Stroke = "#778899";
            this.visual.StrokeThickness = 1;
            this.visual.StrokeDash = "2,2";
            this.visual.Opacity = 0.0;
            this.diagram = diagram;
            //this.visual.IsVisible = false;
        }
        Object.defineProperty(Selector.prototype, "Visual", {
            get: function () {
                return this.visual;
            },
            enumerable: true,
            configurable: true
        });
        Selector.prototype.Start = function (startPoint) {
            this.startPoint = startPoint;
            this.currentPoint = startPoint;
            this.visual.IsVisible = true;
            this.visual.Position = startPoint;
            this.diagram.MainLayer.Append(this.visual);
            this.IsActive = true;
            //console.log(this.startPoint.toString());
        };
        Selector.prototype.End = function () {
            if (!this.IsActive) {
                return;
            }
            //console.log(this.currentPoint.toString());
            this.startPoint = null;
            this.currentPoint = null;
            this.visual.IsVisible = false;
            this.diagram.MainLayer.Remove(this.visual);
            this.IsActive = false;
        };
        Object.defineProperty(Selector.prototype, "Rectangle", {
            get: function () {
                var r = new svg.Rect((this.startPoint.X <= this.currentPoint.X) ? this.startPoint.X : this.currentPoint.X, (this.startPoint.Y <= this.currentPoint.Y) ? this.startPoint.Y : this.currentPoint.Y, this.currentPoint.X - this.startPoint.X, this.currentPoint.Y - this.startPoint.Y);
                if (r.Width < 0) {
                    r.Width *= -1;
                }
                if (r.Height < 0) {
                    r.Height *= -1;
                }
                return r;
            },
            enumerable: true,
            configurable: true
        });
        Selector.prototype.updateCurrentPoint = function (p) {
            this.currentPoint = p;
        };
        Selector.prototype.paint = function (context) {
            var r = this.Rectangle;
            this.visual.Position = r.TopLeft;
            this.visual.Width = r.Width + 1;
            this.visual.Height = r.Height + 1;
        };
        return Selector;
    })();
    RadDiagram.Selector = Selector;
    /**
    * Defines a standard shape template with four connectors.
    */
    var ShapeTemplateBase = (function () {
        function ShapeTemplateBase(id) {
            if (typeof id === "undefined") { id = null; }
            this.IsResizable = true;
            this.DefaultContent = "";
            this.ConnectorTemplates = [
                {
                    Name: "Top",
                    Type: "Data [in]",
                    Description: "Top Connector",
                    GetConnectorPosition: function (parent) {
                        return new svg.Point(Math.floor(parent.Rectangle.Width / 2), 0);
                    },
                    CanConnectTo: function (other) {
                        return other.Template.Name == "Top";
                    }
                },
                {
                    Name: "Right",
                    Type: "Data [in]",
                    Description: "Right Connector",
                    GetConnectorPosition: function (parent) {
                        return new svg.Point(Math.floor(parent.Rectangle.Width), Math.floor(parent.Rectangle.Height / 2));
                    },
                    CanConnectTo: function (other) {
                        return other.Template.Name == "Left";
                    }
                },
                {
                    Name: "Bottom",
                    Type: "Data [out] [array]",
                    Description: "Bottom Connector",
                    GetConnectorPosition: function (parent) {
                        return new svg.Point(Math.floor(parent.Rectangle.Width / 2), parent.Rectangle.Height);
                    },
                    CanConnectTo: function (other) {
                        return other.Template.Name == "Bottom";
                    }
                },
                {
                    Name: "Left",
                    Type: "Data [in]",
                    Description: "Left Connector",
                    GetConnectorPosition: function (parent) {
                        return new svg.Point(0, Math.floor(parent.Rectangle.Height / 2));
                    },
                    CanConnectTo: function (other) {
                        return other.Template.Name == "Right";
                    }
                }
            ];
            this.Id = id;
            this.Width = 150;
            this.Height = 80;
            this.Position = svg.Point.Empty;
            this.Stroke = "Silver";
            this.StrokeThickness = 0;
            this.Background = "#1e90ff";
        }
        ShapeTemplateBase.prototype.Edit = function (element, canvas, point) {
            // will do later on
        };
        ShapeTemplateBase.prototype.Clone = function () {
            var clone = new ShapeTemplateBase();
            clone.Id = this.Id;
            clone.Width = this.Width;
            clone.Height = this.Height;
            clone.Position = this.Position;
            clone.Background = this.Background;
            return clone;
        };
        return ShapeTemplateBase;
    })();
    RadDiagram.ShapeTemplateBase = ShapeTemplateBase;
    /**
    * A collection of pre-defined shapes.
    */
    var Shapes = (function () {
        function Shapes() { }
        Object.defineProperty(Shapes, "Rectangle", {
            get: function () {
                var shape = new ShapeTemplateBase();
                shape.Geometry = "Rectangle";
                return shape;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Shapes, "Triangle", {
            get: function () {
                var shape = new ShapeTemplateBase();
                shape.Geometry = "m2.5,109.24985l61,-106.74985l61,106.74985l-122,0z";
                return shape;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Shapes, "SequentialData", {
            get: function () {
                var shape = new ShapeTemplateBase();
                shape.Geometry = "m50.21875,97.4375l0,0c-26.35457,0 -47.71875,-21.25185 -47.71875,-47.46875l0,0c0,-26.21678 21.36418,-47.46875 47.71875,-47.46875l0,0c12.65584,0 24.79359,5.00155 33.74218,13.90339c8.94862,8.90154 13.97657,20.97617 13.97657,33.56536l0,0c0,12.58895 -5.02795,24.66367 -13.97657,33.56542l13.97657,0l0,13.90333l-47.71875,0z";
                return shape;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Shapes, "Data", {
            get: function () {
                var shape = new ShapeTemplateBase();
                shape.Geometry = "m2.5,97.70305l19.07013,-95.20305l76.27361,0l-19.0702,95.20305l-76.27354,0z";
                return shape;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Shapes, "Wave", {
            get: function () {
                var shape = new ShapeTemplateBase();
                shape.Geometry = "m2.5,15.5967c31.68356,-45.3672 63.37309,45.3642 95.05661,0l0,81.65914c-31.68353,45.36404 -63.37305,-45.36732 -95.05661,0l0,-81.65914z";
                return shape;
            },
            enumerable: true,
            configurable: true
        });
        return Shapes;
    })();
    RadDiagram.Shapes = Shapes;
    /**
    * Node types used when parsing XML.
    */
    (function (NodeTypes) {
        NodeTypes._map = [];
        NodeTypes.ElementNode = 1;
        NodeTypes.AttributeNode = 2;
        NodeTypes.TextNode = 3;
        NodeTypes.CDataNode = 4;
        NodeTypes.EntityReferenceNode = 5;
        NodeTypes.EntityNode = 6;
        NodeTypes.ProcessingInstructionNode = 7;
        NodeTypes.CommentNode = 8;
        NodeTypes.DocumentNode = 9;
        NodeTypes.DocumentTypeNode = 10;
        NodeTypes.DocumentFragmentNode = 11;
        NodeTypes.NotationNode = 12;
    })(RadDiagram.NodeTypes || (RadDiagram.NodeTypes = {}));
    var NodeTypes = RadDiagram.NodeTypes;
    /**
    * RadDiagram XML importer.
    */
    var RDImporter = (function () {
        /**
        * Instantiates a new RadDiagram XML importer.
        * @param diagram the RadDiagram surface into which the XML should be imported.
        */
        function RDImporter(diagram) {
            this.ViewMargin = 10;
            this.BufferSize = 50;
            /**
            * Keeps together the combination of shape id, shape properties defined in the XML, the resulting SVG (for SVG export) and the diagram element (if loaded into a diagram).
            */
            this.shapeCatalog = [];
            /**
            * Keeps together the combination of connection id, shape properties defined in the XML, the resulting SVG (for SVG export) and the diagram element (if loaded into a diagram).
            */
            this.connectionCatalog = [];
            this.mapConnectionId = function (id) {
                for (var i = 0; i < this.connectionCatalog.length; i++) {
                    if (this.connectionCatalog[i].id == id) {
                        return this.connectionCatalog[i];
                    }
                }
                return null;
            };
            this.diagram = diagram;
            //TODO: note to Swa; remove this after debugging
            this.canvas = diagram.Canvas;
            this.discovered = null;
        }
        Object.defineProperty(RDImporter.prototype, "Discovered", {
            get: /**
            * Returns the collection of shapes and connection properties which have been discovered from the incoming XML file.
            */
            function () {
                return this.discovered;
            },
            enumerable: true,
            configurable: true
        });
        RDImporter.prototype.parsePoint = function (pos) {
            if (pos == null) {
                return {
                    x: 0,
                    y: 0
                };
            }
            if (pos.x) {
                return pos;
            } else {
                var points = pos.split(";");
                return {
                    x: points[0],
                    y: points[1]
                };
            }
        };
        RDImporter.prototype.getId = function (doc) {
            return doc.Properties.id;
        };
        RDImporter.prototype.getIsCollapsed = function (d) {
            var raw = d.Properties.iscollapsed;
            if (raw == null) {
                return false;
            }
            if (raw == "true") {
                return true;
            }
            return false;
        };
        RDImporter.prototype.getContainerItems = function (d) {
            var raw = d.Properties.items;
            if (raw == null) {
                return [];
            }
            return raw.split(';');
        };
        RDImporter.prototype.getWidth = function (d, allowAuto) {
            if (typeof allowAuto === "undefined") { allowAuto = false; }
            var raw = d.Properties.size;
            if (raw) {
                var parts = raw.split(';');
                if (allowAuto || parts[0] != 'Auto') {
                    return Math.floor(parseFloat(parts[0]));
                } else {
                    return 100.0;
                }
            }
            return 100.0;
        };
        RDImporter.prototype.getHeight = function (d, allowAuto) {
            if (typeof allowAuto === "undefined") { allowAuto = false; }
            var raw = d.Properties.size;
            if (raw == null) {
                return 100.0;
            }
            var parts = raw.split(';');
            if (allowAuto || parts[1] != 'Auto') {
                return Math.floor(parseFloat(parts[1]));
            } else {
                return 100.0;
            }
        };
        RDImporter.prototype.getBBox = function (item) {
            if (item == null) {
                return null;
            }
            return item.getBBox();
        };
        RDImporter.prototype.getFontSize = function (d) {
            var raw = d.Properties.fontsize;
            if (raw != null) {
                return raw;
            }
            return 11;
        };
        RDImporter.prototype.getFontFamily = function (d) {
            var raw = d.Properties.fontfamily;
            if (raw) {
                return raw;
            }
            return 'Segoe UI';
        };
        RDImporter.prototype.getTitle = function (d) {
            return d.Properties.content;
            /*var raw = d.Properties.Content;
            return raw ? raw : null;*/
        };
        RDImporter.prototype.getPosition = function (d) {
            var raw = d.Properties.position;
            if (raw == null) {
                return null;
            }
            return this.parsePoint(raw);
        };
        RDImporter.prototype.getRotation = function (d) {
            var raw = d.Properties.rotationangle;
            return raw == null ? 0.0 : parseFloat(raw);
        };
        RDImporter.prototype.getBackground = function (d) {
            return this.getColor(d, "background", "#CCCCCC");
        };
        RDImporter.prototype.getForeground = function (d) {
            return this.getColor(d, "foreground", "#525252");
        };
        RDImporter.prototype.getGeometry = function (d) {
            var raw = d.Properties.geometry;
            //console.log('Found geometry: ' + raw);
            if (raw && raw.indexOf("F1") == 0) {
                raw = raw.substring(2);
            }
            return raw ? raw : "";
        };
        RDImporter.prototype.getStroke = function (d) {
            return this.getColor(d, "stroke", "#888888");
        };
        RDImporter.prototype.getBorderBrush = function (d) {
            return this.getColor(d, "borderbrush", "#888888");
        };
        RDImporter.prototype.getStrokeWidth = function (d) {
            var raw = d.Properties.strokethickness;
            return raw ? raw : 1.0;
        };
        RDImporter.prototype.getFillOpacity = function (d) {
            var alpha = 1.0;
            if (d.Properties.Background) {
                var rx = /^#([0-9a-f]{2})[0-9a-f]{6}$/i;
                var m = d.Properties.Background.match(rx);
                if (m) {
                    alpha = parseInt(m[1], 16) / 255;
                }
            }
            return alpha;
        };
        RDImporter.prototype.getSourceId = function (d) {
            var raw = d.Properties.source;
            return raw ? raw : null;
        };
        RDImporter.prototype.getTargetId = function (d) {
            var raw = d.Properties.target;
            return raw ? raw : null;
        };
        RDImporter.prototype.getStartPoint = function (d) {
            var raw = d.Properties.startpoint;
            return raw ? raw : null;
        };
        RDImporter.prototype.getConnectionPoints = function (d) {
            var raw = d.Properties.connectionpoints;
            if (raw == null || raw.length == 0) {
                return null;
            }
            var pts = raw.toString().split(';');
            var list = [];
            for (var i = 0; i < pts.length; i += 2) {
                list.push(new svg.Point(pts[i], pts[i + 1]));
            }
            return list;
        };
        RDImporter.prototype.getEndPoint = function (d) {
            var raw = d.Properties.endpoint;
            return raw ? raw : null;
        };
        RDImporter.prototype.getConnectionType = function (d) {
            var raw = d.Properties.connectiontype;
            return raw ? raw : null;
        };
        RDImporter.prototype.getDashArray = function (d) {
            var raw = d.Properties.strokedasharray;
            return raw ? raw : null;
        };
        RDImporter.prototype.getSourceCapSize = function (d) {
            var raw = d.Properties.sourcecapsize;
            if (raw) {
                var split = raw.split(';');
                return {
                    w: split[0],
                    h: split[1]
                };
            }
            return {
                w: 0,
                h: 0
            };
        };
        RDImporter.prototype.getTargetCapSize = function (d) {
            var raw = d.Properties.targetcapsize;
            if (raw) {
                var split = raw.split(';');
                return {
                    w: split[0],
                    h: split[1]
                };
            }
            return {
                w: 0,
                h: 0
            };
        };
        RDImporter.prototype.getSourceCap = function (d) {
            return this.getCap(d, true);
        };
        RDImporter.prototype.getTargetCap = function (d) {
            return this.getCap(d, false);
        };
        RDImporter.prototype.getCap = function (d, isSource) {
            var direction = isSource ? 'Source' : 'Target';
            var directionCapType = isSource ? 'sourcecaptype' : 'targetcaptype';
            var directionCapSize = isSource ? 'sourcecaptype' : 'targetcaptype';
            var sourcecapsize = this.getSourceCapSize(d);
            var targetcapsize = this.getTargetCapSize(d);
            var getName = function (name) {
                return name + '-' + direction + '-' + id;
            };
            var raw = d.Properties[directionCapType];
            if (raw) {
                if (raw == 'None') {
                    return null;
                } else {
                    var id = d.Properties.id;
                    var w = isSource ? (sourcecapsize == null ? sourcecapsize.w : 0) : (targetcapsize == null ? targetcapsize.w : 0);
                    var h = isSource ? (sourcecapsize == null ? sourcecapsize.h : 0) : (targetcapsize == null ? targetcapsize.h : 0);
                    var orient = 0;
                    if (!isSource) {
                        orient = this.calculateMarkerAngle(d.Properties.startpoint, d.Properties.endpoint);
                    } else {
                        orient = this.calculateMarkerAngle(d.Properties.endpoint, d.Properties.startpoint);
                    }
                    var name;
                    switch (raw) {
                        case 'Arrow1':
                            name = getName('Arrow1');
                            var m = isSource ? svg.Markers.OpenArrowStart : svg.Markers.OpenArrowEnd;
                            m.Id = name;
                            return m;
                        case 'Arrow1Filled':
                            name = getName('Arrow1Filled');
                            var m = isSource ? svg.Markers.ArrowStart : svg.Markers.ArrowEnd;
                            m.Id = name;
                            return m;
                        case 'Arrow2':
                            name = getName('Arrow2');
                            var m = isSource ? svg.Markers.OpenArrowStart : svg.Markers.OpenArrowEnd;
                            m.Id = name;
                            return m;
                        case 'Arrow2Filled':
                            name = getName('Arrow2Filled');
                            var m = isSource ? svg.Markers.ArrowStart : svg.Markers.ArrowEnd;
                            m.Id = name;
                            return m;
                        case 'Arrow3':
                            name = getName('Arrow3');
                            var m = isSource ? svg.Markers.WedgeStart : svg.Markers.WedgeEnd;
                            m.Id = name;
                            return m;
                        case 'Arrow4':
                            name = getName('Arrow4');
                            var m = isSource ? svg.Markers.OpenArrowStart : svg.Markers.OpenArrowEnd;
                            m.Id = name;
                            return m;
                        case 'Arrow4Filled':
                            name = getName('Arrow4Filled');
                            var m = isSource ? svg.Markers.ArrowStart : svg.Markers.ArrowEnd;
                            m.Id = name;
                            return m;
                        case 'Arrow5':
                            // open diamond
                            name = getName('Arrow5');
                            var m = svg.Markers.Diamond;
                            m.Id = name;
                            return m;
                        case 'Arrow5Filled':
                            //filled diamond
                            name = getName('Arrow5Filled');
                            var m = svg.Markers.FilledDiamond;
                            m.Id = name;
                            return m;
                        case 'Arrow6':
                            // open circle
                            name = getName('Arrow6');
                            var m = svg.Markers.Circle;
                            m.Id = name;
                            return m;
                        case 'Arrow6Filled':
                            // filled circle
                            name = getName('Arrow6Filled');
                            var m = svg.Markers.FilledCircle;
                            m.Id = name;
                            return m;
                        default:
                    }
                    return raw;
                }
            }
            return null;
        };
        RDImporter.prototype.getColor = function (d, property, defaultColor) {
            var raw = d.Properties[property];
            if (raw) {
                return "#" + raw.substring(3, 9);
            } else {
                if (d.Items && d.Items.length > 0) {
                    for (var i = 0; i < d.Items.length; i++) {
                        if (d.Items[i].Tag == property) {
                            var b = d.Items[i];
                            if (b.Items.length > 0) {
                                if (b.Items[0].Tag == "solidcolorbrush") {
                                    var p = b.Items[0].Properties.color;
                                    return "#" + p.substring(3, 9);
                                } else if (b.Items[0].Tag == "lineargradientbrush") {
                                    var lingrad = b.Items[0];
                                    var stops = [];
                                    var startPoint = this.parsePoint(lingrad.Properties.startpoint);
                                    var endPoint = this.parsePoint(lingrad.Properties.endpoint);
                                    var angle = 180 * Math.atan2(endPoint.y - startPoint.y, endPoint.x - startPoint.x) / Math.PI;
                                    for (var j = 0; j < lingrad.Items.length; j++) {
                                        var gradstop = lingrad.Items[j];
                                        stops.push({
                                            offset: gradstop.Properties.offset,
                                            color: '#' + gradstop.Properties.color.substring(3, 9),
                                            opacity: 1
                                        });
                                        //should convert the first two chars of the color
                                    }
                                    stops.sort(function (x, y) {
                                        return x.offset - y.offset;
                                    });
                                    var graddef = {
                                        angle: angle,
                                        stops: stops,
                                        type: "linear",
                                        startPoint: startPoint,
                                        endPoint: endPoint
                                    };
                                    return graddef;
                                } else if (b.Items[0].Tag == "radialgradientbrush") {
                                    var lingrad = b.Items[0];
                                    var stops = [];
                                    var origin = this.parsePoint(lingrad.Properties.origin);
                                    var radiusX = parseFloat(lingrad.Properties.radiusx);
                                    var radiusY = parseFloat(lingrad.Properties.radiusy);
                                    for (var j = 0; j < lingrad.Items.length; j++) {
                                        var gradstop = lingrad.Items[j];
                                        stops.push({
                                            offset: gradstop.Properties.offset,
                                            color: '#' + gradstop.Properties.color.substring(3, 9),
                                            opacity: 1
                                        });
                                        //should convert the first two chars of the color
                                    }
                                    stops.sort(function (x, y) {
                                        return x.offset - y.offset;
                                    });
                                    var graddef = null;//TODO: { origin: origin, stops: stops, r: (radiusX + radiusY) * 50, type: "radial" };

                                    return graddef;
                                }
                            }
                            break;
                        }
                    }
                }
            }
            return defaultColor;
        };
        RDImporter.prototype.calculateMarkerAngle = function (startPoint, endPoint) {
            var distante = this.calculateDistance(startPoint, endPoint);
            if (startPoint.x <= endPoint.x && startPoint.y <= endPoint.y) {
                return 180 + (Math.asin((endPoint.y - startPoint.y) / distante) * 180 / Math.PI);
            } else if (startPoint.x > endPoint.x && startPoint.y < endPoint.y) {
                return 270 + (Math.asin((startPoint.x - endPoint.x) / distante) * 180 / Math.PI);
            } else if (startPoint.x >= endPoint.x && startPoint.y > endPoint.y) {
                return Math.asin((startPoint.y - endPoint.y) / distante) * 180 / Math.PI;
            } else {
                return 90 + Math.asin((endPoint.x - startPoint.x) / distante) * 180 / Math.PI;
            }
        };
        RDImporter.prototype.calculateDistance = function (startPoint, endPoint) {
            return Math.sqrt((startPoint.x - endPoint.x) * (startPoint.x - endPoint.x) + (startPoint.y - endPoint.y) * (startPoint.y - endPoint.y));
        };
        RDImporter.prototype.findMinRotatedPosition = function (props) {
            var point1 = this.rotate(0, 0, props.width, props.height, props.rotation);
            var point2 = this.rotate(props.width, 0, props.width, props.height, props.rotation);
            var point3 = this.rotate(0, props.height, props.width, props.height, props.rotation);
            var point4 = this.rotate(props.width, props.height, props.width, props.height, props.rotation);
            return {
                x: Math.min(point1.newX, point2.newX, point3.newX, point4.newX),
                y: Math.min(point1.newY, point2.newY, point3.newY, point4.newY)
            };
        };
        RDImporter.prototype.findMaxRotatedPosition = function (props) {
            var point1 = this.rotate(0, 0, props.width, props.height, props.rotation);
            var point2 = this.rotate(props.width, 0, props.width, props.height, props.rotation);
            var point3 = this.rotate(0, props.height, props.width, props.height, props.rotation);
            var point4 = this.rotate(props.width, props.height, props.width, props.height, props.rotation);
            return {
                x: Math.max(point1.newX, point2.newX, point3.newX, point4.newX),
                y: Math.max(point1.newY, point2.newY, point3.newY, point4.newY)
            };
        };
        RDImporter.prototype.rotate = function (pointX, pointY, rectWidth, rectHeight, angle) {
            // convert angle to radians
            angle = angle * Math.PI / 180.0;
            // calculate center of rectangle
            var centerX = rectWidth / 2.0;
            var centerY = rectHeight / 2.0;
            // get coordinates relative to center
            var dx = pointX - centerX;
            var dy = pointY - centerY;
            // calculate angle and distance
            var a = Math.atan2(dy, dx);
            var dist = Math.sqrt(dx * dx + dy * dy);
            // calculate new angle
            var a2 = a + angle;
            // calculate new coordinates
            var dx2 = Math.cos(a2) * dist;
            var dy2 = Math.sin(a2) * dist;
            // return coordinates relative to top left corner
            return {
                newX: dx2 + centerX,
                newY: dy2 + centerY
            };
        };
        RDImporter.prototype.mapShapeId = function (id) {
            for (var i = 0; i < this.shapeCatalog.length; i++) {
                if (this.shapeCatalog[i].id == id) {
                    return this.shapeCatalog[i];
                }
            }
            return null;
        };
        RDImporter.prototype.extractShapeProperties = function (shape) {
            return {
                position: this.getPosition(shape),
                geometry: this.getGeometry(shape),
                fillOpacity: this.getFillOpacity(shape),
                id: this.getId(shape),
                stroke: this.getBorderBrush(shape),
                rotation: this.getRotation(shape),
                strokeWidth: this.getStrokeWidth(shape),
                title: this.getTitle(shape),
                width: this.getWidth(shape),
                height: this.getHeight(shape),
                fontsize: this.getFontSize(shape),
                fontfill: this.getForeground(shape),
                fontfamily: this.getFontFamily(shape),
                isContainer: (shape.Tag == "raddiagramcontainershape"),
                fill: "",
                iscollapsed: this.getIsCollapsed(shape),
                containerItems: this.getContainerItems(shape)
            };
        };
        RDImporter.prototype.extractConnectionProperties = function (connection) {
            var sourceId = this.getSourceId(connection);
            var targeteId = this.getSourceId(connection);
            var sourceProps = sourceId == null ? null : this.mapShapeId(this.getSourceId(connection)).props;
            var targetProps = targeteId == null ? null : this.mapShapeId(this.getTargetId(connection)).props;
            return {
                id: this.getId(connection),
                stroke: this.getStroke(connection),
                source: sourceProps,
                target: targetProps,
                strokeWidth: this.getStrokeWidth(connection),
                type: this.getConnectionType(connection),
                startpoint: this.getStartPoint(connection),
                endpoint: this.getEndPoint(connection),
                strokedasharray: this.getDashArray(connection),
                sourcecapsize: this.getSourceCapSize(connection),
                targetcapsize: this.getTargetCapSize(connection),
                sourcecap: this.getSourceCap(connection),
                targetcap: this.getTargetCap(connection),
                connectionpoints: this.getConnectionPoints(connection)
            };
        };
        RDImporter.prototype.createVisuals = /**
        * Converts the given diagram model to SVG.
        */
        function (model) {
            if (model == null) {
                return null;
            }
            this.discovered = {
                ShapeProperties: [],
                ConnectionProperties: []
            };
            //var mainLayer = new svg.Group();
            //this.canvas.Append(mainLayer);
            //mainLayer.Id = "mainLayer";
            //var shapeLayer = new svg.Group();
            //shapeLayer.Id = "shapeLayer";
            //mainLayer.Append(shapeLayer);
            //var connectionLayer = new svg.Group();
            //connectionLayer.Id = "connectionLayer";
            //mainLayer.Append(connectionLayer);
            var shapes = model.shapes.Items;
            // sort the shapes by ZIndex
            if (shapes != null) {
                shapes.sort(function (a, b) {
                    return a.Properties.zindex - b.Properties.zindex;
                });
            }
            this.createShapes(shapes);
            var connections = model.connections.Items;
            this.createConnections(connections);
            //mainLayer.options.transform = "translate(" + (this.ViewMargin - minX) + "," + (this.ViewMargin - minY) + ")";
            //var width = Math.abs(maxX - minX);
            //this.view.options.width = width + (2 * this.BufferSize);
            //var height = Math.abs(maxY - minY);
            //this.view.options.height = height + (2 * this.BufferSize);
            //this.view.children = [mainLayer];
            //return this.view.render();
        };
        RDImporter.prototype.createShapes = function (shapes) {
            if (shapes == null) {
                return;
            }
            var minX = Infinity;
            var maxX = -Infinity;
            var minY = Infinity;
            var maxY = -Infinity;
            var collapsedShapes = [];
            for (var k = 0; k < shapes.length; k++) {
                var shape = shapes[k];
                var shapeProps = this.extractShapeProperties(shape);
                this.discovered.ShapeProperties.push(shapeProps);
                var minRotatedPosition = undefined;
                var maxRotatedPosition = undefined;
                if (shapeProps.rotation != 0 && shapeProps.rotation != 180) {
                    minRotatedPosition = this.findMinRotatedPosition(shapeProps);
                    maxRotatedPosition = this.findMaxRotatedPosition(shapeProps);
                }
                var newMinPos = minRotatedPosition ? {
                    x: parseInt(shapeProps.position.x) + parseInt(minRotatedPosition.x),
                    y: parseInt(shapeProps.position.y) + parseInt(minRotatedPosition.y)
                } : shapeProps.position;
                var newMaxPos = maxRotatedPosition ? {
                    x: parseInt(shapeProps.position.x) + parseInt(maxRotatedPosition.x),
                    y: parseInt(shapeProps.position.y) + parseInt(maxRotatedPosition.y)
                } : {
                    x: parseInt(shapeProps.position.x) + parseInt(shapeProps.width),
                    y: parseInt(shapeProps.position.y) + parseInt(shapeProps.height)
                };
                minX = Math.min(minX, newMinPos.x);
                maxX = Math.max(maxX, newMaxPos.x);
                minY = Math.min(minY, newMinPos.y);
                maxY = Math.max(maxY, newMaxPos.y);
                var bg = this.getBackground(shape);
                if (bg != null) {
                    if (typeof (bg) == 'string') {
                        shapeProps.fill = bg;
                    } else//// a gradient definition object
                    {
                        if (bg.type == "linear") {
                            var gr = new svg.LinearGradient();
                            gr.Id = "gradient-" + shapeProps.id;
                            if (bg.startPoint != null) {
                                gr.From = new svg.Point(bg.startPoint.x, bg.startPoint.y);
                            }
                            if (bg.endPoint != null) {
                                gr.To = new svg.Point(bg.endPoint.x, bg.endPoint.y);
                            }
                            var stops = bg.stops;
                            for (var i = 0; i < stops.length; i++) {
                                var gradStop = stops[i];
                                var color = new svg.Color(gradStop.color);
                                var offset = gradStop.offset;
                                var opacity = gradStop.opacity;
                                var s = new svg.GradientStop(color, offset);
                                gr.AddGradientStop(s);
                            }
                            this.diagram.Canvas.AddGradient(gr);
                            shapeProps.fill = gr;
                        } else {
                            //  gr = this.view.createGradient({ id: gradname, cx: bg.origin.x * 100, cy: bg.origin.y * 100, r: bg.r, fx: bg.origin.x * 100, fy: bg.origin.y * 100, stops: bg.stops, type: "radial" });
                            shapeProps.fill = "Gray";
                        }
                        //this.view.definitions[refname] = gr;
                        //throw "Gradients are not supported yet in RadSVG";
                    }
                } else {
                    shapeProps.fill = "gray";
                }
                if (this.generateSVGOnly) {
                    var svgShape = null;
                    if (shape.Tag == "raddiagramcontainershape") {
                        if (shapeProps.iscollapsed) {
                            svgShape = new svg.Path();
                            svgShape.Data = "M0,0 0,100 100,100 100,0z";
                            svgShape.Position = new svg.Point(shapeProps.position.x, shapeProps.position.y);
                            svgShape.Background = shapeProps.fill;
                            svgShape.Width = shapeProps.width;
                            svgShape.Height = 0;
                            svgShape.Id = shapeProps.id;
                            svgShape.Opacity = 0;
                            this.diagram.MainLayer.Append(svgShape);
                            var header = new svg.Rectangle();
                            header.Width = shapeProps.width;
                            header.Height = 25;
                            header.Position = new svg.Point(shapeProps.position.x, shapeProps.position.y);
                            header.Background = "transparent";
                            header.Stroke = "Black";
                            this.diagram.MainLayer.Append(header);
                            if (shapeProps.title != null) {
                                var text = new svg.TextBlock();
                                text.Text = shapeProps.title.trim();
                                text.Native.setAttribute("style", "text-anchor: middle; dominant-baseline: central;");
                                text.dy = 15;
                                text.Position = new svg.Point(header.Position.X + (header.Width / 2), header.Position.Y);
                                text.Background = shapeProps.fontfill;
                                this.diagram.MainLayer.Append(text);
                            }
                            if (shapeProps.containerItems != null && shapeProps.containerItems.length > 0) {
                                for (var i = 0; i < shapeProps.containerItems.length; i++) {
                                    collapsedShapes.push(shapeProps.containerItems[i]);
                                }
                            }
                        } else// non-collapsed container
                        {
                            svgShape = new svg.Path();
                            svgShape.Data = "M0,0 0,100 100,100 100,0z";
                            svgShape.Position = new svg.Point(shapeProps.position.x, shapeProps.position.y);
                            svgShape.Background = shapeProps.fill;
                            svgShape.Width = shapeProps.width;
                            svgShape.Height = shapeProps.height + 30;
                            svgShape.Id = shapeProps.id;
                            this.diagram.MainLayer.Append(svgShape);
                            var header = new svg.Rectangle();
                            header.Width = shapeProps.width;
                            header.Height = 25;
                            header.Position = new svg.Point(shapeProps.position.x, shapeProps.position.y);
                            header.Background = "transparent";
                            header.Stroke = "Black";
                            this.diagram.MainLayer.Append(header);
                            if (shapeProps.title != null) {
                                var text = new svg.TextBlock();
                                text.Text = shapeProps.title.trim();
                                text.Native.setAttribute("style", "text-anchor: middle; dominant-baseline: central;");
                                text.dy = 15;
                                text.Position = new svg.Point(svgShape.Position.X + (shapeProps.width / 2), svgShape.Position.Y);
                                text.Background = shapeProps.fontfill;
                                this.diagram.MainLayer.Append(text);
                            }
                        }
                    } else// not a container
                    {
                        if (collapsedShapes.contains(shapeProps.id)) {
                            continue;
                        }
                        svgShape = new svg.Path();
                        if (shapeProps.geometry == null || shapeProps.geometry.length == 0) {
                            shapeProps.geometry = "M0,0 100,0 100,100 0,100";
                        }
                        svgShape.Data = shapeProps.geometry;
                        svgShape.Position = new svg.Point(shapeProps.position.x, shapeProps.position.y);
                        svgShape.Background = shapeProps.fill;
                        svgShape.Width = shapeProps.width;
                        svgShape.Height = shapeProps.height;
                        svgShape.Id = shapeProps.id;
                        this.diagram.MainLayer.Append(svgShape);
                        if (shapeProps.title != null) {
                            var text = new svg.TextBlock();
                            if (shapeProps.title.indexOf("\n") > -1) {
                                var parts = shapeProps.title.trim().split("\n");
                                var y = 0;
                                for (var i = 0; i < parts.length; i++) {
                                    if (parts[i].trim().length == 0) {
                                        continue;
                                    }
                                    var span = document.createElementNS(RadDiagram.NS, "tspan");
                                    span.textContent = parts[i];
                                    y += 15;
                                    span.setAttribute("x", (shapeProps.position.x + 5).toString());
                                    span.setAttribute("y", (shapeProps.position.y + y).toString());
                                    text.Native.appendChild(span);
                                }
                            } else {
                                text.Text = shapeProps.title.trim();
                                text.dx = 5;
                                if (shapeProps.isContainer) {
                                    text.Position = new svg.Point(svgShape.Position.X, svgShape.Position.Y);
                                } else {
                                    text.Position = new svg.Point(svgShape.Position.X, svgShape.Position.Y + (shapeProps.height / 2));
                                }
                            }
                            text.Background = shapeProps.fontfill;
                            this.diagram.MainLayer.Append(text);
                        }
                    }
                    this.shapeCatalog.push({
                        id: shapeProps.id,
                        original: shape,
                        props: shapeProps,
                        visual: svgShape
                    });
                } else {
                    var template = new ShapeTemplateBase();
                    template.Geometry = shapeProps.geometry;
                    template.Position = new svg.Point(Math.floor(shapeProps.position.x), Math.floor(shapeProps.position.y));
                    template.Background = shapeProps.fill;
                    template.Width = Math.floor(shapeProps.width);
                    template.Height = Math.floor(shapeProps.height);
                    template.Stroke = shapeProps.stroke;
                    template.StrokeThickness = shapeProps.strokeWidth;
                    template.Id = shapeProps.id;
                    template.Rotation = shapeProps.rotation;
                    var newShape = this.diagram.AddShape(template);
                    this.shapeCatalog.push({
                        id: shapeProps.id,
                        original: shape,
                        props: shapeProps,
                        visual: newShape
                    });
                }
            }
        };
        RDImporter.prototype.createConnections = function (connections) {
            if (connections == null) {
                return;
            }
            var minX = Infinity;
            var maxX = -Infinity;
            var minY = Infinity;
            var maxY = -Infinity;
            for (var i = 0; i < connections.length; i++) {
                var connection = connections[i];
                var connectionProps = this.extractConnectionProperties(connection);
                this.discovered.ConnectionProperties.push(connectionProps);
                var startP = this.parsePoint(connectionProps.startpoint);
                var endP = this.parsePoint(connectionProps.endpoint);
                minX = Math.min(minX, Math.min(startP.x, endP.x));
                maxX = Math.max(maxX, Math.max(startP.x, endP.x));
                minY = Math.min(minY, Math.min(startP.y, endP.y));
                maxY = Math.max(maxY, Math.max(startP.y, endP.y));
                if (this.generateSVGOnly) {
                    var svgConnection = new svg.Polyline();
                    var points = [];
                    points.push(new svg.Point(connectionProps.startpoint.x, connectionProps.startpoint.y));
                    if (connectionProps.connectionpoints != null) {
                        for (var i = 0; i < connectionProps.connectionpoints.length; i++) {
                            var p = connectionProps.connectionpoints[i];
                            points.push(p);
                        }
                    }
                    points.push(new svg.Point(connectionProps.endpoint.x, connectionProps.endpoint.y));
                    svgConnection.Points = points;
                    svgConnection.Stroke = connectionProps.stroke;
                    svgConnection.StrokeThickness = connectionProps.strokeWidth;
                    if (connectionProps.targetcap != null) {
                        this.diagram.AddMarker(connectionProps.targetcap);
                        svgConnection.MarkerEnd = connectionProps.targetcap;
                    }
                    if (connectionProps.sourcecap != null) {
                        this.diagram.AddMarker(connectionProps.sourcecap);
                        svgConnection.MarkerStart = connectionProps.sourcecap;
                    }
                    this.diagram.MainLayer.Append(svgConnection);
                } else {
                    var from = this.mapShapeId(connectionProps.source.id).visual;
                    var to = this.mapShapeId(connectionProps.target.id).visual;
                    var fromc = from.Connectors[2];
                    var toc = to.Connectors[2];
                    this.diagram.AddConnection(fromc, toc);
                }
                //connectionProps.startmarker = this.getSourceCapReference(connection, connectionProps);
                //connectionProps.endmarker = this.getTargetCapReference(connection, connectionProps);
                //var con = new Connection(connectionProps);
                //connectionLayer.Append(con);
            }
        };
        RDImporter.prototype.loadModel = /**
        * Loads the RadDiagram model into the TS diagram (and hence SVG).
        */
        function (model) {
            if (model == null) {
                throw "Given model is null";
            }
            this.createVisuals(model);
            this.fixSizes();
        };
        RDImporter.prototype.fixSizes = function () {
            // postprocessing of size because the initial size is only known after it's drawn/added to SVG
            for (var i = 0; i < this.shapeCatalog.length; i++) {
                var shapeProps = this.shapeCatalog[i].props;
                if (shapeProps.iscollapsed) {
                    continue;
                }
                var w = shapeProps.width;
                var h = shapeProps.height;
                var p = shapeProps.position;
                var r = shapeProps.rotation;// in degrees!;

                var item = this.shapeCatalog[i].visual;// if generateSVGOnly this is SVG otherwise it's a Shape

                //var textContent = document.getElementById('content-' + shapeProps.Properties.id); //gets the text content.
                //var parentLayer = document.getElementById('g-' + shapeProps.Properties.id);
                //if (textContent)
                //{
                //    if (w == 'Auto')
                //        textContent.setAttribute("x", (bb.width / 2).toString());
                //    if (h == 'Auto')
                //        textContent.setAttribute("y", (bb.height / 2).toString());
                //}
                //parentLayer.setAttribute("transform", layerMatrix);
                if (this.generateSVGOnly) {
                    var bb = this.getBBox(item.Native);//bounding box

                    var matrix = "translate(" + p.x + "," + p.y + ")" + "rotate(" + r + "," + ((w == 'Auto' ? 100 : w) / 2) + "," + ((h == 'Auto' ? 100 : h) / 2) + ")";
                    matrix += "scale(" + (w != 'Auto' ? w : bb.width) / bb.width + "," + (h != 'Auto' ? h : bb.height) / bb.height + ")";
                    item.Native.setAttribute("transform", matrix);
                } else {
                    //                    var shape = <Shape>item;
                    //                    shape.Height = h;
                    //                    shape.Width = w;
                    //                    if (r != 0)
                    //                    {
                    //                        if(r == 90 || r == 270 || r == 83){
                    //                            shape.Height = w;
                    //                            shape.Width = h;
                    //                            console.log(shape.Id);
                    //                        }
                    //                    }
                }
            }
            //var rb = this.diagram.MainLayer.Native.getBoundingClientRect();
            //this.diagram.Pan = new svg.Point(-rb.left, -rb.top);
        };
        RDImporter.prototype.loadURL = /**
        * Loads the given url which supposedly is the address to a RadDiagram XML file.
        * @param url The URL to the serialized RadDiagram diagram.
        * @param mime The MIME type; this can only be 'text/xml' currently.
        * @param callback An action on the parsed JS object.
        */
        function (url, mime, callback, discovery) {
            if (mime == null || mime.toLowerCase() != "text/xml") {
                throw "Only 'text/xml' is supported for now.";
            }
            var req = new XMLHttpRequest();
            req.open("GET", url, true);
            req.setRequestHeader("Accept", "text/xml");
            var r = this;
            req.onreadystatechange = function () {
                if (req.readyState === 4) {
                    if ((req.status >= 200 && req.status < 300 || req.status === 304) && req.responseXML) {
                        // converting to JS object model cause easier to manip than XML
                        var json = r.xmlToJson(req.responseXML);
                        if (callback != null) {
                            callback(json);
                        }
                        if (json != null) {
                            r.loadModel(json);
                            //mostly in function of unit test
                            if (discovery != null) {
                                discovery(r.discovered);
                            }
                        } else {
                            console.log("Returned content model at '" + url + "' is null");
                        }
                    }
                }
            };
            req.send(null);
        };
        RDImporter.prototype.LoadURL = /**
        * Loads the given url which supposedly is the address to a RadDiagram XML file.
        * @param url The URL to the serialized RadDiagram diagram.
        * @param generateSVGOnly If set to true, only SVG objects will be created instead of interactive diagram elements.
        * @param callback An action on the parsed JS object.
        */
        function (url, generateSVGOnly, callback, discovery) {
            if (typeof generateSVGOnly === "undefined") { generateSVGOnly = true; }
            if (typeof callback === "undefined") { callback = null; }
            if (typeof discovery === "undefined") { discovery = null; }
            this.clearRuntimeObjects();
            this.generateSVGOnly = generateSVGOnly;
            var setDiscovered = function (p) {
                if (discovery != null) {
                    discovery(p);
                }
            };
            this.loadURL(url, "text/xml", callback, setDiscovered);
        };
        RDImporter.prototype.LoadXML = /**
        * Loads the given xml which supposedly a serialized RadDiagram diagram.
        * @param xml A serialized RadDiagram diagram.
        */
        function (xml, generateSVGOnly) {
            if (typeof generateSVGOnly === "undefined") { generateSVGOnly = true; }
            this.clearRuntimeObjects();
            this.generateSVGOnly = generateSVGOnly;
            var xmlDoc;
            var parser;
            var imp = window;
            if (imp != null) {
                parser = new DOMParser();
                xmlDoc = parser.parseFromString(xml, "text/xml");
            } else// Internet Explorer
            {
                xmlDoc = new ActiveXObject("Microsoft.XMLDOM");
                xmlDoc.async = false;
                xmlDoc.loadXML(xml);
            }
            // converting to JS object model cause easier to manip than XML
            var json = this.xmlToJson(xmlDoc);
            if (json != null) {
                this.loadModel(json);
            } else {
                console.log("Returned content model is null");
            }
        };
        RDImporter.prototype.clearRuntimeObjects = function () {
            this.discovered = null;
            this.shapeCatalog = [];
            this.connectionCatalog = [];
        };
        RDImporter.prototype.xmlToJson = /**
        * Converts the given XML to a JavaScript literal object.
        * @param xml An XML node.
        */
        function (xml) {
            var obj = {
            };
            var pointLikeProps = [
                "Position",
                "StartPoint",
                "EndPoint"
            ];
            if (xml.nodeType == NodeTypes.ElementNode) {
                obj["Tag"] = xml.nodeName.toLowerCase();
                // do attributes
                if (xml.attributes.length > 0) {
                    obj["Properties"] = {
                    };
                    for (var j = 0; j < xml.attributes.length; j++) {
                        var attribute = xml.attributes[j];
                        if (pointLikeProps.contains(attribute.nodeName)) {
                            var parts = attribute.nodeValue.split(';');
                            obj["Properties"][attribute.nodeName.toLowerCase()] = {
                                x: parseFloat(parts[0]),
                                y: parseFloat(parts[1])
                            };
                        } else {
                            obj["Properties"][attribute.nodeName.toLowerCase()] = attribute.nodeValue;
                        }
                    }
                    if (!obj["Properties"].hasOwnProperty("content")) {
                        obj["Properties"]["content"] = null;
                    }
                }
                if (xml.hasChildNodes()) {
                    obj["Items"] = [];
                    for (var k = 0; k < xml.childNodes.length; k++) {
                        var item = xml.childNodes.item(k);
                        if (item.nodeType != NodeTypes.ElementNode) {
                            continue;
                        }
                        obj["Items"].push(this.xmlToJson(item));
                    }
                }
            } else if (xml.nodeType == NodeTypes.DocumentNode) {
                var children = (xml).documentElement.childNodes;
                for (var i = 0; i < children.length; i++) {
                    var child = children[i];
                    if (child.nodeType != NodeTypes.TextNode) {
                        var name = child.nodeName.toLowerCase();
                        obj[name] = this.xmlToJson(child);
                        //console.log(name);
                    }
                }
            }
            return obj;
        };
        return RDImporter;
    })();
    RadDiagram.RDImporter = RDImporter;
})(RadDiagram || (RadDiagram = {}));
//@ sourceMappingURL=RadDiagram.js.map
