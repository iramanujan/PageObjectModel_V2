using Automation.Common.Config;
using WebDriverHelper.BrowserFactory;
using WebDriverHelper.DriverFactory;
using WebDriverHelper.Interfaces.DriverFactory;

namespace OrangeHrmLive.Page.Context
{
    public class TestHarnessContext
    {
        public static readonly ToolConfigMember toolConfigMember = ToolConfigReader.GetToolConfig();


        public Browser Browser { get; }


        public TestHarnessContext(IWebDriverFactory factory)
        {
            this.Browser = new Browser(factory);
            if(BrowserContext.browser== null)
            {
                BrowserContext.browser = this.Browser;
            }
        }

        public TestHarnessContext() : this(WebDriverType.Get(toolConfigMember.Browser, toolConfigMember.ExecutionType).Factory)
        {
        }
    
    }
}
