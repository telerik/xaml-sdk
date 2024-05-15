using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Controls;

namespace CustomTabControlRegionAdapter.Infrastructure
{
	public class RadTabControlRegionAdapter : RegionAdapterBase<RadTabControl>
	{
		/// <summary> 
		/// Initializes a new instance of the <see cref="RadTabControlAdapter"/> class. 
		/// </summary> 
		/// <param name="regionBehaviorFactory">The factory used to create the region behaviors to attach to the created regions.</param> 
		public RadTabControlRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory)
			: base(regionBehaviorFactory)
		{
		}

		/// <summary> 
		/// Gets the <see cref="ItemContainerStyleProperty"/> property value. 
		/// </summary> 
		/// <param name="target">Target object of the attached property.</param> 
		/// <returns>Value of the <see cref="ItemContainerStyleProperty"/> property.</returns> 
		public static Style GetItemContainerStyle(DependencyObject target)
		{
			if (target == null) throw new ArgumentNullException("target");
			return (Style)target.GetValue(RadTabControl.ItemContainerStyleProperty);
		}

		/// <summary> 
		/// Adapts a <see cref="RadTabControl"/> to an <see cref="IRegion"/>. 
		/// </summary> 
		/// <param name="region">The new region being used.</param> 
		/// <param name="regionTarget">The object to adapt.</param> 
		protected override void Adapt(IRegion region, RadTabControl regionTarget)
		{
			if (regionTarget == null) throw new ArgumentNullException("regionTarget");
			bool itemsSourceIsSet = regionTarget.ItemsSource != null;

			if (itemsSourceIsSet)
			{
				throw new InvalidOperationException("ItemsControlHasItemsSourceException");
			}
		}

		/// <summary> 
		/// Attach new behaviors. 
		/// </summary> 
		/// <param name="region">The region being used.</param> 
		/// <param name="regionTarget">The object to adapt.</param> 
		/// <remarks> 
		/// This class attaches the base behaviors and also keeps the <see cref="RadTabControl.SelectedItem"/>  
		/// and the <see cref="IRegion.ActiveViews"/> in sync. 
		/// </remarks> 
		protected override void AttachBehaviors(IRegion region, RadTabControl regionTarget)
		{
			if (region == null) throw new ArgumentNullException("region");
			base.AttachBehaviors(region, regionTarget);
			if (!region.Behaviors.ContainsKey(RadTabControlRegionSyncBehavior.BehaviorKey))
			{
				region.Behaviors.Add(RadTabControlRegionSyncBehavior.BehaviorKey, new RadTabControlRegionSyncBehavior { HostControl = regionTarget });
			}
		}

		/// <summary> 
		/// Creates a new instance of <see cref="Region"/>. 
		/// </summary> 
		/// <returns>A new instance of <see cref="Region"/>.</returns> 
		protected override IRegion CreateRegion()
		{
			return new SingleActiveRegion();
		}
	}
}
