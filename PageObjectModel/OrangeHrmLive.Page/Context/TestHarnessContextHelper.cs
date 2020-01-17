using Automation.Common.Config;
using WebDriverHelper.DriverFactory;
namespace OrangeHrmLive.Page.Context
{
    public static class TestHarnessContextHelper
    {

        public static readonly ToolConfigMember toolConfigMember = ToolConfigReader.GetToolConfig();

        public static TestHarnessContext CreateDefault()
        {
            return new TestHarnessContext();
        }
        
        public static TestHarnessContext CreateLocalDriverContext()
        {
            var factory = WebDriverType.Get(toolConfigMember.Browser, ToolConfigMember.WebDriverExecutionType.Local).Factory;
            return new TestHarnessContext(factory);
        }
    }
}
