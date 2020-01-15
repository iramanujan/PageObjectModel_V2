using Automation.Common.Config;
using Automation.Common.Location.Upload;
using Automation.Common.Log;
using Automation.Common.Report;
using Automation.Common.Utils;
using Automation.Common.Wait;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using WebDriverHelper.Interfaces.DriverFactory;
using WebDriverHelper.JScript;
using static Automation.Common.Config.ToolConfigMember;

namespace WebDriverHelper.BrowserFactory
{
    public class Browser: IWebDriver, ITakesScreenshot, IJavaScriptExecutor
    {
        protected static readonly ToolConfigMember toolConfigMember = ToolConfigReader.GetToolConfig();
        private readonly IWebDriverFactory webDriverFactory;
        public IWebDriver objWebDriver;

        public IWebDriver webDriver => objWebDriver ?? (objWebDriver = webDriverFactory.InitializeWebDriver());

        public Browser(IWebDriverFactory webDriverFactory)
        {
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

        public void GetDownloadLocation()
        {
            toolConfigMember.RootDownloadLocation.ToString();
        }

        public void GetUploadLocation()
        {
            toolConfigMember.RootUploadLocation.ToString();
        }

        public Browser Back()
        {
            webDriver.Navigate().Back();
            return this;
        }

        public Browser Forward()
        {
            webDriver.Navigate().Forward();
            return this;
        }

        public Browser Refresh()
        {
            webDriver.Navigate().Refresh();
            JavaScript.ExecuteScript(JScriptType.PageLoad, this.webDriver);
            return this;
        }
        public void ExecuteInFrame(By bySelector, Action action)
        {
            new WebDriverWait(webDriver, TimeSpan.FromMilliseconds(ToolConfigReader.GetToolConfig().ObjectWait))
                .Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(bySelector));

            ExecuteInFrame(action);
        }

        private void ExecuteInFrame(Action action)
        {
            try
            {
                action();
            }
            finally
            {
                SwitchTo().DefaultContent();
            }
        }

        public IWebElement GetElementIfExists(By by)
        {
            return webDriver.FindElements(by).FirstOrDefault();
        }


        public void ExecuteInFrame(int frameIndex, Action action)
        {
            SwitchTo().Frame(frameIndex);

            ExecuteInFrame(action);
        }

        public void DoWithResizedToZeroWindowForIE(Action action)
        {
            if (ToolConfigReader.GetToolConfig().Browser == ToolConfigMember.BrowserType.IE)
            {
                this.Manage().Window.Size = new System.Drawing.Size(0, 0);
                try
                {
                    action();
                }
                finally
                {
                    this.Maximize();
                }
            }
            else
            {
                action();
            }
        }


        public Browser Maximize()
        {
            this.webDriver.Manage().Window.Maximize();
            return this;
        }
        public void CleanupCreatedDirectoriesSafely()
        {
            if (toolConfigMember.Browser != ToolConfigMember.BrowserType.IE)
            {
                StepsExecutor.ExecuteSafely(() => Directory.Delete(GetDownloadPath(), true));
            }
            StepsExecutor.ExecuteSafely(() => Directory.Delete(GetUploadPath(), true));
        }

        public string GetUploadPath()
        {

            return toolConfigMember.RootUploadLocation;
        }
        public string GetDownloadPath()
        {
            
            return toolConfigMember.RootDownloadLocation;
        }

        public object ExecuteAsyncScript(string script, params object[] args)
        {
            try
            {
                return ((IJavaScriptExecutor)objWebDriver).ExecuteAsyncScript(script, args);
            }
            catch (WebDriverTimeoutException)
            {
                // If timeout or any errors
                Logger.Debug(String.Format("Error: Exception thrown while running JS Script:{0}{1}", Environment.NewLine, script));
                throw;
            }
        }


        // </summary>
        public bool WaitTillPageLoad()
        {
            return WaitTillPageLoad(toolConfigMember.ObjectWait / 1000);
        }

