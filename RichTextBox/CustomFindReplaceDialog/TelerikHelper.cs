using System;
using System.Collections.Generic;
using Telerik.Windows.Controls;
using Telerik.Windows.Documents.Model;
using System.Windows;
using System.Windows.Input;
using System.Text;
using Telerik.Windows.Documents.Layout;

namespace Telerik.Windows.Documents
{
    internal static class TelerikHelper
    {

        internal static string CleanUpNewLines(string str)
        {
            StringBuilder result = new StringBuilder(str.Length);
            bool hasSeenCr = false;

            for (int i = 0, l = str.Length; i < l; ++i)
            {
                if (str[i] == '\r')
                {
                    if (hasSeenCr)
                    {
                        result.Append(DocumentEnvironment.NewLine);
                    }

                    hasSeenCr = true;
                }
                else if (str[i] == '\n')
                {
                    result.Append(DocumentEnvironment.NewLine);
                    hasSeenCr = false;
                }
                else
                {
                    if (hasSeenCr)
                    {
                        result.Append(DocumentEnvironment.NewLine);
                        hasSeenCr = false;
                    }

                    result.Append(str[i]);
                }
            }

            if (hasSeenCr)
            {
                result.Append(DocumentEnvironment.NewLine);
            }

            return result.ToString();
        }

    }
}
