using System;
using System.Windows.Input;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.RadialMenu;

namespace ItemCommands
{
    public class RotateLeftCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var radialMenuItem = parameter as RadRadialMenuItem;
            if (radialMenuItem != null)
            {
                var target = radialMenuItem.Menu.TargetElement;
                if (target != null)
                {
                    var transformGroup = (target.RenderTransform as TransformGroup).Children;
                    if (transformGroup != null)
                    {
                        foreach (var transform in transformGroup)
                        {
                            var rotateTranform = transform as RotateTransform;
                            if (rotateTranform != null)
                            {
                                rotateTranform.Angle += 90;
                            }
                        }
                    }
                }
            }

        }

        public event EventHandler CanExecuteChanged;
    }
}
