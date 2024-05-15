using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using Telerik.Pivot.Core;
using Telerik.Pivot.Core.Fields;

namespace CustomDateTimeGroupDescription
{
    /// <summary>
    /// Interaction logic for CustomDateTimeGroup.xaml
    /// </summary>
    public partial class CustomDateTimeGroup : UserControl
    {
        public CustomDateTimeGroup()
        {
            InitializeComponent();
            var provider = this.Resources["dataProvider"] as LocalDataSourceProvider;

            provider.PrepareDescriptionForField += provider_PrepareDescription;
        }

        private void provider_PrepareDescription(object sender, PrepareDescriptionForFieldEventArgs e)
        {
            if (e.DescriptionType == DataProviderDescriptionType.Group && e.FieldInfo.DataType == typeof(DateTime))
            {
                e.Description = new MyDateTimeGroupDescription() { PropertyName = e.FieldInfo.Name };
            }
        }
    }

    public class MyFieldDescriptionProvider : LocalDataSourceFieldDescriptionsProvider
    {
        protected override ContainerNode GetFieldDescriptionHierarchy(IEnumerable<IPivotFieldInfo> descriptions)
        {
            var root = new ContainerNode("Root", ContainerNodeRole.None);
            foreach (var fieldInfoItem in descriptions)
            {
                var fieldDescriptionNode = new FieldInfoNode(fieldInfoItem);
                root.Children.Add(fieldDescriptionNode);
            }
            return root;
        }
    }

    public class MyDateTimeGroupDescription : PropertyGroupDescriptionBase
    {

        protected override void CloneOverride(Cloneable source)
        {
            // Copy all properties from source to 'this' description is applicable.
        }

        protected override Cloneable CreateInstanceCore()
        {
            return new MyDateTimeGroupDescription();
        }

        protected override object GroupNameFromItem(object item, int level)
        {
            var baseValue = base.GroupNameFromItem(item, level);

            if (baseValue == null)
            {
                return null;
            }
            else
            {
                return new MonthYearGroup(((DateTime)baseValue).Year, ((DateTime)baseValue).Month);
            }
        }
    }

    public struct MonthYearGroup : IComparable, IComparable<MonthYearGroup>, IEquatable<MonthYearGroup>
    {
        public MonthYearGroup(int year, int month)
            : this()
        {
            this.Year = year;
            this.Month = month;
        }

        /// <summary>
        /// Gets the Year this <see cref="YearGroup"/> represents.
        /// </summary>
        public int Year
        {
            get;
            set;
        }

        public int Month
        {
            get;
            set;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return String.Format("[{0}-{1}]", this.Year.ToString(CultureInfo.InvariantCulture.NumberFormat), CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(this.Month));
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return (this.Year * this.Month) ^ 211;
        }

        /// <inheritdoc />
        [SuppressMessage("Microsoft.Usage", "CA2231:OverloadOperatorEqualsOnOverridingValueTypeEquals", Justification = "Design choice.")]
        public override bool Equals(object obj)
        {
            if (obj is MonthYearGroup)
            {
                return this.Equals((MonthYearGroup)obj);
            }

            return false;
        }

        /// <inheritdoc />
        public bool Equals(MonthYearGroup other)
        {
            return (this.Year == other.Year && this.Month == other.Month);
        }

        /// <inheritdoc />
        public int CompareTo(object obj)
        {
            if (obj is MonthYearGroup)
            {
                return this.CompareTo((MonthYearGroup)obj);
            }

            throw new ArgumentException("Can not compare.", "obj");
        }

        /// <inheritdoc />
        public int CompareTo(MonthYearGroup other)
        {
            var yearCompare = this.Year.CompareTo(other.Year);
            return yearCompare == 0 ? this.Month.CompareTo(other.Month) : yearCompare;
        }
    }
}
