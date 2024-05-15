using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Office2016InspiredRibbonView_WPF.Appearance
{
    public sealed class Office2016Palette : Freezable
    {
        private struct PropertyNames
        {
            public const string ApplicationButtonBackground = "ApplicationButtonBackground";
            public const string BackstageBackground = "BackstageBackground";
            public const string TabMouseOverForeground = "TabMouseOverForeground";
            public const string TabSelectedForeground = "TabSelectedForeground";
            public const string TabMouseOverBackground = "TabMouseOverBackground";
            public const string TabMouseOverBorderBrush = "TabMouseOverBorderBrush";
            public const string MouseOverBackground = "MouseOverBackground";
            public const string PressedBackground = "PressedBackground";
            public const string MouseOverCheckedBorderBrush = "MouseOverCheckedBorderBrush";
        }
        //Application Button Background
        private static readonly Color defaultWhiteApplicationButtonBackgroundColor = Color.FromArgb(0xFF, 0x2B, 0x57, 0x9A);
        private static readonly Color defaultDarkApplicationButtonBackgroundColor = Colors.Transparent;
        private static readonly Color defaultColorfulApplicationButtonBackgroundColor = Colors.Transparent;

        //Backstage Background
        private static readonly Color defaultWhiteBackstageBackgroundColor = Color.FromArgb(0xFF, 0x2B, 0x57, 0x9A);
        private static readonly Color defaultDarkBackstageBackgroundColor = Color.FromArgb(0xFF, 0x26, 0x26, 0x26);
        private static readonly Color defaultColorfulBackstageBackgroundColor = Color.FromArgb(0xFF, 0x2B, 0x57, 0x9A);

        //QAT and Ribbon Window buttons hover and press background colors:
        private static readonly Color defaultWhiteMouseOverBackgroundColor = Color.FromArgb(0xFF, 0xD5, 0xE1, 0xF2);
        private static readonly Color defaultDarkMouseOverBackgroundColor = Color.FromArgb(0xFF, 0x57, 0x57, 0x57);
        private static readonly Color defaultColorfulMouseOverBackgroundColor = Color.FromArgb(0xFF, 0x3E, 0x6D, 0xB6);

        private static readonly Color defaultWhitePressedBackgroundColor = Color.FromArgb(0xFF, 0xA3, 0xBD, 0xE3);
        private static readonly Color defaultDarkPressedBackgroundColor = Color.FromArgb(0xFF, 0x30, 0x30, 0x30);
        private static readonly Color defaultColorfulPressedBackgroundColor = Color.FromArgb(0xFF, 0x12, 0x40, 0x78);

        //Tab hover background, hover and selected foreground and borderbrush colors
        private static readonly Color defaultWhiteTabMouseOverForegroundColor = Color.FromArgb(0xFF, 0x2B, 0x57, 0x9A);
        private static readonly Color defaultDarkTabMouseOverForegroundColor = Colors.White;
        private static readonly Color defaultColorfulTabMouseOverForegroundColor = Colors.White;

        private static readonly Color defaultWhiteTabSelectedForegroundColor = Color.FromArgb(0xFF, 0x2B, 0x57, 0x9A);
        private static readonly Color defaultDarkTabSelectedForegroundColor = Colors.Black;
        private static readonly Color defaultColorfulTabSelectedForegroundColor = Color.FromArgb(0xFF, 0x2B, 0x57, 0x9A);

        private static readonly Color defaultWhiteTabMouseOverBackgroundColor = Colors.Transparent;
        private static readonly Color defaultDarkTabMouseOverBackgroundColor = Color.FromArgb(0xFF, 0x2B, 0x57, 0x9A);
        private static readonly Color defaultColorfulTabMouseOverBackgroundColor = Color.FromArgb(0xFF, 0x3E, 0x6D, 0xB6);

        private static readonly Color defaultWhiteTabMouseOverBorderBrushColor = Colors.Transparent;
        private static readonly Color defaultDarkTabMouseOverBorderBrushColor = Color.FromArgb(0xFF, 0x2B, 0x57, 0x9A);
        private static readonly Color defaultColorfulTabMouseOverBorderBrushColor = Color.FromArgb(0xFF, 0x3E, 0x6D, 0xB6);

        //Ribbon Buttons MouseOverChecked BorderBrush
        private static readonly Color defaultWhiteMouseOverCheckedBorderBrushColor = Color.FromArgb(0xFF, 0xA3, 0xBD, 0xE3);
        private static readonly Color defaultDarkMouseOverCheckedBorderBrushColor = Color.FromArgb(0xFF, 0x6A, 0x6A, 0x6A);
        private static readonly Color defaultColorfulMouseOverCheckedBorderBrushColor = Color.FromArgb(0xFF, 0x96, 0x96, 0x96);

        private static readonly Office2016Palette palette = new Office2016Palette();

        static Office2016Palette()
        {
            Initialize();
        }

        public static Office2016Palette Palette
        {
            get
            {
                return Office2016Palette.palette;
            }
        }

        internal static Color WhiteApplicationButtonBackgroundColor
        {
            get
            {
                return defaultWhiteApplicationButtonBackgroundColor;
            }
        }

        internal static Color DarkApplicationButtonBackgroundColor
        {
            get
            {
                return defaultDarkApplicationButtonBackgroundColor;
            }
        }

        internal static Color ColorfulApplicationButtonBackgroundColor
        {
            get
            {
                return defaultColorfulApplicationButtonBackgroundColor;
            }
        }

        internal static Color WhiteBackstageBackgroundColor
        {
            get
            {
                return defaultWhiteBackstageBackgroundColor;
            }
        }

        internal static Color DarkBackstageBackgroundColor
        {
            get
            {
                return defaultDarkBackstageBackgroundColor;
            }
        }

        internal static Color ColorfulBackstageBackgroundColor
        {
            get
            {
                return defaultColorfulBackstageBackgroundColor;
            }
        }

        internal static Color WhiteMouseOverBackgroundColor
        {
            get
            {
                return defaultWhiteMouseOverBackgroundColor;
            }
        }

        internal static Color DarkMouseOverBackgroundColor
        {
            get
            {
                return defaultDarkMouseOverBackgroundColor;
            }
        }

        internal static Color ColorfulMouseOverBackgroundColor
        {
            get
            {
                return defaultColorfulMouseOverBackgroundColor;
            }
        }

        internal static Color WhitePressedBackgroundColor
        {
            get
            {
                return defaultWhitePressedBackgroundColor;
            }
        }

        internal static Color DarkPressedBackgroundColor
        {
            get
            {
                return defaultDarkPressedBackgroundColor;
            }
        }

        internal static Color ColorfulPressedBackgroundColor
        {
            get
            {
                return defaultColorfulPressedBackgroundColor;
            }
        }

        internal static Color WhiteTabMouseOverForegroundColor
        {
            get
            {
                return defaultWhiteTabMouseOverForegroundColor;
            }
        }

        internal static Color DarkTabMouseOverForegroundColor
        {
            get
            {
                return defaultDarkTabMouseOverForegroundColor;
            }
        }

        internal static Color ColorfulTabMouseOverForegroundColor
        {
            get
            {
                return defaultColorfulTabMouseOverForegroundColor;
            }
        }

        internal static Color WhiteTabSelectedForegroundColor
        {
            get
            {
                return defaultWhiteTabSelectedForegroundColor;
            }
        }

        internal static Color DarkTabSelectedForegroundColor
        {
            get
            {
                return defaultDarkTabSelectedForegroundColor;
            }
        }

        internal static Color ColorfulTabSelectedForegroundColor
        {
            get
            {
                return defaultColorfulTabSelectedForegroundColor;
            }
        }

        internal static Color WhiteTabMouseOverBackgroundColor
        {
            get
            {
                return defaultWhiteTabMouseOverBackgroundColor;
            }
        }

        internal static Color DarkTabMouseOverBackgroundColor
        {
            get
            {
                return defaultDarkTabMouseOverBackgroundColor;
            }
        }

        internal static Color ColorfulTabMouseOverBackgroundColor
        {
            get
            {
                return defaultColorfulTabMouseOverBackgroundColor;
            }
        }

        internal static Color WhiteTabMouseOverBorderBrushColor
        {
            get
            {
                return defaultWhiteTabMouseOverBorderBrushColor;
            }
        }

        internal static Color DarkTabMouseOverBorderBrushColor
        {
            get
            {
                return defaultDarkTabMouseOverBorderBrushColor;
            }
        }

        internal static Color ColorfulTabMouseOverBorderBrushColor
        {
            get
            {
                return defaultColorfulTabMouseOverBorderBrushColor;
            }
        }


        internal static Color WhiteMouseOverCheckedBorderBrushColor
        {
            get
            {
                return defaultWhiteMouseOverCheckedBorderBrushColor;
            }
        }

        internal static Color DarkMouseOverCheckedBorderBrushColor
        {
            get
            {
                return defaultDarkMouseOverCheckedBorderBrushColor;
            }
        }

        internal static Color ColorfulMouseOverCheckedBorderBrushColor
        {
            get
            {
                return defaultColorfulMouseOverCheckedBorderBrushColor;
            }
        }

        public static readonly DependencyProperty ApplicationButtonBackgroundProperty =
        DependencyProperty.Register("ApplicationButtonBackground", typeof(Color), typeof(Office2016Palette), new PropertyMetadata());

        public static readonly DependencyProperty BackstageBackgroundProperty =
         DependencyProperty.Register("BackstageBackground", typeof(Color), typeof(Office2016Palette), new PropertyMetadata());

        public static readonly DependencyProperty TabMouseOverForegroundProperty =
         DependencyProperty.Register("TabMouseOverForeground", typeof(Color), typeof(Office2016Palette), new PropertyMetadata());

        public static readonly DependencyProperty TabSelectedForegroundProperty =
       DependencyProperty.Register("TabSelectedForeground", typeof(Color), typeof(Office2016Palette), new PropertyMetadata());

        public static readonly DependencyProperty MouseOverBackgroundProperty =
          DependencyProperty.Register("MouseOverBackground", typeof(Color), typeof(Office2016Palette), new PropertyMetadata());

        public static readonly DependencyProperty PressedBackgroundProperty =
         DependencyProperty.Register("PressedBackground", typeof(Color), typeof(Office2016Palette), new PropertyMetadata());

        public static readonly DependencyProperty TabMouseOverBackgroundProperty =
         DependencyProperty.Register("TabMouseOverBackground", typeof(Color), typeof(Office2016Palette), new PropertyMetadata());

        public static readonly DependencyProperty TabMouseOverBorderBrushProperty =
         DependencyProperty.Register("TabMouseOverBorderBrush", typeof(Color), typeof(Office2016Palette), new PropertyMetadata());

        public static readonly DependencyProperty MouseOverCheckedBorderBrushProperty =
        DependencyProperty.Register("MouseOverCheckedBorderBrush", typeof(Color), typeof(Office2016Palette), new PropertyMetadata());

        public Color ApplicationButtonBackground
        {
            get { return (Color)GetValue(ApplicationButtonBackgroundProperty); }
            set { SetValue(ApplicationButtonBackgroundProperty, value); }
        }

        public Color BackstageBackground
        {
            get { return (Color)GetValue(BackstageBackgroundProperty); }
            set { SetValue(BackstageBackgroundProperty, value); }
        }

        public Color MouseOverBackground
        {
            get { return (Color)GetValue(MouseOverBackgroundProperty); }
            set { SetValue(MouseOverBackgroundProperty, value); }
        }

        public Color PressedBackground
        {
            get { return (Color)GetValue(PressedBackgroundProperty); }
            set { SetValue(PressedBackgroundProperty, value); }
        }

        public Color TabMouseOverForeground
        {
            get { return (Color)GetValue(TabMouseOverForegroundProperty); }
            set { SetValue(TabMouseOverForegroundProperty, value); }
        }

        public Color TabSelectedForeground
        {
            get { return (Color)GetValue(TabSelectedForegroundProperty); }
            set { SetValue(TabSelectedForegroundProperty, value); }
        }

        public Color TabMouseOverBackground
        {
            get { return (Color)GetValue(TabMouseOverBackgroundProperty); }
            set { SetValue(TabMouseOverBackgroundProperty, value); }
        }

        public Color TabMouseOverBorderBrush
        {
            get { return (Color)GetValue(TabMouseOverBorderBrushProperty); }
            set { SetValue(TabMouseOverBorderBrushProperty, value); }
        }

        public Color MouseOverCheckedBorderBrush
        {
            get { return (Color)GetValue(MouseOverCheckedBorderBrushProperty); }
            set { SetValue(MouseOverCheckedBorderBrushProperty, value); }
        }

        private static void Initialize()
        {
            palette.ApplicationButtonBackground = defaultColorfulApplicationButtonBackgroundColor;
            palette.BackstageBackground = defaultColorfulBackstageBackgroundColor;
            palette.TabMouseOverForeground = defaultColorfulTabMouseOverForegroundColor;
            palette.TabSelectedForeground = defaultColorfulTabSelectedForegroundColor;
            palette.MouseOverBackground = defaultColorfulMouseOverBackgroundColor;
            palette.PressedBackground = defaultColorfulPressedBackgroundColor;
            palette.TabMouseOverBackground = defaultColorfulTabMouseOverBackgroundColor;
            palette.TabMouseOverBorderBrush = defaultColorfulTabMouseOverBorderBrushColor;
            palette.MouseOverCheckedBorderBrush = defaultColorfulMouseOverCheckedBorderBrushColor;
        }

        internal static bool TryGetResource(Resources key, out string resource)
        {
            bool containsResource = false;
            switch (key)
            {
                case Resources.ApplicationButtonBackground:
                    resource = PropertyNames.ApplicationButtonBackground;
                    containsResource = true;
                    break;
                case Resources.BackstageBackground:
                    resource = PropertyNames.BackstageBackground;
                    containsResource = true;
                    break;
                case Resources.MouseOverBackground:
                    resource = PropertyNames.MouseOverBackground;
                    containsResource = true;
                    break;
                case Resources.PressedBackground:
                    resource = PropertyNames.PressedBackground;
                    containsResource = true;
                    break;
                case Resources.TabMouseOverForeground:
                    resource = PropertyNames.TabMouseOverForeground;
                    containsResource = true;
                    break;
                case Resources.TabSelectedForeground:
                    resource = PropertyNames.TabSelectedForeground;
                    containsResource = true;
                    break;
                case Resources.TabMouseOverBackground:
                    resource = PropertyNames.TabMouseOverBackground;
                    containsResource = true;
                    break;
                case Resources.TabMouseOverBorderBrush:
                    resource = PropertyNames.TabMouseOverBorderBrush;
                    containsResource = true;
                    break;
                case Resources.MouseOverCheckedBorderBrush:
                    resource = PropertyNames.MouseOverCheckedBorderBrush;
                    containsResource = true;
                    break;
                default:
                    resource = string.Empty;
                    break;
            }

            return containsResource;
        }

        protected override Freezable CreateInstanceCore()
        {
            throw new NotImplementedException();
        }
    }
}
