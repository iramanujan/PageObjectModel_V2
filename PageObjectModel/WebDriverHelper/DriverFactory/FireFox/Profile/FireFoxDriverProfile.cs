using Automation.Common.Config;
using OpenQA.Selenium.Firefox;
using System;

namespace WebDriverHelper.DriverFactory.FireFox.Profile
{
    internal class FireFoxDriverProfile
    {
        private static readonly ToolConfigMember toolConfigMember = ToolConfigReader.GetToolConfig();
        internal static FirefoxProfile CreateProfile()
        {
            FirefoxProfile profile;
            try
            {
                profile = toolConfigMember.ProfileName == null ? new FirefoxProfile() : new FirefoxProfile(toolConfigMember.ProfileName);
                profile.SetPreference("browser.download.folderList", 2);
                profile.SetPreference("browser.download.dir", toolConfigMember.RootDownloadLocation);
                profile.SetPreference("browser.download.manager.alertOnEXEOpen", false);
                profile.SetPreference("browser.helperApps.neverAsk.saveToDisk", "text/plain, application/octet-stream");
                if (toolConfigMember.NoCache)
                {
                    profile.SetPreference("browser.cache.disk.enable", false);
                    profile.SetPreference("browser.cache.memory.enable", false);
                    profile.SetPreference("browser.cache.offline.enable", false);
                }
                profile.SetPreference("intl.accept_languages", toolConfigMember.Localization.ToString());
            }
            catch (Exception)
            {
                profile = new FirefoxProfile();
            }
            return profile;
        }

        internal static FirefoxOptions Options()
        {
            FirefoxOptions firefoxOptions = new FirefoxOptions();
            firefoxOptions.SetPreference(FirefoxDriver.ProfileCapabilityName, FireFoxDriverProfile.CreateProfile().ToBase64String());
            firefoxOptions.ToCapabilities();

            return null;
        }
    }
}
