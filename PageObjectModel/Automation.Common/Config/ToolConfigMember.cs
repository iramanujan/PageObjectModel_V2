using CommonHelper.Helper.Attributes;
using System.Runtime.Serialization;

namespace Automation.Common.Config
{
    [DataContract(Namespace = "")]
    public class ToolConfigMember
    {

        public enum LocalizationType
        {
            [StringValue("en")]
            en,
            [StringValue("de")]
            de,
            [StringValue("es")]
            es,
            [StringValue("fr")]
            fr,
            [StringValue("it")]
            it
        }

        public enum BrowserLocalization
        {
            [StringValue("en")]
            en,
            [StringValue("de")]
            de,
            [StringValue("es")]
            es,
            [StringValue("fr")]
            fr,
            [StringValue("it")]
            it
        }

        public enum BrowserType
        {
            [StringValue("Unknown")]
            Unknown = 0,

            [StringValue("Chrome")]
            Chrome = 1,

            [StringValue("InternetExplorer")]
            IE = 2,

            [StringValue("Edge")]
            Edge = 3,

            [StringValue("Firefox")]
            Firefox = 4,

            [StringValue("Safari")]
            Safari = 5
        }

        public enum WebDriverExecutionType
        {
            [StringValue("Local")]
            Local = 1,

            [StringValue("Grid")]
            Grid = 2
        }

        [DataMember(EmitDefaultValue = false, Order = 1)]
        public string Tool { get; private set; }

        [DataMember(EmitDefaultValue = false, Order = 2)]
        public string ToolAssembly { get; private set; }

        [DataMember(EmitDefaultValue = false, Order = 3)]
        public string PageIcons { get; private set; }

        [DataMember(EmitDefaultValue = false, Order = 4)]
        public string PageIconsSerializerClass { get; private set; }

        [DataMember(EmitDefaultValue = false, Order = 5)]
        public string PageLocators { get; private set; }

        [DataMember(EmitDefaultValue = false, Order = 6)]
        public string PageLocatorsSerializerClass { get; private set; }

        [DataMember(EmitDefaultValue = false, Order = 7)]
        public string PageUrls { get; private set; }

        [DataMember(EmitDefaultValue = false, Order = 8)]
        public string PageUrlsSerializerClass { get; private set; }

        [DataMember(EmitDefaultValue = false, Order = 9)]
        public LocalizationType Localization { get; private set; }

        [DataMember(EmitDefaultValue = false, Order = 10)]
        public int ObjectWait { get; private set; }

        [DataMember(EmitDefaultValue = false, Order = 11)]
        public int PollTime { get; private set; }

        [DataMember(EmitDefaultValue = false, Order = 12)]
        public int PageLoadWait { get; private set; }

        [DataMember(EmitDefaultValue = false, Order = 13)]
        public int CommandTimeout { get; private set; }

        [DataMember(EmitDefaultValue = true, Order = 14)]
        public string PageLoadStrategy { get; private set; }

        [DataMember(EmitDefaultValue = true, Order = 15)]
        public bool NoCache { get; private set; }

        [DataMember(EmitDefaultValue = false, Order = 16)]
        public int WaitForFreeSlotOnHubTimeout { get; private set; }

        [DataMember(EmitDefaultValue = false, Order = 17)]
        public string ProfileName { get; private set; }

        [DataMember(EmitDefaultValue = false, Order = 18)]
        public BrowserType Browser { get; private set; }

        [DataMember(EmitDefaultValue = false, Order = 19)]
        public WebDriverExecutionType ExecutionType { get; private set; }

        [DataMember(EmitDefaultValue = false, Order = 20)]
        public string GridHost { get; private set; }

        public string GridUrl => GridHost + "wd/hub";

        [DataMember(EmitDefaultValue = false, Order = 21)]
        public string RootDownloadLocation { get; private set; }

        [DataMember(EmitDefaultValue = false, Order = 22)]
        public string RootUploadLocation { get; private set; }


        [DataMember(EmitDefaultValue = false, Order = 23)]
        public int AsynchronousJavaScriptWait { get; private set; }

        [DataMember(EmitDefaultValue = false, Order = 24)]
        public int ImplicitWait { get; private set; }

        [DataMember(EmitDefaultValue = false, Order = 25)]
        public string ErrorImageLocation { get; private set; }
        [DataMember(EmitDefaultValue = false, Order = 26)]
        public string AutomationReportPath { get; private set; }

        
    }
}
