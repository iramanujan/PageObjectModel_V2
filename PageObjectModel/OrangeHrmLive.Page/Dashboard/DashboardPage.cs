using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrangeHrmLive.Page.Dashboard
{
    public class DashboardPage: BasePage
    {
        private IWebDriver webDriver = new BasePage().browser.webDriver;
        public IWebElement Admin => this.webDriver.FindElement(By.CssSelector("#menu_admin_viewAdminModule"));
    }
}
