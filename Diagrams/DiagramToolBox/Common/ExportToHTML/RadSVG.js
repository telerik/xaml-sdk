var __extends = this.__extends || function (d, b) {
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
/**
* Copyright Telerik.
*
* Disclaimer:
* The TypeScript and SVG libraries allow a fully interactive diagramming
* experience, but are not released or supported as such yet.
* The only part supported for now is the export/import to/from XAML/XML.
*
*/
var RadSVG;
(function (RadSVG) {
    /**
    * Base class for all visuals participating in an SVG drawing.
    */
    var Visual = (function () {
        /**
        * Instanstiates a new visual.
        */
        function Visual() {
        }
        Object.defineProperty(Visual.prototype, "Native", {
            get: /**
            * Gets the native SVG element which this visual wraps.
            */
            function () {
                return this.native;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Visual.prototype, "Id", {
            get: /**
            * Returns the identifier.
            */
            function () {
                return this.Native == null ? null : this.Native.id;
            },
            set: /**
            * Sets the identifier.
            */
            function (value) {
                this.Native.id = value;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Visual.prototype, "Class", {
            get: /**
            * Sets the CSS class of this visual.
            */
            function () {
                if (this.Native.attributes["class"] == null) {
                    return null;
                }
                return this.Native.attributes["class"].value;
            },
            set: /**
            * Gets the CSS class of this visual.
            */
            function (v) {
                if (v == null) {
                    this.Native.removeAttribute("class");
                } else {
                    this.Native.setAttribute("class", v);
                }
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Visual.prototype, "Title", {
            get: /**
            * Sets the title of this visual.
            */
            function () {
                return this.title.textContent;
            },
            set: /**
            * Gets the title of this visual.
            */
            function (v) {
                if (this.title == null) {
                    this.title = document.createElementNS(RadSVG.NS, "title");
                    this.native.appendChild(this.title);
                }
                this.title.textContent = v;
            },
            enumerable: true,
            configurable: true
        });
        Visual.prototype.Initialize = /**
        * Part of the inheritance chain, this assigns the SVG element defined by the inheriting class and the canvas to which this element belongs.
        * The 'super' has to be parameterless, hence the necessity of this Initializer.
        */
        function (native) {
            if (native == null) {
                throw "The native SVG element cannot be null.";
            }
            this.native = native;
            this.ListenToEvents();
        };
        Visual.prototype.ListenToEvents = /**
        * Rewires the native events to API events.
        */
        function () {
            var _this = this;
            this.Native.onmousedown = function (e) {
                return _this.onMouseDown(e);
            };
            this.Native.onmousemove = function (e) {
                return _this.onMouseMove(e);
            };
            this.Native.onmouseup = function (e) {
                return _this.onMouseUp(e);
            };
        };
        Visual.prototype.StopListeningToEvents = /**
        * Detaches the event listeners from the native SVG element.
        */
        function () {
            this.Native.onmousedown = null;
            this.Native.onmousemove = null;
            this.Native.onmouseup = null;
        };
        Object.defineProperty(Visual.prototype, "IsVisible", {
            get: function () {
                if (this.Native.attributes["visibility"] == null) {
                    return true;
                }
                return this.Native.attributes["visibility"].value == "visible";
            },
            set: function (value) {
                this.Native.setAttribute("visibility", (value ? "visible" : "hidden"));
            },
            enumerable: true,
            configurable: true
        });
        Visual.prototype.onMouseDown = function (e) {
            if (this.MouseDown) {
                this.MouseDown(e);
            }
        };
        Visual.prototype.onMouseMove = function (e) {
            if (this.MouseMove) {
                this.MouseMove(e);
            }
        };
        Visual.prototype.onMouseUp = function (e) {
            if (this.MouseUp) {
                this.MouseUp(e);
            }
        };
        Visual.prototype.PrePendTransform = //private onKeyDown(e: KeyboardEvent) { if (this.KeyDown) this.KeyDown(e); }
        //private onKeyPress(e: KeyboardEvent) { if (this.KeyPress) this.KeyPress(e); }
        function (transform) {
            var current = this.Native.attributes["transform"] == null ? "" : this.Native.attributes["transform"].value;
            this.Native.setAttribute("transform", transform.toString() + current);
        };
        Visual.prototype.Transform = function () {
            var transforms = [];
            for (var _i = 0; _i < (arguments.length - 0) ; _i++) {
                transforms[_i] = arguments[_i + 0];
            }
            var current = this.Native.attributes["transform"] == null ? "" : this.Native.attributes["transform"].value;
            var s = current;
            for (var i = 0; i < transforms.length; i++) {
                s += transforms[i].toString();
            }
            this.Native.setAttribute("transform", s.toString());
            return;
            if (current != null) {
                //                var loc = <SVGLocatable><Object>this.Native.parentNode;
                //                //var m = svg.Matrix.Parse(current);
                //                if (loc != null)
                //                {
                //                    var mm= loc.getTransformToElement(this.Native).inverse();
                //                    var m = Matrix.FromSVGMatrix(mm);
                //                    for (var i = 0; i < transforms.length; i++) m = m.Times(transforms[i].ToMatrix());
                //                    this.Native.setAttribute("transform", m.toString());
                //                }
                //                else
                //                {
                //                    throw "The current transform could not be fetched. The Native element is not SVGLocatable.";
                //                }
                var s = current;
                for (var i = 0; i < transforms.length; i++) {
                    s += transforms[i].toString();
                }
                this.Native.setAttribute("transform", s.toString());
            } else {
                var m = Matrix.Unit;
                for (var i = 0; i < transforms.length; i++) {
                    m = m.Times(transforms[i].ToMatrix());
                }
                this.Native.setAttribute("transform", m.toString());
            }
        };
        return Visual;
    })();
    RadSVG.Visual = Visual;
    /**
    * A scaling transformation.
    */
    var Scale = (function () {
        /**
        * Instantiates a new scaling transformation.
        * @param x The horizontal scaling.
        * @param y The vertical scaling.
        */
        function Scale(x, y) {
            if (typeof x === "undefined") { x = null; }
            if (typeof y === "undefined") { y = null; }
            if (x != null) {
                this.ScaleX = x;
            }
            if (y != null) {
                this.ScaleY = y;
            }
        }
        Scale.prototype.ToMatrix = function () {
            return Matrix.Scaling(this.ScaleX, this.ScaleY);
        };
        Scale.prototype.toString = function () {
            return "scale(" + this.ScaleX + "," + this.ScaleY + ")";
        };
        return Scale;
    })();
    RadSVG.Scale = Scale;
    /**
    * Represent an SVG translation.
    */
    var Translation = (function () {
        function Translation(x, y) {
            if (typeof x === "undefined") { x = null; }
            if (typeof y === "undefined") { y = null; }
            if (x != null) {
                this.X = x;
            }
            if (y != null) {
                this.Y = y;
            }
        }
        Object.defineProperty(Translation.prototype, "X", {
            get: function () {
                return this.x;
            },
            set: function (v) {
                this.x = v;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Translation.prototype, "Y", {
            get: function () {
                return this.y;
            },
            set: function (v) {
                this.y = v;
            },
            enumerable: true,
            configurable: true
        });
        Translation.prototype.ToMatrixVector = function () {
            return new MatrixVector(0, 0, 0, 0, this.X, this.Y);
        };
        Translation.prototype.ToMatrix = function () {
            return Matrix.Translation(this.X, this.Y);
        };
        Translation.prototype.toString = function () {
            return "translate(" + this.X + "," + this.Y + ")";
        };
        Translation.prototype.Plus = function (delta) {
            this.X += delta.X;
            this.Y += delta.Y;
        };
        Translation.prototype.Times = function (factor) {
            this.X *= factor;
            this.Y *= factor;
        };
        Object.defineProperty(Translation.prototype, "Length", {
            get: /**
            * Returns the size of this translation considered as a 2D vector.
            */
            function () {
                return Math.sqrt(this.X * this.X + this.Y * this.Y);
            },
            enumerable: true,
            configurable: true
        });
        Translation.prototype.Normalize = /**
        * Normalizes the length of this translation to one.
        */
        function () {
            if (this.Length == 0) {
                return;
            }
            this.Times(1 / this.Length);
        };
        return Translation;
    })();
    RadSVG.Translation = Translation;
    /**
    * Represent an SVG rotation.
    */
    var Rotation = (function () {
        /**
        * Instantiates a new rotation.
        * @param angle The rotation angle in degrees.
        * @param x The rotation center's X coordinate.
        * @param y The rotation center's Y coordinate.
        */
        function Rotation(angle, x, y) {
            if (typeof angle === "undefined") { angle = null; }
            if (typeof x === "undefined") { x = null; }
            if (typeof y === "undefined") { y = null; }
            if (x != null) {
                this.X = x;
            }
            if (y != null) {
                this.Y = y;
            }
            if (angle != null) {
                this.Angle = angle;
            }
        }
        Rotation.prototype.toString = function () {
            if (this.X != null || this.Y != null) {
                return "rotate(" + this.Angle + "," + this.X + "," + this.Y + ")";
            } else {
                return "rotate(" + this.Angle + ")";
            }
        };
        Rotation.prototype.ToMatrix = function () {
            if (this.X == 0 && this.Y == 0) {
                return Matrix.Rotation(this.Angle);
            } else {
                // T*R*T^-1
                return Matrix.Rotation(this.Angle, this.X, this.Y);
            }
        };
        return Rotation;
    })();
    RadSVG.Rotation = Rotation;
    /**
    * A text block visual.
    */
    var TextBlock = (function (_super) {
        __extends(TextBlock, _super);
        function TextBlock(canvas) {
            if (typeof canvas === "undefined") { canvas = null; }
            _super.call(this);
            this.native = document.createElementNS(RadSVG.NS, "text");
            this.Initialize(this.native);
            this.dx = 0;
            this.dy = 3;
            this.FontFamily = "Verdana";
            this.FontVariant = FontVariants.Normal;
            this.Stroke = "steelblue";
            this.FontWeight = FontWeights.Normal;
            this.StrokeThickness = 0;
            this.FontSize = 10;
        }
        Object.defineProperty(TextBlock.prototype, "Background", {
            set: function (v) {
                this.Native.setAttribute("fill", v);
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(TextBlock.prototype, "StrokeThickness", {
            get: function () {
                if (this.Native.attributes["stroke-width"] == null) {
                    return 0.0;
                }
                return parseFloat(this.Native.attributes["stroke-width"].value);
            },
            set: function (value) {
                this.Native.setAttribute("stroke-width", value.toString());
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(TextBlock.prototype, "Position", {
            get: /**
            * Gets the position of this text block.
            */
            function () {
                return new Point(this.native.x.baseVal.getItem(0).value, this.native.y.baseVal.getItem(0).value);
            },
            set: /**
            * Sets the position of this text block.
            */
            function (p) {
                this.native.setAttribute("x", p.X.toString());
                this.native.setAttribute("y", p.Y.toString());
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(TextBlock.prototype, "Stroke", {
            get: function () {
                if (this.Native.attributes["stroke"] == null) {
                    return null;
                }
                return this.Native.attributes["stroke"].value;
            },
            set: function (value) {
                this.Native.setAttribute("stroke", value);
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(TextBlock.prototype, "Text", {
            get: /**
            * Gets the text of this text block.
            */
            function () {
                return this.native.textContent;
            },
            set: /**
            * Sets the text of this text block.
            */
            function (v) {
                this.native.textContent = v;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(TextBlock.prototype, "FontFamily", {
            get: /**
            * Gets the font-family of this text block.
            */
            function () {
                if (this.native.attributes["font-family"] == null) {
                    return null;
                }
                return this.native.attributes["font-family"].value;
            },
            set: /**
            * Sets the font-family of this text block.
            */
            function (v) {
                this.native.setAttribute("font-family", v);
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(TextBlock.prototype, "FontVariant", {
            get: /**
            * Gets the font-family of this text block.
            */
            function () {
                if (this.native.attributes["font-variant"] == null) {
                    return null;
                }
                return TextBlock.ParseFontVariant(this.native.attributes["font-variant"].value);
            },
            set: /**
            * Sets the font-family of this text block.
            */
            function (v) {
                var s = TextBlock.FontVariantString(v);
                if (s != null) {
                    this.native.setAttribute("font-variant", s);
                }
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(TextBlock.prototype, "FontSize", {
            get: /**
            * Gets the font-size of this text block.
            */
            function () {
                if (this.native.attributes["font-size"] == null) {
                    return null;
                }
                return parseFloat(this.native.attributes["font-size"].value);
            },
            set: /**
            * Sets the font-size of this text block.
            */
            function (v) {
                this.native.setAttribute("font-size", v.toString());
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(TextBlock.prototype, "FontWeight", {
            get: /**
            * Gets the font-size of this text block.
            */
            function () {
                if (this.native.attributes["font-weight"] == null) {
                    return FontWeights.NotSet;
                }
                return TextBlock.ParseFontWeight(this.native.attributes["font-weight"].value);
            },
            set: /**
            * Sets the font-size of this text block.
            */
            function (v) {
                var s = TextBlock.FontWeightString(v);
                if (s != null) {
                    this.native.setAttribute("font-weight", s);
                }
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(TextBlock.prototype, "Anchor", {
            get: /**
            * Gets the anchor of this text block.
            */
            function () {
                if (this.native.attributes["text-anchor"] == null) {
                    return null;
                }
                return parseFloat(this.native.attributes["text-anchor"].value);
            },
            set: /**
            * Sets the anchor of this text block.
            */
            function (v) {
                this.native.setAttribute("text-anchor", v.toString());
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(TextBlock.prototype, "dx", {
            get: /**
            * Gets the dx offset of this text block.
            */
            function () {
                if (this.native.attributes["dx"] == null) {
                    return null;
                }
                return parseFloat(this.native.attributes["dx"].value);
            },
            set: /**
            * Sets the dx offset of this text block.
            */
            function (v) {
                this.native.setAttribute("dx", v.toString());
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(TextBlock.prototype, "dy", {
            get: /**
            * Gets the dy offset of this text block.
            */
            function () {
                if (this.native.attributes["dy"] == null) {
                    return null;
                }
                return parseFloat(this.native.attributes["dy"].value);
            },
            set: /**
            * Sets the dy offset of this text block.
            */
            function (v) {
                this.native.setAttribute("dy", v.toString());
            },
            enumerable: true,
            configurable: true
        });
        TextBlock.ParseFontWeight = /**
        * Parses the given string and attempts to convert it to a FontWeights member.
        * @param v A string representing a FontWeights.
        */
        function ParseFontWeight(v) {
            if (v == null) {
                return FontWeights.NotSet;
            }
            switch (v.toLowerCase()) {
                case "normal":
                    return FontWeights.Normal;
                case "bold":
                    return FontWeights.Bold;
                case "bolder":
                    return FontWeights.Bolder;
                case "lighter":
                    return FontWeights.Lighter;
                case "100":
                    return FontWeights.W100;
                case "200":
                    return FontWeights.W200;
                case "300":
                    return FontWeights.W300;
                case "400":
                    return FontWeights.W400;
                case "500":
                    return FontWeights.W500;
                case "600":
                    return FontWeights.W600;
                case "700":
                    return FontWeights.W700;
                case "800":
                    return FontWeights.W800;
                case "900":
                    return FontWeights.W900;
                case "inherit":
                    return FontWeights.Inherit;
            }
            throw "String '" + v + "' could not be parsed to a FontWeights member.";
        };
        TextBlock.FontWeightString = /**
        * Returns a string representation of the given FontWeights value.
        * @param value A FontWeights member.
        */
        function FontWeightString(value) {
            switch (value) {
                case 0:
                    return "normal";
                case 1:
                    return "bold";
                case 2:
                    return "bolder";
                case 3:
                    return "lighter";
                case 4:
                    return "100";
                case 5:
                    return "200";
                case 6:
                    return "300";
                case 7:
                    return "400";
                case 8:
                    return "500";
                case 9:
                    return "600";
                case 10:
                    return "700";
                case 11:
                    return "800";
                case 12:
                    return "900";
                case 13:
                    return "inherit";
                case 14:
                    return null;
            }
            throw "Unexpected FontWeight";
        };
        TextBlock.ParseFontVariant = function ParseFontVariant(v) {
            if (v == null) {
                return FontVariants.NotSet;
            }
            switch (v.toLowerCase()) {
                case "normal":
                    return FontVariants.Normal;
                case "small-caps":
                    return FontVariants.SmallCaps;
            }
        };
        TextBlock.FontVariantString = function FontVariantString(value) {
            switch (value) {
                case 0:
                    return "normal";
                case 1:
                    return "small-caps";
                case 2:
                    return null;
            }
        };
        return TextBlock;
    })(Visual);
    RadSVG.TextBlock = TextBlock;
    /**
    * The values the FontWeight accepts.
    */
    (function (FontWeights) {
        FontWeights._map = [];
        FontWeights.Normal = 0;
        FontWeights.Bold = 1;
        FontWeights.Bolder = 2;
        FontWeights.Lighter = 3;
        FontWeights.W100 = 4;
        FontWeights.W200 = 5;
        FontWeights.W300 = 6;
        FontWeights.W400 = 7;
        FontWeights.W500 = 8;
        FontWeights.W600 = 9;
        FontWeights.W700 = 10;
        FontWeights.W800 = 11;
        FontWeights.W900 = 12;
        FontWeights.Inherit = 13;
        FontWeights.NotSet = 14;
    })(RadSVG.FontWeights || (RadSVG.FontWeights = {}));
    var FontWeights = RadSVG.FontWeights;
    /**
    * The FontVariant values.
    */
    (function (FontVariants) {
        FontVariants._map = [];
        FontVariants.Normal = 0;
        FontVariants.SmallCaps = 1;
        FontVariants.Inherit = 2;
        FontVariants.NotSet = 3;
    })(RadSVG.FontVariants || (RadSVG.FontVariants = {}));
    var FontVariants = RadSVG.FontVariants;
    /**
    * A rectangle visual.
    */
    var Rectangle = (function (_super) {
        __extends(Rectangle, _super);
        /**
        * Instantiates a new rectangle.
        */
        function Rectangle(canvas) {
            if (typeof canvas === "undefined") { canvas = null; }
            _super.call(this);
            this.native = document.createElementNS(RadSVG.NS, "rect");
            this.Initialize(this.native);
        }
        Object.defineProperty(Rectangle.prototype, "Width", {
            get: /**
            * Gets the width of this rectangle.
            */
            function () {
                return this.native.width.baseVal.value;
            },
            set: /**
            * Sets the width of this rectangle.
            */
            function (value) {
                this.native.width.baseVal.value = value;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Rectangle.prototype, "Height", {
            get: /**
            * Gets the height of this rectangle.
            */
            function () {
                return this.native.height.baseVal.value;
            },
            set: /**
            * Sets the height of this rectangle.
            */
            function (value) {
                this.native.height.baseVal.value = value;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Rectangle.prototype, "Background", {
            get: /**
            * Gets the fill of this rectangle.
            */
            function () {
                return this.native.style.fill;
            },
            set: /**
            * Sets the fill of this rectangle.
            */
            function (v) {
                if (typeof (v) == "string") {
                    this.native.style.fill = v;
                }
                if (typeof (v) == "object") {
                    var gr = v;
                    if (gr != null) {
                        if (gr.Id == null) {
                            throw "The gradient needs an Id.";
                        }
                        this.native.style.fill = "url(#" + gr.Id + ")";
                    }
                }
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Rectangle.prototype, "CornerRadius", {
            get: /**
            * Gets the corner radius of this rectangle.
            */
            function () {
                return this.native.rx.baseVal.value;
            },
            set: /**
            * Sets the corner radius of this rectangle.
            */
            function (v) {
                this.native.rx.baseVal.value = v;
                this.native.ry.baseVal.value = v;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Rectangle.prototype, "Opacity", {
            get: /**
            * Gets the opacity.
            */
            function () {
                if (this.Native.attributes["fill-opacity"] == null) {
                    return 1.0;
                }
                return parseFloat(this.Native.attributes["fill-opacity"].value);
            },
            set: function (value) {
                if (value > 1) {
                    value = 1.0;
                }
                if (value < 0) {
                    value = 0.0;
                }
                this.Native.setAttribute("fill-opacity", value.toString());
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Rectangle.prototype, "Position", {
            get: /**
            * Gets the position of this rectangle.
            */
            function () {
                return new Point(this.native.x.baseVal.value, this.native.y.baseVal.value);
            },
            set: /**
            * Sets the position of this rectangle.
            */
            function (p) {
                this.native.x.baseVal.value = p.X;
                this.native.y.baseVal.value = p.Y;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Rectangle.prototype, "StrokeThickness", {
            get: function () {
                if (this.Native.attributes["stroke-width"] == null) {
                    return 0.0;
                }
                return parseFloat(this.Native.attributes["stroke-width"].value);
            },
            set: function (value) {
                this.Native.setAttribute("stroke-width", value.toString());
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Rectangle.prototype, "Stroke", {
            get: function () {
                if (this.Native.attributes["stroke"] == null) {
                    return null;
                }
                return this.Native.attributes["stroke"].value;
            },
            set: function (value) {
                this.Native.setAttribute("stroke", value);
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Rectangle.prototype, "StrokeDash", {
            get: function () {
                if (this.Native.attributes["stroke-dasharray"] == null) {
                    return null;
                }
                return this.Native.attributes["stroke-dasharray"].value;
            },
            set: function (value) {
                this.Native.setAttribute("stroke-dasharray", value);
            },
            enumerable: true,
            configurable: true
        });
        return Rectangle;
    })(Visual);
    RadSVG.Rectangle = Rectangle;
    /**
    * A path.
    */
    var Path = (function (_super) {
        __extends(Path, _super);
        /**
        * Instantiates a new path.
        */
        function Path() {
            _super.call(this);
            this.native = document.createElementNS(RadSVG.NS, "path");
            this.xf = 1;
            this.yf = 1;
            this.Initialize(this.native);
            this.Background = "Black";
            this.Stroke = "Black";
        }
        Object.defineProperty(Path.prototype, "StrokeThickness", {
            get: function () {
                if (this.Native.attributes["stroke-width"] == null) {
                    return 0.0;
                }
                return parseFloat(this.Native.attributes["stroke-width"].value);
            },
            set: function (value) {
                this.Native.setAttribute("stroke-width", value.toString());
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Path.prototype, "Stroke", {
            get: function () {
                if (this.Native.attributes["stroke"] == null) {
                    return null;
                }
                return this.Native.attributes["stroke"].value;
            },
            set: function (value) {
                this.Native.setAttribute("stroke", value);
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Path.prototype, "StrokeDash", {
            get: function () {
                if (this.Native.attributes["stroke-dasharray"] == null) {
                    return null;
                }
                return this.Native.attributes["stroke-dasharray"].value;
            },
            set: function (value) {
                this.Native.setAttribute("stroke-dasharray", value);
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Path.prototype, "Data", {
            get: function () {
                if (this.Native.attributes["d"] == null) {
                    return null;
                }
                return this.Native.attributes["d"].value;
            },
            set: function (value) {
                this.native.setAttribute("d", value);
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Path.prototype, "Position", {
            get: /**
            * Gets the position of this group.
            */
            function () {
                return this.position;
            },
            set: /**
            * Sets the position of this group.
            */
            function (p) {
                this.position = p;
                try {
                    if (this.native.ownerSVGElement == null) {
                        return;
                    }
                } catch (err) {
                    return;
                }
                var tr = this.native.ownerSVGElement.createSVGTransform();
                tr.setTranslate(p.X, p.Y);
                if (this.native.transform.baseVal.numberOfItems == 0) {
                    this.native.transform.baseVal.appendItem(tr);
                } else {
                    this.native.transform.baseVal.replaceItem(tr, 0);
                }
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Path.prototype, "Background", {
            get: /**
            * Gets the fill of this rectangle.
            */
            function () {
                return this.native.style.fill;
            },
            set: /**
            * Sets the fill of this rectangle.
            */
            function (v) {
                if (typeof (v) == "string") {
                    this.native.style.fill = v;
                }
                if (typeof (v) == "object") {
                    var gr = v;
                    if (gr != null) {
                        if (gr.Id == null) {
                            throw "The gradient needs an Id.";
                        }
                        this.native.style.fill = "url(#" + gr.Id + ")";
                    }
                }
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Path.prototype, "Width", {
            get: /**
            * Gets the width of this rectangle.
            */
            function () {
                try {
                    return this.native.getBBox().width;
                } catch (err) {
                    return 0;
                }
            },
            set: /**
            * Sets the width of this rectangle.
            */
            function (value) {
                if (this.Width == 0) {
                    //means most probably that the path is not yet added to the canvas.
                    //console.log("Warning: current path bounding box is nil, assuming that the path's geometry is scaled at 100x100.");
                    this.xf = value / 100;
                } else {
                    this.xf = value / this.Width;
                }
                this.native.setAttribute("transform", "scale(" + this.xf + "," + this.yf + ")");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Path.prototype, "Height", {
            get: /**
            * Gets the height of this rectangle.
            */
            function () {
                try {
                    return this.native.getBBox().height;
                } catch (err) {
                    return 0;
                }
            },
            set: /**
            * Sets the height of this rectangle.
            */
            function (value) {
                if (this.Height == 0) {
                    //means most probably that the path is not yet added to the canvas.
                    console.log("Warning: current path bounding box is nil, assuming that the path's geometry is scaled at 100x100.");
                    this.yf = value / 100;
                } else {
                    this.yf = value / this.Height;
                }
                this.native.setAttribute("transform", "scale(" + this.xf + "," + this.yf + ")");
            },
            enumerable: true,
            configurable: true
        });
        Path.ParseNode = /**
        * Attempts to convert the given Node to a Path.
        * @param A Node.
        */
        function ParseNode(node) {
            if (node == null) {
                return null;
            }
            if (node.localName != "path") {
                return null;
            }
            var path = new Path();
            path.Data = node.attributes["d"] == null ? null : node.attributes["d"].value;
            path.StrokeThickness = node.attributes["stroke-width"] == null ? 0 : parseFloat(node.attributes["stroke-width"].value);
            path.Stroke = node.attributes["stroke"] == null ? null : node.attributes["stroke"].value;
            path.Background = node.attributes["fill"] == null ? null : node.attributes["fill"].value;
            return path;
        };
        return Path;
    })(Visual);
    RadSVG.Path = Path;
    /**
    * A marker
    */
    var Marker = (function (_super) {
        __extends(Marker, _super);
        /**
        * Instantiates a new marker.
        */
        function Marker() {
            _super.call(this);
            this.native = document.createElementNS(RadSVG.NS, "marker");
            this.Initialize(this.native);
        }
        Object.defineProperty(Marker.prototype, "RefX", {
            get: /**
            * Gets the refX of this marker.
            */
            function () {
                if (this.native.attributes["refX"] == null) {
                    return 0;
                }
                return parseFloat(this.native.attributes["refX"].value);
            },
            set: /**
            * Sets the refX of this marker.
            */
            function (value) {
                this.native.refX.baseVal.value = value;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Marker.prototype, "RefY", {
            get: /**
            * Gets the refY of this marker.
            */
            function () {
                if (this.native.attributes["refY"] == null) {
                    return 0;
                }
                return parseFloat(this.native.attributes["refY"].value);
            },
            set: /**
            * Sets the refX of this marker.
            */
            function (value) {
                this.native.refY.baseVal.value = value;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Marker.prototype, "Ref", {
            get: /**
            * Gets the refX and refY of this marker.
            */
            function () {
                return new Point(this.RefX, this.RefY);
            },
            set: /**
            * Sets the refX and refY of this marker.
            */
            function (value) {
                this.RefX = value.X;
                this.RefY = value.Y;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Marker.prototype, "MarkerWidth", {
            get: /**
            * Gets the width of this marker.
            */
            function () {
                if (this.native.attributes["markerWidth"] == null) {
                    return 0;
                }
                return parseFloat(this.native.attributes["markerWidth"].value);
            },
            set: /**
            * Sets the width of this marker.
            */
            function (value) {
                this.native.markerWidth.baseVal.value = value;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Marker.prototype, "MarkerHeight", {
            get: /**
            * Gets the height of this marker.
            */
            function () {
                if (this.native.attributes["markerHeight"] == null) {
                    return 0;
                }
                return parseFloat(this.native.attributes["markerHeight"].value);
            },
            set: /**
            * Sets the height of this marker.
            */
            function (value) {
                this.native.markerHeight.baseVal.value = value;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Marker.prototype, "Size", {
            get: /**
            * Gets the size of this marker.
            */
            function () {
                return new Size(this.MarkerWidth, this.MarkerHeight);
            },
            set: /**
            * Sets the size of this marker.
            */
            function (value) {
                this.MarkerWidth = value.Width;
                this.MarkerHeight = value.Height;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Marker.prototype, "ViewBox", {
            get: /**
            * Gets the size of this marker.
            */
            function () {
                if (this.native.viewBox == null) {
                    return Rect.Empty;
                }
                return new Rect(this.native.viewBox.baseVal.x, this.native.viewBox.baseVal.y, this.native.viewBox.baseVal.width, this.native.viewBox.baseVal.height);
            },
            set: /**
            * Sets the size of this marker.
            */
            function (value) {
                this.native.viewBox.baseVal.height = value.Height;
                this.native.viewBox.baseVal.width = value.Width;
                this.native.viewBox.baseVal.x = value.X;
                this.native.viewBox.baseVal.y = value.Y;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Marker.prototype, "Orientation", {
            get: function () {
                if (this.native.orientType == null) {
                    return MarkerOrientation.NotSet;
                }
                return Marker.ParseOrientation(this.native.orientType.baseVal.toString());
            },
            set: function (value) {
                //value is actually an int
                if (value == MarkerOrientation.NotSet) {
                    return;
                }// not so sure about this one

                var s = Marker.OrientationString(value);
                if (s != null) {
                    this.native.setAttribute("orient", s);
                }
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Marker.prototype, "Path", {
            get: function () {
                return this.path;
            },
            set: function (value) {
                if (value == this.path) {
                    return;
                }
                this.path = value;
                if (this.native.firstChild != null) {
                    this.native.removeChild(this.native.firstChild);
                }
                this.native.appendChild(value.Native);
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Marker.prototype, "MarkerUnits", {
            get: function () {
                if (this.native.orientType == null) {
                    return MarkerUnits.NotSet;
                }
                return Marker.ParseMarkerUnits(this.native.orientType.baseVal.toString());
            },
            set: function (value) {
                if (value == MarkerUnits.NotSet) {
                    return;
                }// not so sure about this one

                var s = Marker.MarkerUnitsString(value);
                if (s != null) {
                    this.native.setAttribute("markerUnits", s);
                }
            },
            enumerable: true,
            configurable: true
        });
        Marker.ParseOrientation = /**
        * Parses the orientation attribute.
        * @param v The value of the 'orient' attribute.
        */
        function ParseOrientation(v) {
            if (v == null) {
                return MarkerOrientation.NotSet;
            }
            if (v.toLowerCase() == "auto") {
                return MarkerOrientation.Auto;
            }
            if (v.toLowerCase() == "angle") {
                return MarkerOrientation.Angle;
            }
            throw "Unexpected value '" + v + "' cannot be converted to a MarkerOrientation.";
        };
        Marker.OrientationString = /**
        * Returns a string representation of the given MarkerOrientation.
        * @param value A MarkerOrientation member.
        */
        function OrientationString(value) {
            switch (value) {
                case 0:
                    return "auto";
                case 1:
                    return "angle";
                case 2:
                    return null;
            }
            throw "Unexpected MarkerOrientation value '" + value + "'.";
        };
        Marker.ParseMarkerUnits = /**
        * Attempts to convert the given string to a MarkerUnits.
        * @param v A string to convert.
        */
        function ParseMarkerUnits(v) {
            if (v == null) {
                return MarkerUnits.NotSet;
            }
            if (v.toLowerCase() == "strokewidth") {
                return MarkerUnits.StrokeWidth;
            }
            if (v.toLowerCase() == "userspaceonuse") {
                return MarkerUnits.UserSpaceOnUse;
            }
            throw "Unexpected MarkerUnits value '" + v + "'.";
        };
        Marker.MarkerUnitsString = function MarkerUnitsString(value) {
            switch (value) {
                case 0:
                    return "strokewidth";
                case 1:
                    return "userspaceonuse";
                case 2:
                    return null;
            }
            throw "Unexpected MarkerUnits value '" + value + "'.";
        };
        Object.defineProperty(Marker.prototype, "Stroke", {
            get: /**
            * Gets the stroke color of the underlying path.
            */
            function () {
                if (this.Path == null) {
                    return null;
                }
                return this.Path.Stroke;
            },
            set: /**
            * Sets the stroke color of the underlying path.
            */
            function (value) {
                if (this.Path != null) {
                    this.Path.Stroke = value;
                }
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Marker.prototype, "Background", {
            get: function () {
                if (this.Path == null) {
                    return null;
                }
                return this.Path.Background;
            },
            set: /**
            * Sets the fill color of the underlying path.
            */
            function (value) {
                if (value == null) {
                    value = "none";
                }
                if (this.Path != null) {
                    this.Path.Background = value;
                }
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Marker.prototype, "Color", {
            set: /**
            * Sets the fill and stroke color of the underlying path in one go.
            * You can set the values separately by accessing the Path property of this marker if needed.
            */
            function (value) {
                if (this.Path != null) {
                    this.Path.Background = value;
                    this.Path.Stroke = value;
                }
            },
            enumerable: true,
            configurable: true
        });
        return Marker;
    })(Visual);
    RadSVG.Marker = Marker;
    /**
    * The possible marker orientation values.
    */
    (function (MarkerOrientation) {
        MarkerOrientation._map = [];
        MarkerOrientation.Auto = 0;
        MarkerOrientation.Angle = 1;
        MarkerOrientation.NotSet = 2;
    })(RadSVG.MarkerOrientation || (RadSVG.MarkerOrientation = {}));
    var MarkerOrientation = RadSVG.MarkerOrientation;
    /**
    * The possible marker unit values.
    */
    (function (MarkerUnits) {
        MarkerUnits._map = [];
        MarkerUnits.StrokeWidth = 0;
        MarkerUnits.UserSpaceOnUse = 1;
        MarkerUnits.NotSet = 2;
    })(RadSVG.MarkerUnits || (RadSVG.MarkerUnits = {}));
    var MarkerUnits = RadSVG.MarkerUnits;
    /**
    * A linear gradient.
    */
    var LinearGradient = (function () {
        /**
        * Instantiates a new Line.
        */
        function LinearGradient(canvas, from, to) {
            if (typeof canvas === "undefined") { canvas = null; }
            if (typeof from === "undefined") { from = null; }
            if (typeof to === "undefined") { to = null; }
            this.native = document.createElementNS(RadSVG.NS, "linearGradient");
            this.stops = [];
            this.canvas = canvas;
            this.from = from;
            this.to = to;
        }
        Object.defineProperty(LinearGradient.prototype, "Native", {
            get: function () {
                return this.native;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(LinearGradient.prototype, "GradientStops", {
            get: function () {
                return this.stops;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(LinearGradient.prototype, "Id", {
            get: function () {
                return this.Native == null ? null : this.Native.id;
            },
            set: function (value) {
                this.Native.id = value;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(LinearGradient.prototype, "From", {
            get: /**
            * Gets the point where the gradient starts.
            */
            function () {
                return this.from;
            },
            set: /**
            * Sets the point where the gradient starts. The value should be in the [0,1] interval.
            */
            function (value) {
                if (this.from != value) {
                    this.Native.setAttribute("x1", (value.X * 100).toString() + "%");
                    this.Native.setAttribute("y1", (value.Y * 100).toString() + "%");
                    this.from = value;
                }
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(LinearGradient.prototype, "To", {
            get: /**
            * Gets the point where the gradient ends.
            */
            function () {
                return this.to;
            },
            set: /**
            * Sets the point where the gradient ends.The value should be in the [0,1] interval.
            */
            function (value) {
                if (this.to != value) {
                    this.Native.setAttribute("x2", (value.X * 100).toString() + "%");
                    this.Native.setAttribute("y2", (value.Y * 100).toString() + "%");
                    this.to = value;
                }
            },
            enumerable: true,
            configurable: true
        });
        LinearGradient.prototype.AddGradientStops = function () {
            var stops = [];
            for (var _i = 0; _i < (arguments.length - 0) ; _i++) {
                stops[_i] = arguments[_i + 0];
            }
            for (var i = 0; i < stops.length; i++) {
                this.AddGradientStop(stops[i]);
            }
        };
        LinearGradient.prototype.AddGradientStop = function (stop) {
            if (stop == null) {
                throw "The given GradientStop is null.";
            }
            this.stops.push(stop);
            this.Native.appendChild(stop.Native);
        };
        LinearGradient.prototype.RemoveGradientStop = function (stop) {
            if (stop == null) {
                throw "The given GradientStop is null.";
            }
            if (!this.stops.contains(stop)) {
                return;
            }
            this.stops.remove(stop);
            this.Native.removeChild(stop.Native);
        };
        return LinearGradient;
    })();
    RadSVG.LinearGradient = LinearGradient;
    /**
    * Represents a gradient stop.
    */
    var GradientStop = (function () {
        function GradientStop(color, offset) {
            if (typeof color === "undefined") { color = null; }
            if (typeof offset === "undefined") { offset = null; }
            this.native = document.createElementNS(RadSVG.NS, "stop");
            this.color = Colors.White;
            if (color == null) {
                this.Color = Colors.White;
            } else {
                this.Color = color;
            }
            if (offset == null) {
                this.Offset = 0.0;
            } else {
                this.Offset = offset;
            }
        }
        Object.defineProperty(GradientStop.prototype, "Native", {
            get: function () {
                return this.native;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(GradientStop.prototype, "Offset", {
            get: function () {
                if (this.native.attributes["offset"] == null) {
                    return 0.0;
                }
                return parseFloat(this.native.attributes["offset"].value);
            },
            set: /**
            * Sets the offset where this gradient stop starts.The value should be in the [0,1] interval.
            */
            function (value) {
                this.native.setAttribute("offset", (value * 100.0).toString() + "%");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(GradientStop.prototype, "Color", {
            get: function () {
                return this.color;
            },
            set: function (value) {
                if (value == null) {
                    throw "The color cannot be null.";
                }
                this.color = value;
                this.native.style.stopColor = value.AsCSS1Color;
                this.native.style.stopOpacity = value.A == null ? "1.0" : value.A.toString();
            },
            enumerable: true,
            configurable: true
        });
        return GradientStop;
    })();
    RadSVG.GradientStop = GradientStop;
    /**
    * Represents an SVG color.
    */
    var Color = (function () {
        function Color(r, g, b, a) {
            if (typeof r === "undefined") { r = null; }
            if (typeof g === "undefined") { g = null; }
            if (typeof b === "undefined") { b = null; }
            if (typeof a === "undefined") { a = null; }
            this.r = 0.0;
            this.g = 0.0;
            this.b = 0.0;
            this.a = 1.0;
            if (r == null) {
                return;
            }
            if (typeof (r) == "string") {
                var s = r;
                if (s.substring(0, 1) == "#") {
                    s = s.substr(1);
                }
                //try predefined ones
                var known = Colors.Parse(s.toLowerCase());
                if (known != null) {
                    this.r = known.R;
                    this.g = known.G;
                    this.b = known.B;
                    return;
                }
                var c = Color.Parse(s.toUpperCase());
                if (c != null) {
                    this.r = c.R;
                    this.g = c.G;
                    this.b = c.B;
                    return;
                } else {
                    throw "The string '" + r + "' could not be converted to a color value.";
                }
            }
            if (typeof (r) == "number") {
                this.r = parseFloat(r);
                this.g = g;
                this.b = b;
                if (a != null) {
                    this.a = a;
                }
                this.FixValues();
            }
        }
        Object.defineProperty(Color.prototype, "R", {
            get: function () {
                return this.r;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Color.prototype, "G", {
            get: function () {
                return this.g;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Color.prototype, "B", {
            get: function () {
                return this.b;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Color.prototype, "A", {
            get: function () {
                return this.a;
            },
            enumerable: true,
            configurable: true
        });
        Color.Parse = function Parse(s) {
            if (s == null || s.length == 0) {
                throw "Empty string";
            }
            s = s.trim();
            var defs = ColorConverters.All;
            for (var i = 0; i < defs.length; i++) {
                var re = new RegExp(defs[i].RegEx);
                var processor = defs[i].Parse;
                var bits = re.exec(s);
                if (bits) {
                    var channels = processor(bits);
                    return new Color(channels[0], channels[1], channels[2]);
                }
            }
        };
        Object.defineProperty(Color.prototype, "AsCSS1", {
            get: function () {
                return 'fill:rgb(' + this.R + ', ' + this.G + ', ' + this.B + '); fill-opacity:' + this.a + ';';
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Color.prototype, "AsCSS1Color", {
            get: function () {
                return 'rgb(' + this.R + ', ' + this.G + ', ' + this.B + ')';
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Color.prototype, "AsCSS3", {
            get: function () {
                return 'fill:rgba(' + this.R + ', ' + this.G + ', ' + this.B + ', ' + this.a + ')';
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Color.prototype, "AsHex6", {
            get: function () {
                return ColorConverters.RgbToHex(this.r, this.g, this.b).toUpperCase();
            },
            enumerable: true,
            configurable: true
        });
        Color.prototype.FixValues = function () {
            this.r = (this.r < 0 || isNaN(this.r)) ? 0 : ((this.r > 255) ? 255 : this.r);
            this.g = (this.g < 0 || isNaN(this.g)) ? 0 : ((this.g > 255) ? 255 : this.g);
            this.b = (this.b < 0 || isNaN(this.b)) ? 0 : ((this.b > 255) ? 255 : this.b);
            this.a = (this.a < 0 || isNaN(this.a)) ? 0 : ((this.a > 255) ? 255 : this.a);
        };
        return Color;
    })();
    RadSVG.Color = Color;
    /**
    * Collects Color conversion utils.
    */
    var ColorConverters = (function () {
        function ColorConverters() { }
        Object.defineProperty(ColorConverters, "All", {
            get: /**
            * Returns an array of all color conversion methods.
            */
            function () {
                return [
                    ColorConverters.HEX6,
                    ColorConverters.HEX3,
                    ColorConverters.RGB
                ];
            },
            enumerable: true,
            configurable: true
        });
        ColorConverters.componentToHex = function componentToHex(c) {
            var hex = c.toString(16);
            return hex.length == 1 ? "0" + hex : hex;
        };
        ColorConverters.RgbToHex = /**
        * Converts the given RGB values to an hexadecimal representation.
        */
        function RgbToHex(r, g, b) {
            return "#" + ColorConverters.componentToHex(r) + ColorConverters.componentToHex(g) + ColorConverters.componentToHex(b);
        };
        Object.defineProperty(ColorConverters, "HEX6", {
            get: /**
            * Returns the hexadecimal (six characters) converter.
            */
            function () {
                return {
                    RegEx: "^(\\w{2})(\\w{2})(\\w{2})$",
                    Parse: function (bits) {
                        return [
                            parseInt(bits[1], 16),
                            parseInt(bits[2], 16),
                            parseInt(bits[3], 16)
                        ];
                    }
                };
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(ColorConverters, "HEX3", {
            get: /**
            * Returns the hexadecimal (three characters) converter.
            */
            function () {
                return {
                    RegEx: "^(\\w{1})(\\w{1})(\\w{1})$",
                    Parse: function (bits) {
                        return [
                            parseInt(bits[1] + bits[1], 16),
                            parseInt(bits[2] + bits[2], 16),
                            parseInt(bits[3] + bits[3], 16)
                        ];
                    }
                };
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(ColorConverters, "RGB", {
            get: /**
            * Returns the RGB converter.
            */
            function () {
                return {
                    RegEx: "^rgb\((\\d{1,3}),\\s*(\\d{1,3}),\\s*(\\d{1,3})\)$",
                    Parse: function (bits) {
                        return [
                            parseInt(bits[1]),
                            parseInt(bits[2]),
                            parseInt(bits[3])
                        ];
                    }
                };
            },
            enumerable: true,
            configurable: true
        });
        return ColorConverters;
    })();
    RadSVG.ColorConverters = ColorConverters;
    /**
    * A collection of predefined colors.
    */
    var Colors = (function () {
        function Colors() { }
        Colors.knownColors = {
            aliceblue: 'f0f8ff',
            antiquewhite: 'faebd7',
            aqua: '00ffff',
            aquamarine: '7fffd4',
            azure: 'f0ffff',
            beige: 'f5f5dc',
            bisque: 'ffe4c4',
            black: '000000',
            blanchedalmond: 'ffebcd',
            blue: '0000ff',
            blueviolet: '8a2be2',
            brown: 'a52a2a',
            burlywood: 'deb887',
            cadetblue: '5f9ea0',
            chartreuse: '7fff00',
            chocolate: 'd2691e',
            coral: 'ff7f50',
            cornflowerblue: '6495ed',
            cornsilk: 'fff8dc',
            crimson: 'dc143c',
            cyan: '00ffff',
            darkblue: '00008b',
            darkcyan: '008b8b',
            darkgoldenrod: 'b8860b',
            darkgray: 'a9a9a9',
            darkgreen: '006400',
            darkkhaki: 'bdb76b',
            darkmagenta: '8b008b',
            darkolivegreen: '556b2f',
            darkorange: 'ff8c00',
            darkorchid: '9932cc',
            darkred: '8b0000',
            darksalmon: 'e9967a',
            darkseagreen: '8fbc8f',
            darkslateblue: '483d8b',
            darkslategray: '2f4f4f',
            darkturquoise: '00ced1',
            darkviolet: '9400d3',
            deeppink: 'ff1493',
            deepskyblue: '00bfff',
            dimgray: '696969',
            dodgerblue: '1e90ff',
            feldspar: 'd19275',
            firebrick: 'b22222',
            floralwhite: 'fffaf0',
            forestgreen: '228b22',
            fuchsia: 'ff00ff',
            gainsboro: 'dcdcdc',
            ghostwhite: 'f8f8ff',
            gold: 'ffd700',
            goldenrod: 'daa520',
            gray: '808080',
            green: '008000',
            greenyellow: 'adff2f',
            honeydew: 'f0fff0',
            hotpink: 'ff69b4',
            indianred: 'cd5c5c',
            indigo: '4b0082',
            ivory: 'fffff0',
            khaki: 'f0e68c',
            lavender: 'e6e6fa',
            lavenderblush: 'fff0f5',
            lawngreen: '7cfc00',
            lemonchiffon: 'fffacd',
            lightblue: 'add8e6',
            lightcoral: 'f08080',
            lightcyan: 'e0ffff',
            lightgoldenrodyellow: 'fafad2',
            lightgrey: 'd3d3d3',
            lightgreen: '90ee90',
            lightpink: 'ffb6c1',
            lightsalmon: 'ffa07a',
            lightseagreen: '20b2aa',
            lightskyblue: '87cefa',
            lightslateblue: '8470ff',
            lightslategray: '778899',
            lightsteelblue: 'b0c4de',
            lightyellow: 'ffffe0',
            lime: '00ff00',
            limegreen: '32cd32',
            linen: 'faf0e6',
            magenta: 'ff00ff',
            maroon: '800000',
            mediumaquamarine: '66cdaa',
            mediumblue: '0000cd',
            mediumorchid: 'ba55d3',
            mediumpurple: '9370d8',
            mediumseagreen: '3cb371',
            mediumslateblue: '7b68ee',
            mediumspringgreen: '00fa9a',
            mediumturquoise: '48d1cc',
            mediumvioletred: 'c71585',
            midnightblue: '191970',
            mintcream: 'f5fffa',
            mistyrose: 'ffe4e1',
            moccasin: 'ffe4b5',
            navajowhite: 'ffdead',
            navy: '000080',
            oldlace: 'fdf5e6',
            olive: '808000',
            olivedrab: '6b8e23',
            orange: 'ffa500',
            orangered: 'ff4500',
            orchid: 'da70d6',
            palegoldenrod: 'eee8aa',
            palegreen: '98fb98',
            paleturquoise: 'afeeee',
            palevioletred: 'd87093',
            papayawhip: 'ffefd5',
            peachpuff: 'ffdab9',
            peru: 'cd853f',
            pink: 'ffc0cb',
            plum: 'dda0dd',
            powderblue: 'b0e0e6',
            purple: '800080',
            red: 'ff0000',
            rosybrown: 'bc8f8f',
            royalblue: '4169e1',
            saddlebrown: '8b4513',
            salmon: 'fa8072',
            sandybrown: 'f4a460',
            seagreen: '2e8b57',
            seashell: 'fff5ee',
            sienna: 'a0522d',
            silver: 'c0c0c0',
            skyblue: '87ceeb',
            slateblue: '6a5acd',
            slategray: '708090',
            snow: 'fffafa',
            springgreen: '00ff7f',
            steelblue: '4682b4',
            tan: 'd2b48c',
            teal: '008080',
            thistle: 'd8bfd8',
            tomato: 'ff6347',
            turquoise: '40e0d0',
            violet: 'ee82ee',
            violetred: 'd02090',
            wheat: 'f5deb3',
            white: 'ffffff',
            whitesmoke: 'f5f5f5',
            yellow: 'ffff00',
            yellowgreen: '9acd32'
        };
        Colors.Parse = function Parse(name) {
            for (var key in Colors.knownColors) {
                if (name == key) {
                    return new Color(Colors.knownColors[key]);
                }
            }
            return null;
        };
        Object.defineProperty(Colors, "AliceBlue", {
            get: function () {
                return new Color("F0F8FF");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "AntiqueWhite", {
            get: function () {
                return new Color("FAEBD7");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "Aqua", {
            get: function () {
                return new Color("00FFFF");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "Aquamarine", {
            get: function () {
                return new Color("7FFFD4");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "Azure", {
            get: function () {
                return new Color("F0FFFF");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "Beige", {
            get: function () {
                return new Color("F5F5DC");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "Bisque", {
            get: function () {
                return new Color("FFE4C4");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "Black", {
            get: function () {
                return new Color("000000");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "BlanchedAlmond", {
            get: function () {
                return new Color("	FFEBCD");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "Blue", {
            get: function () {
                return new Color("0000FF");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "BlueViolet", {
            get: function () {
                return new Color("8A2BE2");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "Brown", {
            get: function () {
                return new Color("A52A2A");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "BurlyWood", {
            get: function () {
                return new Color("DEB887");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "CadetBlue", {
            get: function () {
                return new Color("5F9EA0");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "Chartreuse", {
            get: function () {
                return new Color("7FFF00");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "Chocolate", {
            get: function () {
                return new Color("D2691E");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "Coral", {
            get: function () {
                return new Color("FF7F50");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "CornflowerBlue", {
            get: function () {
                return new Color("	6495ED");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "Cornsilk", {
            get: function () {
                return new Color("FFF8DC");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "Crimson", {
            get: function () {
                return new Color("DC143C");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "Cyan", {
            get: function () {
                return new Color("00FFFF");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "DarkBlue", {
            get: function () {
                return new Color("00008B");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "DarkCyan", {
            get: function () {
                return new Color("008B8B");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "DarkGoldenRod", {
            get: function () {
                return new Color("	B8860B");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "DarkGray", {
            get: function () {
                return new Color("A9A9A9");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "DarkGreen", {
            get: function () {
                return new Color("006400");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "DarkKhaki", {
            get: function () {
                return new Color("BDB76B");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "DarkMagenta", {
            get: function () {
                return new Color("8B008B");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "DarkOliveGreen", {
            get: function () {
                return new Color("	556B2F");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "Darkorange", {
            get: function () {
                return new Color("FF8C00");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "DarkOrchid", {
            get: function () {
                return new Color("9932CC");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "DarkRed", {
            get: function () {
                return new Color("8B0000");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "DarkSalmon", {
            get: function () {
                return new Color("E9967A");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "DarkSeaGreen", {
            get: function () {
                return new Color("8FBC8F");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "DarkSlateBlue", {
            get: function () {
                return new Color("	483D8B");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "DarkSlateGray", {
            get: function () {
                return new Color("	2F4F4F");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "DarkTurquoise", {
            get: function () {
                return new Color("	00CED1");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "DarkViolet", {
            get: function () {
                return new Color("9400D3");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "DeepPink", {
            get: function () {
                return new Color("FF1493");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "DeepSkyBlue", {
            get: function () {
                return new Color("00BFFF");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "DimGray", {
            get: function () {
                return new Color("696969");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "DimGrey", {
            get: function () {
                return new Color("696969");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "DodgerBlue", {
            get: function () {
                return new Color("1E90FF");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "FireBrick", {
            get: function () {
                return new Color("B22222");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "FloralWhite", {
            get: function () {
                return new Color("FFFAF0");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "ForestGreen", {
            get: function () {
                return new Color("228B22");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "Fuchsia", {
            get: function () {
                return new Color("FF00FF");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "Gainsboro", {
            get: function () {
                return new Color("DCDCDC");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "GhostWhite", {
            get: function () {
                return new Color("F8F8FF");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "Gold", {
            get: function () {
                return new Color("FFD700");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "GoldenRod", {
            get: function () {
                return new Color("DAA520");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "Gray", {
            get: function () {
                return new Color("808080");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "Green", {
            get: function () {
                return new Color("008000");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "GreenYellow", {
            get: function () {
                return new Color("ADFF2F");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "HoneyDew", {
            get: function () {
                return new Color("F0FFF0");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "HotPink", {
            get: function () {
                return new Color("FF69B4");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "IndianRed", {
            get: function () {
                return new Color("CD5C5C");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "Indigo", {
            get: function () {
                return new Color("4B0082");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "Ivory", {
            get: function () {
                return new Color("FFFFF0");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "Khaki", {
            get: function () {
                return new Color("F0E68C");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "Lavender", {
            get: function () {
                return new Color("E6E6FA");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "LavenderBlush", {
            get: function () {
                return new Color("	FFF0F5");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "LawnGreen", {
            get: function () {
                return new Color("7CFC00");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "LemonChiffon", {
            get: function () {
                return new Color("FFFACD");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "LightBlue", {
            get: function () {
                return new Color("ADD8E6");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "LightCoral", {
            get: function () {
                return new Color("F08080");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "LightCyan", {
            get: function () {
                return new Color("E0FFFF");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "LightGoldenRodYellow", {
            get: function () {
                return new Color("	FAFAD2");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "LightGray", {
            get: function () {
                return new Color("D3D3D3");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "LightGreen", {
            get: function () {
                return new Color("90EE90");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "LightPink", {
            get: function () {
                return new Color("FFB6C1");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "LightSalmon", {
            get: function () {
                return new Color("FFA07A");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "LightSeaGreen", {
            get: function () {
                return new Color("	20B2AA");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "LightSkyBlue", {
            get: function () {
                return new Color("87CEFA");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "LightSlateGray", {
            get: function () {
                return new Color("	778899");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "LightSteelBlue", {
            get: function () {
                return new Color("	B0C4DE");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "LightYellow", {
            get: function () {
                return new Color("FFFFE0");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "Lime", {
            get: function () {
                return new Color("00FF00");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "LimeGreen", {
            get: function () {
                return new Color("32CD32");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "Linen", {
            get: function () {
                return new Color("FAF0E6");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "Magenta", {
            get: function () {
                return new Color("FF00FF");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "Maroon", {
            get: function () {
                return new Color("800000");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "MediumAquaMarine", {
            get: function () {
                return new Color("	66CDAA");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "MediumBlue", {
            get: function () {
                return new Color("0000CD");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "MediumOrchid", {
            get: function () {
                return new Color("BA55D3");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "MediumPurple", {
            get: function () {
                return new Color("9370DB");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "MediumSeaGreen", {
            get: function () {
                return new Color("	3CB371");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "MediumSlateBlue", {
            get: function () {
                return new Color("	7B68EE");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "MediumSpringGreen", {
            get: function () {
                return new Color("	00FA9A");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "MediumTurquoise", {
            get: function () {
                return new Color("	48D1CC");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "MediumVioletRed", {
            get: function () {
                return new Color("	C71585");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "MidnightBlue", {
            get: function () {
                return new Color("191970");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "MintCream", {
            get: function () {
                return new Color("F5FFFA");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "MistyRose", {
            get: function () {
                return new Color("FFE4E1");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "Moccasin", {
            get: function () {
                return new Color("FFE4B5");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "NavajoWhite", {
            get: function () {
                return new Color("FFDEAD");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "Navy", {
            get: function () {
                return new Color("000080");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "OldLace", {
            get: function () {
                return new Color("FDF5E6");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "Olive", {
            get: function () {
                return new Color("808000");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "OliveDrab", {
            get: function () {
                return new Color("6B8E23");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "Orange", {
            get: function () {
                return new Color("FFA500");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "OrangeRed", {
            get: function () {
                return new Color("FF4500");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "Orchid", {
            get: function () {
                return new Color("DA70D6");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "PaleGoldenRod", {
            get: function () {
                return new Color("	EEE8AA");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "PaleGreen", {
            get: function () {
                return new Color("98FB98");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "PaleTurquoise", {
            get: function () {
                return new Color("	AFEEEE");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "PaleVioletRed", {
            get: function () {
                return new Color("	DB7093");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "PapayaWhip", {
            get: function () {
                return new Color("FFEFD5");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "PeachPuff", {
            get: function () {
                return new Color("FFDAB9");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "Peru", {
            get: function () {
                return new Color("CD853F");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "Pink", {
            get: function () {
                return new Color("FFC0CB");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "Plum", {
            get: function () {
                return new Color("DDA0DD");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "PowderBlue", {
            get: function () {
                return new Color("B0E0E6");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "Purple", {
            get: function () {
                return new Color("800080");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "Red", {
            get: function () {
                return new Color("FF0000");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "RosyBrown", {
            get: function () {
                return new Color("BC8F8F");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "RoyalBlue", {
            get: function () {
                return new Color("4169E1");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "SaddleBrown", {
            get: function () {
                return new Color("8B4513");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "Salmon", {
            get: function () {
                return new Color("FA8072");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "SandyBrown", {
            get: function () {
                return new Color("F4A460");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "SeaGreen", {
            get: function () {
                return new Color("2E8B57");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "SeaShell", {
            get: function () {
                return new Color("FFF5EE");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "Sienna", {
            get: function () {
                return new Color("A0522D");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "Silver", {
            get: function () {
                return new Color("C0C0C0");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "SkyBlue", {
            get: function () {
                return new Color("87CEEB");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "SlateBlue", {
            get: function () {
                return new Color("6A5ACD");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "SlateGray", {
            get: function () {
                return new Color("708090");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "Snow", {
            get: function () {
                return new Color("FFFAFA");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "SpringGreen", {
            get: function () {
                return new Color("0FF7F");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "SteelBlue", {
            get: function () {
                return new Color("4682B4");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "Tan", {
            get: function () {
                return new Color("D2B48C");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "Teal", {
            get: function () {
                return new Color("008080");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "Thistle", {
            get: function () {
                return new Color("D8BFD8");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "Tomato", {
            get: function () {
                return new Color("FF6347");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "Turquoise", {
            get: function () {
                return new Color("40E0D0");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "Violet", {
            get: function () {
                return new Color("EE82EE");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "Wheat", {
            get: function () {
                return new Color("F5DEB3");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "White", {
            get: function () {
                return new Color("FFFFFF");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "WhiteSmoke", {
            get: function () {
                return new Color("F5F5F5");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "Yellow", {
            get: function () {
                return new Color("FFFF00");
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Colors, "YellowGreen", {
            get: function () {
                return new Color("9ACD32");
            },
            enumerable: true,
            configurable: true
        });
        return Colors;
    })();
    RadSVG.Colors = Colors;
    var Gradients = (function () {
        function Gradients() { }
        Object.defineProperty(Gradients, "BlueWhite", {
            get: function () {
                var g = new LinearGradient();
                g.Id = "BlueWhite";
                var b = new GradientStop(Colors.SteelBlue, 0);
                var w = new GradientStop(Colors.White, 1);
                g.AddGradientStops(b, w);
                return g;
            },
            enumerable: true,
            configurable: true
        });
        return Gradients;
    })();
    RadSVG.Gradients = Gradients;
    /**
    * A line visual.
    */
    var Line = (function (_super) {
        __extends(Line, _super);
        /**
        * Instantiates a new Line.
        */
        function Line(from, to) {
            if (typeof from === "undefined") { from = null; }
            if (typeof to === "undefined") { to = null; }
            _super.call(this);
            this.native = document.createElementNS(RadSVG.NS, "line");
            this.Initialize(this.native);
            this.From = from;
            this.To = to;
        }
        Object.defineProperty(Line.prototype, "From", {
            get: /**
            * Gets the point where the line starts.
            */
            function () {
                return this.from;
            },
            set: /**
            * Sets the point where the line starts.
            */
            function (value) {
                if (this.from != value) {
                    this.Native.setAttribute("x1", value.X.toString());
                    this.Native.setAttribute("y1", value.Y.toString());
                    this.from = value;
                }
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Line.prototype, "To", {
            get: /**
            * Gets the point where the line ends.
            */
            function () {
                return this.to;
            },
            set: /**
            * Sets the point where the line ends.
            */
            function (value) {
                if (this.to != value) {
                    this.Native.setAttribute("x2", value.X.toString());
                    this.Native.setAttribute("y2", value.Y.toString());
                    this.to = value;
                }
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Line.prototype, "Opacity", {
            get: /**
            * Gets the opacity.
            */
            function () {
                if (this.Native.attributes["fill-opacity"] == null) {
                    return 1.0;
                }
                return parseFloat(this.Native.attributes["fill-opacity"].value);
            },
            set: /**
            * Sets the opacity.
            */
            function (value) {
                if (value < 0 || value > 1.0) {
                    throw "The opacity should be in the [0,1] interval.";
                }
                this.Native.setAttribute("fill-opacity", value.toString());
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Line.prototype, "StrokeThickness", {
            get: function () {
                if (this.Native.attributes["stroke-width"] == null) {
                    return 0.0;
                }
                return parseFloat(this.Native.attributes["stroke-width"].value);
            },
            set: function (value) {
                this.Native.setAttribute("stroke-width", value.toString());
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Line.prototype, "Stroke", {
            get: function () {
                if (this.Native.attributes["stroke"] == null) {
                    return null;
                }
                return this.Native.attributes["stroke"].value;
            },
            set: function (value) {
                this.Native.setAttribute("stroke", value);
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Line.prototype, "StrokeDash", {
            get: function () {
                if (this.Native.attributes["stroke-dasharray"] == null) {
                    return null;
                }
                return this.Native.attributes["stroke-dasharray"].value;
            },
            set: function (value) {
                this.Native.setAttribute("stroke-dasharray", value);
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Line.prototype, "MarkerEnd", {
            get: function () {
                if (this.Native.attributes["marker-end"] == null) {
                    return null;
                }
                var s = this.Native.attributes["marker-end"].value.toString();
                var id = s.substr(5, s.length - 6);
                var markers = this.Canvas.Markers;
                for (var i = 0; i < markers.length; i++) {
                    if (markers[i].Id == id) {
                        return markers[i];
                    }
                }
                throw "Marker '" + id + "' could not be found in the <defs> collection.";
            },
            set: function (value) {
                if (value.Id == null) {
                    throw "The Marker needs an Id.";
                }
                var s = "url(#" + value.Id + ")";
                this.Native.setAttribute("marker-end", s);
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Line.prototype, "MarkerStart", {
            get: function () {
                if (this.Native.attributes["marker-start"] == null) {
                    return null;
                }
                var s = this.Native.attributes["marker-start"].value.toString();
                var id = s.substr(5, s.length - 6);
                var markers = this.Canvas.Markers;
                for (var i = 0; i < markers.length; i++) {
                    if (markers[i].Id == id) {
                        return markers[i];
                    }
                }
                throw "Marker '" + id + "' could not be found in the <defs> collection.";
            },
            set: function (value) {
                if (value.Id == null) {
                    throw "The Marker needs an Id.";
                }
                var s = "url(#" + value.Id + ")";
                this.Native.setAttribute("marker-start", s);
            },
            enumerable: true,
            configurable: true
        });
        return Line;
    })(Visual);
    RadSVG.Line = Line;
    /**
    * A polyline visual.
    */
    var Polyline = (function (_super) {
        __extends(Polyline, _super);
        /**
        * Instantiates a new Line.
        */
        function Polyline(points) {
            if (typeof points === "undefined") { points = null; }
            _super.call(this);
            this.native = document.createElementNS(RadSVG.NS, "polyline");
            this.Initialize(this.native);
            if (points != null) {
                this.Points = points;
            } else {
                this.points = [];
            }
            this.Stroke = "Black";
            this.StrokeThickness = 2;
            this.Background = "none";
        }
        Object.defineProperty(Polyline.prototype, "Background", {
            get: /**
            * Gets the fill of the polyline.
            */
            function () {
                return this.native.style.fill;
            },
            set: /**
            * Sets the fill of the polyline.
            */
            function (v) {
                if (typeof (v) == "string") {
                    this.native.style.fill = v;
                }
                if (typeof (v) == "object") {
                    var gr = v;
                    if (gr != null) {
                        if (gr.Id == null) {
                            throw "The gradient needs an Id.";
                        }
                        this.native.style.fill = "url(#" + gr.Id + ")";
                    }
                }
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Polyline.prototype, "Points", {
            get: /**
            * Gets the points of the polyline.
            */
            function () {
                return this.points;
            },
            set: /**
            * Sets the points of the polyline.
            */
            function (value) {
                if (this.points != value) {
                    if (value == null || value.length == 0) {
                        this.Native.setAttribute("points", null);
                    } else {
                        var s = "";
                        for (var i = 0; i < value.length; i++) {
                            s += " " + value[i].X + "," + value[i].Y;
                        }
                        s = s.trim();
                        this.Native.setAttribute("points", s);
                    }
                    this.points = value;
                }
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Polyline.prototype, "Opacity", {
            get: /**
            * Gets the opacity.
            */
            function () {
                if (this.Native.attributes["fill-opacity"] == null) {
                    return 1.0;
                }
                return parseFloat(this.Native.attributes["fill-opacity"].value);
            },
            set: /**
            * Sets the opacity.
            */
            function (value) {
                if (value < 0 || value > 1.0) {
                    throw "The opacity should be in the [0,1] interval.";
                }
                this.Native.setAttribute("fill-opacity", value.toString());
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Polyline.prototype, "StrokeThickness", {
            get: function () {
                if (this.Native.attributes["stroke-width"] == null) {
                    return 0.0;
                }
                return parseFloat(this.Native.attributes["stroke-width"].value);
            },
            set: function (value) {
                this.Native.setAttribute("stroke-width", value.toString());
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Polyline.prototype, "Stroke", {
            get: function () {
                if (this.Native.attributes["stroke"] == null) {
                    return null;
                }
                return this.Native.attributes["stroke"].value;
            },
            set: function (value) {
                this.Native.setAttribute("stroke", value);
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Polyline.prototype, "StrokeDash", {
            get: function () {
                if (this.Native.attributes["stroke-dasharray"] == null) {
                    return null;
                }
                return this.Native.attributes["stroke-dasharray"].value;
            },
            set: function (value) {
                this.Native.setAttribute("stroke-dasharray", value);
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Polyline.prototype, "MarkerEnd", {
            get: function () {
                if (this.Native.attributes["marker-end"] == null) {
                    return null;
                }
                var s = this.Native.attributes["marker-end"].value.toString();
                var id = s.substr(5, s.length - 6);
                var markers = this.Canvas.Markers;
                for (var i = 0; i < markers.length; i++) {
                    if (markers[i].Id == id) {
                        return markers[i];
                    }
                }
                throw "Marker '" + id + "' could not be found in the <defs> collection.";
            },
            set: function (value) {
                if (value.Id == null) {
                    throw "The Marker needs an Id.";
                }
                var s = "url(#" + value.Id + ")";
                this.Native.setAttribute("marker-end", s);
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Polyline.prototype, "MarkerStart", {
            get: function () {
                if (this.Native.attributes["marker-start"] == null) {
                    return null;
                }
                var s = this.Native.attributes["marker-start"].value.toString();
                var id = s.substr(5, s.length - 6);
                var markers = this.Canvas.Markers;
                for (var i = 0; i < markers.length; i++) {
                    if (markers[i].Id == id) {
                        return markers[i];
                    }
                }
                throw "Marker '" + id + "' could not be found in the <defs> collection.";
            },
            set: function (value) {
                if (value.Id == null) {
                    throw "The Marker needs an Id.";
                }
                var s = "url(#" + value.Id + ")";
                this.Native.setAttribute("marker-start", s);
            },
            enumerable: true,
            configurable: true
        });
        return Polyline;
    })(Visual);
    RadSVG.Polyline = Polyline;
    /**
    * A group visual.
    */
    var Group = (function (_super) {
        __extends(Group, _super);
        /**
        * Instantiates a new group.
        */
        function Group() {
            _super.call(this);
            this.native = document.createElementNS(RadSVG.NS, "g");
            this.Initialize(this.native);
            this.position = new Point(0, 0);
        }
        Object.defineProperty(Group.prototype, "Position", {
            get: /**
            * Gets the position of this group.
            */
            function () {
                return this.position;
            },
            set: /**
            * Sets the position of this group.
            */
            function (p) {
                this.position = p;
                try {
                    if (this.native.ownerSVGElement == null) {
                        this.native.setAttribute("transform", "translate(" + p.X + "," + p.Y + ")");
                        return;
                    }
                } catch (err) {
                    return;
                }
                var tr = this.native.ownerSVGElement.createSVGTransform();
                tr.setTranslate(p.X, p.Y);
                if (this.native.transform.baseVal.numberOfItems == 0) {
                    this.native.transform.baseVal.appendItem(tr);
                } else {
                    this.native.transform.baseVal.replaceItem(tr, 0);
                }
            },
            enumerable: true,
            configurable: true
        });
        Group.prototype.Append = /**
        * Appends a visual to this group.
        */
        function (visual) {
            this.Native.appendChild(visual.Native);
            visual.Canvas = this.Canvas;
        };
        Group.prototype.Remove = function (visual) {
            this.Native.removeChild(visual.Native);
        };
        return Group;
    })(Visual);
    RadSVG.Group = Group;
    /**
    * Defines a rectangular region.
    */
    var Rect = (function () {
        /**
        * Instantiates a new rectangle.
        * @param x The horizontal coordinate of the upper-left corner.
        * @param y The vertical coordinate of the upper-left corner.
        * @param width The width of the rectangle.
        * @param height The height of the rectangle.
        */
        function Rect(x, y, width, height) {
            if (typeof x === "undefined") { x = NaN; }
            if (typeof y === "undefined") { y = NaN; }
            if (typeof width === "undefined") { width = NaN; }
            if (typeof height === "undefined") { height = NaN; }
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }
        Rect.prototype.Contains = /**
        * Determines whether the given point is contained inside this rectangle.
        */
        function (point) {
            if (isNaN(this.X) || isNaN(this.Y) || isNaN(this.Width) || isNaN(this.Height)) {
                throw "This rectangle is not fully specified and containment is hence undefined.";
            }
            if (isNaN(point.X) || isNaN(point.Y)) {
                throw "The point is not fully specified (NaN) and containment is hence undefined.";
            }
            return ((point.X >= this.X) && (point.X <= (this.X + this.Width)) && (point.Y >= this.Y) && (point.Y <= (this.Y + this.Height)));
        };
        Rect.prototype.Inflate = /**
        * Inflates this rectangle with the given amount.
        * @param dx The horizontal inflation which is also the vertical one if the vertical inflation is not specified.
        * @param dy The vertical inflation.
        */
        function (dx, dy) {
            if (typeof dy === "undefined") { dy = null; }
            if (dy == null) {
                dy = dx;
            }
            this.X -= dx;
            this.Y -= dy;
            this.Width += dx + dx + 1;
            this.Height += dy + dy + 1;
            return this;
        };
        Rect.prototype.Offset = /**
        * Offsets this rectangle with the given amount.
        * @param dx The horizontal offset which is also the vertical one if the vertical offset is not specified.
        * @param dy The vertical offset.
        */
        function (dx, dy) {
            if (typeof dy === "undefined") { dy = NaN; }
            if (isNaN(dy)) {
                dy = dx;
            }
            this.X += dx;
            this.Y += dy;
        };
        Rect.prototype.Union = /**
        * Returns the union of the current rectangle with the given one.
        * @param r The rectangle to union with the current one.
        */
        function (r) {
            var x1 = Math.min(this.X, r.X);
            var y1 = Math.min(this.Y, r.Y);
            var x2 = Math.max((this.X + this.Width), (r.X + r.Width));
            var y2 = Math.max((this.Y + this.Height), (r.Y + r.Height));
            return new Rect(x1, y1, x2 - x1, y2 - y1);
        };
        Object.defineProperty(Rect.prototype, "TopLeft", {
            get: /**
            * Get the upper-left corner position of this rectangle.
            */
            function () {
                return new Point(this.X, this.Y);
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Rect.prototype, "BottomRight", {
            get: /**
            * Get the bottom-right corner position of this rectangle.
            */
            function () {
                return new Point(this.X + this.Width, this.Y + this.Height);
            },
            enumerable: true,
            configurable: true
        });
        Rect.prototype.Clone = /**
        * Returns a clone of this rectangle.
        */
        function () {
            return new Rect(this.X, this.Y, this.Width, this.Height);
        };
        Rect.Create = function Create(x, y, w, h) {
            return new Rect(x, y, w, h);
        };
        Object.defineProperty(Rect, "Empty", {
            get: function () {
                return new Rect(0, 0, 0, 0);
            },
            enumerable: true,
            configurable: true
        });
        Rect.FromPoints = function FromPoints(p, q) {
            if (isNaN(p.X) || isNaN(p.Y) || isNaN(q.X) || isNaN(q.Y)) {
                throw "Some values are NaN.";
            }
            return new Rect(Math.min(p.X, q.X), Math.min(p.Y, q.Y), Math.abs(p.X - q.X), Math.abs(p.Y - q.Y));
        };
        return Rect;
    })();
    RadSVG.Rect = Rect;
    /**
    * The Point structure.
    */
    var Point = (function () {
        function Point(x, y) {
            this.X = x;
            this.Y = y;
        }
        Object.defineProperty(Point, "Empty", {
            get: function () {
                return new Point(0, 0);
            },
            enumerable: true,
            configurable: true
        });
        Point.prototype.Plus = function (p) {
            return new Point(this.X + p.X, this.Y + p.Y);
        };
        Point.prototype.Minus = function (p) {
            return new Point(this.X - p.X, this.Y - p.Y);
        };
        Point.prototype.Times = function (s) {
            return new Point(this.X * s, this.Y * s);
        };
        Point.prototype.Normalize = function () {
            if (this.Length == 0) {
                return Point.Empty;
            }
            return this.Times(1 / this.Length);
        };
        Object.defineProperty(Point.prototype, "Length", {
            get: function () {
                return Math.sqrt(this.X * this.X + this.Y * this.Y);
            },
            enumerable: true,
            configurable: true
        });
        Point.prototype.toString = function () {
            return "(" + this.X + "," + this.Y + ")";
        };
        Object.defineProperty(Point.prototype, "LengthSquared", {
            get: function () {
                return (this.X * this.X + this.Y * this.Y);
            },
            enumerable: true,
            configurable: true
        });
        Point.MiddleOf = function MiddleOf(p, q) {
            return new Point(q.X - p.X, q.Y - p.Y).Times(0.5).Plus(p);
        };
        Point.prototype.ToPolar = function (useDegrees) {
            if (typeof useDegrees === "undefined") { useDegrees = false; }
            var factor = 1;
            if (useDegrees) {
                factor = 180 / Math.PI;
            }
            var a = Math.atan2(Math.abs(this.Y), Math.abs(this.X));
            var halfpi = Math.PI / 2;
            if (this.X == 0) {
                // note that the angle goes down and not the usual mathematical convention
                if (this.Y == 0) {
                    return new Polar(0, 0);
                }
                if (this.Y > 0) {
                    return new Polar(this.Length, factor * halfpi);
                }
                if (this.Y < 0) {
                    return new Polar(this.Length, factor * 3 * halfpi);
                }
            } else if (this.X > 0) {
                if (this.Y == 0) {
                    return new Polar(this.Length, 0);
                }
                if (this.Y > 0) {
                    return new Polar(this.Length, factor * a);
                }
                if (this.Y < 0) {
                    return new Polar(this.Length, factor * (4 * halfpi - a));
                }
            } else {
                if (this.Y == 0) {
                    return new Polar(this.Length, 2 * halfpi);
                }
                if (this.Y > 0) {
                    return new Polar(this.Length, factor * (2 * halfpi - a));
                }
                if (this.Y < 0) {
                    return new Polar(this.Length, factor * (2 * halfpi + a));
                }
            }
        };
        return Point;
    })();
    RadSVG.Point = Point;
    var Polar = (function () {
        function Polar(r, a) {
            if (typeof r === "undefined") { r = null; }
            if (typeof a === "undefined") { a = null; }
            this.R = r;
            this.Angle = a;
        }
        return Polar;
    })();
    RadSVG.Polar = Polar;
    var Matrix = (function () {
        function Matrix(a, b, c, d, e, f) {
            if (typeof a === "undefined") { a = null; }
            if (typeof b === "undefined") { b = null; }
            if (typeof c === "undefined") { c = null; }
            if (typeof d === "undefined") { d = null; }
            if (typeof e === "undefined") { e = null; }
            if (typeof f === "undefined") { f = null; }
            if (a != null) {
                this.a = a;
            }
            if (b != null) {
                this.b = b;
            }
            if (c != null) {
                this.c = c;
            }
            if (d != null) {
                this.d = d;
            }
            if (e != null) {
                this.e = e;
            }
            if (f != null) {
                this.f = f;
            }
        }
        Matrix.prototype.Plus = function (m) {
            this.a += m.a;
            this.b += m.b;
            this.c += m.c;
            this.d += m.d;
            this.e += m.e;
            this.f += m.f;
        };
        Matrix.prototype.Minus = function (m) {
            this.a -= m.a;
            this.b -= m.b;
            this.c -= m.c;
            this.d -= m.d;
            this.e -= m.e;
            this.f -= m.f;
        };
        Matrix.prototype.Times = function (m) {
            return Matrix.FromList([
                this.a * m.a + this.c * m.b,
                this.b * m.a + this.d * m.b,
                this.a * m.c + this.c * m.d,
                this.b * m.c + this.d * m.d,
                this.a * m.e + this.c * m.f + this.e,
                this.b * m.e + this.d * m.f + this.f
            ]);
        };
        Matrix.prototype.Apply = function (p) {
            return new Point(this.a * p.X + this.c * p.Y + this.e, this.b * p.X + this.d * p.Y + this.f);
        };
        Matrix.FromSVGMatrix = function FromSVGMatrix(vm) {
            var m = new Matrix();
            m.a = vm.a;
            m.b = vm.b;
            m.c = vm.c;
            m.d = vm.d;
            m.e = vm.e;
            m.f = vm.f;
            return m;
        };
        Matrix.FromMatrixVector = function FromMatrixVector(v) {
            var m = new Matrix();
            m.a = v.a;
            m.b = v.b;
            m.c = v.c;
            m.d = v.d;
            m.e = v.e;
            m.f = v.f;
            return m;
        };
        Matrix.FromList = function FromList(v) {
            if (v.length != 6) {
                throw "The given list should consist of six elements.";
            }
            var m = new Matrix();
            m.a = v[0];
            m.b = v[1];
            m.c = v[2];
            m.d = v[3];
            m.e = v[4];
            m.f = v[5];
            return m;
        };
        Matrix.Translation = function Translation(x, y) {
            var m = new Matrix();
            m.a = 1;
            m.b = 0;
            m.c = 0;
            m.d = 1;
            m.e = x;
            m.f = y;
            return m;
        };
        Object.defineProperty(Matrix, "Unit", {
            get: function () {
                return Matrix.FromList([
                    1,
                    0,
                    0,
                    1,
                    0,
                    0
                ]);
            },
            enumerable: true,
            configurable: true
        });
        Matrix.prototype.toString = function () {
            return "matrix(" + this.a + " " + this.b + " " + this.c + " " + this.d + " " + this.e + " " + this.f + ")";
        };
        Matrix.Rotation = /*
        * Returns the rotation matrix for the given angle.
        * @param angle The angle in radians.
        */
        function Rotation(angle, x, y) {
            if (typeof x === "undefined") { x = 0; }
            if (typeof y === "undefined") { y = 0; }
            var m = new Matrix();
            m.a = Math.cos(angle * Math.PI / 180);
            m.b = Math.sin(angle * Math.PI / 180);
            m.c = -m.b;
            m.d = m.a;
            m.e = x - x * m.a + y * m.b;
            m.f = y - y * m.a - x * m.b;
            return m;
        };
        Matrix.Scaling = /*
        * Returns the scaling matrix for the given factor.
        * @param factor The scaling factor.
        */
        function Scaling(scaleX, scaleY) {
            if (typeof scaleY === "undefined") { scaleY = null; }
            if (scaleY == null) {
                scaleY = scaleX;
            }
            var m = new Matrix();
            m.a = scaleX;
            m.b = 0;
            m.c = 0;
            m.d = scaleY;
            m.e = 0;
            m.f = 0;
            return m;
        };
        Matrix.Parse = function Parse(v) {
            if (v == null) {
                return null;
            }
            v = v.trim();
            // of the form "matrix(...)"
            if (v.slice(0, 6).toLowerCase() == "matrix") {
                var nums = v.slice(7, v.length - 1).trim();
                var parts = nums.split(",");
                if (parts.length == 6) {
                    return Matrix.FromList(parts.map(function (p) {
                        return parseFloat(p);
                    }));
                }
                parts = nums.split(" ");
                if (parts.length == 6) {
                    return Matrix.FromList(parts.map(function (p) {
                        return parseFloat(p);
                    }));
                }
            }
            // of the form "(...)"
            if (v.slice(0, 1) == "(" && v.slice(v.length - 1) == ")") {
                v = v.substr(1, v.length - 1);
            }
            if (v.indexOf(",") > 0) {
                var parts = v.split(",");
                if (parts.length == 6) {
                    return Matrix.FromList(parts.map(function (p) {
                        return parseFloat(p);
                    }));
                }
            }
            if (v.indexOf(" ") > 0) {
                var parts = v.split(" ");
                if (parts.length == 6) {
                    return Matrix.FromList(parts.map(function (p) {
                        return parseFloat(p);
                    }));
                }
            }
            return null;
        };
        return Matrix;
    })();
    RadSVG.Matrix = Matrix;
    var MatrixVector = (function () {
        //constructor();
        function MatrixVector(a, b, c, d, e, f) {
            if (typeof a === "undefined") { a = null; }
            if (typeof b === "undefined") { b = null; }
            if (typeof c === "undefined") { c = null; }
            if (typeof d === "undefined") { d = null; }
            if (typeof e === "undefined") { e = null; }
            if (typeof f === "undefined") { f = null; }
            if (a != null) {
                this.a = a;
            }
            if (b != null) {
                this.b = b;
            }
            if (c != null) {
                this.c = c;
            }
            if (d != null) {
                this.d = d;
            }
            if (e != null) {
                this.e = e;
            }
            if (f != null) {
                this.f = f;
            }
        }
        MatrixVector.FromMatrix = /**
        * Returns a MatrixVector from the given Matrix values.
        * @param m A Matrix.
        */
        function FromMatrix(m) {
            var v = new MatrixVector();
            v.a = m.a;
            v.b = m.b;
            v.c = m.c;
            v.d = m.d;
            v.e = m.e;
            v.f = m.f;
            return v;
        };
        return MatrixVector;
    })();
    RadSVG.MatrixVector = MatrixVector;
    /**
    * The Size structure.
    */
    var Size = (function () {
        function Size(width, height) {
            if (typeof height === "undefined") { height = null; }
            if (height == null) {
                height = width;
            }
            this.Width = width;
            this.Height = height;
        }
        Object.defineProperty(Size, "Empty", {
            get: function () {
                return new Size(0);
            },
            enumerable: true,
            configurable: true
        });
        return Size;
    })();
    RadSVG.Size = Size;
    /**
    * The SVG namespace (http://www.w3.org/2000/svg).
    */
    RadSVG.NS = "http://www.w3.org/2000/svg";
    /**
    * A range of values.
    */
    var Range = (function () {
        function Range(start, stop, step) {
            if (typeof stop === "undefined") { stop = null; }
            if (typeof step === "undefined") { step = null; }
            if (step == null) {
                step = 1;
                if (stop == null) {
                    stop = start;
                    start = 0;
                }
            }
            if ((stop - start) / step === Infinity) {
                throw "Infinite range defined.";
            }
            var range = [];
            var i = -1;
            var j;
            var k = this.RangeIntegerScale(Math.abs(step));
            start *= k;
            stop *= k;
            step *= k;
            if (step < 0) {
                while ((j = start + step * ++i) > stop) {
                    range.push(j / k);
                }
            } else {
                while ((j = start + step * ++i) < stop) {
                    range.push(j / k);
                }
            }
            return range;
        }
        Range.prototype.RangeIntegerScale = function (x) {
            var k = 1;
            while (x * k % 1) {
                k *= 10;
            }
            return k;
        };
        Range.Create = function Create(start, stop, step) {
            if (typeof stop === "undefined") { stop = null; }
            if (typeof step === "undefined") { step = null; }
            return new Range(start, stop, step);
        };
        return Range;
    })();
    RadSVG.Range = Range;
    /**
    * Utilities related to stochastic variables.
    */
    var Random = (function () {
        function Random() { }
        Random.NormalVariable = function NormalVariable(mean, deviation) {
            if (typeof mean === "undefined") { mean = 0; }
            if (typeof deviation === "undefined") { deviation = 1; }
            return function () {
                var x, y, r;
                do {
                    x = Math.random() * 2 - 1;
                    y = Math.random() * 2 - 1;
                    r = x * x + y * y;
                } while (!r || r > 1);
                return mean + deviation * x * Math.sqrt(-2 * Math.log(r) / r);
            };
        };
        Object.defineProperty(Random, "RandomId", {
            get: function () {
                return Math.floor((1 + Math.random()) * 0x1000000).toString(16).substring(1);
            },
            enumerable: true,
            configurable: true
        });
        return Random;
    })();
    RadSVG.Random = Random;
    /**
    * A circle visual.
    */
    var Circle = (function (_super) {
        __extends(Circle, _super);
        /**
        * Instantiates a new circle.
        */
        function Circle() {
            _super.call(this);
            this.native = document.createElementNS(RadSVG.NS, "circle");
            this.Initialize(this.native);
        }
        Object.defineProperty(Circle.prototype, "Stroke", {
            get: function () {
                if (this.Native.attributes["stroke"] == null) {
                    return null;
                }
                return this.Native.attributes["stroke"].value;
            },
            set: function (value) {
                this.Native.setAttribute("stroke", value);
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Circle.prototype, "StrokeThickness", {
            get: function () {
                if (this.Native.attributes["stroke-width"] == null) {
                    return 0.0;
                }
                return parseFloat(this.Native.attributes["stroke-width"].value);
            },
            set: function (value) {
                this.Native.setAttribute("stroke-width", value.toString());
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Circle.prototype, "Opacity", {
            get: function () {
                if (this.Native.attributes["fill-opacity"] == null) {
                    return 1.0;
                }
                return parseFloat(this.Native.attributes["fill-opacity"].value);
            },
            set: function (value) {
                if (value > 1) {
                    value = 1.0;
                }
                if (value < 0) {
                    value = 0.0;
                }
                this.Native.setAttribute("fill-opacity", value.toString());
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Circle.prototype, "Radius", {
            get: /**
            * Gets the radius of the circle.
            */
            function () {
                return this.native.r.baseVal.value;
            },
            set: /**
            * Sets the radius of the circle.
            */
            function (value) {
                this.native.r.baseVal.value = value;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Circle.prototype, "Center", {
            get: /**
            * Gets the center of the circle.
            */
            function () {
                return new Point(this.native.cx.baseVal.value, this.native.cy.baseVal.value);
            },
            set: /**
            * Sets the center of the circle.
            */
            function (value) {
                this.native.cx.baseVal.value = value.X;
                this.native.cy.baseVal.value = value.Y;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Circle.prototype, "Position", {
            get: /**
            * Gets the position of the top-left of the circle.
            */
            function () {
                return new Point(this.Center.X - this.Radius, this.Center.Y - this.Radius);
            },
            set: /**
            * Sets the position of the top-left of the circle.
            */
            function (p) {
                this.Center = new Point(p.X + this.Radius, p.Y + this.Radius);
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Circle.prototype, "Background", {
            get: /**
            * Gets the fill of the circle.
            */
            function () {
                return this.native.style.fill;
            },
            set: /**
            * Sets the fill of the circle.
            */
            function (v) {
                if (typeof (v) == "string") {
                    this.native.style.fill = v;
                }
                if (typeof (v) == "object") {
                    var gr = v;
                    if (gr != null) {
                        if (gr.Id == null) {
                            throw "The gradient needs an Id.";
                        }
                        this.native.style.fill = "url(#" + gr.Id + ")";
                    }
                }
            },
            enumerable: true,
            configurable: true
        });
        return Circle;
    })(Visual);
    RadSVG.Circle = Circle;
    /**
    * Defines the options when instantiating a new SVG Canvas.
    */
    var CanvasOptions = (function () {
        function CanvasOptions() {
            this.Width = 1024;
            this.Height = 768;
            this.BackgroundColor = "White";
        }
        return CanvasOptions;
    })();
    RadSVG.CanvasOptions = CanvasOptions;
    /**
    * Defines the root SVG surface inside which all visual things happen.
    */
    var Canvas = (function (_super) {
        __extends(Canvas, _super);
        function Canvas(host, options) {
            if (typeof options === "undefined") { options = new CanvasOptions(); }
            _super.call(this);
            this.markers = [];
            this.gradients = [];
            this.native = document.createElementNS(RadSVG.NS, "svg");
            this.defsNode = document.createElementNS(RadSVG.NS, "defs");
            this.defsPresent = false;
            this.Visuals = [];
            this.Initialize(this.native);
            this.HostElement = host;
            this.InsertSVGRootElement(options);
            this.native.style.background = options.BackgroundColor;
            //this.ListenToEvents();
            this.Canvas = this;
            //this.HostElement.onkeydown = function (e: KeyboardEvent) { alert('there you go'); };
            //ensure tabindex so the the canvas receives key events
            this.HostElement.setAttribute("tabindex", "0");
            this.HostElement.focus();
        }
        Object.defineProperty(Canvas.prototype, "KeyPress", {
            set: /// defining this on the Visual level is somewhat problematic; SVG doesn't play well with the keyboard
            function (f) {
                this.HostElement.addEventListener("keypress", f);
                //this.HostElement.addEventListener("keypress", function (e: KeyboardEvent) { console.log("Pressed; " + e.charCode); });
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Canvas.prototype, "KeyDown", {
            set: function (f) {
                this.HostElement.addEventListener("keydown", f);
                //this.HostElement.addEventListener("keydown", function (e: KeyboardEvent) { console.log("Down; " + e.charCode); });
            },
            enumerable: true,
            configurable: true
        });
        Canvas.prototype.Focus = function () {
            this.HostElement.focus();
        };
        Canvas.prototype.InsertSVGRootElement = ///<summary>Inserts the actual SVG element in the HTML host.</summary>
        function (options) {
            this.HostElement.style.width = options.Width.toString();
            this.HostElement.style.height = options.Height.toString();
            this.native.style.width = options.Width.toString();
            this.native.style.height = options.Height.toString();
            this.native.setAttribute("width", options.Width.toString());
            this.native.setAttribute("height", options.Height.toString());
            this.native.id = "SVGRoot";
            this.HostElement.appendChild(this.native);
        };
        Canvas.prototype.Append = function (shape) {
            this.native.appendChild(shape.Native);
            shape.Canvas = this;
            this.Visuals.push(shape);
            // in case the shape position was assigned before the shape was appended to the canvas.
            shape.Position = shape.Position;
            return this;
        };
        Canvas.prototype.Remove = function (visual) {
            if (this.Visuals.indexOf(visual) >= 0) {
                this.native.removeChild(visual.Native);
                visual.Canvas = null;
                this.Visuals.remove(visual);
                return this;
            }
            return null;
        };
        Canvas.prototype.InsertBefore = function (visual, beforeVisual) {
            this.native.insertBefore(visual.Native, beforeVisual.Native);
            visual.Canvas = this;
            this.Visuals.push(visual);
            return this;
        };
        Canvas.prototype.GetTransformedPoint = function (x, y) {
            var p = this.native.createSVGPoint();
            p.x = x;
            p.y = y;
            return p.matrixTransform(this.native.getScreenCTM().inverse());
        };
        Object.defineProperty(Canvas.prototype, "Cursor", {
            get: function () {
                return (this.Native).style.cursor;
            },
            set: function (value) {
                (this.Native).style.cursor = value;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Canvas.prototype, "Markers", {
            get: /**
            * Returns the markers defined in this canvas.
            */
            function () {
                return this.markers;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Canvas.prototype, "Gradients", {
            get: /**
            * Returns the gradients defined in this canvas.
            */
            function () {
                return this.gradients;
            },
            enumerable: true,
            configurable: true
        });
        Canvas.prototype.ensureDefsNode = function () {
            if (this.defsPresent) {
                return;
            }
            if (this.native.childNodes.length > 0) {
                this.native.insertBefore(this.defsNode, this.native.childNodes[0]);
            } else {
                this.native.appendChild(this.defsNode);
            }
            this.defsPresent = true;
        };
        Canvas.prototype.AddMarker = /**
        * Adds a marker to the definitions.
        */
        function (marker) {
            this.ensureDefsNode();
            this.defsNode.appendChild(marker.Native);
            this.markers.push(marker);
        };
        Canvas.prototype.RemoveMarker = /**
        * Removes a marker from the definitions.
        */
        function (marker) {
            if (marker == null) {
                throw "The given Marker is null";
            }
            if (!this.markers.contains(marker)) {
                throw "The given Marker is not part of the Canvas";
            }
            this.defsNode.removeChild(marker.Native);
            this.markers.remove(marker);
        };
        Canvas.prototype.RemoveGradient = /**
        * Removes a gradient from the definitions.
        */
        function (gradient) {
            if (gradient == null) {
                throw "The given Gradient is null";
            }
            if (!this.gradients.contains(gradient)) {
                throw "The given Gradient is not part of the Canvas";
            }
            this.defsNode.removeChild(gradient.Native);
            this.gradients.remove(gradient);
        };
        Canvas.prototype.AddGradient = /**
        * Adds a gradient to the definitions.
        */
        function (gradient) {
            this.ensureDefsNode();
            this.defsNode.appendChild(gradient.Native);
            this.gradients.push(gradient);
        };
        Canvas.prototype.ClearMarkers = function () {
            if (this.markers.length == 0) {
                return;
            }
            //var toremove = [];
            //for (var i = 0; i < this.defsNode.childNodes.length; i++)
            //{
            //    var item = this.defsNode.childNodes[i];
            //    if (item.nodeName.toLowerCase() == "marker") toremove.push(item);
            //}
            //for (var i = 0; i < toremove.length; i++) this.defsNode.removeChild(toremove[i]);
            for (var i = 0; i < this.markers.length; i++) {
                this.defsNode.removeChild(this.markers[i].Native);
            }
            this.markers = [];
        };
        Canvas.prototype.ClearGradients = function () {
            if (this.gradients.length == 0) {
                return;
            }
            //var toremove = [];
            //for (var i = 0; i < this.defsNode.childNodes.length; i++)
            //{
            //    var item = this.defsNode.childNodes[i];
            //    if (item.nodeName.toLowerCase() == "linearGradient") toremove.push(item);
            //}
            //for (var i = 0; i < toremove.length; i++) this.defsNode.removeChild(toremove[i]);
            for (var i = 0; i < this.gradients.length; i++) {
                this.defsNode.removeChild(this.gradients[i].Native);
            }
            this.gradients = [];
        };
        Canvas.prototype.Clear = function () {
            this.ClearMarkers();
            this.ClearGradients();
            while (this.Visuals.length > 0) {
                this.Remove(this.Visuals[0]);
            }
        };
        return Canvas;
    })(Visual);
    RadSVG.Canvas = Canvas;
    /**
    * A collection of predefined markers.
    */
    var Markers = (function () {
        function Markers() { }
        Object.defineProperty(Markers, "ArrowStart", {
            get: /**
            * Gets a standard (sharp) arrow head marker pointing to the left.
            */
            function () {
                var marker = new Marker();
                var path = new Path();
                path.Data = "m0,50l100,40l-30,-40l30,-40z";
                path.StrokeThickness = 10;
                marker.Path = path;
                marker.ViewBox = new Rect(0, 0, 100, 100);
                marker.Size = new Size(10, 10);
                marker.Id = "Arrow" + Random.RandomId;
                marker.Ref = new Point(50, 50);
                marker.Orientation = MarkerOrientation.Auto;
                marker.MarkerUnits = MarkerUnits.StrokeWidth;
                return marker;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Markers, "ArrowEnd", {
            get: /**
            * Gets a standard (sharp) arrow head marker pointing to the right.
            */
            function () {
                var marker = new Marker();
                var path = new Path();
                path.Data = "m100,50l-100,40l30,-40l-30,-40z";
                path.StrokeThickness = 10;
                marker.Path = path;
                marker.ViewBox = new Rect(0, 0, 100, 100);
                marker.Size = new Size(10, 10);
                marker.Id = "Arrow" + Random.RandomId;
                marker.Ref = new Point(50, 50);
                marker.Orientation = MarkerOrientation.Auto;
                marker.MarkerUnits = MarkerUnits.StrokeWidth;
                return marker;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Markers, "FilledCircle", {
            get: /**
            * Gets a standard closed circle arrow head marker.
            */
            function () {
                var marker = new Marker();
                var circle = new RadSVG.Circle();
                circle.Radius = 30;
                circle.Center = new Point(50, 50);
                circle.StrokeThickness = 10;
                marker.Path = circle;
                marker.ViewBox = new Rect(0, 0, 100, 100);
                marker.Size = new Size(10, 10);
                marker.Id = "FilledCircle" + Random.RandomId;
                marker.Ref = new Point(50, 50);
                marker.Orientation = MarkerOrientation.Auto;
                marker.MarkerUnits = MarkerUnits.StrokeWidth;
                return marker;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Markers, "Circle", {
            get: /**
            * Gets a standard circle arrow head marker.
            */
            function () {
                var marker = new Marker();
                var circle = new RadSVG.Circle();
                circle.Radius = 30;
                circle.Center = new Point(50, 50);
                circle.StrokeThickness = 10;
                marker.Path = circle;
                marker.Background = "none";
                circle.Stroke = "Black";
                marker.ViewBox = new Rect(0, 0, 100, 100);
                marker.Size = new Size(10, 10);
                marker.Id = "Circle" + Random.RandomId;
                marker.Ref = new Point(50, 50);
                marker.Orientation = MarkerOrientation.Auto;
                marker.MarkerUnits = MarkerUnits.StrokeWidth;
                return marker;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Markers, "OpenArrowStart", {
            get: /**
            * Gets an open start arrow marker.
            */
            function () {
                var marker = new Marker();
                var path = new Path();
                path.Data = "m0,50l100,40l-30,-40l30,-40l-100,40z";
                path.StrokeThickness = 10;
                marker.Path = path;
                marker.Background = "none";
                marker.ViewBox = new Rect(0, 0, 100, 100);
                marker.Size = new Size(10, 10);
                marker.Id = "OpenArrowStart" + Random.RandomId;
                marker.Ref = new Point(50, 50);
                marker.Orientation = MarkerOrientation.Auto;
                marker.MarkerUnits = MarkerUnits.StrokeWidth;
                return marker;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Markers, "OpenArrowEnd", {
            get: /**
            * Gets an open end arrow marker.
            */
            function () {
                var marker = new Marker();
                var path = new Path();
                path.Data = "m100,50l-100,40l30,-40l-30,-40z";
                path.StrokeThickness = 10;
                marker.Path = path;
                marker.Background = "none";
                marker.ViewBox = new Rect(0, 0, 100, 100);
                marker.Size = new Size(10, 10);
                marker.Id = "OpenArrowEnd" + Random.RandomId;
                marker.Ref = new Point(50, 50);
                marker.Orientation = MarkerOrientation.Auto;
                marker.MarkerUnits = MarkerUnits.StrokeWidth;
                return marker;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Markers, "FilledDiamond", {
            get: /**
            * Gets a filled diamond marker.
            */
            function () {
                var marker = new Marker();
                var path = new Path();
                path.Data = "m20,20l0,60l60,0l0,-60l-60,0z";
                path.Transform(new Rotation(45, 50, 50));
                path.StrokeThickness = 10;
                marker.Path = path;
                marker.ViewBox = new Rect(0, 0, 100, 100);
                marker.Size = new Size(10, 10);
                marker.Id = "FilledDiamond" + Random.RandomId;
                marker.Ref = new Point(50, 50);
                marker.Orientation = MarkerOrientation.Auto;
                marker.MarkerUnits = MarkerUnits.StrokeWidth;
                return marker;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Markers, "Diamond", {
            get: /**
            * Gets a diamond marker.
            */
            function () {
                var marker = new Marker();
                var path = new Path();
                path.Data = "m20,20l0,60l60,0l0,-60l-60,0z";
                path.Transform(new Rotation(45, 50, 50));
                path.StrokeThickness = 10;
                marker.Path = path;
                marker.Background = "none";
                marker.ViewBox = new Rect(0, 0, 100, 100);
                marker.Size = new Size(10, 10);
                marker.Id = "Diamond" + Random.RandomId;
                marker.Ref = new Point(50, 50);
                marker.Orientation = MarkerOrientation.Auto;
                marker.MarkerUnits = MarkerUnits.StrokeWidth;
                return marker;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Markers, "WedgeStart", {
            get: /**
            * Gets a wedge start marker.
            */
            function () {
                var marker = new Marker();
                var path = new Path();
                path.Data = "m0,50l100,40l-94,-40l94,-40l-100,40z";
                // path.Transform(new Rotation(45, 50, 50));
                path.StrokeThickness = 10;
                marker.Path = path;
                marker.ViewBox = new Rect(0, 0, 100, 100);
                marker.Size = new Size(10, 10);
                marker.Id = "WedgeStart" + Random.RandomId;
                marker.Ref = new Point(50, 50);
                marker.Orientation = MarkerOrientation.Auto;
                marker.MarkerUnits = MarkerUnits.StrokeWidth;
                return marker;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Markers, "WedgeEnd", {
            get: /**
            * Gets a wedge end marker.
            */
            function () {
                var marker = new Marker();
                var path = new Path();
                path.Data = "m0,50l100,40l-94,-40l94,-40l-100,40z";
                path.Transform(new Rotation(180, 50, 50));
                path.StrokeThickness = 10;
                marker.Path = path;
                marker.ViewBox = new Rect(0, 0, 100, 100);
                marker.Size = new Size(10, 10);
                marker.Id = "WedgeEnd" + Random.RandomId;
                marker.Ref = new Point(50, 50);
                marker.Orientation = MarkerOrientation.Auto;
                marker.MarkerUnits = MarkerUnits.StrokeWidth;
                return marker;
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Markers, "Square", {
            get: /**
            * Gets a square end marker.
            */
            function () {
                var marker = new Marker();
                var path = new Path();
                path.Data = "m20,20l0,60l60,0l0,-60z";
                path.StrokeThickness = 10;
                marker.Path = path;
                marker.ViewBox = new Rect(0, 0, 100, 100);
                marker.Size = new Size(10, 10);
                marker.Id = "Square" + Random.RandomId;
                marker.Ref = new Point(50, 50);
                marker.Orientation = MarkerOrientation.Auto;
                marker.MarkerUnits = MarkerUnits.StrokeWidth;
                return marker;
            },
            enumerable: true,
            configurable: true
        });
        return Markers;
    })();
    RadSVG.Markers = Markers;
})(RadSVG || (RadSVG = {}));
//any(f:(i:any)=>bool): bool;
//Array.prototype.any = function (f: (i: any) => bool =null)
//{
//    var l = this;
//    if (f != null) l = this.map(f);
//    return l.length > 0;
//}
Array.prototype.remove = function () {
    var what, a = arguments, L = a.length, ax;
    while (L && this.length) {
        what = a[--L];
        while ((ax = this.indexOf(what)) !== -1) {
            this.splice(ax, 1);
        }
    }
    return this;
};
Array.prototype.flatten = function () {
    return Array.prototype.concat.apply([], this);
};
Array.prototype.distinct = function () {
    var a = this;
    var r = [];
    for (var i = 0; i < a.length; i++) {
        if (r.indexOf(a[i]) < 0) {
            r.push(a[i]);
        }
    }
    return r;// bug in TypeScript really

};
Array.prototype.contains = function (obj) {
    var i = this.length;
    while (i--) {
        if (this[i] == obj) {
            return true;
        }
    }
    return false;
};
//@ sourceMappingURL=RadSVG.js.map
