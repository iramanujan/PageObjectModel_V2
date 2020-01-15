using Automation.Common.Config;
using Automation.Common.Location.Upload;
using OpenQA.Selenium;
using System;

namespace WebDriverHelper.DriverFactory.Base
{
    public abstract class BaseRemoteDriverFactory
    {
        protected abstract ICapabilities Capabilities { get; }

        protected static readonly ToolConfigMember toolConfigMember = ToolConfigReader.GetToolConfig();
        protected Lazy<string> downloadLocation;
        protected Lazy<UploadLocation> uploadLocation;

        protected string browserName = toolConfigMember.Browser.ToString();
        protected string gridUrl = toolConfigMember.GridUrl.ToString();
        protected int commandTimeout = toolConfigMember.CommandTimeout;
        protected string gridHost = toolConfigMember.GridHost.ToString();

    }
}
