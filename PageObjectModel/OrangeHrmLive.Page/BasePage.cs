using OpenQA.Selenium.Support.PageObjects;
using OrangeHrmLive.Page.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverHelper.BrowserFactory;

namespace OrangeHrmLive.Page
{
    public class BasePage
    {
        public Browser browser = BrowserContext.browser;
    }
}
