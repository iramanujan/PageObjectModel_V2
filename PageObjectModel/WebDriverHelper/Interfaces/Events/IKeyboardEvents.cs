using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDriverHelper.Interfaces.Events
{
    public interface IKeyboardEvents
    {
        IKeyboardEvents SendKeys(IWebElement element, string keysToSend);
        IKeyboardEvents KeyDown(IWebElement element, string theKey);
        IKeyboardEvents KeyUp(IWebElement element, string theKey);
    }
}
