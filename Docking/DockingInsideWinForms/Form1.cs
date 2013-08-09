using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.Windows.Controls;

namespace DockingInsideWinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            WindowInteropabilityHelper.SetWindowInteropabilityAdapter(this.dockingControl1, new Adapter(this));
        }

        private class Adapter : IWindowInteropabilityAdapter
        {
            private Form1 form;
            public Adapter(Form1 form)
            {
                this.form = form;
            }

            public int AbsoluteLeft
            {
                get { return this.form.PointToScreen(this.form.elementHost1.Location).X; }
            }

            public int AbsoluteTop
            {
                get { return this.form.PointToScreen(this.form.elementHost1.Location).Y; }
            }

            public IntPtr Handle
            {
                get { return this.form.Handle; }
            }
        }

    }
}
