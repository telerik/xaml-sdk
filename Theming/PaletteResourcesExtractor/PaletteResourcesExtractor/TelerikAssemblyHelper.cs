using System;
using System.Linq;
using System.Reflection;

namespace PaletteResourcesExtractor
{
    public static class TelerikAssemblyHelper
    {
        private static readonly DateTime minimumSupportedVersionDate = new DateTime(2023, 7, 3);
        private static Assembly controlsAssembly;

        public static Assembly GetTelerikAssemblyCache()
        {
            if (controlsAssembly != null)
            {
                return controlsAssembly;
            }

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly assembly in assemblies)
            {
                string name = assembly.GetName().Name;
                if (name.Equals("Telerik.Windows.Controls"))
                {
                    controlsAssembly = assembly;
                    return assembly;
                }
            }
            return null;
        }

        // The Telerik.Windows.Controls.dll version that support this tool is 2023.2.703 and later.
        public static bool CheckIfSupported()
        {
            Assembly telerikAssembly = GetTelerikAssemblyCache();
            var fileVersionAttribute = telerikAssembly.GetCustomAttributes(false).OfType<AssemblyFileVersionAttribute>().FirstOrDefault();            
            DateTime versionDate = GetDateByFileVersion(fileVersionAttribute);
            return versionDate >= minimumSupportedVersionDate;
        }

        private static DateTime GetDateByFileVersion(AssemblyFileVersionAttribute fileVersion)
        {
            string[] versionParts = fileVersion.Version.Split('.');
            int year = int.Parse(versionParts[0]);            
            string dateNumber = versionParts[2];
            int month = -1;
            int day = -1;
            if (dateNumber.Length == 3)
            {
                month = int.Parse(dateNumber[0].ToString());
                day = int.Parse(dateNumber.Substring(1, 2));
            }
            else
            {
                month = int.Parse(dateNumber.Substring(0, 2));
                day = int.Parse(dateNumber.Substring(2, 2));
            }

            return new DateTime(year, month, day);
        }
    }
}
