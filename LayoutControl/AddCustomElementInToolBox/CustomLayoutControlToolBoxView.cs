using System;
using System.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.LayoutControl;

namespace AddCustomElementInToolBox
{
    /* The CustomLayoutControlToolBoxView and the converter used in the NewItemsTemplate in XAML 
     * are implemented only to allow setting a custom icon for the custom toolbox item.
     * If you don't need a custom icon for the custom item you can skip using this class. 
     * In this case the default icon (a rectangle) will be used */
    public class CustomLayoutControlToolBoxView : LayoutControlToolBoxView
    {
        private RadListBox newItemsControl;
        private RadTreeView structureTreeControl;       
                
        public static readonly DependencyProperty NewItemsTemplateProperty =
            DependencyProperty.Register(
                "NewItemsTemplate",                 
                typeof(DataTemplate), 
                typeof(CustomLayoutControlToolBoxView), 
                new PropertyMetadata(null, OnNewItemsTemplateChanged));

        public static readonly DependencyProperty StructureItemsTemplateProperty =
         DependencyProperty.Register(
             "StructureItemsTemplate",
             typeof(DataTemplate),
             typeof(CustomLayoutControlToolBoxView),
             new PropertyMetadata(null, OnStructureItemsTemplatePropertyChanged));


        public DataTemplate NewItemsTemplate
        {
            get { return (DataTemplate)GetValue(NewItemsTemplateProperty); }
            set { SetValue(NewItemsTemplateProperty, value); }
        }

        public DataTemplate StructureItemsTemplate
        {
            get { return (DataTemplate)GetValue(StructureItemsTemplateProperty); }
            set { SetValue(StructureItemsTemplateProperty, value); }
        }

        private static void OnNewItemsTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CustomLayoutControlToolBoxView self = (CustomLayoutControlToolBoxView)d;            
            self.UpdateNewItemsTemplate((DataTemplate)e.NewValue);
        }       

        private static void OnStructureItemsTemplatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CustomLayoutControlToolBoxView self = (CustomLayoutControlToolBoxView)d;
            self.UpdateStructureItemsTemplate((DataTemplate)e.NewValue);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.newItemsControl = this.GetTemplateChild("newItemsList") as RadListBox;
            this.structureTreeControl = this.GetTemplateChild("PART_StructureTree") as RadTreeView;

            this.UpdateNewItemsTemplate(this.NewItemsTemplate);
            this.UpdateStructureItemsTemplate(this.StructureItemsTemplate);
        }

        private void UpdateStructureItemsTemplate(DataTemplate template)
        {
            if (this.structureTreeControl != null)
            {
                this.structureTreeControl.ItemTemplate = template;
            }
        }

        private void UpdateNewItemsTemplate(DataTemplate template)
        {
            if (this.newItemsControl != null)
            {
                this.newItemsControl.ItemTemplate = template;
            }
        }
    }
}
