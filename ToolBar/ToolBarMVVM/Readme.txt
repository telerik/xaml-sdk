The ToolBar MVVM sample demonstrates the following features and custommization techniques in the RadToolBar:
	
	1) DataBinding the RadToolBar to collection of ViewModels.
	2) DataTemplaceSelector which decides what type of control to add in the toolbar depending on the ViewModel's type.
	3) Custom Styles that upgrade the predefined styles in the RadToolBar.
	
Notes: The predefined styles are located in Resources.xaml. 
	   The custom styles are located in Example.xamla and are based on the predefined ones.
	   The ColorPicker uses the predefined RadSplitButtonStyle to achieve consistency when used in Toolbar. 
	   Otherwise it will have borders, non-transparent background etc. because Toolbar does not have predefined style for RadColorPicker.