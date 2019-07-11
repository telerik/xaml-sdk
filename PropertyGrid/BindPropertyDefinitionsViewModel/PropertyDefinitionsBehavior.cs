using System.Collections;
using System.Collections.Specialized;
using System.Windows;
using Telerik.Windows.Controls;

namespace BindPropertyDefinitionsViewModel
{
    public class PropertyDefinitionsBehavior : ViewModelBase
    {
        private readonly RadPropertyGrid propertyGrid = null;
        private readonly INotifyCollectionChanged PropertyDefinitions = null;

        public static readonly DependencyProperty PropertyDefinitionsProperty
            = DependencyProperty.RegisterAttached("PropertyDefinitions", typeof(INotifyCollectionChanged), typeof(PropertyDefinitionsBehavior),
                new PropertyMetadata(new PropertyChangedCallback(OnPropertyDefinitionsPropertyChanged)));

        public static void SetPropertyDefinitions(DependencyObject dependencyObject, INotifyCollectionChanged PropertyDefinitions)
        {
            dependencyObject.SetValue(PropertyDefinitionsProperty, PropertyDefinitions);
        }

        public static INotifyCollectionChanged GetPropertyDefinitions(DependencyObject dependencyObject)
        {
            return (INotifyCollectionChanged)dependencyObject.GetValue(PropertyDefinitionsProperty);
        }

        private static void OnPropertyDefinitionsPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            RadPropertyGrid propertyGrid = dependencyObject as RadPropertyGrid;
            INotifyCollectionChanged PropertyDefinitions = e.NewValue as INotifyCollectionChanged;

            if (propertyGrid != null && PropertyDefinitions != null)
            {
                PropertyDefinitionsBehavior behavior = new PropertyDefinitionsBehavior(propertyGrid, PropertyDefinitions);
                behavior.Attach();
            }
        }

        private void Attach()
        {
            if (propertyGrid != null && PropertyDefinitions != null)
            {
                Transfer(GetPropertyDefinitions(propertyGrid) as IList, propertyGrid.PropertyDefinitions);

                PropertyDefinitions.CollectionChanged -= ContextPropertyDefinitions_CollectionChanged;
                PropertyDefinitions.CollectionChanged += ContextPropertyDefinitions_CollectionChanged;
            }
        }

        public PropertyDefinitionsBehavior(RadPropertyGrid propertyGrid, INotifyCollectionChanged PropertyDefinitions)
        {
            this.propertyGrid = propertyGrid;
            this.PropertyDefinitions = PropertyDefinitions;
        }

        void ContextPropertyDefinitions_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UnsubscribeFromEvents();

            Transfer(GetPropertyDefinitions(propertyGrid) as IList, propertyGrid.PropertyDefinitions);

            SubscribeToEvents();
        }

        void propertyGridPropertyDefinitions_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            UnsubscribeFromEvents();

            Transfer(propertyGrid.PropertyDefinitions, GetPropertyDefinitions(propertyGrid) as IList);

            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            propertyGrid.PropertyDefinitions.CollectionChanged += propertyGridPropertyDefinitions_CollectionChanged;

            if (GetPropertyDefinitions(propertyGrid) != null)
            {
                GetPropertyDefinitions(propertyGrid).CollectionChanged += ContextPropertyDefinitions_CollectionChanged;
            }
        }

        private void UnsubscribeFromEvents()
        {
            propertyGrid.PropertyDefinitions.CollectionChanged -= propertyGridPropertyDefinitions_CollectionChanged;

            if (GetPropertyDefinitions(propertyGrid) != null)
            {
                GetPropertyDefinitions(propertyGrid).CollectionChanged -= ContextPropertyDefinitions_CollectionChanged;
            }
        }

        public static void Transfer(IList source, IList target)
        {
            if (source == null || target == null)
                return;

            target.Clear();

            foreach (object o in source)
            {
                target.Add(o);
            }
        }
}
}
