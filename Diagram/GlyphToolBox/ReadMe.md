The GlyphToolBox Diagram SDK shows how you can extend the default Diagram Toolbox with the Glyphs collection we provide from R3 2016.
Glyphs are SVG images built via with special FontFamily you can apply to TextBlock / Button / DiagramTextShape etc.
In the example Glyphs are added in the Diagram ToolBox, DiagramSettingsPane, RadAutoCompleteBox and RadButtons.
Below are listed some demo specifics:

	-- Diagram Settings Pane uses custom style for its TextTab - the style is located in App.xaml
	-- Diagram ToolBox needs ItemTemplate selector in order to use TextShapes displaying Glyphs
	-- To display a glyph in simple TextBlock you need two settings:
		- FontFamily="{StaticResource TelerikWebUI}"
		- Text set to special string located in /Telerik.Windows.Controls;component/Themes/FontResources.xaml
		- the strings are in these forms: &#xe62e; &#xe67f; etc
	-- On copy paste, the glyph string needs to be encoded / decoded
	-- Glyphs are read from the file /Telerik.Windows.Controls;component/Themes/FontResources.xaml in method InitializeToolBox()