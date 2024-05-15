using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Markup;
using System.Xml;
using Telerik.Windows.Controls;

namespace PaletteResourcesExtractor
{
    public static class PaletteResourcesManager
    {
        private static readonly Type themeBaseType = typeof(Theme);
        private static XmlWriterSettings xmlWriterSettings;
        private static IEnumerable<Type> paletteTypes;
        private static IEnumerable<Type> colorVariationTypes;
        
        static PaletteResourcesManager()
        {
            xmlWriterSettings = new XmlWriterSettings()
            {
                Indent = true,
                OmitXmlDeclaration = true,
                IndentChars = "\t"
            };

            Assembly controlsAssembly = TelerikAssemblyHelper.GetTelerikAssemblyCache();
            paletteTypes = controlsAssembly.GetTypes().Where(x => x.IsSubclassOf(typeof(ThemePalette)));
            colorVariationTypes = controlsAssembly.GetTypes().Where(x => x.Name == "ColorVariation");
        }

        public static List<Type> GetThemesWithPalettes()
        {
            var themeTypesWithPalettes = new List<Type>();
            var themeTypes = TelerikAssemblyHelper.GetTelerikAssemblyCache().GetTypes().Where(x => x.IsSubclassOf(typeof(Theme)));
            foreach (Type themeType in themeTypes)
            {
                string themeName = themeType.Name.Replace("Theme", string.Empty);
                Type paletteType = paletteTypes.FirstOrDefault(x => x.Name.Contains(themeName));
                if (paletteType != null)
                {
                    themeTypesWithPalettes.Add(themeType);
                }
            }
            return themeTypesWithPalettes;
        }

        public static List<PaletteResourceInfo> ExtractPaletteResourcesForTheme(Type themeType)
        {
            if (themeType.BaseType != themeBaseType)
            {
                throw new ArgumentException(string.Format("'{0}' must derive from '{1}'", themeType.FullName, themeBaseType.FullName));
            }

            string themeName = themeType.Name.Replace("Theme", string.Empty);
            Type paletteType = paletteTypes.FirstOrDefault(x => x.Name.Replace("Palette", string.Empty).Equals(themeName));
            if (paletteType == null)
            {
                throw new ArgumentException(string.Format("'{0}' doesn't support palettes", themeType.FullName));
            }

            List<PaletteResourceInfo> paletteResourceInfos = new List<PaletteResourceInfo>();
            ResourceDictionary paletteResources = GetPaletteResources(paletteType);
            Type colorVariationEnumType = colorVariationTypes.FirstOrDefault(x => x.FullName.Contains(paletteType.Name));
            if (colorVariationEnumType != null)
            {
                ThemePalette paletteInstance = (ThemePalette)paletteType.GetProperty("Palette").GetValue(null, null);
                MethodInfo loadPresetMethod = paletteType.GetMethod("LoadPreset");
                Array colorVariations = Enum.GetValues(colorVariationEnumType);
                foreach (var variation in colorVariations)
                {
                    loadPresetMethod.Invoke(paletteInstance, new object[1] { variation });
                    paletteResourceInfos.Add(CreatePaletteInfo(paletteType, paletteType.Name + variation));
                }
            }
            else
            {
                paletteResourceInfos.Add(CreatePaletteInfo(paletteType, paletteType.Name));
            }

            if (themeType == typeof(Windows11Theme))
            {
                Type sizeHelperType = typeof(Windows11ThemeSizeHelper);                
                paletteResourceInfos.Add(CreatePaletteInfo(sizeHelperType, sizeHelperType.Name + "Standard"));
                Windows11ThemeSizeHelper.Helper.IsInCompactMode = true;
                paletteResourceInfos.Add(CreatePaletteInfo(sizeHelperType, sizeHelperType.Name + "Compact"));
            }

            return paletteResourceInfos;
        }
        
        /// <summary>Prepares all palette resource dictionaries and save them to files.</summary>
        /// <param name="baseFolderPath">The path should be relative or absolute and it should end with / character. For example: "../../PaletteResources/"</param>
        public static void SavePaletteResourcesToFiles(string baseFolderPath)
        {
            foreach (Type type in paletteTypes)
            {
                ResourceDictionary dictionary = GetPaletteResources(type);               
                Type colorVariationEnumType = colorVariationTypes.FirstOrDefault(x => x.FullName.Contains(type.Name));
                if (colorVariationEnumType != null)
                {
                    ThemePalette paletteInstance = (ThemePalette)type.GetProperty("Palette").GetValue(null, null);
                    MethodInfo loadPresetMethod = type.GetMethod("LoadPreset");
                    Array colorVariations = Enum.GetValues(colorVariationEnumType);
                    foreach (var variation in colorVariations)
                    {
                        loadPresetMethod.Invoke(paletteInstance, new object[1] { variation });
                        SavePaletteResourcesToFile(dictionary, baseFolderPath + type.Name + variation + "Resources.xaml");
                    }
                }
                else
                {
                    SavePaletteResourcesToFile(dictionary, baseFolderPath + type.Name + "Resources.xaml");
                }
            }
        }

        public static void SavePaletteResourcesToFile(ResourceDictionary paletteResources, string resourceFilePath)
        {
            XmlWriter writer = XmlWriter.Create(resourceFilePath, xmlWriterSettings);
            XamlWriter.Save(paletteResources, writer);
        }

        private static ResourceDictionary GetPaletteResources(Type paletteType)
        {
            var dictionaryProperty = paletteType.GetProperty("ResourceDictionary", BindingFlags.Static | BindingFlags.NonPublic);
            var dictionary = (ResourceDictionary)dictionaryProperty.GetValue(null, null);
            return dictionary;
        }

        private static PaletteResourceInfo CreatePaletteInfo(Type resourceObjectType, string resourceName)
        {
            ResourceDictionary resources = GetPaletteResources(resourceObjectType);
            string resourcesString = SavePaletteResourcesToString(resources);
            return new PaletteResourceInfo()
            {
                Name = resourceName,
                Content = resourcesString,
                Resources = resources.ToDictionaryEntryArray(),
            };
        }

        private static string SavePaletteResourcesToString(ResourceDictionary paletteResources)
        {
            StringBuilder result = new StringBuilder();
            XmlWriter writer = XmlWriter.Create(result, xmlWriterSettings);
            XamlWriter.Save(paletteResources, writer);
            return result.ToString();
        }
    }
}
