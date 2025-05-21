﻿using CustomFunctions.Functions;
using CustomFunctions.Resources;
using System.Windows;
using Telerik.Windows.Documents.Spreadsheet.Expressions.Functions;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.OpenXml.Xlsx;

namespace CustomFunctions
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.RegisterCustomFunctions();

            this.LoadResourceFile("Resources/CustomFunctions.xlsx");
        }

        void LoadResourceFile(string filePath)
        {
            XlsxFormatProvider formatProvider = new XlsxFormatProvider();
            using (var stream = ResourceHelper.GetResourceStream(filePath))
            {
                this.radSpreadsheet.Workbook = formatProvider.Import(stream, null);
            }
        }

        void RegisterCustomFunctions()
        {
            FunctionManager.RegisterFunction(new Arguments());
            FunctionManager.RegisterFunction(new GeoMean());
            FunctionManager.RegisterFunction(new E());
            FunctionManager.RegisterFunction(new Add());
            FunctionManager.RegisterFunction(new RepeatString());
            FunctionManager.RegisterFunction(new Nand());
            FunctionManager.RegisterFunction(new CustomFunctions.Functions.Upper());
            FunctionManager.RegisterFunction(new CustomFunctions.Functions.Indirect());
        }
    }
}
