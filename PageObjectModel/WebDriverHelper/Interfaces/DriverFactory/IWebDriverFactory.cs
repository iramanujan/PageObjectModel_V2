using OpenQA.Selenium;

namespace WebDriverHelper.Interfaces.DriverFactory
{
    public interface IWebDriverFactory
    {
        IWebDriver InitializeWebDriver();
    }
}
