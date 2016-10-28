using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using Telerik.Windows.Controls.ChartView;

namespace TriangleIndices
{
    public partial class MainWindow : Window
    {
        private int rowsCount;
        private int columnsCount;

        public MainWindow()
        {
            InitializeComponent();

            PlotInfo3D[,] dataTable = DataProvider.GetDataFromTable();
            this.rowsCount = dataTable.GetLength(0);
            this.columnsCount = dataTable.GetLength(1);

            List<PlotInfo3D> dataList = new List<PlotInfo3D>();
            foreach (PlotInfo3D pi in dataTable)
            {
                dataList.Add(pi);
            }

            this.DataContext = dataList;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.surfaceSeries1.TriangleIndices = null;
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            this.surfaceSeries1.TriangleIndices = SurfaceSeries3D.GenerateTriangleIndices(this.rowsCount, this.columnsCount);
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            this.surfaceSeries1.TriangleIndices = CreateTriangleIndexesForTable(this.rowsCount, this.columnsCount);
        }

        public static Int32Collection CreateTriangleIndexesForTable(int rowsCount, int columnsCount)
        {
            // This method creates triangle indexes in the same manner as the SurfaceSeries3D.GenerateTriangleIndices() method.

            Int32Collection triangleIndexes = new Int32Collection(rowsCount * columnsCount);

            for (int row = 0; row < rowsCount - 1; row++)
            {
                for (int column = 0; column < columnsCount - 1; column += 1)
                {
                    int topLeft = (row * columnsCount) + column;
                    int topRight = topLeft + 1;
                    int bottomLeft = topLeft + columnsCount;
                    int bottomRight = bottomLeft + 1;

                    AddTriangle(triangleIndexes, topLeft, topRight, bottomLeft);
                    AddTriangle(triangleIndexes, topRight, bottomRight, bottomLeft);
                }
            }

            return triangleIndexes;
        }

        private static void AddTriangle(Int32Collection triangleIndexes, int index1, int index2, int index3)
        {
            triangleIndexes.Add(index1);
            triangleIndexes.Add(index2);
            triangleIndexes.Add(index3);
        }
    }
}
