using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Data;

namespace CustomColumnEditor
{
    /// <summary>
    /// Interaction logic for CustomCaptainEditor.xaml
    /// </summary>
    public partial class CustomCaptainEditor : UserControl
    {
        public static readonly DependencyProperty CaptainNameProperty =
           DependencyProperty.Register("CaptainName", typeof(String), typeof(CustomCaptainEditor), new PropertyMetadata(null));

        public static readonly DependencyProperty CaptainPositionProperty =
            DependencyProperty.Register("CaptainPosition", typeof(Position), typeof(CustomCaptainEditor), new PropertyMetadata(null));

        public CustomCaptainEditor()
        {
            InitializeComponent();
        }

        public String CaptainName
        {
            get
            {
                return (String)this.GetValue(CaptainNameProperty);
            }
            set
            {
                this.SetValue(CaptainNameProperty, value);
            }
        }

        public Position CaptainPosition
        {
            get
            {
                return (Position)this.GetValue(CaptainPositionProperty);
            }
            set
            {
                this.SetValue(CaptainPositionProperty, value);
            }
        }

        private IEnumerable<EnumMemberViewModel> positions;

        public IEnumerable<EnumMemberViewModel> Positions
        {
            get
            {
                if (this.positions == null)
                {
                    this.positions = EnumDataSource.FromType(typeof(Position));
                }
                return this.positions;
            }
        }
    }
}
