using System;
using System.ComponentModel;
using System.Linq;

namespace FastGridExportWithSpreadStreamProcessing
{
    public enum Position
    {
        /// <summary>
        /// In Silverlight, you can use the DisplayAttribute.ShortName data annotation as well.
        /// </summary>
        [Description("Goalkeeper")]
        GK,

        [Description("Defender")]
        DF,

        [Description("Midfield")]
        MF,

        [Description("Forward")]
        FW
    }
}