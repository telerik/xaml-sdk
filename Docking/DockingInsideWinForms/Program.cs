using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DockingInsideWinForms
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // This is the solution for the exception thrown when dragging a pane. Some code in the Docking control is asking for the application's MainWindow, 
            // which in WinForms is missing. That is why this lines of code are necessary.
            if (System.Windows.Application.Current == null)
            {
                new System.Windows.Application();
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
