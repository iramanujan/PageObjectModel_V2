
using Automation.Common.Config;
using Automation.Common.Log;
using Automation.Common.Utils;
using OpenQA.Selenium;
using System;
using System.Collections.ObjectModel;
using static Automation.Common.Config.ToolConfigMember;

namespace WebDriverHelper.BrowserFactory
{
    public class BaseBrowser
    {
        private IWebDriver webDriver = null;
        public BaseBrowser(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }
        public string Url { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string Title => throw new NotImplementedException();

        public string PageSource => throw new NotImplementedException();

        public string CurrentWindowHandle => throw new NotImplementedException();

        public ReadOnlyCollection<string> WindowHandles => throw new NotImplementedException();

        public void Close()
        {
            this.webDriver.Close();
        }

        public IOptions Manage()
        {
            throw new NotImplementedException();
        }

        public INavigation Navigate()
        {
            throw new NotImplementedException();
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
                    switch (ToolConfigReader.GetToolConfig().Browser)
                    {
                        case BrowserType.IE:
                            ProcessUtils.KillProcesses("iexplore");
                            ProcessUtils.KillProcesses("IEDriverServer");
                            break;
                        case BrowserType.Chrome:
                            ProcessUtils.KillProcesses("chrome.exe");
                            ProcessUtils.KillProcesses("chromedriver.exe");
                            break;
                        case BrowserType.Firefox:
                            ProcessUtils.KillProcesses("firefox.exe");
                            ProcessUtils.KillProcesses("geckodriver.exe");
                            break;
                    }
                }

                finally
                {
                    this.webDriver = null;
                }
            }
        }

   

        public void SwitchToWindowHandle(string windowHandle)
        {
            this.webDriver.SwitchTo().Window(windowHandle);
        }
    }
}
