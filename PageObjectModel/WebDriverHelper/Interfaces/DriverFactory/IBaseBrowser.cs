using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace WebDriverHelper.Interfaces.DriverFactory
{
    interface IBaseBrowser
    {

        string Url { get; set; }

        string Title { get; }

        string PageSource { get; }

        string CurrentWindowHandle { get; }

        ReadOnlyCollection<string> WindowHandles { get; }

        void Close();

        IOptions Manage();

        INavigation Navigate();

        void Quit();

        void SwitchToWindowHandle(string windowHandle);

        IBaseBrowser Refresh();

    }
}
