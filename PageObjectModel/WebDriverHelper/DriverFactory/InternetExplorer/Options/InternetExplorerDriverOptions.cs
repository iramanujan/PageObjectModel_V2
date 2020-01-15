using OpenQA.Selenium.IE;

namespace WebDriverHelper.DriverFactory.InternetExplorer.Options
{
    class InternetExplorerDriverOptions
    {
        public static InternetExplorerOptions GetChromeOptionsInternetExplorerOptions()
        {
            InternetExplorerOptions options = new InternetExplorerOptions();
            options.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
            options.ElementScrollBehavior = InternetExplorerElementScrollBehavior.Bottom;

            options.IgnoreZoomLevel = true;
            options.RequireWindowFocus = true;
            options.EnsureCleanSession = true;
            options.EnableNativeEvents = false;

            options.AddAdditionalCapability("InternetExplorerDriver.FORCE_CREATE_PROCESS", true);
            return options;
        }
    }
}
