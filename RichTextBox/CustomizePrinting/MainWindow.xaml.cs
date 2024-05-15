using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Documents.Model;
using Telerik.Windows.Documents.UI;

namespace CustomizePrinting
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeDocument();

         
        }

        private void InitializeDocument()
        {
            if (this.radRichTextBox != null)
            { 
                this.radRichTextBox.Insert("Sample text.");
            }
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {

            RadDocument document = this.radRichTextBox.Document;
            if (document != null)
            {
                PrintDialog printDialog = new PrintDialog();
                PrintQueue printQueue = this.FindPrintQueueByName(this.PrinterName.Text);
                if (printQueue == null)
                {
                    printQueue = printDialog.PrintQueue;
                }

                int pagesCount = document.FirstLayoutBox.Children.Count;
                printQueue.DefaultPrintTicket.PageMediaSize = new PageMediaSize(PageMediaSizeName.ISODLEnvelope);
                printDialog.PrintQueue = printQueue;
                printDialog.MinPage = 1;
                printDialog.MaxPage = (uint)pagesCount;

                this.radRichTextBox.Print(printDialog, new PrintSettings());
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
