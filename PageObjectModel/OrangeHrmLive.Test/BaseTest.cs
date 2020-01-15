using Automation.Common.Config;
using Automation.Common.Utils;
using NUnit.Framework;
using OpenQA.Selenium;
using OrangeHrmLive.Page.Context;
using System;
using static Automation.Common.Config.ToolConfigMember;

namespace OrangeHrmLive.Test
{
    public class BaseTest: WebDriverScreenshotable
    {
        public static readonly ToolConfigMember toolConfigMember = ToolConfigReader.GetToolConfig();
        protected TestHarnessContext myContext { get; } = TestHarnessContextHelper.CreateDefault();

        public override ITakesScreenshot TakesScreenshot => myContext.Browser;
        
        protected void ExecuteSafely(Action steps)
        {
            StepsExecutor.ExecuteSafely(steps);
        }

        [SetUp]
        public void BaseTestOneTimeSetUp()
        {
            var browser = myContext.Browser;
            browser.Open(toolConfigMember.PageUrls);
        }
        [TearDown]
        public void BaseTestOneTimeTearDown()
        {
            //Cleanup upload and download directories and close the browser after test class is completed.
            ExecuteSafely(myContext.Browser.CleanupCreatedDirectoriesSafely);
            ExecuteSafely(myContext.Browser.Quit);
            switch (ToolConfigReader.GetToolConfig().Browser)
            {
                case BrowserType.IE:
                    //Killing IE driver process if exists
                    ProcessUtils.KillProcesses("iexplore");
                    ProcessUtils.KillProcesses("IEDriverServer");
                    break;
                case BrowserType.Chrome:
                    ProcessUtils.KillProcesses("chrome");
                    ProcessUtils.KillProcesses("chromedriver");
                    ProcessUtils.KillProcesses("chrome.exe");
                    ProcessUtils.KillProcesses("chromedriver.exe");
                    break;
                case BrowserType.Firefox:
                    ProcessUtils.KillProcesses("firefox.exe");
                    ProcessUtils.KillProcesses("geckodriver.exe");
                    ProcessUtils.KillProcesses("firefox");
                    ProcessUtils.KillProcesses("geckodriver");
                    break;
            }
        }
    }
}
