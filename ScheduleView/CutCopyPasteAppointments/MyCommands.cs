using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;

namespace CutCopyPasteAppointments
{
	public static class MyCommands
	{
		public static ICommand CutCommand { get; private set; }
		public static ICommand CopyCommand { get; private set; }
		public static ICommand PasteCommand { get; private set; }

		static MyCommands()
		{
			CutCommand = new RoutedUICommand("CutAppointments", "CutCommand", typeof(MyCommands));
			CopyCommand = new RoutedUICommand("CopyAppointments", "CopyCommand", typeof(MyCommands));
			PasteCommand = new RoutedUICommand("PasteAppointment", "PasteCommand", typeof(MyCommands));
		}
	}
}