        /// <summary>
        /// Scroll whole page to Top
        /// </summary>
        public void ScrollTop()
        {
            Logger.LogExecute("Scroll page top");
            ExecuteScript("$(window).scrollTop(0)");
            WaitTillAjaxLoad();
        }

        /// <summary>
        /// Scroll whole page to Bottom
        /// </summary>
        public void ScrollBottom()
        {
            Logger.LogExecute("Scroll page top");
            ExecuteScript("$(window).scrollTop($(document).height())");
            WaitTillAjaxLoad();
        }

        /// <summary>
        /// Waits till the page is load.
        /// </summary>
        /// <param name="numberOfSeconds"> Number of seconds for timeout</param>
        public bool WaitTillPageLoad(int numberOfSeconds)
        {
            try
            {
                Wait(numberOfSeconds).Until((driver) =>
                {
                    try
                    {
                        return ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").ToString().Contains("complete");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        return false;
                    }
                });
            }
            catch (WebDriverTimeoutException)
            {
                // If timeout, then page is not loaded. For all other exceptions, do not catch.
            }
            return false;
        }

        public WebDriverWait Wait(int numberOfSeconds)
        {
            return new WebDriverWait(objWebDriver, TimeSpan.FromSeconds(numberOfSeconds));
        }

        public WebDriverWait Wait()
        {
            return new WebDriverWait(objWebDriver, TimeSpan.FromMilliseconds(toolConfigMember.ObjectWait));
        }

        /// <summary>
        /// Waits till the AJAX is loaded.
        /// </summary>
        public bool WaitTillAjaxLoad()
        {
            return WaitTillAjaxLoad(toolConfigMember.ObjectWait / 1000);
        }

        /// <summary>
        /// Wait for AJAX calls to complete (using jQuery)
        /// </summary>
        /// <param name="numberOfSeconds"> Number of seconds for timeout</param>
        public bool WaitTillAjaxLoad(int numberOfSeconds)
        {
            try
            {
                Wait(numberOfSeconds).Until((driver) =>
                {
                    try
                    {
                        return (bool)((IJavaScriptExecutor)driver).ExecuteScript("return (typeof jQuery != 'undefined') && (jQuery.active === 0)");
                    }
                    catch (Exception e)
                    {
                        if (e is InvalidOperationException)
                        {
                            Console.WriteLine("WaitTillAjaxLoad threw InvalidOperationException with message '{0}'", e.Message);
                        }
                        else
                        {
                            Console.WriteLine(e);
                        }
                        return false;
                    }
                });
            }
            catch (WebDriverTimeoutException)
            {
                // If timeout, then AJAX calls are not completed. For all other exceptions, do not catch.
            }
            return false;
        }

        public bool WaitTillAjaxLoadIfExists(int numberOfSeconds = -1)
        {
            try
            {
                Wait(numberOfSeconds == -1 ? toolConfigMember.ObjectWait / 1000 : numberOfSeconds).Until((driver) =>
                {
                    try
                    {
                        return (bool)((IJavaScriptExecutor)driver).ExecuteScript("return (typeof jQuery == 'undefined') || (jQuery.active === 0)");
                    }
                    catch (Exception e)
                    {
                        if (e is InvalidOperationException)
                        {
                            Console.WriteLine("WaitTillAjaxLoad threw InvalidOperationException with message '{0}'", e.Message);
                        }
                        else
                        {
                            Console.WriteLine(e);
                        }
                        return false;
                    }
                });
            }
            catch (WebDriverTimeoutException)
            {
                // If timeout, then AJAX calls are not completed. For all other exceptions, do not catch.
            }
            return false;
        }

        public void ClearLocalStorage(string key)
        {
            ExecuteScript($"delete localStorage['{key}']");
        }

        #region IWebDriver overrides
        /// <summary>
        /// Closes this browser instance.
        /// </summary>
        public void Close()
        {
            objWebDriver.Close();
        }

        /// <summary>
        /// Quits this driver, closing every associated window.
        /// Make objWebDriver null to have ability to start clear session
        /// </summary>
        /// 

