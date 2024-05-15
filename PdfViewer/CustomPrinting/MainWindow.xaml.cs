using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Documents.Fixed.Model;
using Telerik.Windows.Documents.Fixed.Print;

namespace CustomPrinting
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            RadFixedDocument document = this.viewer.Document;
            if (document != null && document.Pages != null)
            {
                PrintDialog printDialog = new PrintDialog();
                PrintQueue printQueue = this.FindPrintQueueByName(this.PrinterName.Text);
                if (printQueue == null)
                {
                    printQueue = printDialog.PrintQueue;
                }

                printQueue.DefaultPrintTicket.PageMediaSize = new PageMediaSize(PageMediaSizeName.ISODLEnvelope);
                printDialog.PrintQueue = printQueue;
                printDialog.MinPage = 1;
                printDialog.MaxPage = (uint)document.Pages.Count;

                this.viewer.Print(printDialog, PrintSettings.Default);
            }
        }

        private PrintQueue FindPrintQueueByName(string name)
        {
            PrintServer server = new PrintServer();
            foreach (PrintQueue queue in server.GetPrintQueues(new EnumeratedPrintQueueTypes[] { EnumeratedPrintQueueTypes.Connections, EnumeratedPrintQueueTypes.Local }))
            {
                if (queue.Name == name)
                {
                    return queue;
                }
            }

            return null;
        }
    }
}
