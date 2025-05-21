using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Map;

namespace Routing
{
    public class RoutePointModel : ViewModelBase
    {
        private bool isSelected;
        private bool isMouseOver;

        public RoutePointModel()
        {
            this.MouseEnterCommand = new DelegateCommand(this.OnEllipseMouseEnterExecuted);
            this.MouseLeaveCommand = new DelegateCommand(this.OnEllipseMouseLeaveExecuted);
        }

        public DelegateCommand MouseEnterCommand { get; set; }
        public DelegateCommand MouseLeaveCommand { get; set; }

        public bool IsSelected
        {
            get
            {
                return this.isSelected;
            }
            set
            {
                if (this.isSelected != value)
                {
                    this.isSelected = value;
                    this.OnPropertyChanged("IsSelected");
                }
            }
        }

        public bool IsMouseOver
        {
            get
            {
                return this.isMouseOver;
            }
            set
            {
                if (this.isMouseOver != value)
                {
                    this.isMouseOver = value;
                    this.OnPropertyChanged("IsMouseOver");
                }
            }
        }

        private void OnEllipseMouseEnterExecuted(object param)
        {
            this.IsMouseOver = true;
        }

        private void OnEllipseMouseLeaveExecuted(object param)
        {
            this.IsMouseOver = false;
        }

        public bool IsStartOrEnd { get; set; }
        public string Caption { get; set; }

        public string Instruction { get; set; }

        public Location Location { get; set; }
    }
}
