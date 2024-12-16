using System;
using System.ComponentModel;
using System.Linq;

namespace FastGridExportWithSpreadStreamProcessing
{
    public enum Position
    {
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