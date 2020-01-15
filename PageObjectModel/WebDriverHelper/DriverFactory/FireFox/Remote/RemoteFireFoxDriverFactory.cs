using Automation.Common.Location.Download;
using Automation.Common.Location.Upload;
using Automation.Common.Log;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using WebDriverHelper.DriverFactory.Base;
using WebDriverHelper.DriverFactory.Chrome.Options;
using WebDriverHelper.Grid;
using WebDriverHelper.Interfaces.DriverFactory;

namespace WebDriverHelper.DriverFactory.FireFox.Remote
{
    public class RemoteFireFoxDriverFactory : BaseRemoteDriverFactory, IWebDriverFactory
    {
        private IWebDriver webDriver = null;

        protected override ICapabilities Capabilities => ChromeDriverOptions.CreateDefaultChromeOptions().ToCapabilities();

        private void BeforeWebDriverSetupSetps()
        {
            downloadLocation = new Lazy<string>(() => DownloadLocation.CreateWebDriverDirectory(toolConfigMember.Browser.ToString() + toolConfigMember.ExecutionType.ToString(), toolConfigMember.RootDownloadLocation));
            uploadLocation = new Lazy<UploadLocation>(() => UploadLocation.Create(toolConfigMember.Browser.ToString() + toolConfigMember.ExecutionType.ToString(), true, toolConfigMember.RootUploadLocation));
        }

        public IWebDriver InitializeWebDriver()
        {
            BeforeWebDriverSetupSetps();
            Logger.LogExecute($"ATTEMPT TO CREATE REMOTE {browserName.ToUpper()} DRIVER");
            var remoteWebDriver = new RemoteWebDriver(new Uri(gridUrl), Capabilities, TimeSpan.FromMilliseconds(commandTimeout));
            Logger.LogExecute($"CREATED REMOTE {browserName.ToUpper()} DRIVER ON HOST {GridConfigHelper.GetRemoteDriverHostName(remoteWebDriver, gridHost)}");
            webDriver = remoteWebDriver;
            return webDriver;
        }
    }
}
