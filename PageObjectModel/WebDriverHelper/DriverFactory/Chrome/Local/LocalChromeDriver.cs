
using Automation.Common.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using WebDriverHelper.DriverFactory.Base;
using WebDriverHelper.DriverFactory.Chrome.Options;
using WebDriverHelper.Interfaces.DriverFactory;

namespace WebDriverHelper.DriverFactory.Chrome.Local
{
    public class LocalChromeDriver : BaseLocalDriverFactory, IWebDriverFactory
    {
        private IWebDriver webDriver = null;
        private ChromeOptions chromeOptions = null;
        private ChromeDriverService chromeDriverService = null;

        private void BeforeWebDriverSetupSetps()
        {
            this.chromeOptions = ChromeDriverOptions.CreateDefaultChromeOptions();
            this.chromeDriverService = ChromeDriverService.CreateDefaultService(FileUtils.GetCurrentlyExecutingDirectory());
        }

        public IWebDriver InitializeWebDriver()
        {
            BeforeWebDriverSetupSetps();
            webDriver = new ChromeDriver(chromeDriverService, chromeOptions, TimeSpan.FromSeconds(toolConfigMember.CommandTimeout));
            return this.webDriver;
        }
    }
}
