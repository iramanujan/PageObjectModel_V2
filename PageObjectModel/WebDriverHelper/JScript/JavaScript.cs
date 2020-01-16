using Automation.Common.Config;
using Automation.Common.Log;
using Automation.Common.Wait;
using CommonHelper.Helper.Attributes;
using OpenQA.Selenium;
using System;
using System.ComponentModel;

namespace WebDriverHelper.JScript
{
    public enum JScriptType
    {
        [Description("var nodes = arguments[0].childNodes;" +
                     "var text = '';" +
                     "for (var i = 0; i < nodes.length; i++) {" +
                     "    if (nodes[i].nodeName === '#text') {" +
                     "        text += nodes[i].nodeValue; " +
                     "    }" +
                     "}" +
                     "return text;")]
        NodeTextWithoutChildrenScript = 0,

        [Description("arguments[0].style.visibility='visible'; arguments[0].style.opacity = 1;")]
        ChooseFile = 1,

        [Description("arguments[0].scrollIntoView(false)")]
        ScrollIntoViewScript = 2,

        [Description("return document.readyState")]
        PageLoad = 3,

        [Description("return (typeof jQuery != 'undefined') && (jQuery.active === 0)")]
        AjaxLoad = 4,

        [Description("return (typeof jQuery == 'undefined') || (jQuery.active === 0)")]
        AjaxLoadIfExists = 5,

        [Description("$(window).scrollTop($(document).height())")]
        ScrollBottom = 6,

        [Description("$(window).scrollTop(0)")]
        ScrollTop = 7,


        [Description("arguments[0].scrollIntoView(true)")]
        ScrollToElementTop = 8,

        [Description("arguments[0].scrollIntoView(false)")]
        ScrollToElementBottom = 9,


        [Description(@"var n='REPNAME'+'=';var cookies=decodeURIComponent(document.cookie).split(';');" +
                    @"for(var i=0;i<cookies.length;i++){var c=cookies[i];while (c.charAt(0)==' '){" +
                    @"c=c.substring(1);}if (c.indexOf(n)==0&&c.length!=n.length)" +
                    @"{return c.substring(n.length, c.length);}}return ''")]
        CookieNamed = 10,

    }

    public enum AsyncScript
    {
        [Description("var nodes = arguments[0].childNodes;" +
                     "var text = '';" +
                     "for (var i = 0; i < nodes.length; i++) {" +
                     "    if (nodes[i].nodeName === '#text') {" +
                     "        text += nodes[i].nodeValue; " +
                     "    }" +
                     "}" +
                     "return text;")]
        NodeTextWithoutChildrenScript = 0,

        [Description("arguments[0].style.visibility='visible'; arguments[0].style.opacity = 1;")]
        ChooseFile = 1,

        [Description("arguments[0].scrollIntoView(false)")]
        ScrollIntoViewScript = 2,

        [Description("return document.readyState")]
        PageLoad = 3,

        [Description("return (typeof jQuery != 'undefined') && (jQuery.active === 0)")]
        AjaxLoad = 4,

        [Description("return (typeof jQuery == 'undefined') || (jQuery.active === 0)")]
        AjaxLoadIfExists = 5,

        [Description("$(window).scrollTop($(document).height())")]
        ScrollBottom = 6,

        [Description("$(window).scrollTop(0)")]
        ScrollTop = 7
    }

    public class JavaScript
    {
        public readonly ToolConfigMember toolConfigMember = ToolConfigReader.GetToolConfig();

        public object ExecuteScript(string jScriptType, IWebDriver webDriver)
        {
            try
            {
                var retVal = ((IJavaScriptExecutor)webDriver).ExecuteScript(jScriptType);
                return true;
            }
            catch (InvalidOperationException ObjInvalidOperationException)
            {
                Logger.Debug("WaitTillAjaxLoad threw InvalidOperationException with message '{0}'", ObjInvalidOperationException.Message);
                return false;
            }
            catch (WebDriverTimeoutException ObjWebDriverTimeoutException)
            {
                Logger.Debug(String.Format("Error: Exception thrown while running JS Script:{0}{1}{2}", Environment.NewLine, jScriptType), ObjWebDriverTimeoutException.Message);
                return false;
            }
            catch (Exception ObjException)
            {
                Logger.Debug(String.Format("Error: Exception thrown while running JS Script:{0}", Environment.NewLine, ObjException.Message));
                return false;
            }
        }

