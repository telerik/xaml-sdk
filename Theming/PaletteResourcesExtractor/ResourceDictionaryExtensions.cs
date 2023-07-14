using System.Collections;
using System.Windows;

namespace PaletteResourcesExtractor
{
    public static class ResourceDictionaryExtensions
    {
        public static DictionaryEntry[] ToDictionaryEntryArray(this ResourceDictionary dictionary)
        {
            var dictionaryEntries = new DictionaryEntry[dictionary.Count];
            dictionary.CopyTo(dictionaryEntries, 0);
            return dictionaryEntries;
        }

        public static ResourceDictionary ToResourceDictionary(this DictionaryEntry[] dictionaryEntries)
        {
            var dictionary = new ResourceDictionary();
            foreach (DictionaryEntry entry in dictionaryEntries)
            {
                dictionary.Add(entry.Key, entry.Value);
            }
            return dictionary;
        }

    }
}
