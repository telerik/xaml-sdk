using System.Collections.Generic;

namespace TriangleIndices
{
    public static class DataProvider
    {
        private static int columnsCount = 10;

        internal static PlotInfo3D[,] GetDataFromTable()
        {
            List<PlotInfo2D> pattern = GetPattern();
            PlotInfo3D[,] data = new PlotInfo3D[pattern.Count, columnsCount];

            for (int r = 0; r < pattern.Count; r++)
            {
                PlotInfo2D pi = pattern[r];

                for (int c = 0; c < columnsCount; c++)
                {
                    data[r, c] = new PlotInfo3D { X = pi.X, Y = c, Z = pi.Y + c };
                }
            }

            return data;
        }

        private static List<PlotInfo2D> GetPattern()
        {
            List<PlotInfo2D> pattern = new List<PlotInfo2D>();
            pattern.Add(new PlotInfo2D { X = 1, Y = 1, });
            pattern.Add(new PlotInfo2D { X = 2, Y = 2, });
            pattern.Add(new PlotInfo2D { X = 3, Y = 3, });
            pattern.Add(new PlotInfo2D { X = 4, Y = 4, });
            pattern.Add(new PlotInfo2D { X = 6, Y = 8, });
            pattern.Add(new PlotInfo2D { X = 9, Y = 15, });
            pattern.Add(new PlotInfo2D { X = 13, Y = 30, });
            pattern.Add(new PlotInfo2D { X = 14, Y = 40, });
            pattern.Add(new PlotInfo2D { X = 12, Y = 50, });
            pattern.Add(new PlotInfo2D { X = 10, Y = 55, });
            pattern.Add(new PlotInfo2D { X = 7, Y = 50, });
            pattern.Add(new PlotInfo2D { X = 5, Y = 40, });
            pattern.Add(new PlotInfo2D { X = 6, Y = 30, });
            pattern.Add(new PlotInfo2D { X = 8, Y = 15, });
            pattern.Add(new PlotInfo2D { X = 11, Y = 8, });
            pattern.Add(new PlotInfo2D { X = 15, Y = 4, });
            pattern.Add(new PlotInfo2D { X = 16, Y = 3, });
            pattern.Add(new PlotInfo2D { X = 17, Y = 2, });

            return pattern;
        }
    }
}
