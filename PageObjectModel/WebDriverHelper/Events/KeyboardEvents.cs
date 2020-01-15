using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using WebDriverHelper.Interfaces.Events;

namespace WebDriverHelper.Events
{
    public class KeyboardEvents : IKeyboardEvents
    {

        private readonly IWebDriver ObjWebDriver;
        private readonly Actions ObjActions;
        public KeyboardEvents(IWebDriver webDriver)
        {
            this.ObjWebDriver = webDriver;
            this.ObjActions = new Actions(this.ObjWebDriver);
        }

        public IKeyboardEvents KeyDown(IWebElement element, string Key)
        {
            this.ObjActions.KeyDown(element, Key).Build().Perform();
            return this;
        }

        public IKeyboardEvents KeyDown(string Key)
        {
            this.ObjActions.KeyDown(Key).Build().Perform();
            return this;
        }

        public IKeyboardEvents KeyUp(string Key)
        {
            this.ObjActions.KeyUp(Key).Build().Perform();
            return this;
        }

        public IKeyboardEvents KeyUp(IWebElement element, string Key)
        {
            this.ObjActions.KeyUp(element, Key).Build().Perform();
            return this;
        }

        public IKeyboardEvents SendKeys(IWebElement element, string keysToSend)
        {
            this.ObjActions.SendKeys(element, keysToSend).Build().Perform();
            return this;
        }
    }
}
