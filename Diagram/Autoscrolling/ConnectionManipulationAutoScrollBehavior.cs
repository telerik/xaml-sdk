using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Diagrams;
using Telerik.Windows.Diagrams.Core;

namespace Autoscrolling
{
    public class ConnectionManipulationAutoScrollBehavior
    {
        private const int ScrollingStep = 5; 
        private static Dictionary<RadDiagram, DiagramScrollingInfo> diagramInfos = new Dictionary<RadDiagram, DiagramScrollingInfo>();        

        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached(
            "IsEnabled", 
            typeof(bool), 
            typeof(ConnectionManipulationAutoScrollBehavior), 
            new PropertyMetadata(false, OnIsEnabledChanged));

        public static bool GetIsEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsEnabledProperty);
        }

        public static void SetIsEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(IsEnabledProperty, value);
        }

        private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {            
            var diagram = (RadDiagram)d;

            if (e.OldValue != null)
            {
                ConnectionManipulationAutoScrollBehavior.DetachHandlers(diagram);                
            }

            if (e.NewValue != null)
            {
                bool isEnabled = (bool)e.NewValue;
                if (isEnabled)
                {
                    ConnectionManipulationAutoScrollBehavior.AddAssosiation(diagram);
                    diagramInfos[diagram].ScrollingTimer.Tick += ScrollingTimer_Tick;
                    diagram.PreviewMouseMove += diagram_PreviewMouseMove;
                }
                else
                {
                    ConnectionManipulationAutoScrollBehavior.DetachHandlers(diagram);
                    ConnectionManipulationAutoScrollBehavior.RemoveAssosiation(diagram);
                }
            }
        }       

        static void diagram_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            var diagram = (RadDiagram)sender;
            var diagramInfo = diagramInfos[diagram];

            if (IsAutoScrollingAllowed(diagramInfo))
            {
                diagramInfo.AutoScrollingDirection = ConnectionManipulationAutoScrollBehavior.GetScrollingDirection(diagram, e.GetPosition(diagram));

                if (!diagramInfo.ScrollingTimer.IsEnabled && diagramInfo.AutoScrollingDirection != ScrollingDirection.None)
                {
                    diagramInfo.ScrollingTimer.Start();
                }

                // if the auto scrolling is running cancel the default drag operation
                if (diagramInfo.ScrollingTimer.IsEnabled)
                {
                    e.Handled = true;
                }
            }            
        }

        static void ScrollingTimer_Tick(object sender, EventArgs e)
        {
            var diagramInfoPair = diagramInfos.FirstOrDefault(x => x.Value.ScrollingTimer == (DispatcherTimer)sender);
            var diagramInfo = diagramInfoPair.Value;
            var diagram = diagramInfoPair.Key;

            if (ShouldAutoScrollingStop(diagramInfo))
            {
                diagramInfo.ScrollingTimer.Stop();
                return;
            }

            var mousePosition = Mouse.GetPosition(diagram);
            var transformedPosition = diagram.GetTransformedPoint(mousePosition);

            UpdateConnectionPosition(diagramInfo, mousePosition, transformedPosition);

            if (diagramInfo.DraggingService.IsDragging)
            {
                diagramInfo.DraggingService.Drag(transformedPosition);
            }

            ScrollDiagram(diagramInfo, diagram);

        }

        private static void RemoveAssosiation(RadDiagram diagram)
        {
            diagramInfos.Remove(diagram);
        }

        private static void AddAssosiation(RadDiagram diagram)
        {
            if (!diagramInfos.ContainsKey(diagram))
            {
                diagramInfos[diagram] = CreateDiagramInfo(diagram);
            }
        }

        private static void DetachHandlers(RadDiagram diagram)
        {
            if (diagramInfos.Keys.Contains(diagram) && (diagramInfos[diagram].ScrollingTimer != null))
            {
                diagramInfos[diagram].ScrollingTimer.Tick -= ScrollingTimer_Tick;
            }
            diagram.PreviewMouseMove -= diagram_PreviewMouseMove;
        }

        private static DiagramScrollingInfo CreateDiagramInfo(RadDiagram diagram)
        {
            var toolService = diagram.ServiceLocator.GetService<IToolService>() as ToolService;
            var diagramInfo = new DiagramScrollingInfo()
            {
                ConnectionTool = (ConnectionTool)toolService.FindTool(ConnectionTool.ToolName),
                ConnectionManipulationTool = (ConnectionManipulationTool)toolService.FindTool(ConnectionManipulationTool.ToolName),
                ScrollingTimer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(1) },
                DraggingService = (DraggingService)diagram.ServiceLocator.GetService<IDraggingService>(),
            };            
            
            return diagramInfo;
        }
       
        private static ScrollingDirection GetScrollingDirection(RadDiagram diagram, Point currentPosition)
        {
            var diagramPosition = diagram.GetTransformedPoint(currentPosition);

            if (diagramPosition.Y <= diagram.Viewport.Top)
            {
                return ScrollingDirection.Up;
            }
            else if (diagramPosition.Y >= diagram.Viewport.Bottom)
            {
                return ScrollingDirection.Down;
            }
            else if (diagramPosition.X <= diagram.Viewport.Left)
            {
                return ScrollingDirection.Left;
            }
            else if (diagramPosition.X >= diagram.Viewport.Right)
            {
                return ScrollingDirection.Right;
            }
            else
            {
                return ScrollingDirection.None;
            }
        }        

        private static void UpdateConnectionPosition(DiagramScrollingInfo diagramInfo, Point mousePosition, Point transformedPosition)
        {
            var arguments = new PointerArgs(mousePosition, transformedPosition);

            diagramInfo.ConnectionTool.MouseMove(arguments);
            diagramInfo.ConnectionManipulationTool.MouseMove(arguments);
        }

        private static void ScrollDiagram(DiagramScrollingInfo diagramInfo, RadDiagram diagram)
        {
            switch (diagramInfo.AutoScrollingDirection)
            {
                case ScrollingDirection.Up:
                    diagram.Scroll(0, ConnectionManipulationAutoScrollBehavior.ScrollingStep);
                    break;
                case ScrollingDirection.Down:
                    diagram.Scroll(0, -ConnectionManipulationAutoScrollBehavior.ScrollingStep);
                    break;
                case ScrollingDirection.Right:
                    diagram.Scroll(-ConnectionManipulationAutoScrollBehavior.ScrollingStep, 0);
                    break;
                case ScrollingDirection.Left:
                    diagram.Scroll(ConnectionManipulationAutoScrollBehavior.ScrollingStep, 0);
                    break;
            }
        }

        private static bool ShouldAutoScrollingStop(DiagramScrollingInfo diagramInfo)
        {
            return diagramInfo.AutoScrollingDirection == ScrollingDirection.None ||
                    (!diagramInfo.ConnectionTool.IsActive && 
                    !diagramInfo.ConnectionManipulationTool.IsActive && 
                    !diagramInfo.DraggingService.IsDragging);
        }

        private static bool IsAutoScrollingAllowed(DiagramScrollingInfo diagramInfo)
        {
            return diagramInfo.ConnectionTool.IsActive ||
                diagramInfo.ConnectionManipulationTool.IsActive ||
                diagramInfo.DraggingService.IsDragging;
        }
    }  
}
