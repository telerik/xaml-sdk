using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlagEnumEditor
{
    [Flags]
    public enum Permissions
    {
        All = -1,
        None = 0,
        Read = 1,
        Write = 2,
        Execute = 4
    }
}
