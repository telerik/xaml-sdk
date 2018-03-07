/*The MIT License(MIT)

Copyright(c) 2013 Max Truxa

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
the Software, and to permit persons to whom the Software is furnished to do so,
subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

/* Portions of maxtruxa/AccentColors/AccentColors/AccentColor.cs file as modified by Telerik AD - Copyright © 2018 Telerik AD. All rights reserved.
*/

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace FluentAccentColors
{
    public class AccentColorSet
    {
        static AccentColorSet[] accentColorSetCollection;
        static AccentColorSet activeColorSet;
        private UInt32 colorSet;

        AccentColorSet(UInt32 colorSet, Boolean active)
        {
            this.colorSet = colorSet;
            this.Active = active;
        }

        // would read the current theme variation from the registry
        public static ThemeVariation Variation
        {
            get
            {
                var reg = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize");
                if (reg == null)
                {
                    return ThemeVariation.Light;
                }
                return (ThemeVariation)reg.GetValue("AppsUseLightTheme");
            }
        }

        public static Color RgbFromArgbAndBackgroundColor(Color targetColor, Color backgroundColor)
        {
            var baseAlpha = targetColor.A / 255.0;
            var reverseAlpha = 1.0 - baseAlpha;
            byte redValue = (byte)((targetColor.R * baseAlpha) + (backgroundColor.R * reverseAlpha));
            byte greenValue = (byte)((targetColor.G * baseAlpha) + (backgroundColor.G * reverseAlpha));
            byte blueValue = (byte)((targetColor.B * baseAlpha) + (backgroundColor.B * reverseAlpha));

            return Color.FromArgb(255, redValue, greenValue, blueValue);
        }

        public static byte TransfromPercent(double d)
        {
            return (byte)(d * 255.0);
        }

        public static AccentColorSet[] AccentColorSetCollection
        {
            get
            {
                if (accentColorSetCollection == null)
                {
                    UInt32 colorSetCount = UXTheme.GetImmersiveColorSetCount();

                    List<AccentColorSet> colorSets = new List<AccentColorSet>();
                    for (UInt32 i = 0; i < colorSetCount; i++)
                    {
                        colorSets.Add(new AccentColorSet(i, false));
                    }

                    AccentColorSetCollection = colorSets.ToArray();
                }

                return accentColorSetCollection;
            }
            private set
            {
                accentColorSetCollection = value;
            }
        }

        public static AccentColorSet ActiveSet
        {
            get
            {
                UInt32 activeSet = UXTheme.GetImmersiveUserColorSetPreference(false, false);
                ActiveSet = AccentColorSetCollection[Math.Min(activeSet, AccentColorSetCollection.Length - 1)];
                return activeColorSet;
            }
            private set
            {
                if (activeColorSet != null) activeColorSet.Active = false;

                value.Active = true;
                activeColorSet = value;
            }
        }

        public Boolean Active { get; private set; }

        public Color this[String colorName]
        {
            get
            {
                IntPtr name = IntPtr.Zero;
                UInt32 colorType;

                try
                {
                    name = Marshal.StringToHGlobalUni("Immersive" + colorName);
                    colorType = UXTheme.GetImmersiveColorTypeFromName(name);
                    try
                    {
                        if (colorType == 0xFFFFFFFF) throw new InvalidOperationException();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("The OS system on your machine is not Windows 10. This project required to use Windows 10.");
                    }
                }
                finally
                {
                    if (name != IntPtr.Zero)
                    {
                        Marshal.FreeHGlobal(name);
                        name = IntPtr.Zero;
                    }
                }

                return this[colorType];
            }
        }

        public Color this[UInt32 colorType]
        {
            get
            {
                UInt32 nativeColor = UXTheme.GetImmersiveColorFromColorSetEx(this.colorSet, colorType, false, 0);
                //if (nativeColor == 0)
                //    throw new InvalidOperationException();
                return Color.FromArgb(
                    (Byte)((0xFF000000 & nativeColor) >> 24),
                    (Byte)((0x000000FF & nativeColor) >> 0),
                    (Byte)((0x0000FF00 & nativeColor) >> 8),
                    (Byte)((0x00FF0000 & nativeColor) >> 16)
                    );
            }
        }

        public UInt32 ColorSet
        {
            get { return colorSet; }
            set { colorSet = value; }
        }

        // HACK: GetAllColorNames collects the available color names by brute forcing the OS function.
        //   Since there is currently no known way to retrieve all possible color names,
        //   the method below just tries all indices from 0 to 0xFFF ignoring errors.
        public List<String> GetAllColorNames()
        {
            List<String> allColorNames = new List<String>();
            for (UInt32 i = 0; i < 0xFFF; i++)
            {
                IntPtr typeNamePtr = UXTheme.GetImmersiveColorNamedTypeByIndex(i);
                if (typeNamePtr != IntPtr.Zero)
                {
                    IntPtr typeName = (IntPtr)Marshal.PtrToStructure(typeNamePtr, typeof(IntPtr));
                    allColorNames.Add(Marshal.PtrToStringUni(typeName));
                }
            }

            return allColorNames;
        }

        static class UXTheme
        {
            [DllImport("uxtheme.dll", EntryPoint = "#98", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto)]
            public static extern UInt32 GetImmersiveUserColorSetPreference(Boolean forceCheckRegistry, Boolean skipCheckOnFail);

            [DllImport("uxtheme.dll", EntryPoint = "#94", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto)]
            public static extern UInt32 GetImmersiveColorSetCount();

            [DllImport("uxtheme.dll", EntryPoint = "#95", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto)]
            public static extern UInt32 GetImmersiveColorFromColorSetEx(UInt32 immersiveColorSet, UInt32 immersiveColorType,
                Boolean ignoreHighContrast, UInt32 highContrastCacheMode);

            [DllImport("uxtheme.dll", EntryPoint = "#96", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto)]
            public static extern UInt32 GetImmersiveColorTypeFromName(IntPtr name);

            [DllImport("uxtheme.dll", EntryPoint = "#100", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto)]
            public static extern IntPtr GetImmersiveColorNamedTypeByIndex(UInt32 index);
        }
    }
}
