using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Windows.Cloud;
using Telerik.Everlive.Sdk.Core;
using System.IO;
using System.Threading;
using Telerik.Everlive.Sdk.Core.Handlers.Data;
using Telerik.Everlive.Sdk.Core.Query.Definition.FormData;
using Telerik.Everlive.Sdk.Core.Result;
using Telerik.Everlive.Sdk.Core.Model.Result;

namespace CustomTelerikBackendServicesProvider
{
    public static class TelerikBackendServicesUtilities
    {
        internal static IEnumerable<string> ListFiles(string apiKey)
        {
            EverliveAppSettings settings = new EverliveAppSettings();
            settings.ServiceUrl = "api.everlive.com";
            settings.ApiKey = apiKey;
            var application = new EverliveApp(settings);
            AppHandler currentAppHandler = application.WorkWith();
            var files = currentAppHandler.Files().GetAll().ExecuteSync();
            return files.Select(f => f.Filename);
        }
    }
}
