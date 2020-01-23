using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using WebDriverHelper.BrowserFactory;

namespace WebDriverHelper.Events
{
    public class KeyboardEvents
    {

        private readonly IWebDriver ObjWebDriver;
        private readonly Actions ObjActions;
        public KeyboardEvents(IWebDriver webDriver)
        {
            this.ObjWebDriver = webDriver;
            this.ObjActions = new Actions(this.ObjWebDriver);
        }
        public KeyboardEvents() : this(BrowserObjectContext.browser.webDriver)
        {

        }

        public void KeyDown(IWebElement element, string Key)
        {
            this.ObjActions.KeyDown(element, Key).Build().Perform();
        }

        public void KeyUp(IWebElement element, string Key)
        {
            this.ObjActions.KeyUp(element, Key).Build().Perform();
        }

        public void SendKeys(IWebElement element, string keysToSend)
        {
            this.ObjActions.SendKeys(element, keysToSend).Build().Perform();
        }
    }
}
