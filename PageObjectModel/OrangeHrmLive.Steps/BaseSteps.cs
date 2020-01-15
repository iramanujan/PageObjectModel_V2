using Automation.Common.Log;
using OpenQA.Selenium;
using OrangeHrmLive.Page.Context;
using OrangeHrmLive.Steps.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverHelper.BrowserFactory;

namespace OrangeHrmLive.Steps
{
    public class BaseSteps
    {
        public Browser browser = BrowserContext.browser;

    }
}
