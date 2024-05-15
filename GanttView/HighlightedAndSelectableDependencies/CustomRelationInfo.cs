using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using Telerik.Windows.Controls.GanttView;
using Telerik.Windows.Core;

namespace HighlightedAndSelectableDependencies
{
    public class CustomRelationInfo : RelationInfo
    {
       
        public CustomRelationInfo(IGanttTask taskTo, IDependency dependency, Range<int> groupsRange, Range<long> timeRange)
            : base(taskTo, dependency, groupsRange, timeRange)
        {

        }

        public Brush Foreground
        {
            get
            {
                return (this.Dependency as CustomDependency).Color;
            }
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as CustomRelationInfo);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
