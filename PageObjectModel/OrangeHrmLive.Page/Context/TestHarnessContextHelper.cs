using Automation.Common.Config;
using WebDriverHelper.DriverFactory;
namespace OrangeHrmLive.Page.Context
{
    public static class TestHarnessContextHelper
    {

        public static readonly ToolConfigMember toolConfigMember = ToolConfigReader.GetToolConfig();


        /// <summary>
        /// Creates default context based on default <see cref="WebDriverType.Factory"/>
        /// </summary>
        public static TestHarnessContext CreateDefault()
        {
            return new TestHarnessContext();
        }

        /// <summary>
        /// Creates context based on the local driver factory, like
        /// ChromeDriverLocalFactory, FireFoxDriverLocalFactory, IEDriverLocalFactory
        /// </summary>
        public static TestHarnessContext CreateLocalDriverContext()
        {
            var factory = WebDriverType.Get(toolConfigMember.Browser, ToolConfigMember.WebDriverExecutionType.Local).Factory;
            return new TestHarnessContext(factory);
        }
    }
}
