using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Data;
using System.Collections.Generic;

namespace FilteringCollectionProperties
{
	public class CollectionPropertyColumnFilterDescriptor : MemberColumnFilterDescriptor
	{
		private static readonly MethodInfo EnumerableCastMethod = typeof(Enumerable).GetMethod("Cast");
		private static MethodInfo GenericContainsMethod = GetGenericContainsMethodInfo();
		
		public CollectionPropertyColumnFilterDescriptor(GridViewColumn column) : base(column)
		{
		}

		private static MethodInfo GetGenericContainsMethodInfo()
		{
			// get the Enumerable.Contains<TSource>(IEnumerable<TSource> source, TSource value) method,
			// because it is impossible to get it through Type.GetMethod().
			var methodCall = ((MethodCallExpression)((Expression<Func<IEnumerable<object>, bool>>)(source => source.Contains(null))).Body).Method.GetGenericMethodDefinition();
			return methodCall.MakeGenericMethod(typeof(object));
		}
 
		public override Expression CreateFilterExpression(Expression instance)
		{
			// WorkingDays
			string propertyName = base.Member;

			// person.WorkingDays
			MemberExpression collectionPropertyAccessor = Expression.Property(instance, propertyName);

			// person.WorkingDays.Cast<object>()
			MethodCallExpression genericCollectionPropertyAccessor = Expression.Call(null
				, EnumerableCastMethod.MakeGenericMethod(new[] { typeof(object) })
				, collectionPropertyAccessor);

			IFieldFilterDescriptor fieldFilter = ((IColumnFilterDescriptor)this).FieldFilter;

			Expression f1Expression = null;
			if (fieldFilter.Filter1.Value != FilterDescriptor.UnsetValue)
			{
				// Build the UPPER field filter, i.e. field filter 1
				// "Monday"
				ConstantExpression f1Value = Expression.Constant(fieldFilter.Filter1.Value);

				// person.WorkingDays.Cast<object>().Contains("Monday")
				f1Expression = Expression.Call(
					CollectionPropertyColumnFilterDescriptor.GenericContainsMethod
					, genericCollectionPropertyAccessor
					, f1Value);

				if (fieldFilter.Filter1.Operator == FilterOperator.DoesNotContain)
				{
					// !person.WorkingDays.Cast<object>().Contains("Monday")
					f1Expression = Expression.Not(f1Expression);
				}
			}

			Expression f2Expression = null;
			if (fieldFilter.Filter2.Value != FilterDescriptor.UnsetValue)
			{
				// Build the LOWER field filter, i.e. field filter 2
				// "Tuesday"
				ConstantExpression f2Value = Expression.Constant(fieldFilter.Filter2.Value);

				// person.WorkingDays.Cast<object>().Contains("Tuesday")
				f2Expression = Expression.Call(
					CollectionPropertyColumnFilterDescriptor.GenericContainsMethod
					, genericCollectionPropertyAccessor
					, f2Value);

				if (fieldFilter.Filter2.Operator == FilterOperator.DoesNotContain)
				{
					// !person.WorkingDays.Cast<object>().Contains("Tuesday")
					f2Expression = Expression.Not(f2Expression);
				}
			}

			if (f1Expression == null)
			{
				if (f2Expression != null)
				{
					return f2Expression;
				}

				return Expression.Constant(true);
			}

			if (f2Expression == null)
			{
				if (f1Expression != null)
				{
					return f1Expression;
				}

				return Expression.Constant(true);
			}

			switch (fieldFilter.LogicalOperator)
			{
				case FilterCompositionLogicalOperator.And:
					return Expression.And(f1Expression, f2Expression);
				case FilterCompositionLogicalOperator.Or:
					return Expression.Or(f1Expression, f2Expression);
				default:
					throw new InvalidOperationException();
			}
		}
	}
}
