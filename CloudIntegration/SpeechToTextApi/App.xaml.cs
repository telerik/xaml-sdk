using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Telerik.Windows.Controls;

namespace SpeechToTextApi
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public App()
		{
			this.InitializeComponent();
			StyleManager.ApplicationTheme = new Office2016Theme();
		}
	}
}
