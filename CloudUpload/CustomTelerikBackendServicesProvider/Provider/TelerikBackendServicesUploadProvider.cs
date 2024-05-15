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
    // Everlive sdk downloaded from: https://platform.telerik.com/#downloads/backendservices
    public class TelerikBackendServicesUploadProvider : ICloudUploadProvider
    {
        private EverliveApp application;

        public TelerikBackendServicesUploadProvider(string apiKey, bool useHttps = false)
        {
            EverliveAppSettings settings = new EverliveAppSettings();
            settings.ServiceUrl = "api.everlive.com";
            settings.ApiKey = apiKey;
            settings.UseHttps = useHttps;
            this.application = new EverliveApp(settings);
        }

        public System.Threading.Tasks.Task<object> UploadFileAsync(string fileName, Stream fileStream, CloudUploadFileProgressChanged uploadProgressChanged, CancellationToken cancellationToken)
        {
            Func<object> f = () =>
            {
                long fileSize = fileStream.Length;
                if (uploadProgressChanged != null)
                {
                    uploadProgressChanged(fileSize / 10);
                }
                if (cancellationToken != CancellationToken.None && cancellationToken.IsCancellationRequested)
                {
                    return null;
                }

                object guid = UploadFileSync(fileName, fileStream);
                
                if (uploadProgressChanged != null)
                {
                    uploadProgressChanged(fileSize);
                }

                return guid;
            };

            fileStream.Position = 0;
            return System.Threading.Tasks.Task.Factory.StartNew(f, cancellationToken);
        }

        private object UploadFileSync(string fileName, Stream fileStream)
        {
            AppHandler currentAppHandler = this.application.WorkWith();
            FileField fileField = new FileField("file", fileName, "image/jpg", fileStream);
            CreateResultItem item = currentAppHandler.Files().Upload(fileField).ExecuteSync();

            return item.Id;
        }
    }
}
