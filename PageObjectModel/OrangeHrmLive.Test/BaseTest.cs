using Automation.Common.Config;

using Automation.Common.Utils;
using AventStack.ExtentReports;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OrangeHrmLive.Page.Context;
using System;
using WebDriverHelper.Report;
using static Automation.Common.Config.ToolConfigMember;

namespace OrangeHrmLive.Test
{
    public class BaseTest: WebDriverScreenshotable
    {
        public new static readonly ToolConfigMember toolConfigMember = ToolConfigReader.GetToolConfig();
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
            browser.InitBrowser(toolConfigMember.PageUrls);
        }

        [TearDown]
        public void BaseTestOneTimeTearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Passed)
            {
                ExtentReportsUtils.test.Pass("Test Case {"+TestContext.CurrentContext.Test.Name+ "} is Passed Successfully.", ExtentReportsUtils.GetMediaEntityModelProvider());
            }
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                ExtentReportsUtils.test.Fail("Test Case {" + TestContext.CurrentContext.Test.Name + "} is Failed.", ExtentReportsUtils.GetMediaEntityModelProvider());
            }
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Warning)
            {
                ExtentReportsUtils.test.Warning("Test Case {" + TestContext.CurrentContext.Test.Name + "} is showing Warning.", ExtentReportsUtils.GetMediaEntityModelProvider());
            }
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Skipped)
            {
                ExtentReportsUtils.test.Skip("Test Case {" + TestContext.CurrentContext.Test.Name + "} is Skipped", ExtentReportsUtils.GetMediaEntityModelProvider());
            }
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

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            ExecuteSafely(myContext.Browser.CleanupCreatedDirectoriesSafely);
            ExtentReportsUtils.ExtentReportsTearDown();
        }

    }
}
