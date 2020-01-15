using System.Configuration;
namespace Automation.Common.Utils
{
    public static class ConfigSettingsUtils
    {
        public static string ApplicationPath => ConfigurationManager.AppSettings["ApplicationPath"];
        public static string ApplicationName => ConfigurationManager.AppSettings["ApplicationName"];
        public static string ProcessName => ConfigurationManager.AppSettings["ProcessName"];
        public static string TestTempFolder => ConfigurationManager.AppSettings["TestTempFolder"];

    }
}
