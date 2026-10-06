using System;
using Telerik.Windows.Controls;

namespace Localization
{
    public class CustomLocalizationManager : LocalizationManager
    {
        public override string GetStringOverride(string key)
        {
            switch (key)
            {
                case "Ok":
                    return "~Ok~";
                case "Cancel":
                    return "~Cancel~";

                //Insert any other keys that you need.
            }
            return base.GetStringOverride(key);
        }
    }
}
