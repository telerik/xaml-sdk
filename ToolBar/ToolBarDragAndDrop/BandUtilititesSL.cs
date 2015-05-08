using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Linq;
using Telerik.Windows.Controls;

namespace ToolBarDragAndDrop_SL
{
    public static partial class ToolBarTrayUtilitiesSL
    {
        private static class BandsUtilities
        {
            internal struct ToolBarPositionInfo
            {
                internal readonly int? Band;
                internal readonly int? NewBand;
                internal readonly int BandIndex;
                internal readonly List<RadToolBar> ToolBars;

                internal ToolBarPositionInfo(int? band, int? newBand, int bandIndex, List<RadToolBar> toolBars)
                {
                    this.Band = band;
                    this.NewBand = newBand;
                    this.BandIndex = bandIndex;
                    this.ToolBars = toolBars;
                }
            }

            private struct BandInfo
            {
                internal int? Band;
                internal int? NewBand;
            }

            internal static ToolBarPositionInfo CalculateToolBarPositionInfo(RadToolBar sourceToolBar, RadToolBarTray tray, Point mousePosition)
            {
                var band = CalculateBand(sourceToolBar, tray, mousePosition);

                List<RadToolBar> toolBars = new List<RadToolBar>();
                foreach (RadToolBar toolBar in tray.Items)
                {
                    if (toolBar != sourceToolBar && toolBar.Band == band.Band)
                    {
                        toolBars.Add(toolBar);
                    }
                }
                toolBars = toolBars.OrderBy(tb => tb.BandIndex).ToList();

                int bandIndex = CalculateBandIndex(tray.Orientation, sourceToolBar, toolBars, mousePosition);

                return new ToolBarPositionInfo(band.Band, band.NewBand, bandIndex, toolBars);
            }

            internal static void UpdateToolBarPosition(RadToolBarTray tray, RadToolBar toolBar, ToolBarPositionInfo positionInfo, bool allowNewBandCreation)
            {
                toolBar.Band = (allowNewBandCreation && positionInfo.NewBand.HasValue) ? positionInfo.NewBand.Value : (positionInfo.Band.HasValue ? positionInfo.Band.Value : 0);
                toolBar.BandIndex = positionInfo.BandIndex;
                for (int i = 0; i < positionInfo.BandIndex; i++)
                {
                    positionInfo.ToolBars[i].BandIndex = i < positionInfo.BandIndex ? i : i + 1;
                }
            }

            private static BandInfo CalculateBand(RadToolBar toolBar, RadToolBarTray tray, Point mousePosition)
            {
                double position = tray.Orientation == Orientation.Horizontal ? mousePosition.Y : mousePosition.X;
                double newBandVicinity = 6;

                int? newBand = null;
                if (position < newBandVicinity)
                {
                    // Create new band, to left or to top.
                    newBand = -1;
                }

                var bands = GetBandsDict(tray);
                double trayLength = tray.Orientation == Orientation.Horizontal ? tray.ActualHeight : tray.ActualWidth;

                if (trayLength - newBandVicinity < position)
                {
                    // Create new band, to right or to bottom.
                    newBand = bands.Keys.Count;
                }

                Func<RadToolBar, double> length = tb => tray.Orientation == Orientation.Horizontal ?
                    (tb.ActualHeight + tb.Margin.Top + tb.Margin.Bottom) :
                    (tb.ActualWidth + tb.Margin.Left + tb.Margin.Right);
                List<int> keys = bands.Keys.ToList();
                int? existingBand = null;
                double currentPosition = 0;

                for (int i = 0; i < keys.Count; i++)
                {
                    existingBand = keys[i];
                    double bandLength = length(bands[keys[i]][0]);
                    if (position <= currentPosition + bandLength)
                    {
                        if (keys[i] == 1 &&
                            bands[keys[0]].Count == 1 &&
                            bands[keys[0]][0] == toolBar &&
                            position < currentPosition + (1 * newBandVicinity))
                        {
                            // Avoid flicker when the dragged toolbar is the only element in the first band.
                            existingBand = 0;
                        }
                        else if (keys[i] == keys.Count - 2 &&
                            bands[keys[keys.Count - 1]].Count == 1 &&
                            bands[keys[keys.Count - 1]][0] == toolBar &&
                            currentPosition + bandLength - (1 * newBandVicinity) < position)
                        {
                            // Avoid flicker when the dragged toolbar is the only element in the last band.
                            existingBand = keys.Count - 1;
                        }

                        break;
                    }

                    currentPosition += bandLength;
                }

                if (newBand.HasValue && existingBand.HasValue && bands[existingBand.Value].Count == 1 && bands[existingBand.Value][0] == toolBar)
                {
                    // A new band will not be created because the dragged bar is the only element in the existing band.
                    newBand = null;
                }

                return new BandInfo { Band = existingBand, NewBand = newBand };
            }

            private static int CalculateBandIndex(Orientation orientation, RadToolBar sourceToolBar, List<RadToolBar> toolBars, Point mousePosition)
            {
                double position = orientation == Orientation.Horizontal ? mousePosition.X : mousePosition.Y;
                Func<RadToolBar, double> length = tb => orientation == Orientation.Horizontal ? (tb.ActualWidth + tb.Margin.Left + tb.Margin.Right) : (tb.ActualHeight + tb.Margin.Top + tb.Margin.Bottom);

                double currentPosition = 0;
                int index = 0;
                foreach (RadToolBar toolBar in toolBars)
                {
                    if (toolBar != sourceToolBar)
                    {
                        double toolBarLength = length(toolBar);
                        if (position <= currentPosition + toolBarLength)
                        {
                            bool positionDraggedBarAfterCurrentBar = (currentPosition + toolBarLength - position) < (position - currentPosition);
                            if (positionDraggedBarAfterCurrentBar)
                            {
                                index++;
                            }
                            break;
                        }

                        currentPosition += toolBarLength;
                        index++;
                    }
                }

                return index;
            }

            private static Dictionary<int, List<RadToolBar>> GetBandsDict(RadToolBarTray tray)
            {
                Dictionary<int, List<RadToolBar>> bandsDict = new Dictionary<int, List<RadToolBar>>();
                foreach (RadToolBar toolBar in tray.Items)
                {
                    if (!bandsDict.ContainsKey(toolBar.Band))
                    {
                        bandsDict[toolBar.Band] = new List<RadToolBar>();
                    }

                    bandsDict[toolBar.Band].Add(toolBar);
                }
                return bandsDict;
            }
        }
    }
}
