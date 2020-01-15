using Automation.Common.Config;
using OpenQA.Selenium.Chrome;

namespace WebDriverHelper.DriverFactory.Chrome.Options
{
    class ChromeDriverOptions
    {
        private static readonly ToolConfigMember toolConfigMember = ToolConfigReader.GetToolConfig();
        public static ChromeOptions CreateDefaultChromeOptions()
        {
            var options = new ChromeOptions();

            options.AddUserProfilePreference("safebrowsing.enabled", true);
            options.AddUserProfilePreference("download.default_directory", toolConfigMember.RootDownloadLocation);

            options.AddArguments("--test-type");
            options.AddArguments("--no-sandbox");
            options.AddArgument("--start-maximized");
            options.AddArgument("--ignore-certificate-errors");
            options.AddArgument("--disable-popup-blocking");
            options.AddArgument("--incognito");
            options.AddArgument("--enable-precise-memory-info");
            options.AddArgument("--disable-default-apps");
            options.AddArgument("test-type=browser");
            options.AddArgument("disable-infobars");

            if (ToolConfigReader.GetToolConfig().NoCache)
            {
                options.AddArguments("--incognito");
            }

            options.AddArguments($"--lang={toolConfigMember.Localization.ToString()}");

            return options;
        }
    }
}
