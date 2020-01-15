using System.Configuration;

namespace Automation.Common.Config
{
    public class ToolConfigReader
    {
        public static readonly string ToolConfigSection = "ToolConfigMember";

        private static readonly ToolConfigMember toolConfigMember;

        //static constructor to initialize only once per domain
        static ToolConfigReader()
        {
            toolConfigMember = ConfigurationManager.GetSection(ToolConfigReader.ToolConfigSection) as ToolConfigMember;
        }
        public static ToolConfigMember GetToolConfig() => toolConfigMember;
    }
}
