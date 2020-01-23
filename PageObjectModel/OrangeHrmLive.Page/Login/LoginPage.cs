using OpenQA.Selenium;
using System;

namespace OrangeHrmLive.Page.Login
{
    public class LoginPage: BasePage
    {

        private IWebDriver webDriver = new BasePage().browser.webDriver;  
        public IWebElement UserName => this.webDriver.FindElement(By.CssSelector("#txtUsername"));
        public IWebElement Password => this.webDriver.FindElement(By.CssSelector("#txtPassword"));
        public IWebElement Login => this.webDriver.FindElement(By.CssSelector("#btnLogin"));
        public IWebElement Message => this.webDriver.FindElement(By.CssSelector("#spanMessage"));
        public String ErrorMessage => Message.Text.Trim();
    }
}
