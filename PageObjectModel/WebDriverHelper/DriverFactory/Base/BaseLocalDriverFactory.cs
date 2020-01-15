using Automation.Common.Config;
using Automation.Common.Location.Download;
using Automation.Common.Location.Upload;
using System;

namespace WebDriverHelper.DriverFactory.Base
{
    public class BaseLocalDriverFactory
    {
        protected static readonly ToolConfigMember toolConfigMember = ToolConfigReader.GetToolConfig();
        protected Lazy<string> downloadLocation = new Lazy<string>(() => DownloadLocation.CreateWebDriverDirectory(toolConfigMember.Browser.ToString() + toolConfigMember.ExecutionType.ToString(), toolConfigMember.RootDownloadLocation));
        protected Lazy<UploadLocation> uploadLocation = new Lazy<UploadLocation>(() => UploadLocation.Create(toolConfigMember.Browser.ToString() + toolConfigMember.ExecutionType.ToString(), true, toolConfigMember.RootUploadLocation));
    }
}
