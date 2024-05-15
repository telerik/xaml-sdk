using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Telerik.Windows.Controls;
using System.Windows.Media;
using Telerik.Windows.Controls.RadialMenu;

namespace ItemCommands
{
    public class ExampleViewModel : ViewModelBase
    {
        private TransformGroup renderTransform;

        public ExampleViewModel()
        {
            this.ScaleXCommand = new DelegateCommand(new Action<object>(this.OnScaleXCommandExecuted));
            this.ScaleYCommand = new DelegateCommand(new Action<object>(this.OnScaleYCommandExecuted));
            var transformCollection = new TransformCollection();
            transformCollection.Add(new RotateTransform());
            transformCollection.Add(new ScaleTransform());
            this.renderTransform = new TransformGroup() { Children = transformCollection };
        }

        /// <summary>
        /// Gets or sets RenderTransform and notifies for changes
        /// </summary>
        public TransformGroup RenderTransform
        {
            get
            {
                return this.renderTransform;
            }

            set
            {
                if (this.renderTransform != value)
                {
                    this.renderTransform = value;
                    this.OnPropertyChanged(() => this.RenderTransform);
                }
            }
        }

        public ICommand ScaleXCommand { get; private set; }

        public ICommand ScaleYCommand { get; private set; }

        private void OnScaleXCommandExecuted(object obj)
        {
            foreach (var transform in this.RenderTransform.Children)
            {
                var scaleTranform = transform as ScaleTransform;
                if (scaleTranform != null)
                {
                    var item = obj as RadRadialMenuItem;
                    if (item != null)
                    {
                        if (item.Header.ToString().Contains("+"))
                        {
                            scaleTranform.ScaleX += 0.1;
                        }
                        else
                        {
                            scaleTranform.ScaleX -= 0.1;
                        }
                    }
                }
            }
        }

        private void OnScaleYCommandExecuted(object obj)
        {
            foreach (var transform in this.RenderTransform.Children)
            {
                var scaleTranform = transform as ScaleTransform;
                if (scaleTranform != null)
                {
                    var item = obj as RadRadialMenuItem;
                    if (item != null)
                    {
                        if (item.Header.ToString().Contains("+"))
                        {
                            scaleTranform.ScaleY += 0.1;
                        }
                        else
                        {
                            scaleTranform.ScaleY -= 0.1;
                        }
                    }
                }
            }
        }
    }
}
