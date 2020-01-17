using Automation.Common.Config;
using Automation.Common.Log;
using Automation.Common.Utils;
using OpenQA.Selenium;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using WebDriverHelper.Interfaces.DriverFactory;
using WebDriverHelper.JScript;
using WebDriverHelper.Report;
using static Automation.Common.Config.ToolConfigMember;

namespace WebDriverHelper.BrowserFactory
{
    public class Browser : JavaScript, ITakesScreenshot, IWebDriver
    {
        protected new static readonly ToolConfigMember toolConfigMember = ToolConfigReader.GetToolConfig();
        private readonly IWebDriverFactory webDriverFactory;
        public IWebDriver objWebDriver;
        public IWebDriver webDriver => objWebDriver ?? (objWebDriver = webDriverFactory.InitializeWebDriver());

        #region Constructor
        public Browser(IWebDriverFactory webDriverFactory)
        {
            CreateUploadDwonloadDirectory();
            this.webDriverFactory = webDriverFactory;
            new ExtentReportsUtils().ExtentReportsSetup();
        }

        public Browser(IWebDriver webDriver)
        {
            this.objWebDriver = webDriver;
        }

        public Browser()
        {

        }
        
        
        
        #endregion

        #region Functions

        public IWebDriver GetWebDriverObject()
        {
            return this.webDriver;
        }

        public Screenshot GetScreenshot()
        {
            return ((ITakesScreenshot)webDriver).GetScreenshot();
        }

        public Browser InitBrowser(string url)
        {
            webDriver.Navigate().GoToUrl(url);
            WaitTillPageLoad(webDriver);
            webDriver.SwitchTo().DefaultContent();
            return this;
        }

        public Browser OpenUrl(string url)
        {
            this.webDriver.Navigate().GoToUrl(url);
            return this;
        }

        public Browser Maximize()
        {
            this.webDriver.Manage().Window.Maximize();
            return this;
        }
        
        public Browser Forward()
        {
            this.webDriver.Navigate().Forward();
            return this;
        }

        public Browser ClearCookies()
        {
            this.webDriver.Manage().Cookies.DeleteAllCookies();
            return this;
        }

        public Browser Back()
        {
            this.webDriver.Navigate().Back();
            return this;
        }

        public Browser Refresh()
        {
            webDriver.Navigate().Refresh();
            WaitTillPageLoad(webDriver);
            return this;
        }

        public void CloseCurrentTab()
        {
            this.webDriver.Close();
        }

        public string GetUrl()
        {
            return this.webDriver.Url;
        }
        public string GetDownloadPath()
        {
            return toolConfigMember.RootDownloadLocation;
        }

        public string GetUploadPath()
        {
            return toolConfigMember.RootUploadLocation;
        }

        public ReadOnlyCollection<string> GetWindowHandles()
        {
            return this.webDriver.WindowHandles;
        }

        public void SwitchToWindowHandle(string windowHandle)
        {
            webDriver.SwitchTo().Window(windowHandle);
        }

        public void SwitchHandleToNewWindowByPartialUrl(string urlPart)
        {

            if (GetDecodedUrl().Contains(urlPart)) { return; }
            ReadOnlyCollection<string> handles = webDriver.WindowHandles;
            foreach (string handle in handles.Reverse())
            {
                webDriver.SwitchTo().Window(handle);
                if (GetDecodedUrl().Contains(urlPart))
                {
                    WaitTillPageLoad(webDriver);
                    WaitTillAjaxLoad(webDriver);
                    return;
                }
            }
        }

        public string GetDecodedUrl()
        {
            var url = webDriver.Url;
            return HttpUtility.UrlDecode(url);
        }

        public void Close()
        {
            this.webDriver.Close();
        }

        public void Dispose()
        {
            Quit();
        }

        public void Quit()
        {
            if (this.webDriver != null)
            {
                try
                {
                    foreach (var window in this.webDriver.WindowHandles)
                    {
                        SwitchToWindowHandle(window);
                        Close();
                    }
                    this.webDriver.Quit();
                }

                catch (Exception ex)
                {
                    Logger.Error($"Unable to Quit the browser. Reason: {ex.Message}");
                    switch (toolConfigMember.Browser)
                    {
                        case BrowserType.IE:

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

                finally
                {
                    this.objWebDriver = null;
                }
            }
        }

        public IOptions Manage()
        {
            return this.webDriver.Manage();
        }

        public INavigation Navigate()
        {
            return this.webDriver.Navigate();
        }

        public ITargetLocator SwitchTo()
        {
            return this.webDriver.SwitchTo();
        }

        public IWebElement FindElement(By by)
        {
            return this.webDriver.FindElement(by);
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return this.webDriver.FindElements(by);
        }

        public IWebElement GetElementIfExists(By by)
        {
            return this.webDriver.FindElements(by).FirstOrDefault();
        }

        public void CleanupCreatedDirectoriesSafely()
        {
            StepsExecutor.ExecuteSafely(() => Directory.Delete(GetDownloadPath(), true));
            StepsExecutor.ExecuteSafely(() => Directory.Delete(GetUploadPath(), true));
        }

        public void CreateUploadDwonloadDirectory()
        {
            StepsExecutor.ExecuteSafely(() => Directory.CreateDirectory(GetDownloadPath()));
            StepsExecutor.ExecuteSafely(() => Directory.CreateDirectory(GetUploadPath()));
        }

        #endregion

        #region Properties
        public string Url
        {
            get { return this.webDriver.Url; }
            set { this.webDriver.Url = value; }
        }

        public string Title
        {
            get { return this.webDriver.Title; }
        }

        public string PageSource
        {
            get { return this.webDriver.PageSource; }
        }

        public string CurrentWindowHandle
        {
            get { return this.webDriver.CurrentWindowHandle; }
        }

        public ReadOnlyCollection<string> WindowHandles
        {
            get { return this.webDriver.WindowHandles; }
        }
        #endregion

    }
}


