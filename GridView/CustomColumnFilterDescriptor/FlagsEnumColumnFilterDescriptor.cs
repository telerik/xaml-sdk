using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace CustomColumnFilterDescriptor
{
	/// <summary>
	/// A MemberColumnFilterDescriptor handling flags enum properties.
	/// </summary>
	public class FlagsEnumColumnFilterDescriptor<T> : MemberColumnFilterDescriptor
	{
		private readonly static MethodInfo passesFilterMethodInfo = 
			typeof(FlagsEnumColumnFilterDescriptor<T>).GetMethod("PassesFilter");
		
		// This is the delegate that we will call in order to get the property value of an item.
		private Delegate getPropertyValueDelegate;
		
		// The parent column.
		private readonly GridViewColumn column;
		
		/// <summary>
		/// Initializes a new instance of the <see cref="FlagsEnumColumnFilterDescriptor" /> class.
		/// </summary>
		/// <param name="column">The column.</param>
		public FlagsEnumColumnFilterDescriptor(GridViewColumn column) : base(column)
		{
			if (!(typeof(T).IsEnum))
			{
				throw new ArgumentException("Generic Type must be a System.Enum");
			}

			this.column = column;
		}

		/// <summary>
		/// Creates a predicate filter expression used for collection filtering.
		/// </summary>
		/// <param name="instance">The instance expression, which will be used for filtering.</param>
		/// <returns>A predicate filter expression.</returns>
		public override Expression CreateFilterExpression(Expression instance)
		{
			// Please note that this expression will work only when the LINQ query provider is LINQ-to-Objects
			// i.e. the data is an in-memory collection. Calling the PassesFilter method cannot be done with 
			// LINQ-to-SQL or LINQ-to-Entities, since such function does not exist on SQL Server.
			
			// $person
			ParameterExpression parameter = (ParameterExpression)instance;
			
			// this
			ConstantExpression thisExpression = Expression.Constant(this, typeof(FlagsEnumColumnFilterDescriptor<T>));
			
			// this.PassesFilter(person)
			MethodCallExpression filteringExpression = Expression.Call(thisExpression, passesFilterMethodInfo, parameter);

			// This expression simply calls the PassesFilter method to determine whether an item passes the filter.
			// This is the expression that our data engine will use in order to see whether each person passes the current filter.
			// The data engine will build something like this:
			// var result = sourceCollection.Where(person => this.PassesFilter(person));
			// and return only persons that pass the filtering criteria.
			return filteringExpression;
		}

		/// <summary>
		/// Determines whether a data item passes the current distinct values filter.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <returns></returns>
		public bool PassesFilter(object item)
		{
			var checkedDistinctValues = ((IColumnFilterDescriptor)this).DistinctFilter.DistinctValues.ToList();

			if (checkedDistinctValues.Count == 0)
			{
				// The user has not checked anything, so every item passes the filter.
				return true;
			}

			// These are the WorkingDays for the particular item. i.e. Person.
			// We cast both enum's to int so we can "bitwise and" them.
			int itemPropertyValue = (int)this.getPropertyValueDelegate.DynamicInvoke(item);

			foreach (int checkedDistinctValue in checkedDistinctValues)
			{
				if ((itemPropertyValue & checkedDistinctValue) == checkedDistinctValue)
				{
					// If the item has the specific flag set then it passes the filter.
					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// Refreshes the column filter descriptor from its parent column.
		/// </summary>
		public override void Refresh()
		{
			base.Refresh();

			if (this.column.ItemType == null || string.IsNullOrEmpty(base.Member))
			{
				return;
			}

			// Once the ItemType, i.e. Person and the Member, i.e. "WorkingDays" are known
			// we can build a delegate that returns the WorkingDays for a Person.
			// We will need this delegate in the PassesFilter method.

			// typeof(Person)
			Type itemType = this.column.ItemType;

			// WorkingDays property
			PropertyInfo propertyInfo = itemType.GetProperty(this.Member);

			// $item
			ParameterExpression parameterExpression = Expression.Parameter(itemType, "item");

			// item.WorkingDays
			MemberExpression getPropertyExpression = Expression.MakeMemberAccess(parameterExpression, propertyInfo);

			// (item) => item.WorkingDays
			LambdaExpression getPropertyLambda = Expression.Lambda(getPropertyExpression, parameterExpression);

			// This is a method that we will invoke in order to read the value of the enum property of each item,
			// i.e. calling this method on a Person will return his WorkingDays.
			this.getPropertyValueDelegate = getPropertyLambda.Compile();
		}
	}
}
