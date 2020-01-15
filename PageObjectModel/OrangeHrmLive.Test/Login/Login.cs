using Automation.Common.Log;
using Automation.Common.Report;
using Automation.Common.Utils;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using CommonHelper.Helper.Attributes;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OrangeHrmLive.Steps.Login;
using System;

namespace OrangeHrmLive.Test.Login
{
    [TestFixture]
    class Login : BaseTest
    {
        private LoginStep ObjLoginStep;

        [SetUp]
        public void SetUp()
        {
            this.ObjLoginStep = new LoginStep();
        }

        [Test]
        [TestCase("Admin", "admin123", TestName = "Validate Login With Valid User.")]
        [TestCase("Admin", "admin1234", TestName = "Validate Login With InValid User.")]
        public void ValidateLogin(string username, string password)
        {
            ExtentReportsUtils.CreateTest(TestContext.CurrentContext.Test.Name);
            this.ObjLoginStep.verifyLogin(username, password);
        }

        [Test]
        [TestCase(LoginStep.ErrorMessageType.UserNameEmpty, "", "admin123", TestName = "Validate Error Message Username cannot be empty.")]
        [TestCase(LoginStep.ErrorMessageType.PasswordEmpty, "Admin", "", TestName = "Validate Error Message Password cannot be empty.")]
        [TestCase(LoginStep.ErrorMessageType.PasswordEmpty, "admin123", "Admin", TestName = "Validate Error Message Invalid credentials.")]
        public void ValidateErrorMessage(LoginStep.ErrorMessageType errorMessageType, string userName, string password)
        {
            ExtentReportsUtils.CreateTest(TestContext.CurrentContext.Test.Name);
            this.ObjLoginStep.VerifyErrorMessage(errorMessageType, userName, password);
        }

        [TearDown]
        public void TearDown()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;

            var stackTrace = "<pre>" + TestContext.CurrentContext.Result.StackTrace + "</pre>";

            var errorMessage = TestContext.CurrentContext.Result.Message;
            if (status == TestStatus.Failed)
            {
                MakeAndSaveScreenshot(TestContext.CurrentContext.Test.MethodName + "_" + TestContext.CurrentContext.Test.Name);
                ExtentReportsUtils.test.Log(Status.Fail, stackTrace + errorMessage, MediaEntityBuilder.CreateScreenCaptureFromPath(ScreenshotImagPath).Build());

            }
            if (status == TestStatus.Warning)
            {
                MakeAndSaveScreenshot(TestContext.CurrentContext.Test.MethodName + "_" + TestContext.CurrentContext.Test.Name);
                ExtentReportsUtils.test.Log(Status.Warning, "", MediaEntityBuilder.CreateScreenCaptureFromPath(ScreenshotImagPath).Build());
            }
        }

    }
}
