
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OrangeHrmLive.Steps.Admin.SystemUsers;
using OrangeHrmLive.Steps.Login;
using System;
using WebDriverHelper.Report;

namespace OrangeHrmLive.Test.Login
{
    [TestFixture]
    class Login : BaseTest
    {
        private LoginStep ObjLoginStep;
        private SystemUsersStep ObjSystemUsersStep = null;

        [SetUp]
        public void SetUp()
        {
            this.ObjLoginStep = new LoginStep();
            this.ObjSystemUsersStep = new SystemUsersStep();
        }

        [Test]
        [TestCase("Admin", "admin123", TestName = "Validate Login With Valid User.", Author = "Anuj Jain")]
        public void ValidateLogin(string username, string password)
        {
            ExtentReportsUtils.CreateTest(TestContext.CurrentContext.Test.Name);
            this.ObjLoginStep.verifyLogin(username, password);
            this.ObjSystemUsersStep.NavigateToSystemUsers();
        }

        [Test]
        [TestCase("Admin", "admin1234", TestName = "Validate Login With InValid User.")]
        public void ValidateInValidLogin(string username, string password)
        {
            ExtentReportsUtils.CreateTest(TestContext.CurrentContext.Test.Name);
            this.ObjLoginStep.verifyLogin(username, password, false);
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

        [TearDown][po]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                ExtentReportsUtils.test.Fail(Exception e., ExtentReportsUtils.GetMediaEntityModelProvider());
            }
        }

    }
}
