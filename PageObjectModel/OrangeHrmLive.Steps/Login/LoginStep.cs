using Automation.Common.Log;
using Automation.Common.Wait;
using CommonHelper.Helper.Attributes;
using NUnit.Framework;
using OrangeHrmLive.Page.Dashboard;
using OrangeHrmLive.Page.Login;
using System;
using WebDriverHelper.Report;
using static OrangeHrmLive.Page.Pages.Pages;
using DescriptionAttribute = System.ComponentModel.DescriptionAttribute;

namespace OrangeHrmLive.Steps.Login
{
    public class LoginStep: BaseSteps
    {
        private LoginPage ObjLoginPage = null;
        private DashboardPage dashboardPage = null;
        public LoginStep():base()
        {
            ObjLoginPage  = new LoginPage();
            dashboardPage = new DashboardPage();
        }
         

        public enum ErrorMessageType
        {
            [Description("Username cannot be empty")]
            UserNameEmpty = 0,

            [Description("Password cannot be empty")]
            PasswordEmpty = 1,

            [Description("Invalid credentials")]
            InvalidCredentials = 2
        }


        public void LoginOrangeHRM(string userName, string password)
        {
            ExtentReportsUtils.test.Info("Enter User Name and Password.");
            ObjLoginPage.UserName.SendKeys(userName);
            ObjLoginPage.Password.SendKeys(password);
            ObjLoginPage.Login.Submit();
        }

        public void verifyLogin(string userName, string password,bool IsValidUser = true)
        {
            LoginOrangeHRM(userName, password);
            ExtentReportsUtils.test.Info("Validate After Login Dashboard Page Appear Or Not.");
            if (IsValidUser)
            {
                Assert.IsTrue(browser.webDriver.Url.ToLower().Contains(PageName.Dashboard.GetDescription().ToString().ToLower()));
                ExtentReportsUtils.test.Pass("Validate After Login Dashboard Page.\nExpected Page: Dashboard.\n" + "Actual Page: Dashboard.", ExtentReportsUtils.GetMediaEntityModelProvider());
            }
            else
            {
                Assert.IsTrue(browser.webDriver.Url.ToLower().Contains("validateCredentials".ToLower()));
                ExtentReportsUtils.test.Pass("Validate Login For InValid User.\nExpected Page: Login Page.\n" + "Actual Page: Login Page.", ExtentReportsUtils.GetMediaEntityModelProvider());
            }

            
        }

        private void WaitForErrorMessage()
        {
            Waiter.SpinWaitEnsureSatisfied(() =>
            {
                Logger.LogExecute($"Wait For Error Message....");
                try
                {
                    var msg = ObjLoginPage.ErrorMessage;
                }
                catch (Exception e)
                {
                    Logger.Error(e.Message);
                }
                return true;
            }, TimeSpan.FromSeconds(5), TimeSpan.FromMilliseconds(200), $"Could not Set clipboard to text ");
        }

        public void VerifyErrorMessage(ErrorMessageType errorMessageType, string userName, string password)
        {
            if (errorMessageType.Equals(ErrorMessageType.UserNameEmpty))
            {
                Logger.LogValidate("Verify User Empty Error Message");
                ObjLoginPage.Password.SendKeys(password);
                ObjLoginPage.Login.Submit();
                WaitForErrorMessage();
                Assert.AreEqual(ErrorMessageType.UserNameEmpty.GetDescription(), ObjLoginPage.ErrorMessage, "Error Message is not matched.");
            }
            if (errorMessageType.Equals(ErrorMessageType.PasswordEmpty))
            {
                Logger.LogValidate("Verify Password Empty Error Message");
                ObjLoginPage.UserName.SendKeys(userName);
                ObjLoginPage.Login.Submit();
                WaitForErrorMessage();
                Assert.AreEqual(ErrorMessageType.PasswordEmpty.GetDescription(), ObjLoginPage.ErrorMessage, "Error Message is not matched.");
            }
            if (errorMessageType.Equals(ErrorMessageType.InvalidCredentials))
            {
                Logger.LogValidate("Verify Invalid Credentials Error Message");
                ObjLoginPage.UserName.SendKeys(userName);
                ObjLoginPage.Password.SendKeys(password);
                ObjLoginPage.Login.Submit();
                WaitForErrorMessage();
                Assert.AreEqual(ErrorMessageType.InvalidCredentials.GetDescription(), ObjLoginPage.ErrorMessage, "Error Message is not matched.");
            }
        }
    }
}
