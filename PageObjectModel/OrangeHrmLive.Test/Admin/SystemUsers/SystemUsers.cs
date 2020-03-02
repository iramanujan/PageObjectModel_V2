using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OrangeHrmLive.Steps.Admin.SystemUsers;
using OrangeHrmLive.Steps.Login;
using System;
using WebDriverHelper.Report;

namespace OrangeHrmLive.Test.Admin.SystemUsers
{
    [TestFixture]
    public class SystemUsers: BaseTest
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
        [TestCase("Admin", "admin123", TestName = "Test", Author = "Anuj Jain")]
        public void ValidateLogin(string username, string password)
        {
            ExtentReportsUtils.CreateTest(TestContext.CurrentContext.Test.Name);
            this.ObjLoginStep.verifyLogin(username, password);
            this.ObjSystemUsersStep.NavigateToSystemUsers();
        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                ExtentReportsUtils.test.Fail(TestContext.CurrentContext.Result.Message, ExtentReportsUtils.GetMediaEntityModelProvider());
            }
        }
    }
}