        public void SwitchToWindowHandle(string windowHandle)
        {
            this.webDriver.SwitchTo().Window(windowHandle);
        }
        public void Quit()
        {
            if (objWebDriver != null)
            {
                try
                {
                    foreach (var window in objWebDriver.WindowHandles)
                    {
                        SwitchToWindowHandle(window);
                        Close();
                    }
                    objWebDriver.Quit();
                }

                catch (Exception ex)
                {
                    Logger.Error($"Unable to Quit the browser. Reason: {ex.Message}");
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

                finally
                {
                    objWebDriver = null;
                }
            }
        }

        public IOptions Manage()
        {
            return objWebDriver.Manage();
        }

        public INavigation Navigate()
        {
            return objWebDriver.Navigate();
        }

    
        public ITargetLocator SwitchTo()
        {
            return objWebDriver.SwitchTo();
        }

        public string Url
        {
            get { return objWebDriver.Url; }
            set { objWebDriver.Url = value; }
        }

        public string Title
        {
            get { return objWebDriver.Title; }
        }

        public string PageSource
        {
            get { return objWebDriver.PageSource; }
        }

        public string CurrentWindowHandle
        {
            get { return objWebDriver.CurrentWindowHandle; }
        }

        public ReadOnlyCollection<string> WindowHandles
        {
            get { return objWebDriver.WindowHandles; }
        }

        public IWebElement FindElement(By @by)
        {
            return objWebDriver.FindElement(@by);
        }

        public ReadOnlyCollection<IWebElement> FindElements(By @by)
        {
            return objWebDriver.FindElements(@by);
        }

        public void Dispose()
        {
            Quit();
        }

        public Browser Open(string url)
        {
            this.webDriver.Navigate().GoToUrl(url);
            WaitTillPageLoad();
            this.webDriver.SwitchTo().DefaultContent();
            return this;
        }

        public Screenshot GetScreenshot()
        {
            return ((ITakesScreenshot)objWebDriver).GetScreenshot();
        }

        #endregion


        ReadOnlyCollection<string> IWebDriver.WindowHandles => throw new NotImplementedException();

        /// <summary>
        /// Scrolls to the spcified element with use of JS
        /// </summary>
        /// <param name="webElement"></param>
        /// <returns></returns>
        public IWebElement JsScrollToElement(IWebElement webElement, bool alignToTop = false)
        {
            ExecuteScript($"arguments[0].scrollIntoView('{alignToTop}');", webElement);
            return webElement;
        }

        /// <summary>
        /// Returns value of cookie
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string JsGetCookieNamed(string name)
        {
            var script = $@"var n='{name}'+'=';var cookies=decodeURIComponent(document.cookie).split(';');" +
                    @"for(var i=0;i<cookies.length;i++){var c=cookies[i];while (c.charAt(0)==' '){" +
                    @"c=c.substring(1);}if (c.indexOf(n)==0&&c.length!=n.length)" +
                    @"{return c.substring(n.length, c.length);}}return ''";
            return this.ExecuteScript(script) as string;
        }

        /// <summary>
        /// Runs JS
        /// </summary>
        /// <param name="script">script to run</param>
        /// <param name="args">optional args</param>
        public object ExecuteScript(string script, params object[] args)
        {
            try
            {
                return ((IJavaScriptExecutor)objWebDriver).ExecuteScript(script, args);
            }
            catch (WebDriverTimeoutException)
            {
                // If timeout or any errors
                Logger.Debug(String.Format("Error: Exception thrown while running JS Script:{0}{1}", Environment.NewLine, script));
            }
            //TODO: check if we could just throw exception
            return null;
        }

        public void getJavaScriptConsoleLogs()
        {
            Logger.Log("====================================================");
            Logger.Log("Browser Console logs Starts:-");
            IReadOnlyCollection<LogEntry> logEntries = this.Manage().Logs.GetLog(LogType.Browser);
            foreach (var logEntry in logEntries)
            {
                Logger.Log(logEntry.Timestamp + " - " + logEntry.Message);
            }
            Logger.Log("Browser Console logs Ends:-");
            Logger.Log("====================================================");
        }
    }
}
