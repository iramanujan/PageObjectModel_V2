using OpenQA.Selenium;
using WebDriverHelper.HtmlElement;

namespace OrangeHrmLive.Page.Dashboard
{
    public class DashboardPage : BasePage
    {
        //private readonly string pageLoadedText = "Leave Entitlements and Usage Report";
        //private readonly string pageUrl = "/index.php/dashboard";
        private WebElementHelper webElementHelper = new WebElementHelper();
        private IWebDriver webDriver = new BasePage().browser.webDriver;
        public IWebElement Admin => webElementHelper.GetWebElement(By.CssSelector("#menu_admin_viewAdminModule"));
        public IWebElement UserManagement => webElementHelper.GetWebElement(By.CssSelector("#menu_admin_UserManagement"));
        public IWebElement User => webElementHelper.GetWebElement(By.CssSelector("#menu_admin_viewSystemUsers"));

    }
}
