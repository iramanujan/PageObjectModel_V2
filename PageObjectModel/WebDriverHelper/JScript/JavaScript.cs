using Automation.Common.Log;
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
        AjaxLoad = 4
    }

    public static class JavaScript
    {
        public static object ExecuteScript(JScriptType jScriptType, IWebDriver webDriver)
        {
            try
            {
                return ((IJavaScriptExecutor)webDriver).ExecuteScript(jScriptType.GetDescription());
            }
            catch (WebDriverTimeoutException)
            {
                Logger.Debug(String.Format("Error: Exception thrown while running JS Script:{0}{1}", Environment.NewLine, jScriptType.GetDescription()));
            }
            return null;
        }

        public static object ExecuteScript(JScriptType jScriptType, IWebDriver webDriver, IWebElement webElement)
        {
            try
            {
                return ((IJavaScriptExecutor)webDriver).ExecuteScript(jScriptType.GetDescription(), webElement);
            }
            catch (WebDriverTimeoutException)
            {
                Logger.Debug(String.Format("Error: Exception thrown while running JS Script:{0}{1}", Environment.NewLine, jScriptType.GetDescription()));
            }
            return null;
        }
    }
}
