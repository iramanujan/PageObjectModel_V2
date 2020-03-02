using Automation.Common.Wait;
using OrangeHrmLive.Page.Admin.SystemUsers;
using OrangeHrmLive.Page.Dashboard;
using System;
using WebDriverHelper.Events;

namespace OrangeHrmLive.Steps.Admin.SystemUsers
{
    public class SystemUsersStep : BaseSteps
    {

        private DashboardPage dashboardPage = null;
        private SystemUsersPage systemUsersPage = null;
        private MouseEvents mouseEvents = null;
        private KeyboardEvents keyboardEvents = null;

        public SystemUsersStep() : base()
        {
            dashboardPage = new DashboardPage();
            systemUsersPage = new SystemUsersPage();
            mouseEvents = new MouseEvents(browser.webDriver);
            keyboardEvents = new KeyboardEvents(browser.webDriver);
        }


        public void NavigateToSystemUsers()
        {
            dashboardPage.Admin.Click();
            dashboardPage.UserManagement.Click();
            dashboardPage.User.Click();
            browser.WaitTillPageLoad(browser.webDriver);
            
            var Column = systemUsersPage.GetSystemUserInformationFromTable("Username", SystemUsersPage.CellPosition.VALUE_BASE, "Admin","", true).Text;
            var first = systemUsersPage.GetSystemUserInformationFromTable("User Role", SystemUsersPage.CellPosition.FIRST, "", "", false).Text;
            var last = systemUsersPage.GetSystemUserInformationFromTable("Username", SystemUsersPage.CellPosition.LAST, "", "", true).Text;

        }

    }
}
