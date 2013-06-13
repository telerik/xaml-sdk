using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Controls.ChartView;

namespace Crosshair
{
    public partial class MainPage : UserControl
    {
        private bool areLabelsFollowingCursor;

        public MainPage()
        {
            InitializeComponent();

            this.DataContext = new List<EmployeeData> 
            {
                new EmployeeData { Name = "Jorma", Age = 23, Salary = 10000, },
                new EmployeeData { Name = "Andy", Age = 26, Salary = 20000, },
                new EmployeeData { Name = "Akiva", Age = 33, Salary = 14000, },
                new EmployeeData { Name = "Kenny", Age = 21, Salary = 15000, },
                new EmployeeData { Name = "Cartman", Age = 26, Salary = 10000, },
                new EmployeeData { Name = "Shreg", Age = 36, Salary = 18000, },
                new EmployeeData { Name = "Ivailo", Age = 31, Salary = 18000, },
                new EmployeeData { Name = "Jeff", Age = 37, Salary = 15000, },
                new EmployeeData { Name = "Shaquant", Age = 28, Salary = 16000, },
            };
        }

        private void RadioButtonOutside_Click(object sender, RoutedEventArgs e)
        {
            var crosshair = (ChartCrosshairBehavior)this.chart1.Behaviors[0];
            crosshair.HorizontalLineLabelDefinition = new ChartAnnotationLabelDefinition()
            {
                Location = ChartAnnotationLabelLocation.Left,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Center,
            };
            crosshair.VerticalLineLabelDefinition = new ChartAnnotationLabelDefinition()
            {
                Location = ChartAnnotationLabelLocation.Bottom,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
            };
        }

        private void RadioButtonOutsideAndOpposite_Click(object sender, RoutedEventArgs e)
        {
            this.chart1.ClipToBounds = false;
            var crosshair = (ChartCrosshairBehavior)this.chart1.Behaviors[0];
            crosshair.HorizontalLineLabelDefinition = new ChartAnnotationLabelDefinition()
            {
                Location = ChartAnnotationLabelLocation.Right,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
            };
            crosshair.VerticalLineLabelDefinition = new ChartAnnotationLabelDefinition()
            {
                Location = ChartAnnotationLabelLocation.Top,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Bottom,
            };
        }

        private void RadioButtonInside_Click(object sender, RoutedEventArgs e)
        {
            var crosshair = (ChartCrosshairBehavior)this.chart1.Behaviors[0];
            crosshair.HorizontalLineLabelDefinition = new ChartAnnotationLabelDefinition()
            {
                Location = ChartAnnotationLabelLocation.Left,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
            };
            crosshair.VerticalLineLabelDefinition = new ChartAnnotationLabelDefinition()
            {
                Location = ChartAnnotationLabelLocation.Bottom,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Bottom,
            };
        }

        private void RadioButtonInsideAndOpposite_Click(object sender, RoutedEventArgs e)
        {
            this.chart1.ClipToBounds = false;
            var crosshair = (ChartCrosshairBehavior)this.chart1.Behaviors[0];
            crosshair.HorizontalLineLabelDefinition = new ChartAnnotationLabelDefinition()
            {
                Location = ChartAnnotationLabelLocation.Right,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Center,
            };
            crosshair.VerticalLineLabelDefinition = new ChartAnnotationLabelDefinition()
            {
                Location = ChartAnnotationLabelLocation.Top,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
            };
        }

        private void RadioButtonFollowCursor_Click(object sender, RoutedEventArgs e)
        {
            this.areLabelsFollowingCursor = true;
            var crosshair = (ChartCrosshairBehavior)this.chart1.Behaviors[0];
            crosshair.VerticalLineLabelDefinition = new ChartAnnotationLabelDefinition()
            {
                Location = ChartAnnotationLabelLocation.Inside,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Bottom,
                DefaultVisualStyle = this.Resources["verticalLineLabelStyle"] as Style,
                HorizontalOffset = 2,
                VerticalOffset = 0,
            };
            crosshair.HorizontalLineLabelDefinition = new ChartAnnotationLabelDefinition()
            {
                Location = ChartAnnotationLabelLocation.Inside,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Bottom,
                DefaultVisualStyle = this.Resources["horizontalLineLabelStyle"] as Style,
                HorizontalOffset = 2,
                VerticalOffset = 2,
            };
        }

