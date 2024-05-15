using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Diagrams;
using Telerik.Windows.Controls.Diagrams.Extensions;

namespace GlyphToolBox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RadDiagramToolboxItem selectedToolBoxItem;
        Dictionary<int, GlyphInfo> allGlyphs = new Dictionary<int, GlyphInfo>();
        Dictionary<string, Gallery> glyphGalleries = new Dictionary<string, Gallery>()
        {
            {"e0", new Gallery(){ Header ="NavigationLayout" }},
            {"e1", new Gallery(){ Header= "Action" }},
            {"e2", new Gallery(){ Header= "Media" }},
            {"e3", new Gallery(){ Header= "Toggle" }},
            {"e4", new Gallery(){ Header= "AlertNotification" }},
            {"e5", new Gallery(){ Header= "Image" }},
            {"e6", new Gallery(){ Header= "Editor" }},
            {"e7", new Gallery(){ Header= "Map" }},
            {"e8", new Gallery(){ Header= "Social" }},
            {"e9", new Gallery(){ Header= "File" }},
            {"ea", new Gallery(){ Header= "Charts" }}
        };
        Dictionary<string, ObservableCollection<GalleryItem>> sortedGallleries = new Dictionary<string, ObservableCollection<GalleryItem>>();

        Random random = new Random();

        public MainWindow()
        {          
            InitializeComponent();
            this.InitializeToolBox();
            this.GetRandom_N_Glyphs(10);
        }

        private void InitializeToolBox()
        {
            HierarchicalGalleryItemsCollection galleries = new HierarchicalGalleryItemsCollection();
            foreach (string key in glyphGalleries.Keys)
            {
                sortedGallleries[key] = new ObservableCollection<GalleryItem>();
            }

            // Fake Galleries with no items, it serves for label only.
            galleries.Insert(0, new Gallery() { Header = "SHAPES" });
            galleries.Add(new Gallery() { Header = "GLYPHS" });

            ResourceDictionary dict = new ResourceDictionary();
            dict.Source = new System.Uri("/Telerik.Windows.Controls;component/Themes/FontResources.xaml", System.UriKind.RelativeOrAbsolute);

            ObservableCollection<GlyphInfo> glyphs = new ObservableCollection<GlyphInfo>();

            int i = 0;
            foreach (var item in dict.Keys)
            {
                if (!item.ToString().StartsWith("Glyph"))
                {
                    continue;
                }

                GalleryItem galleryItem = new GalleryItem() { ItemType = "Glyph" };
                galleryItem.Header = SplitString(item.ToString());

                GlyphInfo glyph = new GlyphInfo() { GlyphName = galleryItem.Header, GlyphContent = dict[item] };
                byte[] bytes = System.Text.Encoding.Unicode.GetBytes(glyph.GlyphContent.ToString());
                string categoryCode = bytes[1].ToString("x2"); //returns e0 e1 ... e9  
                string categoryCode2 = bytes[0].ToString("x2");
                glyph.Category = glyphGalleries[categoryCode].Header.Trim();
                glyphs.Add(glyph);

                // Gallery item has no Tag property so we will use the shapes' one. We will sort the gallery items by the this tag (hex of the glyph).
                galleryItem.Shape = new RadDiagramTextShape() { Content = dict[item], Tag = categoryCode + categoryCode2 };
               
                allGlyphs[i++] = glyph;
                sortedGallleries[categoryCode].Add(galleryItem);
            }

            foreach (string key in sortedGallleries.Keys)
            {
                foreach (GalleryItem item in sortedGallleries[key].OrderBy(x => x.Shape.Tag.ToString()))
	            {
                    this.glyphGalleries[key].Items.Add(item);
	            }
                galleries.Add(this.glyphGalleries[key]);
            }          

            this.toolbox.ItemsSource = galleries;
            this.autoComplete.ItemsSource = glyphs;
        }

        private void GetRandomGlyphsButton_Clicked(object sender, RoutedEventArgs e)
        {
            this.GetRandom_N_Glyphs(10);
        }

        private void GetRandom_N_Glyphs(int n)
        {
            for (int i = 0; i < n; i++)
            {
                RadDiagramTextShape shape = new RadDiagramTextShape()
                {
                    Content = allGlyphs[random.Next(0, allGlyphs.Count)].GlyphContent,
                    Width = 64,
                    Height = 64,
                };
                this.diagram.Items.Add(shape);
            }          
            this.diagram.Layout();
            this.diagram.AutoFitAsync(new Thickness(10), true);
        }

        private void Diagram_ShapeSerialized(object sender, ShapeSerializationRoutedEventArgs e)
        {
            if (!(e.Shape is RadDiagramTextShape))
                return;

            e.SerializationInfo["Content"] = null;
            e.SerializationInfo["SVG"] = Base64Encode((string)e.Shape.Content);
        }

        private void Diagram_ShapeDeserialized(object sender, ShapeSerializationRoutedEventArgs e)
        {
            if (!(e.Shape is RadDiagramTextShape))
                return;

            if (e.SerializationInfo["SVG"] != null)
            {
                e.Shape.Content = Base64Decode((string)e.SerializationInfo["SVG"]);
            }

            if (double.IsNaN(e.Shape.Width) && double.IsNaN(e.Shape.Height))
            {
                e.Shape.Width = 64;
                e.Shape.Height = 64;   
            }
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        private static string SplitString(string input)
        {
            StringBuilder builder = new StringBuilder();
            foreach (char c in input)
            {
                if (Char.IsUpper(c) && builder.Length > 0) builder.Append(' ');
                builder.Append(c);
            }
            input = builder.ToString().Substring(6);
            return input;
        }

        private void Toolbox_Loaded(object sender, RoutedEventArgs e)
        {
            int count = this.toolbox.Items.Count;
            for (int i = 0; i < count; i++)
            {
                RadDiagramToolboxGroup container = this.toolbox.ItemContainerGenerator.ContainerFromIndex(i) as RadDiagramToolboxGroup;
                if (i == 0 || i == 5)
                {
                    container.FontWeight = FontWeights.Bold;
                    container.IsHitTestVisible = false;
                    container.Margin = i == 0 ? new Thickness(0) : new Thickness(0, 6, 0, 0);
                }
                else
                {
                    container.Margin = new Thickness(8, 0, 0, 0);
                }
            }
        }

        private void AutoComplete_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            GlyphInfo selectedGlyp = this.autoComplete.SelectedItem as GlyphInfo;
            if (selectedGlyp != null)
            {
                Gallery foundGallery = null;
                foreach (Gallery gallery in this.toolbox.Items)
                {
                    if (gallery.Header.ToString().Contains(selectedGlyp.Category))
                    {
                        foundGallery = gallery;
                        this.toolbox.SelectedItem = gallery;
                    }
                }

                if (foundGallery != null)
                {
                    this.BringToolBoxItemIntoView(foundGallery, selectedGlyp.GlyphName);
                }
            }
        }

        private void BringToolBoxItemIntoView(Gallery gallery, string searchHeader)
        {
            foreach (GalleryItem galleryItem in gallery.Items)
            {
                if (galleryItem.Header == searchHeader)
                {
                    RadDiagramToolboxGroup groupContainer = this.toolbox.ItemContainerGenerator.ContainerFromItem(gallery) as RadDiagramToolboxGroup;
                    if (groupContainer != null)
                    {
                        RadDiagramToolboxItem tbItemContainer = groupContainer.ItemContainerGenerator.ContainerFromItem(galleryItem) as RadDiagramToolboxItem;
                        if (tbItemContainer != null)
                        {
                            if (this.selectedToolBoxItem != null)
                            {
                                this.selectedToolBoxItem.ClearValue(RadDiagramToolboxItem.BackgroundProperty);
                            }
                            this.selectedToolBoxItem = tbItemContainer;
                            this.selectedToolBoxItem.Background = new SolidColorBrush(Office2016Palette.Palette.PressedColor);
                            tbItemContainer.BringIntoView();
                            return;
                        }
                    }
                }
            }
        }
    }
}