        public object ExecuteScript(string jScriptType, IWebDriver webDriver, IWebElement webElement)
        {
            try
            {
                return ((IJavaScriptExecutor)webDriver).ExecuteScript(jScriptType, webElement);
            }
            catch (InvalidOperationException ObjInvalidOperationException)
            {
                Logger.Debug("WaitTillAjaxLoad threw InvalidOperationException with message '{0}'", ObjInvalidOperationException.Message);
                return false;
            }
            catch (WebDriverTimeoutException ObjWebDriverTimeoutException)
            {
                Logger.Debug(String.Format("Error: Exception thrown while running JS Script:{0}{1}{2}", Environment.NewLine, jScriptType), ObjWebDriverTimeoutException.Message);
                return false;
            }
            catch (Exception ObjException)
            {
                Logger.Debug(String.Format("Error: Exception thrown while running JS Script:{0}", Environment.NewLine, ObjException.Message));
                return false;
            }
        }

        public object ExecuteScript(JScriptType jScriptType, IWebDriver webDriver)
        {
            try
            {
                var retVal = ((IJavaScriptExecutor)webDriver).ExecuteScript(jScriptType.GetDescription());
                return true;
            }
            catch (InvalidOperationException ObjInvalidOperationException)
            {
                Logger.Debug("WaitTillAjaxLoad threw InvalidOperationException with message '{0}'", ObjInvalidOperationException.Message);
                return false;
            }
            catch (WebDriverTimeoutException ObjWebDriverTimeoutException)
            {
                Logger.Debug(String.Format("Error: Exception thrown while running JS Script:{0}{1}{2}", Environment.NewLine, jScriptType.GetDescription()), ObjWebDriverTimeoutException.Message);
                return false;
            }
            catch (Exception ObjException)
            {
                Logger.Debug(String.Format("Error: Exception thrown while running JS Script:{0}", Environment.NewLine, ObjException.Message));
                return false;
            }
        }

        public object ExecuteScript(JScriptType jScriptType, IWebDriver webDriver, IWebElement webElement)
        {
            try
            {
                return ((IJavaScriptExecutor)webDriver).ExecuteScript(jScriptType.GetDescription(), webElement);
            }
            catch (InvalidOperationException ObjInvalidOperationException)
            {
                Logger.Debug("WaitTillAjaxLoad threw InvalidOperationException with message '{0}'", ObjInvalidOperationException.Message);
                return false;
            }
            catch (WebDriverTimeoutException ObjWebDriverTimeoutException)
            {
                Logger.Debug(String.Format("Error: Exception thrown while running JS Script:{0}{1}{2}", Environment.NewLine, jScriptType.GetDescription()), ObjWebDriverTimeoutException.Message);
                return false;
            }
            catch (Exception ObjException)
            {
                Logger.Debug(String.Format("Error: Exception thrown while running JS Script:{0}", Environment.NewLine, ObjException.Message));
                return false;
            }
        }

        public object ExecuteAsyncScript(AsyncScript asyncScript, IWebDriver webDriver, params object[] args)
        {
            try
            {
                return ((IJavaScriptExecutor)webDriver).ExecuteAsyncScript(asyncScript.GetDescription(), args);
            }
            catch (WebDriverTimeoutException)
            {
                Logger.Error(String.Format("Error: Exception thrown while running JS Script:{0}{1}", Environment.NewLine, asyncScript.GetDescription()));
                return null;
            }
        }

        public object WaitTillAjaxLoad(IWebDriver webDriver)
        {
            return WaitTillAjaxLoad(webDriver, toolConfigMember.ObjectWait / 1000);
        }

