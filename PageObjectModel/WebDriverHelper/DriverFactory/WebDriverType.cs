using Headspring;

using NUnit.Framework;
using System.Linq;
using WebDriverHelper.DriverFactory.Chrome.Local;
using WebDriverHelper.DriverFactory.Chrome.Remote;
using WebDriverHelper.DriverFactory.FireFox.Local;
using WebDriverHelper.DriverFactory.FireFox.Remote;
using WebDriverHelper.Interfaces.DriverFactory;
using static Automation.Common.Config.ToolConfigMember;

namespace WebDriverHelper.DriverFactory
{
    public abstract class WebDriverType : Enumeration<WebDriverType>
    {
        public static readonly WebDriverType ChromeLocal = new ChromeLocalType();

        public static readonly WebDriverType ChromeRemote = new ChromeRemoteType();

        //public static readonly WebDriverType IELocal = new IELocalType();

        //public static readonly WebDriverType IERemote = new IERemoteType();

        public static readonly WebDriverType FirefoxLocal = new FirefoxLocalType();

        public static readonly WebDriverType FirefoxRemote = new FirefoxRemoteType();

        protected WebDriverType(int value,BrowserType browserType, WebDriverExecutionType executionType) : base(value: value, displayName: browserType + executionType.ToString())
        {
            BrowserType = browserType;
            ExecutionType = executionType;
        }

        public BrowserType BrowserType { get; set; }

        public WebDriverExecutionType ExecutionType { get; set; }

        public BrowserLocalization Localization { get; set; }

        public static WebDriverType Get(BrowserType browserType, WebDriverExecutionType executionType)
        {
            var targetWebDriverType =
                GetAll().FirstOrDefault(wd => wd.BrowserType == browserType && wd.ExecutionType == executionType);
            Assert.IsNotNull(targetWebDriverType, $"WebDriverType with properties BrowserType='{browserType}' ExecutionType={executionType} not found");
            return targetWebDriverType;
        }

        public abstract IWebDriverFactory Factory { get; }

        private class ChromeLocalType : WebDriverType
        {
            public ChromeLocalType() : base(1, BrowserType.Chrome, WebDriverExecutionType.Local)
            {
            }
            public override IWebDriverFactory Factory => new LocalChromeDriver();
        }

        private class ChromeRemoteType : WebDriverType
        {
            public ChromeRemoteType() : base(2, BrowserType.Chrome, WebDriverExecutionType.Grid)
            {
            }
            public override IWebDriverFactory Factory => new RemoteChromeDriver();
        }

        //private class IELocalType : WebDriverType
        //{
        //    public IELocalType() : base(3, BrowserType.IE, WebDriverExecutionType.Local)
        //    {
        //    }
        //    public override IWebDriverFactory Factory => new IEDriverLocalFactory();
        //}

        //private class IERemoteType : WebDriverType
        //{
        //    public IERemoteType() : base(4, BrowserType.IE, WebDriverExecutionType.Grid)
        //    {
        //    }
        //    public override IWebDriverFactory Factory => new IEDriverRemoteFactory();
        //}

        private class FirefoxLocalType : WebDriverType
        {
            public FirefoxLocalType() : base(5, BrowserType.Firefox, WebDriverExecutionType.Local)
            {
            }
            public override IWebDriverFactory Factory => new LocalFireFoxDriverFactory();
        }

        private class FirefoxRemoteType : WebDriverType
        {
            public FirefoxRemoteType() : base(6, BrowserType.Firefox, WebDriverExecutionType.Grid)
            {
            }
            public override IWebDriverFactory Factory => new RemoteFireFoxDriverFactory();
        }
    }
}
