using System;

namespace Office2016InspiredRibbonView_WPF.Appearance
{
    public class AppearanceChangedEventArgs : EventArgs
    {
        public Office2016ColorVariations ColorVariation { get; private set; }

        public AppearanceChangedEventArgs(Office2016ColorVariations colorVariation)
        {
            this.ColorVariation = colorVariation;
        }
    }
}