        public object WaitTillAjaxLoad(IWebDriver webDriver, int waitTimeInSec = -1)
        {
            try
            {
                Waiter.Wait(webDriver, waitTimeInSec == -1 ? toolConfigMember.ObjectWait / 1000 : waitTimeInSec).Until((webDriver) =>
                 {
                     try
                     {
                         return (bool)((IJavaScriptExecutor)webDriver).ExecuteScript(JScriptType.AjaxLoad.GetDescription());
                     }
                     catch (Exception ObjException)
                     {
                         if (ObjException is InvalidOperationException)
                         {
                             Logger.Error(String.Format("Wait Till Ajax Load: Exception Thrown While Running JS Script. '{0}'", ObjException.Message));
                         }
                         else
                         {
                             Logger.Error(String.Format("Wait Till Ajax Load: Exception Thrown While Running JS Script.'{0}'", ObjException.Message));
                         }
                         return false;
                     }
                 });
            }
            catch (WebDriverTimeoutException ObjWebDriverTimeoutException)
            {
                Logger.Debug(String.Format("Wait Till Ajax Load: Exception Thrown While Running JS Script.:{0}", ObjWebDriverTimeoutException.Message));
            }
            return false;
        }

        public object WaitTillAjaxLoadIfExists(IWebDriver webDriver)
        {
            return WaitTillAjaxLoadIfExists(webDriver, toolConfigMember.ObjectWait / 1000);
        }

        public object WaitTillAjaxLoadIfExists(IWebDriver webDriver, int waitTimeInSec = -1)
        {
            try
            {
                Waiter.Wait(webDriver, waitTimeInSec == -1 ? toolConfigMember.ObjectWait / 1000 : waitTimeInSec).Until((driver) =>
                {
                    try
                    {
                        return (bool)((IJavaScriptExecutor)driver).ExecuteScript(JScriptType.AjaxLoadIfExists.GetDescription());
                    }
                    catch (Exception ObjException)
                    {
                        if (ObjException is InvalidOperationException)
                        {
                            Logger.Error(String.Format("Wait Till Ajax Load If Exists: Exception Thrown While Running JS Script. '{0}'", ObjException.Message));
                        }
                        else
                        {
                            Logger.Error(String.Format("Wait Till Ajax Load If Exists: Exception Thrown While Running JS Script.'{0}'", ObjException.Message));
                        }
                        return false;
                    }
                });
            }
            catch (WebDriverTimeoutException ObjWebDriverTimeoutException)
            {
                Logger.Error(String.Format("Wait Till Ajax Load If Exists: Exception Thrown While Running JS Script.:{0}", ObjWebDriverTimeoutException.Message));
            }
            return false;
        }

        public object WaitTillPageLoad(IWebDriver webDriver)
        {
            return WaitTillPageLoad(webDriver, toolConfigMember.PageLoadWait / 1000);
        }

        public object WaitTillPageLoad(IWebDriver webDriver, int waitTimeInSec)
        {
            try
            {
                Waiter.Wait(webDriver, waitTimeInSec == -1 ? toolConfigMember.PageLoadWait / 1000 : waitTimeInSec).Until((driver) =>
                {
                    try
                    {
                        return ((IJavaScriptExecutor)driver).ExecuteScript(JScriptType.PageLoad.GetDescription()).ToString().Contains("complete");
                    }
                    catch (Exception ObjException)
                    {
                        Logger.Error(String.Format("Wait Till Page Load: Exception Thrown While Running JS Script.:{0}", ObjException.Message));
                        return false;
                    }
                });
            }
            catch (WebDriverTimeoutException ObjWebDriverTimeoutException)
            {
                Logger.Debug(String.Format("Wait Till Page Load: Exception Thrown While Running JS Script.:{0}", ObjWebDriverTimeoutException.Message));
            }
            return false;
        }

        public void ScrollBottom(IWebDriver webDriver)
        {
            ExecuteScript(JScriptType.ScrollBottom.GetDescription(), webDriver);
            WaitTillAjaxLoad(webDriver);
        }

        public void ScrollTop(JScriptType jScriptType, IWebDriver webDriver)
        {
            ExecuteScript(JScriptType.ScrollTop.GetDescription(), webDriver);
            WaitTillAjaxLoad(webDriver);
        }

        public IWebElement JsScrollToElement(IWebDriver webDriver, IWebElement webElement, bool alignToTop = false)
        {
            ExecuteScript(alignToTop == false ? JScriptType.ScrollBottom : JScriptType.ScrollTop, webDriver, webElement);
            return webElement;
        }

        public string JsGetCookieNamed(string name, IWebDriver webDriver)
        {
            var script = JScriptType.CookieNamed.GetDescription().Replace("REPNAME", name);
            return ExecuteScript(script, webDriver) as string;
        }
    }
}