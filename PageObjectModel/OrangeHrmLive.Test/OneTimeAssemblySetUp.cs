using NUnit.Framework;
using WebDriverHelper.Report;

namespace OrangeHrmLive.Test
{
    [SetUpFixture]
    class OneTimeAssemblySetUp
    {

        [OneTimeSetUp]
        public void OneTimeSetup()
        {


        }
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            ExtentReportsUtils.ExtentReportsTearDown();
        }
    }
}