        private void ChartCrosshairBehavior_PositionChanged(object sender, ChartCrosshairPositionChangedEventArgs e)
        {
            if (this.areLabelsFollowingCursor)
            {
                var crosshair = (ChartCrosshairBehavior)this.chart1.Behaviors[0];
                if (e.Position.X < 100 && crosshair.HorizontalLineLabelDefinition.HorizontalAlignment == HorizontalAlignment.Right)
                {
                    if (e.Position.Y > 60)
                    {
                        crosshair.VerticalLineLabelDefinition = new ChartAnnotationLabelDefinition()
                        {
                            Location = ChartAnnotationLabelLocation.Inside,
                            HorizontalAlignment = HorizontalAlignment.Left,
                            VerticalAlignment = crosshair.VerticalLineLabelDefinition.VerticalAlignment,
                            DefaultVisualStyle = crosshair.VerticalLineLabelDefinition.DefaultVisualStyle,
                            HorizontalOffset = 2,
                            VerticalOffset = 0,
                        };
                        crosshair.HorizontalLineLabelDefinition = new ChartAnnotationLabelDefinition()
                        {
                            Location = ChartAnnotationLabelLocation.Inside,
                            HorizontalAlignment = HorizontalAlignment.Left,
                            VerticalAlignment = crosshair.HorizontalLineLabelDefinition.VerticalAlignment,
                            DefaultVisualStyle = crosshair.HorizontalLineLabelDefinition.DefaultVisualStyle,
                            HorizontalOffset = 2,
                            VerticalOffset = 2,
                        };
                    }
                    else
                    {
                        crosshair.VerticalLineLabelDefinition = new ChartAnnotationLabelDefinition()
                        {
                            Location = ChartAnnotationLabelLocation.Inside,
                            HorizontalAlignment = HorizontalAlignment.Left,
                            VerticalAlignment = crosshair.VerticalLineLabelDefinition.VerticalAlignment,
                            DefaultVisualStyle = crosshair.VerticalLineLabelDefinition.DefaultVisualStyle,
                            HorizontalOffset = 2,
                            VerticalOffset = 2,
                        };
                        crosshair.HorizontalLineLabelDefinition = new ChartAnnotationLabelDefinition()
                        {
                            Location = ChartAnnotationLabelLocation.Inside,
                            HorizontalAlignment = HorizontalAlignment.Left,
                            VerticalAlignment = crosshair.HorizontalLineLabelDefinition.VerticalAlignment,
                            DefaultVisualStyle = crosshair.HorizontalLineLabelDefinition.DefaultVisualStyle,
                            HorizontalOffset = 2,
                            VerticalOffset = 0,
                        };
                    }
                }

                if (e.Position.X > 100 && crosshair.HorizontalLineLabelDefinition.HorizontalAlignment == HorizontalAlignment.Left)
                {
                    if (e.Position.Y > 60)
                    {
                        crosshair.VerticalLineLabelDefinition = new ChartAnnotationLabelDefinition()
                        {
                            Location = ChartAnnotationLabelLocation.Inside,
                            HorizontalAlignment = HorizontalAlignment.Right,
                            VerticalAlignment = crosshair.VerticalLineLabelDefinition.VerticalAlignment,
                            DefaultVisualStyle = crosshair.VerticalLineLabelDefinition.DefaultVisualStyle,
                            HorizontalOffset = 2,
                            VerticalOffset = 0,
                        };
                        crosshair.HorizontalLineLabelDefinition = new ChartAnnotationLabelDefinition()
                        {
                            Location = ChartAnnotationLabelLocation.Inside,
                            HorizontalAlignment = HorizontalAlignment.Right,
                            VerticalAlignment = crosshair.HorizontalLineLabelDefinition.VerticalAlignment,
                            DefaultVisualStyle = crosshair.HorizontalLineLabelDefinition.DefaultVisualStyle,
                            HorizontalOffset = 2,
                            VerticalOffset = 2,
                        };
                    }
                    else
                    {
                        crosshair.VerticalLineLabelDefinition = new ChartAnnotationLabelDefinition()
                        {
                            Location = ChartAnnotationLabelLocation.Inside,
                            HorizontalAlignment = HorizontalAlignment.Right,
                            VerticalAlignment = crosshair.VerticalLineLabelDefinition.VerticalAlignment,
                            DefaultVisualStyle = crosshair.VerticalLineLabelDefinition.DefaultVisualStyle,
                            HorizontalOffset = 2,
                            VerticalOffset = 2,
                        };
                        crosshair.HorizontalLineLabelDefinition = new ChartAnnotationLabelDefinition()
                        {
                            Location = ChartAnnotationLabelLocation.Inside,
                            HorizontalAlignment = HorizontalAlignment.Right,
                            VerticalAlignment = crosshair.HorizontalLineLabelDefinition.VerticalAlignment,
                            DefaultVisualStyle = crosshair.HorizontalLineLabelDefinition.DefaultVisualStyle,
                            HorizontalOffset = 2,
                            VerticalOffset = 0,
                        };
                    }
                }

                if (e.Position.Y < 60 && crosshair.HorizontalLineLabelDefinition.VerticalAlignment == VerticalAlignment.Bottom)
                {
                    crosshair.VerticalLineLabelDefinition = new ChartAnnotationLabelDefinition()
                    {
                        Location = ChartAnnotationLabelLocation.Inside,
                        HorizontalAlignment = crosshair.VerticalLineLabelDefinition.HorizontalAlignment,
                        VerticalAlignment = VerticalAlignment.Top,
                        DefaultVisualStyle = crosshair.VerticalLineLabelDefinition.DefaultVisualStyle,
                        HorizontalOffset = 2,
                        VerticalOffset = 2,
                    };
                    crosshair.HorizontalLineLabelDefinition = new ChartAnnotationLabelDefinition()
                    {
                        Location = ChartAnnotationLabelLocation.Inside,
                        HorizontalAlignment = crosshair.HorizontalLineLabelDefinition.HorizontalAlignment,
                        VerticalAlignment = VerticalAlignment.Top,
                        DefaultVisualStyle = crosshair.HorizontalLineLabelDefinition.DefaultVisualStyle,
                        HorizontalOffset = 2,
                        VerticalOffset = 0,
                    };
                }

                if (e.Position.Y > 60 && crosshair.HorizontalLineLabelDefinition.VerticalAlignment == VerticalAlignment.Top)
                {
                    crosshair.VerticalLineLabelDefinition = new ChartAnnotationLabelDefinition()
                    {
                        Location = ChartAnnotationLabelLocation.Inside,
                        HorizontalAlignment = crosshair.VerticalLineLabelDefinition.HorizontalAlignment,
                        VerticalAlignment = VerticalAlignment.Bottom,
                        DefaultVisualStyle = crosshair.VerticalLineLabelDefinition.DefaultVisualStyle,
                        HorizontalOffset = 2,
                        VerticalOffset = 0,
                    };
                    crosshair.HorizontalLineLabelDefinition = new ChartAnnotationLabelDefinition()
                    {
                        Location = ChartAnnotationLabelLocation.Inside,
                        HorizontalAlignment = crosshair.HorizontalLineLabelDefinition.HorizontalAlignment,
                        VerticalAlignment = VerticalAlignment.Bottom,
                        DefaultVisualStyle = crosshair.HorizontalLineLabelDefinition.DefaultVisualStyle,
                        HorizontalOffset = 2,
                        VerticalOffset = 2,
                    };
                }

            }
        }

        private void RadioButtonFollowCursor_Unchecked(object sender, RoutedEventArgs e)
        {
            this.areLabelsFollowingCursor = false;
        }
    }
}
