using System;
using System.Collections.Generic;
using System.Windows;
using Telerik.Charting;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ChartView;

namespace TrackBallSyncedCharts
{
    public static class ChartUtilities
    {
        public static readonly DependencyProperty TrackBallGroupProperty = DependencyProperty.RegisterAttached(
            "TrackBallGroup",
            typeof(string),
            typeof(ChartUtilities),
            new PropertyMetadata(TrackBallGroupChanged));

        public static readonly DependencyProperty TrackBallStaysOnMouseLeaveProperty = DependencyProperty.RegisterAttached(
            "TrackBallStaysOnMouseLeave",
            typeof(bool),
            typeof(ChartUtilities),
            new PropertyMetadata(TrackBallStaysOnMouseLeaveChanged));

        private static Dictionary<string, List<WeakReference>> groupToBehaviorsDict = new Dictionary<string, List<WeakReference>>();

        public static string GetTrackBallGroup(DependencyObject obj)
        {
            return (string)obj.GetValue(TrackBallGroupProperty);
        }

        public static void SetTrackBallGroup(DependencyObject obj, string value)
        {
            obj.SetValue(TrackBallGroupProperty, value);
        }

        public static bool GetTrackBallStaysOnMouseLeave(DependencyObject obj)
        {
            return (bool)obj.GetValue(TrackBallStaysOnMouseLeaveProperty);
        }

        public static void SetTrackBallStaysOnMouseLeave(DependencyObject obj, bool value)
        {
            obj.SetValue(TrackBallStaysOnMouseLeaveProperty, value);
        }

        public static void MoveTrackBall(ChartTrackBallBehavior behav, NavigateDirection direction)
        {
            var chart = (RadCartesianChart)behav.Chart;
            object currentXCategory = null;
            if (chart.PlotAreaClip.X <= behav.Position.X && behav.Position.X <= chart.PlotAreaClip.Right)
            {
                currentXCategory = chart.ConvertPointToData(behav.Position).FirstValue;
            }

            Func<int, int> next = GetPositionXFunction(direction);
            object adjacentXCategory = null;
            int startX = GetCoercedPositionX(behav);
            for (int x = startX; chart.PlotAreaClip.X <= x && x <= chart.PlotAreaClip.Right; x = next(x))
            {
                DataTuple tuple = chart.ConvertPointToData(new Point(x, 0));
                if (!object.Equals(currentXCategory, tuple.FirstValue))
                {
                    adjacentXCategory = tuple.FirstValue;
                    break;
                }
            }

            var adjacentPosition = chart.ConvertDataToPoint(new DataTuple(adjacentXCategory, null));
            adjacentPosition.Y = behav.Chart.ActualHeight / 2;

            behav.Position = adjacentPosition;
        }

        private static void TrackBallGroupChanged(DependencyObject target, DependencyPropertyChangedEventArgs args)
        {
            ChartTrackBallBehavior behavior = (ChartTrackBallBehavior)target;

            string oldGroup = (string)args.OldValue;
            if (oldGroup != null)
            {
                behavior.TrackInfoUpdated -= behavior_TrackInfoUpdated;
                CleanUp(groupToBehaviorsDict[oldGroup], behavior);
                if (groupToBehaviorsDict[oldGroup].Count == 0)
                {
                    groupToBehaviorsDict.Remove(oldGroup);
                }
            }

            string newGroup = (string)args.NewValue;
            if (newGroup != null)
            {
                if (!groupToBehaviorsDict.ContainsKey(newGroup))
                {
                    groupToBehaviorsDict[newGroup] = new List<WeakReference>();
                }
                groupToBehaviorsDict[newGroup].Add(new WeakReference(behavior));
                behavior.TrackInfoUpdated += behavior_TrackInfoUpdated;
            }
        }

        private static void TrackBallStaysOnMouseLeaveChanged(DependencyObject target, DependencyPropertyChangedEventArgs args)
        {
            ChartTrackBallBehavior behavior = (ChartTrackBallBehavior)target;
            if ((bool)args.NewValue)
            {
                behavior.PositionChanging += behavior_PositionChanging;
            }
            else
            {
                behavior.PositionChanging -= behavior_PositionChanging;
            }
        }

        private static void behavior_TrackInfoUpdated(object sender, TrackBallInfoEventArgs e)
        {
            ChartTrackBallBehavior behavior = (ChartTrackBallBehavior)sender;
            var chart = (RadCartesianChart)behavior.Chart;
            object xCategory = null;
            if (e.Context.DataPointInfos.Count != 0)
            {
                var center = e.Context.DataPointInfos[0].DataPoint.LayoutSlot.Center;
                var dataTuple = chart.ConvertPointToData(new Point(center.X, center.Y));
                xCategory = dataTuple.FirstValue;
            }
            MoveTrackBalls(GetTrackBallGroup(behavior), xCategory, behavior);
        }

        private static void MoveTrackBalls(string group, object xCategory, ChartTrackBallBehavior behaviorOriginator)
        {
            foreach (var wr in groupToBehaviorsDict[group])
            {
                var behav = wr.Target as ChartTrackBallBehavior;
                if (behav != null && behav != behaviorOriginator)
                {
                    var chart = (RadCartesianChart)behav.Chart;
                    var position = chart.ConvertDataToPoint(new DataTuple(xCategory, null));
                    position.Y = chart.ActualHeight / 2;

                    behav.Position = position;
                }
            }
            CleanUp(groupToBehaviorsDict[group]);
        }

        private static void CleanUp(List<WeakReference> list, object elementToRemove = null)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (!list[i].IsAlive || list[i].Target == elementToRemove)
                {
                    list.RemoveAt(i);
                }
            }
        }

        private static void behavior_PositionChanging(object sender, TrackBallPositionChangingEventArgs e)
        {
            ChartTrackBallBehavior behav = (ChartTrackBallBehavior)sender;
            Point pos = new Point(e.NewPosition.X - behav.Chart.PanOffset.X, e.NewPosition.Y - behav.Chart.PanOffset.Y);
            if (!behav.Chart.PlotAreaClip.Contains(pos.X, pos.Y))
            {
                e.NewPosition = e.PreviousPosition;
            }
        }

        private static Func<int, int> GetPositionXFunction(NavigateDirection direction)
        {
            Func<int, int> next = null;
            if (direction == NavigateDirection.Left)
            {
                next = i => --i;
            }
            else
            {
                next = i => ++i;
            }

            return next;
        }

        private static int GetCoercedPositionX(ChartTrackBallBehavior behav)
        {
            var chart = (RadCartesianChart)behav.Chart;
            double positionX = double.IsNaN(behav.Position.X) ? chart.PlotAreaClip.X : behav.Position.X;
            int coercedX = (int)Math.Max(chart.PlotAreaClip.X, Math.Min(chart.PlotAreaClip.Right, positionX));
            return coercedX;
        }

        public enum NavigateDirection
        {
            Left,
            Right,
        }
    }
}
